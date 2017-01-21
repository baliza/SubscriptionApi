using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Core.Models;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class NewsletterRepository : INewsletterRepository
    {
        private static readonly ConcurrentDictionary<string, Newsletter> Newsletters = new ConcurrentDictionary<string, Newsletter>();

        public NewsletterRepository()
        {
            var newsletterGuid = Guid.Parse("755302af-6569-40e2-a49a-74f7882d68c6");
            var item = new Newsletter
            {
                Key = newsletterGuid.ToString(),
                Name = "Sport Challenge",
                Start = new DateTime(2017, 1, 1),
                End = new DateTime(2027, 1, 1),
            };
            Newsletters[item.Key] = item;
        }

        public Newsletter Add(Newsletter item)
        {
            item.Key = Guid.NewGuid().ToString();
            Newsletters[item.Key] = item;
            return item;
        }

        public IEnumerable<Newsletter> GetAll()
        {
            return Newsletters.Values;
        }

        public Newsletter Find(string id)
        {
            Newsletter item;
            Newsletters.TryGetValue(id, out item);
            return item;
        }
    }
}
