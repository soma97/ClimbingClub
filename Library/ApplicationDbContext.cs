using ClimbingClub.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingClub.Library
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Loaning> Loanings { get; set; }
        public DbSet<MembershipFee> MembershipFees { get; set; }
        public DbSet<GearItem> GearItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ClimbingClubData.db");
            base.OnConfiguring(optionsBuilder);
        }
    }

}
