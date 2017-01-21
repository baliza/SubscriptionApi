using System.Collections.Generic;
using Core.Models;

namespace Core.Repositories
{
    public interface INewsletterRepository
    {
        Newsletter Find(string id);
    }
}
    

