using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class OrderSearchArg
    {
        public string OrderId { get; set; }
        public string CustName { get; set; }
        public string CustId { get; set; }
        public string ShipperId { get; set; }
        public string OrderDate { get; set; }
        public string RequiredDate { get; set; }
        public string ShippedDate { get; set; }
        public string EmpId { get; set; }
        public string DeleteOrderId { get; set; }
    }
}