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
        
        public ActionResult Index()
        {
            List<Order> ordersList = new List<Order>();

            ViewBag.ProductsInDB = database.GetAllProducts();

            int orderId = database.GetOrdersCount();
            ordersList = database.GetOrderInfo(orderId);

            ViewBag.ProductsInCart = ordersList;
            ViewBag.TotalOrderPrice = database.GetTotalOrderPrice();
            ViewBag.OrderDate = database.GetOrderDate();
            ViewBag.OrdersCount = database.GetAllOrdersCountForDate(DateTime.Now.Date.ToString("d"));

            return View();
        }

        [HttpPost]
        public ActionResult AddProductInOrder(List<Order> orders)
        {
            Order order = new Order();

            int currentOrderId = database.GetOrdersCount();

            currentOrderId++;

            foreach (Order existingOrder in orders)
            {
                order.OrderId = currentOrderId;
                order.ProductId = existingOrder.ProductId;
                order.ProductName = existingOrder.ProductName;
                order.ProductPrice = existingOrder.ProductPrice;
                order.ProductQuantity = existingOrder.ProductQuantity;
                order.OrderDate = DateTime.Now.Date;

                database.AddOrderInDatabase(order);
            }

            return RedirectToAction("Index");
        }
    }
}