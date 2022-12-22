using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;

namespace SamuraiApp.UI
{
    public class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        
        public static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            //AddSamurai();
            //AddSamurais("Lile", "Ana");
            //AddVariousTypes();
            //RetrieveAndUpdateSamurai();
            RetrieveAndUpdateMultipleSamurais();
            //GetSamurais();
        }
        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Julie"};
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddSamurais(params string[] names)
        {
            foreach (var name in names)
            {
                _context.Samurais.Add(new Samurai { Name = name});                
            }
            _context.SaveChanges();
        }
        private static void AddVariousTypes()
        {
            _context.Samurais.AddRange( 
                new Samurai { Name = "Michael"},
                new Samurai { Name = "Koby" });
            _context.Battles.AddRange(
                new Battle { Name = "Kagemusha" },
                new Battle { Name = "Didgori" });
            _context.SaveChanges();
        }
        private static void GetSamurais()
        {
            Console.WriteLine($"Samurais in Db: {_context.Samurais.Count()} \n");
            foreach (var item in _context.Samurais)
            {
                Console.WriteLine(item.Name);
            }
        }
        private static void QueryFilters()
        {
            var query = _context.Samurais.Where(s => EF.Functions.Like(s.Name, "J%")).ToList();
        }
        public static void QueryAggregates() 
        {            
            //var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Sampson");
            //var samurai = _context.Samurais.FirstOrDefault(s => s.Id == 2);
            //searches by primary key
            var samurai = _context.Samurais.Find(2);
        }
        public static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Petre");
            samurai.Name += " San";
            _context.SaveChanges();
        }
        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Skip(2).Take(3).ToList();
            samurais.ForEach(s => s.Name += " San") ;
            _context.SaveChanges();
        }
        private static void RetrieveAndDeleteSamurai()
        {
            var samurai = _context.Samurais.Find(2);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }
    }
}
