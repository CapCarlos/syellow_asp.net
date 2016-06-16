using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication2.Models
{
    /// <summary>
    /// 訂單服務
    /// </summary>
    public class OrderService
    {
            /// <summary>
            /// 新增訂單
            /// </summary>
            public void InsertOrder()
            {

            }

            /// <summary>
            /// 刪除訂單
            /// </summary>
            public void DeleteOrderById(string orderid)
            {
                DataTable dt = new DataTable();
                String sql = @"DELETE FROM Sales.Orders WHERE OrderID = @orderid";
                try {
                    using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.Add(new SqlParameter("@orderid", orderid));
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                catch(Exception ex) {
                    throw ex;
                }
             }

            /// <summary>
            /// 更新訂單
            /// </summary>
            public void UpdateOrder()
            {

            }

            /// <summary>
            /// 取得訂單
            /// </summary>
            /// <param name="id">訂單ID</param>
            /// <returns></returns>
            public Models.Order GetOrderById(string id)
            {
                Models.Order result = new Order();
                //result.CustId = "111";
                //result.CustName = "mis";
                return result;
            }
        
            /// <summary>
            /// 取得訂單(多筆)
            /// </summary>
            /// <returns></returns>
            public List<Models.Order> GetSelectOrder(Models.OrderSearchArg arg)
            {
                DataTable dt = new DataTable();
                
                String sql = @"select A.[OrderID],B.[CustomerID],B.[CompanyName] as CustName,D.EmployeeID,A.[OrderDate],A.[ShippedDate],
                                (D.[FirstName]+D.[LastName]) as EmployeeName,A.[Freight],A.[RequiredDate],A.[ShipAddress],A.[ShipCity],
                                A.[ShipCountry],C.ShipperID,C.CompanyName AS ShipName,A.ShipPostalCode,A.ShipRegion
                                from [Sales].[Orders] AS A INNER JOIN [Sales].[Customers] AS B ON A.CustomerID=B.CustomerID
                                INNER JOIN [Sales].[Shippers] AS C ON A.ShipperID=C.ShipperID
                                INNER JOIN [HR].[Employees] AS D ON A.EmployeeID=D.EmployeeID
                                WHERE (A.[OrderID] like @OrderId or @OrderId='')
                                and (B.[CompanyName] like @CustName or @CustName='')
                                and (A.[EmployeeID] like @EmpId or @EmpId = '')                               
                                and (A.OrderDate like @OrderDate or @OrderDate='')
                                and (A.ShippedDate like @ShippedDate or @ShippedDate='')
                                and (A.RequiredDate like @RequiredDate or @RequiredDate='')";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@OrderId", arg.OrderId == null ? string.Empty : arg.OrderId));
                    cmd.Parameters.Add(new SqlParameter("@CustName", arg.CustName == null ? string.Empty : arg.CustName));
                    cmd.Parameters.Add(new SqlParameter("@EmpId", arg.EmpId == null ? string.Empty : arg.EmpId));
                    cmd.Parameters.Add(new SqlParameter("@ShipperId", arg.ShipperId == null ? string.Empty : arg.ShipperId));
                    cmd.Parameters.Add(new SqlParameter("@OrderDate", arg.OrderDate == null ? string.Empty : arg.OrderDate));
                    cmd.Parameters.Add(new SqlParameter("@ShippedDate", arg.ShippedDate == null ? string.Empty : arg.ShippedDate));
                    cmd.Parameters.Add(new SqlParameter("@RequiredDate", arg.RequiredDate == null ? string.Empty : arg.RequiredDate));
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                    sqlAdapter.Fill(dt);
                    conn.Close();
                }
                return this.MapOrderDataToList(dt);
            }
        
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        private List<Order> MapOrderDataToList(DataTable dt)
        {
            List<Models.Order> result = new List<Order>();

            foreach (DataRow row in dt.Rows)
            {
                result.Add(new Order()
                {
                    CustId = row["CustomerID"].ToString(),
                    CustName = row["CustName"].ToString(),
                    EmpId = (int)row["EmployeeID"],
                    EmpName = row["EmployeeName"].ToString(),
                    Freight = (decimal)row["Freight"],
                    OrderDate = row["OrderDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["OrderDate"],
                    OrderId = (int)row["OrderID"],
                    RequiredDate = row["RequiredDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["RequiredDate"],
                    ShipAddress = row["ShipAddress"].ToString(),
                    ShipCity = row["ShipCity"].ToString(),
                    ShipCountry = row["ShipCountry"].ToString(),
                    ShipName = row["ShipName"].ToString(),
                    ShippedDate = row["ShippedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["ShippedDate"],
                    ShipperId = (int)row["ShipperID"],
                    ShipPostalCode = row["ShipPostalCode"].ToString(),
                    ShipRegion = row["ShipRegion"].ToString()
                });
            }
            return result;
        }
    }
    
}