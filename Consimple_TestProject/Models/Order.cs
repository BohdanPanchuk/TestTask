using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consimple_TestProject.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Щоб знайти для якої покупки товар. 
        // Наприклад, orderId = 1 для декількох товарів.
        // Для наступної групи orderId = 2.
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}