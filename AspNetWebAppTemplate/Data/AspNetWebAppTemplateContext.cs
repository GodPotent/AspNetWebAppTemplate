using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetWebAppTemplate.Models;

namespace AspNetWebAppTemplate.Data
{
    public class AspNetWebAppTemplateContext : DbContext
    {
        public AspNetWebAppTemplateContext (DbContextOptions<AspNetWebAppTemplateContext> options)
            : base(options)
        {
        }

        public DbSet<AspNetWebAppTemplate.Models.User> User { get; set; } = default!;
    }
}
