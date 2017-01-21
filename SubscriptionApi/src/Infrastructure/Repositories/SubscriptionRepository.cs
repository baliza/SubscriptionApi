using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Core.Models;
using Core.Services;

namespace Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private static readonly ConcurrentDictionary<string, Subscription> Subscriptions = new ConcurrentDictionary<string, Subscription>();

        public SubscriptionRepository()
        {
            Add(new Subscription { FirstName = "Item1" });
        }

        public Subscription Add(Subscription item)
        {
            item.Key = Guid.NewGuid().ToString();
            Subscriptions[item.Key] = item;
            return item;
        }

        public IEnumerable<Subscription> GetAll()
        {
            return Subscriptions.Values;
        }

        public Subscription Find(string id)
        {
            Subscription item;
            Subscriptions.TryGetValue(id, out item);
            return item;
        }

        public Subscription Remove(string id)
        {
            Subscription item; ;
            Subscriptions.TryRemove(id, out item);
            return item;
        }

        public void Update(Subscription item)
        {
            Subscriptions[item.Key] = item;
        }
    }
}
