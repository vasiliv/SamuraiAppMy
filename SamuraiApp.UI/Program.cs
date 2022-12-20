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
            AddVariousTypes();
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
    }
}
