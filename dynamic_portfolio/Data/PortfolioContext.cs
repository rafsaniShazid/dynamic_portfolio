using System.Data.Entity;
using dynamic_portfolio.Models;

namespace dynamic_portfolio.Data
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext() : base("name=PortfolioDb")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Profile> Profiles { get; set; }
    }
}