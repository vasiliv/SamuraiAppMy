using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
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
            //RetrieveAndUpdateMultipleSamurais();
            //QueryAndUpdateBattlesDisconnected();
            //InsertNewSamuraiWithQuote();
            //AddQuotesToExistingQuoteWhileTracked();
            //AddQuoteToExistingSamuraiNotTracked(5);
            //Simpler_AddQuoteToExistingSamuraiNotTracked(6);
            EagerLoadSamuraiWithQuotes();
            //GetSamurais();
        }
        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Julie" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddSamurais(params string[] names)
        {
            foreach (var name in names)
            {
                _context.Samurais.Add(new Samurai { Name = name });
            }
            _context.SaveChanges();
        }
        private static void AddVariousTypes()
        {
            _context.Samurais.AddRange(
                new Samurai { Name = "Michael" },
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
            samurais.ForEach(s => s.Name += " San");
            _context.SaveChanges();
        }
        private static void RetrieveAndDeleteSamurai()
        {
            var samurai = _context.Samurais.Find(2);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }
        private static void QueryAndUpdateBattlesDisconnected()
        {
            List<Battle> disconnectedBattles;
            using (var context1 = new SamuraiContext())
            {
                disconnectedBattles = context1.Battles.ToList();
            }
            var firstBattle = disconnectedBattles.Find(b => b.BattleId == 1);
            var secondBattle = disconnectedBattles.Find(b => b.BattleId == 2);

            firstBattle.StartDate = new DateTime(1115, 10, 25);
            firstBattle.EndDate = new DateTime(1115, 10, 26);

            secondBattle.StartDate = new DateTime(1129, 10, 13);
            secondBattle.EndDate = new DateTime(1129, 10, 14);

            using (var context2 = new SamuraiContext())
            {
                context2.Update(firstBattle);
                context2.Update(secondBattle);
                context2.SaveChanges();
            }
        }
        private static void InsertNewSamuraiWithQuote()
        {
            var samurai = new Samurai
            {
                Name = "Takeda",
                Quotes = new List<Quote>
                { 
                    new Quote() { Text = "rac mogiva davitao yvela sheni tavitao" }
                }
            };
            _context.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddQuotesToExistingQuoteWhileTracked()
        {
            var samurai = _context.Samurais.Find(3);
            samurai.Quotes.Add(new Quote() { Text = "rac ar gergeba ar shegergeba" });
            _context.SaveChanges();
        }
        private static void AddQuoteToExistingSamuraiNotTracked(int samuraiId)
        {
            var samurai = _context.Samurais.Find(samuraiId);
            samurai.Quotes.Add(new Quote() { Text = "kvici gvarze xtiso" });
            using (var context = new SamuraiContext())
            {
                context.Samurais.Update(samurai);
                context.SaveChanges();
            }
        }
        private static void Simpler_AddQuoteToExistingSamuraiNotTracked(int samuraiId)
        {
            var quote = new Quote { Text = "chits aprena undoda, xelis aqnevas elodebodao", SamuraiId = samuraiId };
            using (var context = new SamuraiContext())
            {
                context.Quote.Add(quote);
                context.SaveChanges();
            }
        }
        private static void EagerLoadSamuraiWithQuotes()
        {
            var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();
        }
        private static void ProjectSamuraisWithQuotes()
        {
            //using anonymous types
            var samuraisWithQuotes = _context.Samurais.Select(s => new {s.Id, s.Name, s.Quotes, NumberOfQuotes = s.Quotes.Count() }).ToList();
        }
    }
}
