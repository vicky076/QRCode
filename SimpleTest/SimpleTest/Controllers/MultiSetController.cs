using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleTest.Models;
using System.IO;
using System.Data;

namespace SimpleTest.Controllers
{
    public class MultiSetController : baseController
    {
        //
        // GET: /MultiSet/
        public List<MutiDataList> MutiList = new List<MutiDataList>();
        public ActionResult MultiSet()
        {
            ViewBag.Count = -1;
            return View();
        }

        [HttpPost]
        public ActionResult MultiSet(MultiSet model)
        {
             ViewBag.Count  = 0;
            if (model.MutiSetting != null && model.MutiSetting.Count > 0)
            {
                foreach (var item in model.MutiSetting)
                {
                    MutiDataList DataList = new Models.MutiDataList();
                    DataList.P_cCardStart = item.P_cCardStart;
                    DataList.P_cCardEnd = item.P_cCardEnd;
                    DataList.P_cActID = item.P_cActID;
                    MutiList.Add(DataList);
                }
                ViewBag.Query = MutiList;
                ViewBag.Count = model.MutiSetting.Count - 1;

                foreach (var item in model.MutiSetting)
                {
                    #region 驗證
                    if (string.IsNullOrEmpty(item.P_cCardStart).Equals(true) || string.IsNullOrEmpty(item.P_cCardEnd).Equals(true))
                    {
                        ModelState.AddModelError("ErrorMessage", "發生錯誤：卡號不得空白！");
                        return View();
                    }

                    if (item.P_cCardStart.Trim().Length < 6 || item.P_cCardEnd.Trim().Length < 6)
                    {
                        ModelState.AddModelError("ErrorMessage", "發生錯誤：卡號長度錯誤！");
                        return View();
                    }

                    if (item.P_cCardStart.Trim().Substring(0, 6) != item.P_cCardEnd.Trim().Substring(0, 6))
                    {
                        ModelState.AddModelError("ErrorMessage", "發生錯誤：卡號前六碼需相同！");
                        return View();
                    }
                    #endregion

                    MyCardNoSetWCF.Service1Client wsMyCardNoSetWCF = new MyCardNoSetWCF.Service1Client();
                    MyCardNoSetWCF.ReturnValueIns result = new MyCardNoSetWCF.ReturnValueIns();
                    ErrorReference.wsError ErrorLog = new ErrorReference.wsError();
                    int Intreturnno;
                    string inputValue = item.P_cCardStart + "|" + item.P_cCardEnd + "|" + item.P_cActID + "|" + UserAccount;
                    try
                    {
                        //WorkLogTxt("MultiSet|" + inputValue);
                        //result = wsMyCardNoSetWCF.MyCard_SaveByCardBetweenInsert(item.P_cCardStart.Trim(), item.P_cCardEnd.Trim(), item.P_cActID, UserAccount);
                    }
                    catch (Exception ex)
                    {
                        Intreturnno = ErrorLog.InsertErrorLog(Server.MapPath("MultiSet"), "MultiSet資料時發生錯誤：" + inputValue + "|" + ex.ToString(), CustIp);
                        WorkLogTxt("MultiSet資料時發生錯誤|" + inputValue + ex.ToString());
                        ModelState.AddModelError("ErrorMessage", "發生錯誤:" + ex.ToString());
                        return View();
                    }

                    if (result.ReturnMsgNo != 1)
                    {
                        ModelState.AddModelError("ErrorMessage", "發生錯誤:" + result.ReturnMsg);
                        return View();
                    }

                    ViewBag.MsgNo = result.ReturnMsgNo.ToString();
                    ModelState.AddModelError("ErrorMessage", "多筆建立成功");
                }
            }
            return View(model);
        }

        //public ActionResult InsertRow(MultiSet model)
        //{
        //    if (model.MutiSetting != null && model.MutiSetting.Count > 0)
        //    {
        //        foreach (var item in model.MutiSetting)
        //        {
        //            MutiDataList DataList = new Models.MutiDataList();
        //            DataList.P_cCardStart = item.P_cCardStart;
        //            DataList.P_cCardEnd = item.P_cCardEnd;
        //            DataList.P_cActID = item.P_cActID;
        //            MutiList.Add(DataList);
        //        }
        //    }

        //    MutiDataList DataListNew = new Models.MutiDataList();
        //    DataListNew.P_cCardStart = "";
        //    DataListNew.P_cCardEnd = "";
        //    DataListNew.P_cActID = "";
        //    MutiList.Add(DataListNew);

        //    TempData["NewMutiList"] = MutiList;
        //    TempData["InsertRow"] = "InsertRow";
        //    model.MutiSetting = (List<MutiDataList>)TempData["NewMutiList"];
        //    return RedirectToAction("MultiSet", "MultiSet");
        //}

    }
}
