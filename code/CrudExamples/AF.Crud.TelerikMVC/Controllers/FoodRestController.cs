using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AB.Crud.DataAccess;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;

namespace AF.Crud.TelerikMVC.Controllers
{
    public class FoodRestController : Controller
    {
        // GET: FoodRest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FoodDescriptions([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                using (var context = new FoodContext())
                {
                    return Json(context.FoodDescriptions.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public ActionResult Edit(string id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}