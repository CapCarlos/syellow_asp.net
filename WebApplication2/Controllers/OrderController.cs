using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class OrderController : Controller
    {
       
        private Boolean seachOrder = false;
        // GET: Order
        [HttpGet]
        public ActionResult Index()
        {
            Models.CodeService codeService = new Models.CodeService();
            ViewBag.EmpData = codeService.GetEmpName();
            ViewBag.ShipperData = codeService.GetShipperName();
            ViewBag.seachOrder = seachOrder;

            return View();
        }
        /// <summary>
        /// 取得訂單資訊
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(Models.OrderSearchArg arg)
        {
            Models.OrderService orderService = new Models.OrderService();
            ViewBag.selectOrder = orderService.GetSelectOrder(arg);

            Models.CodeService codeService = new Models.CodeService();
            ViewBag.EmpData = codeService.GetEmpName();
            ViewBag.ShipperData = codeService.GetShipperName();

            seachOrder = true;
            ViewBag.seachOrder = seachOrder;

            return View("Index");
        }

        /// <summary>
        /// 依訂單編號刪除定單
        /// </summary>
        /// <param name="OrderId"></param>
        /// <return></return>
        [HttpPost]
        public JsonResult deleteOrder(string orderid)
        {
            try {
                Models.OrderService orderService = new Models.OrderService();
                orderService.DeleteOrderById(orderid);

                Models.OrderDetailsService orderdetailservice = new Models.OrderDetailsService();
                orderdetailservice.deleteOrderDetail(orderid);

                return this.Json(true);
            }
            catch {
                return this.Json(false);
            }

        }

        /// <summary>
        /// 顯示新增訂單畫面
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertOrder()
        {
            Models.CodeService codeService = new Models.CodeService();
            ViewBag.CustCodeData = codeService.GetCustName();
            ViewBag.EmpCodeData = codeService.GetEmpName();
            ViewBag.ShipCodeData = codeService.GetShipperName();

            return View(new Models.Order());
        }


    }
}