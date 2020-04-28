using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;


namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory>  context;

        public ProductCategoryManagerController(IRepository<ProductCategory> context )
        {
            this.context = context;
        }
        // GET: ProductMAnager
        public ActionResult Index()
        {
            List<ProductCategory> productcategories = context.Collection().ToList();
            return View(productcategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = context.Find(Id);
            if (productCategory == null)
                return HttpNotFound();
            else
                return View(productCategory);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategory == null)
                return HttpNotFound();
            else
            {
                if (!ModelState.IsValid)
                    return View(productCategory);

                productCategoryToEdit.Category = productCategory.Category;               

                context.Commit();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(String Id)
        {
            ProductCategory productCategoryToDel = context.Find(Id);
            if (productCategoryToDel == null)
                return HttpNotFound();
            else
                return View(productCategoryToDel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfrmDelete(String Id)
        {
            ProductCategory productCategoryToDel = context.Find(Id);
            if (productCategoryToDel == null)
                return HttpNotFound();
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

    }
}