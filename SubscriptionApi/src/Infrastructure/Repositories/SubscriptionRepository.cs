using Core.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository, IDisposable
    {
        private readonly Database.MarketingEntities context;

        private bool _disposed;

        public SubscriptionRepository()
        {
            context = new Database.MarketingEntities();
        }

        public Subscription Add(Subscription item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Subscription Find(string key)
        {
            var s = context.Subscriptions.FirstOrDefault(x => x.Id.ToString() == key);
            return s == null ? null : MapOut(s);
        }

        public IList<Subscription> FindAll(string email)
        {
            var s = context.Subscriptions.Where(x => x.Email == email).Select(MapOut);

            return s.ToList();
        }

        public IEnumerable<Subscription> GetAll()
        {
            return context.Subscriptions.Select(MapOut).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            _disposed = true;
        }
        private static Subscription MapOut(Database.Subscription subscription)
        {
            return new Subscription
            {
                NewsletterId = subscription.NewsletterId.ToString(),
                DateOfBirth = subscription.DateOfBirth,
                Email = subscription.Email,
                Gender = subscription.Gender,
                Id = subscription.Id.ToString(),
                FirstName = subscription.FirstName,
                MarketingConsent = subscription.AllowConsentForMarketing
            };
        }

        private Database.Subscription MapIn(Subscription subscription)
        {
            return new Database.Subscription
            {
                NewsletterId = Guid.Parse(subscription.NewsletterId),
                DateOfBirth = subscription.DateOfBirth,
                Email = subscription.Email,
                Gender = subscription.Gender,
                Id = Guid.Parse(subscription.Id),
                FirstName = subscription.FirstName,
                AllowConsentForMarketing = subscription.MarketingConsent
            };
        }
    }
}

//}