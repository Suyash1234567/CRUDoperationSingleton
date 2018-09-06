using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;



namespace Crud_New.Models
{
    public class DBOperations
    {
        ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        private SqlCommand cmd;
        private SqlConnection conn;
        private SqlDataAdapter adp;

        private static DBOperations instance = null;
        static readonly object instance1 = new object();

        private DBOperations()
        {

        }

        public static DBOperations getInstance()
        {
            if (instance == null)
            {
                lock (instance1)
                {
                    if (instance == null)
                    {
                        instance = new DBOperations();
                    }
                }
            }
            return instance;
        }

        public int AddProduct(ProductCategory productCat, string connStr)
        {
            using (conn = new SqlConnection(connStr))
            {
                conn.Open();
                cmd = new SqlCommand("insert into productTable (ProductName,ProductPrice,CategoryId,DateCreated)values(@name,@price,@catId,@dateCreated)", conn);
                cmd.Parameters.AddWithValue("@name", productCat.ProductName);
                cmd.Parameters.AddWithValue("@price", productCat.ProductPrice);
                cmd.Parameters.AddWithValue("@catId", productCat.CategoryId);
                cmd.Parameters.AddWithValue("@dateCreated", DateTime.Now.Date);
                var result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
        }

        public int EditProduct(ProductCategory productCat, string connStr)
        {
            using (conn = new SqlConnection(connStr))
            {
                conn.Open();
                cmd = new SqlCommand("update productTable SET ProductName='"+productCat.ProductName+"',ProductPrice='"+productCat.ProductPrice+ "' WHERE ProductId="+productCat.ProductId, conn);

                //UPDATE table_name SET column1 = value1, column2 = value2, ... WHERE condition;


                //cmd.Parameters.AddWithValue("@name", productCat.ProductName);
                //cmd.Parameters.AddWithValue("@price", productCat.ProductPrice);
                //cmd.Parameters.AddWithValue("@catId", productCat.CategoryId);
                //cmd.Parameters.AddWithValue("@dateCreated", DateTime.Now.Date);
                var result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
        }

        public int DeleteProduct(int prodId, string connStr)
        {
            using (conn = new SqlConnection(connStr))
            {
                conn.Open();
                cmd = new SqlCommand("Delete from productTable WHERE ProductId=" + prodId, conn);

               
                var result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
        }

        public List<ProductCategory> getAllProducts(string connStr)
        {
            using (conn = new SqlConnection(connStr))
            {
                ///For returning object of Products
                List<ProductCategory> obj = new List<ProductCategory>();
                conn.Open();
                cmd = new SqlCommand("select a.ProductId,a.ProductName,a.ProductPrice,a.CategoryId,a.DateCreated, b.CategoryName,b.IsActive from productTable a inner join ProductCategory  b on a.CategoryId = b.CategoryId ", conn);

                //For Selecting records we need Data Adapter and Data Table
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
               foreach(DataRow dr in dt.Rows)
                {
                    ProductCategory cat = new ProductCategory();
                    cat.ProductId = Convert.ToInt16(dr["ProductId"].ToString());
                    cat.ProductName = Convert.ToString(dr["ProductName"].ToString());
                    cat.ProductPrice = Convert.ToString(dr["ProductPrice"].ToString());
                    cat.CategoryId = Convert.ToInt16(dr["CategoryId"].ToString());
                    cat.DateCreated = Convert.ToString(dr["DateCreated"].ToString());
                    cat.CategoryName = Convert.ToString(dr["CategoryName"].ToString());
                    obj.Add(cat);
                }
                return obj;
            }
        }

        public ProductCategory GetProductDetails(int ProdId, string connStr)
        {
            using (conn = new SqlConnection(connStr))
            {
                ///For returning object of Products
                List<ProductCategory> obj = new List<ProductCategory>();
                conn.Open();
                cmd = new SqlCommand("select a.ProductId,a.ProductName,a.ProductPrice,a.CategoryId,a.DateCreated, b.CategoryName,b.IsActive from productTable a inner join ProductCategory  b on a.CategoryId = b.CategoryId Where ProductId=" + ProdId, conn);

                //For Selecting records we need Data Adapter and Data Table
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ProductCategory cat = new ProductCategory();
                foreach (DataRow dr in dt.Rows)
                {
                    
                    cat.ProductId = Convert.ToInt16(dr["ProductId"].ToString());
                    cat.ProductName = Convert.ToString(dr["ProductName"].ToString());
                    cat.ProductPrice = Convert.ToString(dr["ProductPrice"].ToString());
                    cat.CategoryId = Convert.ToInt16(dr["CategoryId"].ToString());
                    cat.DateCreated = Convert.ToString(dr["DateCreated"].ToString());
                    cat.CategoryName = Convert.ToString(dr["CategoryName"].ToString());
                    obj.Add(cat);
                }
                return cat;
            }
        }

        //var root = configurationBuilder.Build;
        //private string connStr = configurationBuilder.Build().ConnectionStrings["DemoConnection"].ToString();
    }
}
