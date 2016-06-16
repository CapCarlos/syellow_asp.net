using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class CodeService
    {
        private int prid=-1;
        /// <summary>
        /// 取得員工編號、員工名稱
        /// </summary>
        /// <returns>員工編號、員工名稱</returns>
        public List<SelectListItem> GetEmpName()
        {
           
            DataTable dt = new DataTable();
            string sql = @"select [EmployeeID] as CodeId,([FirstName]+[LastName]) as CodeName  from [HR].[Employees]";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        /// <summary>
        /// 取得出貨公司編號、出貨公司名稱
        /// </summary>
        /// <returns>出貨公司編號、出貨公司名稱</returns>
        public List<SelectListItem> GetShipperName()
        {
           
            DataTable dt = new DataTable();
            string sql = @"select [ShipperID] as CodeId ,[CompanyName] as CodeName from [Sales].[Shippers]";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        /// <summary>
        /// 取得客戶編號、客戶名稱
        /// </summary>
        /// <returns>客戶編號、客戶名稱</returns>
        public List<SelectListItem> GetCustName()
        {
           
            DataTable dt = new DataTable();
            string sql = @"select [CustomerID] as CodeId ,[CompanyName] as CodeName from [Sales].[Customers]";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }


        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        private List<SelectListItem> MapCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();


            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CodeName"].ToString(),
                    Value = row["CodeId"].ToString(),
                    Selected= row["CodeId"].Equals(prid)
                });
            }
            return result;
        }
    }
}