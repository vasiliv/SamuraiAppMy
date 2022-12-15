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
            AddSamurai();
            GetSamurais();
        }
        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Julie"};
            _context.Samurais.Add(samurai);
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
