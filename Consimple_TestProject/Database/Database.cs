using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Consimple_TestProject.Models;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Consimple_TestProject.Helper
{
    public class Database
    {
        public static string connectionString = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\ProjectDatabase.mdf";

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

            using (SqlConnection sqlConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + connectionString + ";Integrated Security=True; Connect Timeout = 30"))
            {
                string sqlRequest = @"SELECT * FROM [dbo].[Products];";

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
                string sqlRequest = @"INSERT INTO [Orders] (Id, OrderId, ProductId, ProductName, ProductPrice, ProductQuantity, OrderDate) VALUES(" + order.Id + ", " + order.OrderId + ", " + order.ProductId + ", " + "'" + order.ProductName + "', " + order.ProductPrice + "," + order.ProductQuantity + "," + order.OrderDate + ")";
                SqlCommand sqlCommand = new SqlCommand(sqlRequest, sqlConnection);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}