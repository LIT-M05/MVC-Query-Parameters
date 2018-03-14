using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class NorthwindManager
    {
        private string _connectionString;

        public NorthwindManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Order> GetOrders()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Orders";
            connection.Open();
            List<Order> orders = new List<Order>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Order order = new Order
                {
                    Id = (int)reader["OrderId"],
                    OrderDate = (DateTime)reader["OrderDate"],
                    ShipAddress = (string)reader["ShipAddress"],
                    ShipName = (string)reader["ShipName"]
                };
                //object shippedDate = reader["ShippedDate"];
                //if (shippedDate != DBNull.Value)
                //{
                //    order.ShippedDate = (DateTime)shippedDate;
                //}
                //object shipRegion = reader["ShipRegion"];
                //if (shipRegion != DBNull.Value)
                //{
                //    order.ShipRegion = (string) shipRegion;
                //}
                order.ShippedDate = reader.GetOrNull<DateTime?>("ShippedDate");
                order.ShipRegion = reader.GetOrNull<string>("ShipRegion");
                orders.Add(order);
            }

            return orders;
        }

        public IEnumerable<OrderDetail> GetOrderDetailsFor97()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT od.* FROM [Order Details] od 
                              JOIN Orders o 
                              ON od.OrderId = o.OrderId
                              WHERE o.OrderDate BETWEEN '01/01/1997' AND '12/31/1997'";
            connection.Open();
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                orderDetails.Add(new OrderDetail
                {
                    OrderId = (int)reader["OrderId"],
                    Quantity = (short)reader["Quantity"],
                    UnitPrice = (decimal)reader["UnitPrice"]
                });
            }

            return orderDetails;
        }

        public IEnumerable<OrderDetail> DetailsForOrder(int orderId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM [Order Details] WHERE 
                                OrderId = @orderId";
            cmd.Parameters.AddWithValue("@orderId", orderId);
            connection.Open();
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                orderDetails.Add(new OrderDetail
                {
                    OrderId = (int)reader["OrderId"],
                    Quantity = (short)reader["Quantity"],
                    UnitPrice = (decimal)reader["UnitPrice"]
                });
            }

            return orderDetails;
        }

        public IEnumerable<Category> GetCategories()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Categories";
            connection.Open();
            List<Category> categories = new List<Category>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Id = (int)reader["CategoryId"],
                    Description = (string)reader["Description"],
                    Name = (string)reader["CategoryName"]
                });
            }

            return categories;
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Products WHERE CategoryId = @catid";
            cmd.Parameters.AddWithValue("@catid", categoryId);
            connection.Open();
            List<Product> products = new List<Product>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = (int)reader["ProductId"],
                    Name = (string)reader["ProductName"],
                    UnitPrice = (decimal)reader["UnitPrice"],
                    QuantityPerUnit = (string)reader["QuantityPerUnit"],
                    UnitsInStock = (short)reader["UnitsInStock"]
                });
            }

            return products;
        }

        public IEnumerable<Product> SearchProducts(string searchText)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Products WHERE ProductName LIKE @prodName";
            cmd.Parameters.AddWithValue("@prodName", $"%{searchText}%");
            connection.Open();
            List<Product> products = new List<Product>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = (int)reader["ProductId"],
                    Name = (string)reader["ProductName"],
                    UnitPrice = (decimal)reader["UnitPrice"],
                    QuantityPerUnit = (string)reader["QuantityPerUnit"],
                    UnitsInStock = (short)reader["UnitsInStock"]
                });
            }

            return products;
        }

        public string GetCategoryName(int categoryId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT Top 1 CategoryName 
                                FROM Categories WHERE CategoryId = @catid";
            cmd.Parameters.AddWithValue("@catid", categoryId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return (string) reader["CategoryName"];
            }

            return null;
        }
    }

    public static class ReaderExtension
    {
        public static T GetOrNull<T>(this SqlDataReader reader, string columnName)
        {
            object obj = reader[columnName];
            if (obj != DBNull.Value)
            {
                return (T) obj;
            }

            return default(T);
        }
    }
}