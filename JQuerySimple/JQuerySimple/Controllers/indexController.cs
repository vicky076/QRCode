using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using JQuerySimple.Models;

namespace JQuerySimple.Controllers
{
    public class indexController : Controller
    {
        public ActionResult Index()
        {
            ViewData["SelectData"] = new List<SelectListItem>{
                    new SelectListItem {Text = "Columns0", Value = "0"}, 
                    new SelectListItem {Text = "Columns1", Value = "1"}, 
                    new SelectListItem {Text = "Columns2", Value = "2"},
                    new SelectListItem {Text = "Columns3", Value = "3"}
            };
            return View();
        }

        //處理option資料
        public ActionResult OptIndex(string gender, index[] model)
        {
            string OptText, OptVal;
            if (model != null)
            {
                foreach (index Optmodel in model)
                {
                    OptText = Optmodel.OptTxt;
                    OptVal = Optmodel.OptVal;
                }
            }

            return Json("1", JsonRequestBehavior.DenyGet);
        }
    }
}
