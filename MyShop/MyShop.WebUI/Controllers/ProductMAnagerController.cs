﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productcontext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productcontext;
            productCategories = productCategoryContext;
        }
        // GET: ProductMAnager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
                return HttpNotFound();
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);
            if (product == null)
                return HttpNotFound();
            else
            {
                if (!ModelState.IsValid)
                    return View(product);

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Iamge = product.Iamge;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(String Id)
        {
            Product productToDel = context.Find(Id);
            if (productToDel == null)
                return HttpNotFound();
            else
                return View(productToDel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfrmDelete(String Id)
        {
            Product productToDel = context.Find(Id);
            if (productToDel == null)
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