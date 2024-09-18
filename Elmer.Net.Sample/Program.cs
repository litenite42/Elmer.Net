using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Elmer.Net.Extensions;
using Elmer.Net.Core;
using Microsoft.Extensions.DependencyInjection;
using Elmer.Net.Sample.Data;
using Elmer.Net.Sample.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ElmerNetSampleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ElmerNetSampleContext") ?? throw new InvalidOperationException("Connection string 'ElmerNetSampleContext' not found.")));

builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddReCaptcha<MyCaptcha>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
