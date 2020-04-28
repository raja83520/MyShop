using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {

        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
                productCategories = new List<ProductCategory>();
        }
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }
        public void Insert(ProductCategory prod)
        {
            productCategories.Add(prod);
        }
        public void Update(ProductCategory prod)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == prod.Id);
            if (productCategoryToUpdate != null)
                productCategoryToUpdate = prod;
            else
                throw new Exception("Product not found!!");
        }
        public ProductCategory Find(String Id)
        {
            ProductCategory productCategoryToFind = productCategories.Find(p => p.Id == Id);
            if (productCategoryToFind != null)
                return productCategoryToFind;
            else
                throw new Exception("ProductCategory not found!!");
        }
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);
            if (productCategoryToDelete != null)
                productCategories.Remove(productCategoryToDelete);
            else
                throw new Exception("ProductCategory not found!!");
        }
    }
}
