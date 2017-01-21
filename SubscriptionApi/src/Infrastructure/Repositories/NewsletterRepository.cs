using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Add(new Newsletter
                {
                    Key = newsletterGuid.ToString(),
                    Name = "Newsletter name",
                    Start = new DateTime(1, 1, 2017),
                    End = new DateTime(1, 1, 2027),
                }
            );
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
