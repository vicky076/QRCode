using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using SimpleTest.Models;

namespace SimpleTest.Controllers
{
    public class importController : baseController
    {
        //
        // GET: /import/
        public ActionResult Import()
        {
            ViewBag.Error = "注意，檔案需為UTF8編碼";
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            if (file == null)
            {
                ViewBag.Error = "請選擇上傳檔案";
                return View();
            }

            //檢查
            StreamReader sr = new StreamReader(file.InputStream, System.Text.Encoding.Default);
            string data = sr.ReadToEnd();
            string[] rows = data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            ViewBag.Error = "";
            for (int i = 0; i < rows.Length; i++)
            {
                string[] row_data = rows[i].Split(',');
                if (row_data.Length != 4)
                {
                    ViewBag.Error += "格式錯誤於第" + (i + 1) + "行" + "<BR>";
                    return View();
                }

                #region 必填
                if (string.IsNullOrEmpty(row_data[0]).Equals(true))
                    ViewBag.Error += "卡號區間(起)不得為空<BR>";
                if (string.IsNullOrEmpty(row_data[1]).Equals(true))
                    ViewBag.Error += "卡號區間(迄)不得為空<BR>";
                if (string.IsNullOrEmpty(row_data[2]).Equals(true))
                    ViewBag.Error += "輸入領獎專區活動ID不得為空<BR>";
                #endregion
                #region 檢查格式
                insert model = new insert();
                if (!Regex.IsMatch(row_data[0], @"^[0-9a-zA-Z]*"))
                    ViewBag.Error += "卡號區間(起)格式錯誤於第" + (i + 1) + "行" + "<BR>";
                if (!Regex.IsMatch(row_data[1], @"^[0-9a-zA-Z]*"))
                    ViewBag.Error += "卡號區間(迄)格式錯誤於第" + (i + 1) + "行" + "<BR>";
                if (!Regex.IsMatch(row_data[2], @"^[0-9a-zA-Z]*"))
                    ViewBag.Error += "輸入領獎專區活動ID格式錯誤於第" + (i + 1) + "行" + "<BR>";
                #endregion

                if (row_data[0].Trim().Substring(0, 6) != row_data[1].Trim().Substring(0, 6))
                    ViewBag.Error += "卡號前六碼需相同！" + (i + 1) + "行" + "<BR>";

                if (Convert.ToInt32(row_data[1].Trim().Substring(6, 10)) < Convert.ToInt32(row_data[0].Trim().Substring(6, 10)))
                    ViewBag.Error += "結束卡號不得小於起始卡號！" + (i + 1) + "行" + "<BR>";

                if (Convert.ToInt32(row_data[1].Trim().Substring(6, 10)) == Convert.ToInt32(row_data[0].Trim().Substring(6, 10)))
                    ViewBag.Error += "起始卡號不得等於結束卡號！" + (i + 1) + "行" + "<BR>";
            }

            MyCardNoSetWCF.Service1Client wsMyCardNoSetWCF = new MyCardNoSetWCF.Service1Client();
            MyCardNoSetWCF.ReturnValueIns result = new MyCardNoSetWCF.ReturnValueIns();
            ErrorReference.wsError ErrorLog = new ErrorReference.wsError();
            //更新資料
            for (int i = 0; i < rows.Length; i++)
            {
                string[] row_data = rows[i].Split(',');
                try
                {
                    result = wsMyCardNoSetWCF.MyCard_SaveByCardBetweenInsert(row_data[0].Trim(), row_data[1].Trim(), row_data[2].Trim(), UserAccount);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (result.ReturnMsgNo != 1)
                {
                    ViewBag.Error = "匯入中斷於" + (i + 1) + "行" + result.ReturnMsg;
                    return View();
                }
            }
            ViewBag.Msg = "批次匯入成功！";
            return View("index");
        }
    }
}
