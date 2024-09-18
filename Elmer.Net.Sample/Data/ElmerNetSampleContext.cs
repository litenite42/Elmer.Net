using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Elmer.Net.Sample.Models;

namespace Elmer.Net.Sample.Data
{
    public class ElmerNetSampleContext : DbContext
    {
        public ElmerNetSampleContext (DbContextOptions<ElmerNetSampleContext> options)
            : base(options)
        {
        }

        public DbSet<Elmer.Net.Sample.Models.Course> Course { get; set; } = default!;
    }
}
