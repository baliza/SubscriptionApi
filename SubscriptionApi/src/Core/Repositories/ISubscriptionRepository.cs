using System.Collections.Generic;
using Core.Models;

namespace Core.Repositories
{
    public interface ISubscriptionRepository
    {
        Subscription Add(Subscription subscription);
        IEnumerable<Subscription> GetAll();
        Subscription Find(string id);
        IList<Subscription> FindAll(string email);       
    }
}
    

