using Core.Models;
using Core.Repositories;
using System;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class NewsletterRepository : INewsletterRepository, IDisposable
    {
        private readonly Database.MarketingEntities _context;

        private bool _disposed;

        public NewsletterRepository()
        {
            _context = new Database.MarketingEntities();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Newsletter Find(string id)
        {
            var gId = Guid.Parse(id);
            var n = _context.Newsletters.FirstOrDefault(x => x.Id == gId);
            if (n == null) return null;

            return new Newsletter
            {
                Id = n.Id.ToString(),
                Name = n.Name,
                Start = n.Start,
                End = n.End,
            };
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}