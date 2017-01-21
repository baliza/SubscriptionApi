using System.Collections.Generic;
using Core.Models;

namespace Core.Repositories
{
    public interface ISubscriptionRepository
    {
        Subscription Add(Subscription item);
        IEnumerable<Subscription> GetAll();
        Subscription Find(string key);
        Subscription Remove(string key);
        void Update(Subscription item);
    }
}
    

