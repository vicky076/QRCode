using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleTest.Models;
using System.Text;
using System.Data;

namespace SimpleTest.Controllers
{
    public class insertController : baseController
    {
        //
        // GET: /insert/

        public ActionResult Insert()
        {
            ChkButton("SimpleTest/insert");

            insert model = new insert();
            return View(model);
        }

        [HttpPost]
        public ActionResult Insert(insert model)
        {
            ChkButton("SimpleTest/insert");
            if (Session["qButton"].ToString() != "true")
            {
                throw new Exception("使用者沒有權限");
            }

            if (model.P_cCardStart.Trim().Substring(0, 6) != model.P_cCardEnd.Trim().Substring(0, 6))
            {
                ModelState.AddModelError("ErrorMessage", "發生錯誤：卡號前六碼需相同！");
                return View(model);
            }

            if (Convert.ToInt32(model.P_cCardEnd.Trim().Substring(6, 10)) < Convert.ToInt32(model.P_cCardStart.Trim().Substring(6, 10)))
            {
                ModelState.AddModelError("ErrorMessage", "發生錯誤：結束卡號不得小於起始卡號！");
                return View(model);
            }

            if (Convert.ToInt32(model.P_cCardEnd.Trim().Substring(6, 10)) == Convert.ToInt32(model.P_cCardStart.Trim().Substring(6, 10)))
            {
                ModelState.AddModelError("ErrorMessage", "發生錯誤：起始卡號不得等於結束卡號！");
                return View(model);
            }

            MyCardNoSetWCF.Service1Client wsMyCardNoSetWCF = new MyCardNoSetWCF.Service1Client();
            MyCardNoSetWCF.ReturnValueIns result = new MyCardNoSetWCF.ReturnValueIns();
            ErrorReference.wsError ErrorLog = new ErrorReference.wsError();
            int Intreturnno;
            string inputValue = model.P_cCardStart + "|" + model.P_cCardEnd + "|" + model.P_cActID + "|" + UserAccount;
            try
            {
                WorkLogTxt("insert|" + inputValue);
                result = wsMyCardNoSetWCF.MyCard_SaveByCardBetweenInsert(model.P_cCardStart.Trim(), model.P_cCardEnd.Trim(), model.P_cActID, UserAccount);
            }
            catch (Exception ex)
            {
                Intreturnno = ErrorLog.InsertErrorLog(Server.MapPath("insert"), "insert資料時發生錯誤：" + inputValue + "|" + ex.ToString(), CustIp);
                WorkLogTxt("insert資料時發生錯誤|" + inputValue + ex.ToString());
                ModelState.AddModelError("ErrorMessage", "發生錯誤:" + ex.ToString());
                return View();
            }

            if (result.ReturnMsgNo != 1)
            {
                ModelState.AddModelError("ErrorMessage", "發生錯誤:" + result.ReturnMsg);
                return View(model);
            }

            ViewBag.MsgNo = result.ReturnMsgNo.ToString();
            ModelState.AddModelError("ErrorMessage", "建立成功，建檔編號：" + result.ReturnSn.ToString());

            return View(model);
        }
    }
}
