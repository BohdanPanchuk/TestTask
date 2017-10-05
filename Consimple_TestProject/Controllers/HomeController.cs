using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Consimple_TestProject.Models;
using Consimple_TestProject.Helper;

namespace Consimple_TestProject.Controllers
{
    public class HomeController : Controller
    {
        Database database = new Database();
        List<Order> ordersList = new List<Order>();
        
        public ActionResult Index()
        {
            ViewBag.ProductsInDB = database.GetAllProducts();
            
            if (ordersList.Count != 0)
            {
                ordersList = database.GetAllOrders();
            }

            ViewBag.ProductsInCart = ordersList;

            return View();
        }

        [HttpPost]
        public ActionResult AddProductInOrder(int productId, string productName, int productPrice, int productQuantity, bool nextOrder)
        {
            Order order = new Order();

            if (ordersList.Count != 0)
            {
                ordersList = database.GetAllOrders();
            }

            order.Id = 1;
            order.OrderId = 1;
            order.ProductId = productId;
            order.ProductName = productName;
            order.ProductPrice = productPrice;
            order.ProductQuantity = productQuantity;
            order.OrderDate = DateTime.Now.Date;

            if (ordersList.Count != 0)
            {
                foreach (Order existingOrder in ordersList)
                {
                    if (order.Id == existingOrder.Id)
                    {
                        order.Id++;
                    }
                }
            }

            if (nextOrder == true)
            {
                foreach (Order existingOrder in ordersList)
                {
                    if (order.OrderId == existingOrder.OrderId)
                    {
                        order.OrderId++;
                    }
                }
            }

            ordersList.Add(order);

            database.AddOrderInDatabase(order);

            ViewBag.ProductsInCart = ordersList;

            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult AddOrderInDatabase()
        //{
        //    foreach(Order order in ordersList)
        //    {
        //        database.AddOrderInDatabase(order);
        //    }

        //    return RedirectToAction("Index");
        //}
    }
}