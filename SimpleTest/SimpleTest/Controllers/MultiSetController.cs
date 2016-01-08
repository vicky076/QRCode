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
            ViewBag.Count = 0;
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

        //相片輪播
        public ActionResult MultiImg()
        {
            string File = "/images/";  //資料夾路徑
            string Str = "";
            //組html顯示語法
            for (int i = 1; i <= 3; i++)
            {
                Str += "<p><a class='group2'  href='" + File + "ohoopee" + i.ToString() + ".jpg'" + "' title= 範例照片" + i.ToString() + ">範例照片" + i.ToString() + "</a></p>";
            }

            ViewBag.QueryImage = Str;
            return View();
        }

        //多筆上傳
        [HttpPost]
        public ActionResult MultiImg(FormCollection form)
        {
            if (Request.Files.Count == 0)
            {
                ViewBag.Error = "請選擇上傳檔案";
                return View();
            }

            string FileName, AttachedFile;
            string FilePath = Server.MapPath("~/ImgUpload/");

            //檢查:資料夾是否存在(若沒有則建立它)
            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);

            for (int i = 0; i < Request.Files.Count; i++)
            {
                FileName = Path.GetFileName(Request.Files[i].FileName);
                AttachedFile = Path.GetExtension(FileName);

                if (AttachedFile.ToLower() == ".jpeg" || AttachedFile.ToLower() == ".jpg" || AttachedFile.ToLower() == ".png" || AttachedFile.ToLower() == ".gif")
                {
                    //檢查檔案大小
                    if (Request.TotalBytes > 10000000)
                    {
                        ViewBag.Error = "檔案大小超過限制(約10MB)。";
                        return View();
                    }

                    //檢查:檔案是否存在(若沒有則建立它)
                    if (System.IO.File.Exists(FilePath + FileName))
                        System.IO.File.Delete(FilePath + FileName);

                    try
                    {
                        Request.Files[i].SaveAs(Path.Combine(FilePath, FileName));
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "上傳發生錯誤。" + ex.ToString();
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = "副檔名格式不符(.jpeg/.jpg/.png/.gif)";
                    return View();
                }
            }

            ViewBag.Error = "上傳成功。";
            combinationHTML();
            return View();
        }

        //組HTML顯示
        public void combinationHTML()
        {
            string FilePath = Server.MapPath("~/ImgUpload/");
            string File = "/ImgUpload/";  //資料夾路徑
            string Str = "";
            string FileName = "";

            //取資料夾內檔案個數
            foreach (var item in System.IO.Directory.GetFiles(FilePath))
            {
                FileName = Path.GetFileName(item);
                Str += "<p><a class='group2'  href='" + File + FileName + "' title= 照片>" + FileName + "</a></p>";
            }
            ViewBag.QueryImage = Str;
        }
    }
}
