/*
    HTML Elements necessary for verifying authenticity of user interaction before allowing form to submit
*/
class RecaptchaElements {
    /*
        Form Element Selector to assess user interaction of
    */
    Form;
    /*
        Submit Button Selector to watch for click event
    */
    Submit;
    constructor(object) {
        const { form, submit } = { ...object };

        let _form = form || console.debug('No Form Selector Provided. Defaulting to "form"') || "form";
        let _submit = submit || console.debug('No Submit Button Selector Provided. Defaulting to "button[type=submit]."') || "button[type='submit']";

        this.Form = document.querySelector(_form) || console.error(`No element found with selector: ${_form}`);
        this.Submit = document.querySelector(_submit) || console.error(`No element found with selector: ${_submit}`);
    }
}

class ReCaptchaConfiguration {
    Recaptcha; Before; Fail; Always;
    constructor(object) {
        const { action, post, before, fail, always } = { ...object };

        this.Action = action || console.debug('Missing action name. Defaulting to "genericSubmit"') || 'genericSubmit';
        this.Post = post || console.debug('Logic to submit to backend for verification missing.');
        this.Before = before ||  console.debug('Optional Logic before submitting form to application missing; note this is to application and not reCAPTCHA');
        this.Fail = fail || console.debug('Optional logic to handle failed submission to application missing; note this is to the application and not reCAPTCHA');
        this.Always = always || console.debug('Optional logic to clean up all requests sent to the application.');
    }
}

class Elmer {
    #elem;
    #cfg;
    constructor(elements /* RecaptchaElements */, configuration /* RecaptchaConfiguration */) {
        this.#elem = elements;
        this.#cfg = configuration;

        if (!elements) {
            elements = new RecaptchaElements();
        }


        if (!configuration) {
            configuration = new ReCaptchaConfiguration();
        }

        elements.Submit.addEventListener('click', (event) => {
            event.preventDefault();
            event.stopPropagation();

            const data = elements.Submit.dataset;
            const _this = this;
            grecaptcha.ready(function () {
                grecaptcha.execute(data.siteKey, { action: configuration.Action }).then(async function (token) {
                    configuration.Before && configuration.Before();
                    // Add your logic to submit to your backend server here.
                    const response = await fetch(data.verifyUrl, {
                        method: 'post',
                        body: JSON.stringify({ response: token, secret: data.siteKey }),
                        headers: {
                            'Accept': 'application/json; charset=utf-8',
                            'Content-Type': 'application/json;charset=UTF-8'
                        }
                    });

                    if (!response.ok) {
                        console.error(response)
                        return;
                    }

                    const content = await response.json();

                    if (configuration.Post) {
                        configuration.Post(content);
                    } else {
                        _this.Handle(content, configuration.Action);
                    }

                    configuration.Always && configuration.Always();
                });
            });
        });
    }

    Handle(content, actionName) {
        if (!Elmer.Interpret(content, actionName)) {
            console.error('User Interaction Assessment failed.');
            return;
        }

        this.#elem.Form.submit();
    }

    static Interpret(recaptchaInfo, expectedAction, minScore = 0.7) {
        const { score, action, challenge_ts, hostname, "error-codes": errorCodes } = { ...recaptchaInfo };

        if (errorCodes && errorCodes.length) {
            console.debug(errorCodes.join());
            return false;
        }

        if (!action || action !== expectedAction) {
            console.debug("Invalid action name");
            return false;
        }

        if (!score || score < minScore) {
            console.debug("Invalid score or score too low.");
            return false;
        }

        return true;
    }
}