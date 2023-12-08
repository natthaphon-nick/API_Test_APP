using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication2.Models;
using WebApplication2.Common;
using System.Threading.Tasks;
using WebApplication2.Controllers;
using static WebApplication2.Common.db;

namespace WebApplication2.Services
{
    public static class ShopService
    {
        public static List<Item> GetItems(int productID)
        {
            db conn = new db();
            string sql = @"";
            if (productID != 0)
            {
                sql = @"Select * From Tester.dbo.Shopping Where ProductID = " + productID;

            }
            else
            {
                sql = @"Select * From Tester.dbo.Shopping";
            }
            SqlDataReader dr = conn.ExecQuery(sql);
            var items = new List<Item>();
            while (dr.Read())
            {
                items.Add(new Item()
                {
                    Product_ID = Convert.ToInt32(dr["ProductID"].ToString()),
                    Product_Name = dr["ProductName"].ToString(),
                    Price = dr["Price"].ToString(),
                    Unit = Convert.ToInt32(dr["Unit"].ToString())   
                });
            }
            conn.Close();
            return items;
        }

        public static int PostItems(Item item)
        {
            db conn = new db();
            string sql = @"";
            List<SqlParam> sqlParam = new List<SqlParam>();
                sql = @"
                    UPDATE Shopping
                    SET Unit = N'" + item.Unit + "'" +
                    "WHERE ProductID = @ProductID";
            sqlParam.Add(new SqlParam() { name = "@ProductID", value = item.Product_ID });
            sqlParam.Add(new SqlParam() { name = "@Unit", value = item.Unit });
            int recordCount = conn.ExecNotQuery(sql, sqlParam);
            conn.Close();
            return recordCount;
        }


        public static List<Cart> GetCart()
        {
            db conn = new db();
            string sql = @"Select * From Tester.dbo.Cart";
            SqlDataReader dr = conn.ExecQuery(sql);
            var items = new List<Cart>();
            while (dr.Read())
            {
                items.Add(new Cart()
                {
                    Cart_ID = Convert.ToInt32(dr["Cart_ID"].ToString()),
                    Product_ID = dr["ProductID"].ToString(),
                    Product_Name = dr["ProductName"].ToString(),
                    Price = Convert.ToInt32(dr["Price"].ToString()),
                    Unit = Convert.ToInt32(dr["Unit"].ToString())
                });
            }
            conn.Close();
            return items;
        }

        public static List<total> report()
        {
            db conn = new db();
            string sql = @"SELECT sum(Price*Unit) AS total  FROM [Tester].[dbo].[Cart]";
            SqlDataReader dr = conn.ExecQuery(sql);
            var items = new List<total>();
            while (dr.Read())
            {
                items.Add(new total()
                {
                    Total = Convert.ToDouble(dr["total"].ToString())
                });
                
            }
            conn.Close();
            return items;
        }

        public static int PostCart(Cart cart)
        {
            db conn = new db();
            string sql = @"";
            List<SqlParam> sqlParam = new List<SqlParam>();
            if (cart.Cart_ID == 0)
            {
                sql = @"
                 INSERT INTO Tester.dbo.Cart
                (ProductID, ProductName, Price,Unit)
                values
                ( N'" + cart.Product_ID + "', N'" + cart.Product_Name + "', N'" + cart.Price + "', N'" + cart.Unit + "')  ";
            }
            else
            {
                sql = @"
                    UPDATE Tester.dbo.Cart
                    SET ProductID =  N'" + cart.Product_ID + "', " +
                        "ProductName =  N'" + cart.Product_Name + "', " +
                        "Price =  N'" + cart.Price + "', " +
                        "Unit = N'" + cart.Unit + "'" +
                    "WHERE Cart_ID = @Cart_ID";
            }
            sqlParam.Add(new SqlParam() { name = "@Cart_ID", value = cart.Cart_ID });
            sqlParam.Add(new SqlParam() { name = "@ProductID", value = cart.Product_ID });
            sqlParam.Add(new SqlParam() { name = "@ProductName", value = cart.Product_Name });
            sqlParam.Add(new SqlParam() { name = "@Price", value = cart.Price });
            sqlParam.Add(new SqlParam() { name = "@Unit", value = cart.Unit });
            int recordCount = conn.ExecNotQuery(sql, sqlParam);
            conn.Close();
            return recordCount;
        }

        public static bool DeleteCart(int cartid)
        {
            db conn = new db();
            int recNo = conn.ExecNotQuery(
                @"DELETE FROM Tester.dbo.Cart
                    WHERE Cart_ID =" + cartid);
            conn.Close();
            return (recNo > 0);
        }

        public static int DeleteAll(Item item)
        {
            db conn = new db();
            string sql = @"";
            List<SqlParam> sqlParam = new List<SqlParam>();
            sql = @"
                    UPDATE Tester.dbo.Shopping
                    SET Unit = N'" + item.Unit + "'" +
                   "WHERE Product_ID = @Product_ID";
            sqlParam.Add(new SqlParam() { name = "@ProductID", value = item.Product_ID });
            sqlParam.Add(new SqlParam() { name = "@Unit", value = item.Unit });
            int recordCount = conn.ExecNotQuery(sql, sqlParam);
            conn.Close();
            return recordCount;
        }
    }
}
