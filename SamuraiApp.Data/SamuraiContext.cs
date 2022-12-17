using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        private string connectionString = "server=(localdb)\\MSSQLLocalDB;database=SamuraiAppDataMy;Trusted_Connection=true";
        public SamuraiContext() 
        {

        }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quote { get; set; }
        //public DbSet<BattleSamurai> BattleSamurais { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.Battles)
                .WithMany(b => b.Samurais)
                .UsingEntity<BattleSamurai>
                    (bs => bs.HasOne<Battle>().WithMany(),
                     bs => bs.HasOne<Samurai>().WithMany())
                .Property(bs => bs.DateJoined)
                .HasDefaultValueSql("getdate()");
        }
    }
}
