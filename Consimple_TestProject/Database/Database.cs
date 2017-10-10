using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Consimple_TestProject.Models;
using System.Data.SqlClient;

namespace Consimple_TestProject.Helper
{
    public class Database
    {
        private static string connectionString = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\ProjectDatabase.mdf";
        private int totalOrderPrice;
        private DateTime orderDate;

        public List<Product> GetAllProducts()
        {
            List<Product> productsList = new List<Product>();

            using (SqlConnection sqlConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + connectionString + ";Integrated Security=True; Connect Timeout = 30"))
            {
                string sqlRequest = @"SELECT * FROM [dbo].[Products];";
                
                SqlCommand sqlCommand = new SqlCommand(sqlRequest, sqlConnection);

                sqlConnection.Open();

                var reader = sqlCommand.ExecuteReader();

                while(reader.Read())
                {
                    Product product = new Product();

                    product.Id = (int)reader["Id"];
                    product.Name = (string)reader["Name"];
                    
                    productsList.Add(product);
                }

                sqlConnection.Close();
            }

            return productsList;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> ordersList = new List<Order>();
            Order order = new Order();

            using (SqlConnection sqlConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + connectionString + ";Integrated Security=True; Connect Timeout = 30"))
            {
                string sqlRequest = @"SELECT * FROM [dbo].[Orders];";

                SqlCommand sqlCommand = new SqlCommand(sqlRequest, sqlConnection);

                sqlConnection.Open();

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    order.Id = (int)reader["Id"];
                    order.OrderId = (int)reader["OrderId"];
                    order.ProductId = (int)reader["ProductId"];
                    order.ProductName = (string)reader["ProductName"];
                    order.ProductPrice = (int)reader["ProductPrice"];
                    order.ProductQuantity = (int)reader["ProductQuantity"];
                    order.OrderDate = (DateTime)reader["OrderDate"];

                    ordersList.Add(order);
                }

                sqlConnection.Close();
            }

            return ordersList;
        }

        public List<Order> GetOrderInfo(int orderId)
        {
            List<Order> ordersList = new List<Order>();

            using (SqlConnection sqlConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + connectionString + ";Integrated Security=True; Connect Timeout = 30"))
            {
                string sqlRequest = @"SELECT * FROM Orders WHERE OrderId = " + orderId + ";";

                SqlCommand sqlCommand = new SqlCommand(sqlRequest, sqlConnection);

                sqlConnection.Open();

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Order order = new Order();

                    order.Id = (int)reader["Id"];
                    order.OrderId = (int)reader["OrderId"];
                    order.ProductId = (int)reader["ProductId"];
                    order.ProductName = (string)reader["ProductName"];
                    order.ProductPrice = (int)reader["ProductPrice"];
                    order.ProductQuantity = (int)reader["ProductQuantity"];
                    order.OrderDate = (DateTime)reader["OrderDate"];

                    totalOrderPrice += order.ProductPrice * order.ProductQuantity;

                    orderDate = order.OrderDate;

                    ordersList.Add(order);
                }

                sqlConnection.Close();
            }

            return ordersList;
        }

        public void AddOrderInDatabase(Order order)
        {
            using (SqlConnection sqlConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + connectionString + ";Integrated Security=True; Connect Timeout = 30"))
            {
                string sqlRequest = @"DECLARE @currentdate date
                                      SET @currentDate = (SELECT CONVERT(date, " + "'" + order.OrderDate + "'" + "))"
                                   + "INSERT INTO [dbo].[Orders] (OrderId, ProductId, ProductName, ProductPrice, ProductQuantity, OrderDate) VALUES(" + order.OrderId + ", " + order.ProductId + ", " + "'" + order.ProductName + "', " + order.ProductPrice + "," + order.ProductQuantity + "," + "@currentDate" + ")";
                SqlCommand sqlCommand = new SqlCommand(sqlRequest, sqlConnection);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
        
        public int GetOrdersCount()
        {
            int ordersCount;

            using (SqlConnection sqlConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + connectionString + ";Integrated Security=True; Connect Timeout = 30"))
            {
                string sqlRequest = @"SELECT COUNT(DISTINCT OrderId) FROM Orders";
                SqlCommand sqlCommand = new SqlCommand(sqlRequest, sqlConnection);
                sqlConnection.Open();

                var reader = sqlCommand.ExecuteScalar();
                ordersCount = (int)reader;

                sqlConnection.Close();
            }

            return ordersCount;
        }

        public int GetTotalOrderPrice()
        {
            return totalOrderPrice;
        }

        public DateTime GetOrderDate()
        {
            return orderDate;
        }

        // date in "yyyy-dd-mm" format
        public int GetAllOrdersCountForDate(string OrderDate)
        {
            int ordersCount;

            using (SqlConnection sqlConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + connectionString + ";Integrated Security=True; Connect Timeout = 30"))
            {
                string sqlRequest = @"SELECT COUNT(DISTINCT OrderId) FROM Orders WHERE OrderDate = " + "'" + OrderDate + "'";
                SqlCommand sqlCommand = new SqlCommand(sqlRequest, sqlConnection);
                sqlConnection.Open();

                var reader = sqlCommand.ExecuteScalar();
                ordersCount = (int)reader;

                sqlConnection.Close();
            }

            return ordersCount;
        }
    }
}