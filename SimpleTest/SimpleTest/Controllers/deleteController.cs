using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using SimpleTest.Models;

namespace SimpleTest.Controllers
{
    public class deleteController : baseController
    {
        //
        // GET: /delete/

        public ActionResult delete(string Sn, string P_cCardStart, string P_cCardEnd, string CreateUser)
        {
            ChkButton("SimpleTest/delete");

            if (string.IsNullOrEmpty(Sn).Equals(true) || string.IsNullOrEmpty(P_cCardStart).Equals(true) || string.IsNullOrEmpty(P_cCardEnd).Equals(true) || string.IsNullOrEmpty(CreateUser).Equals(true))
            {
                throw new Exception("請依照正常方式進入此頁!!");
            }

            ViewBag.Card = P_cCardStart + " ~ " + P_cCardEnd;
            insert model = new insert();
            model.P_iSn = Sn;
            model.P_cCardStart = P_cCardStart;
            model.P_cCardEnd = P_cCardEnd;
            model.P_cCreateUser = CreateUser;

            return View(model);
        }

        [HttpPost]
        public ActionResult delete(insert model)
        {
            ChkButton("SimpleTest/delete");
            if (Session["qButton"].ToString() != "true")
            {
                throw new Exception("使用者沒有權限");
            }

            MyCardNoSetWCF.Service1Client wsMyCardNoSetWCF = new MyCardNoSetWCF.Service1Client();
            MyCardNoSetWCF.ReturnValue result = new MyCardNoSetWCF.ReturnValue();
            ErrorReference.wsError ErrorLog = new ErrorReference.wsError();
            int Intreturnno;
            string inputValue = model.P_iSn.ToString() + "|0" + model.P_cProcDesc + "|" + UserAccount + "|" + DateTime.Now;
            try
            {
                WorkLogTxt("delete|" + inputValue);
                result = wsMyCardNoSetWCF.MyCard_SaveByCardBetweenUpdate(Convert.ToInt32(model.P_iSn), 0, model.P_cProcDesc, UserAccount);
            }
            catch (Exception ex)
            {
                Intreturnno = ErrorLog.InsertErrorLog(Server.MapPath("delete"), "delete資料時發生錯誤：" + inputValue + "|" + ex.ToString(), CustIp);
                WorkLogTxt("delete資料時發生錯誤|" + inputValue + ex.ToString());
                ModelState.AddModelError("ErrorMessage", "發生錯誤:" + ex.ToString());
                return View();
            }

            if (result.ReturnMsgNo != 1)
            {
                ModelState.AddModelError("ErrorMessage", "發生錯誤:" + result.ReturnMsg);
                return View(model);
            }

            ModelState.AddModelError("ErrorMessage", "修改完成");
            ViewBag.MsgNo = result.ReturnMsgNo.ToString();
            ViewBag.Card = model.P_cCardStart + " ~ " + model.P_cCardEnd;
            return View(model);
        }
    }
}
