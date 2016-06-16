using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WebApplication2.Models
{
    public class OrderDetailsService
    {

        /// <summary>
        /// 刪除訂單所有明細
        /// </summary>
        /// <param name="orderid"></param>
        public void deleteOrderDetail(string orderid)
        {
            DataTable dt = new DataTable();
            String sql = @"DELETE FROM Sales.OrderDetails WHERE OrderID = @orderid";
            try
            {
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@orderid", orderid));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetDBConnectionString()
        {
            throw new NotImplementedException();
        }
    }
}