using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Models;

namespace Api.Controllers
{

    public interface IProductsService
    {
        Product Add(Product item);
        IEnumerable<Product> GetAll();
        Product Find(int id);
        Product Find(string name);
        Product Remove(int id);
        void Update(Product item);
    }

    public class ProductsService : IProductsService
    {
        private static readonly ConcurrentDictionary<int, Product> Products = new ConcurrentDictionary<int, Product>();

        static ProductsService()
        {
            var ps = new List<Product>
            {
                new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
                new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
                new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
            };
            foreach (var p in ps)            
                Products[p.Id] = p;            
        }

        public Product Add(Product item)
        {
            item.Id = Products.Count+1;
            Products[item.Id] = item;
            return item;
        }

        public IEnumerable<Product> GetAll()
        {
            return Products.Values;
        }
        
        public Product Find(string name)
        {
            var item = Products.FirstOrDefault(p => p.Value.Name == name).Value;
            return item;
        }
        public Product Remove(int id)
        {
            Product item; ;
            Products.TryRemove(id, out item);
            return item;
        }

        public void Update(Product item)
        {
            Products[item.Id] = item;
        }

        public Product Find(int id)
        {
            Product item; ;
            Products.TryGetValue(id, out item);
            return item;
        }
    }
}