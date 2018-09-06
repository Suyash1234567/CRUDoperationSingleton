using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Crud_New.Models;
using Microsoft.Extensions.Configuration;

namespace Crud_New.Controllers
{

    public class HomeController : Controller
    {
        //private readonly DBOperations dBOperations;
        //public HomeController{dBOperations=DBOperations}



        ProductViewModel model = new ProductViewModel();
        private IConfiguration _config;
        public HomeController(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult Index()
        {
            var myString = _config.GetSection("MyConnection").GetSection("ConnString").Value;
            DBOperations dbObj = DBOperations.getInstance();
            List<ProductCategory> products = dbObj.getAllProducts(myString);
            model.ProductList = products;
            return View(model);
        }


        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(ProductCategory product)
        {
            var myString = _config.GetSection("MyConnection").GetSection("ConnString").Value;
            DBOperations dbObj = DBOperations.getInstance();
            int res= dbObj.AddProduct(product,myString);
            List<ProductCategory> products = dbObj.getAllProducts(myString);
            model.ProductList = products;
            //return View(model);

            return RedirectToAction("Index", "Home");
            
        }

        public IActionResult Edit(int Id)
        {
            var myString = _config.GetSection("MyConnection").GetSection("ConnString").Value;
            DBOperations dbObj = DBOperations.getInstance();
            ProductCategory product = dbObj.GetProductDetails(Id, myString);
           // model.ProductList = product;
            return View(product);
           // return View();
        }

        public IActionResult Delete(int Id)
        {
            var myString = _config.GetSection("MyConnection").GetSection("ConnString").Value;
            DBOperations dbObj = DBOperations.getInstance();
            ProductCategory product = dbObj.GetProductDetails(Id, myString);
            // model.ProductList = product;
            return View(product);
            // return View();
        }

        public IActionResult Detail(int Id)
        {
            var myString = _config.GetSection("MyConnection").GetSection("ConnString").Value;
            DBOperations dbObj = DBOperations.getInstance();
            ProductCategory product = dbObj.GetProductDetails(Id, myString);
            // model.ProductList = product;
            return View(product);
            // return View();
        }

        [HttpPost]
        public IActionResult Edit(ProductCategory product)
        {
            var myString = _config.GetSection("MyConnection").GetSection("ConnString").Value;
            DBOperations dbObj = DBOperations.getInstance();
            int res = dbObj.EditProduct(product, myString);
            List<ProductCategory> products = dbObj.getAllProducts(myString);
            model.ProductList = products;
            //return View(model);

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public IActionResult Delete(ProductCategory product)
        {
            var myString = _config.GetSection("MyConnection").GetSection("ConnString").Value;
            DBOperations dbObj = DBOperations.getInstance();
            int res = dbObj.DeleteProduct(product.ProductId, myString);
            List<ProductCategory> products = dbObj.getAllProducts(myString);
            model.ProductList = products;
            //return View(model);

            return RedirectToAction("Index", "Home");

        }




        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            ViewData["COnn"] = _config.GetSection("Data").GetSection("ProductConnection").GetSection("ConnectionString").Value; ;
            var myString= _config.GetSection("MyConnection").GetSection("ConnString").Value; ;
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
