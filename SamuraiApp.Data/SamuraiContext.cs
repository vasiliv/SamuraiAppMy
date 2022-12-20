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
        public DbSet<Battle> Battles { get; set; }
        //public DbSet<BattleSamurai> BattleSamurais { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString)
                // add logger
                .LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.Battles) /*Battles property of the Samurai class*/ 
                .WithMany(b => b.Samurais) /*Samurais property of the Battle class*/
                .UsingEntity<BattleSamurai>
                    (bs => bs.HasOne<Battle>().WithMany(), /*bs or battlesamurai has one-to-many relationship with Battle*/
                     bs => bs.HasOne<Samurai>().WithMany()) /*bs or battlesamurai has one-to-many relationship with Samurai*/
                .Property(bs => bs.DateJoined)
                .HasDefaultValueSql("getdate()");
        }
    }
}
