using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GreenpeaceWeatherAdvisory.Models
{
    public class DBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Advisory> Advisories { get; set; }

        public DbSet<ChikkaMessage> ChikkaMessages { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
    }
}