﻿using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
                products = new List<Product>();
        }
        public void Commit()
        {
            cache["products"] = products;
        }
        public void Insert(Product prod)
        {
            products.Add(prod);
        }
        public void Update(Product prod)
        {
            Product productToUpdate = products.Find(p => p.Id == prod.Id);
            if (productToUpdate != null)
                productToUpdate = prod;
            else
                throw new Exception("Product not found!!");
        }
        public Product Find(String Id)
        {
            Product productToFind = products.Find(p => p.Id == Id);
            if (productToFind != null)
                return productToFind;
            else
                throw new Exception("Product not found!!");
        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete != null)
                products.Remove(productToDelete);
            else
                throw new Exception("Product not found!!");
        }
    }
}
