using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleTest.Models;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Net.Mail;
using System.Web.UI;
using NPOI;
using NPOI.HSSF;
using NPOI.HSSF.Util;
using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace SimpleTest.Controllers
{
    public class indexController : baseController
    {
        //
        // GET: /index/
        public List<DataList> QueryList = new List<DataList>();
        public ActionResult Index()
        {
            ChkButton("SimpleTest/index");
            if (Session["qButton"].ToString() != "true")
            {
                throw new Exception("使用者沒有權限");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(string CardNo, string Sn, string ActIDQ, string newpage)
        {
            ChkButton("SimpleTest/index");
            if (Session["qButton"].ToString() != "true")
            {
                throw new Exception("使用者沒有權限");
            }

            MyCardNoSetWCF.Service1Client wsMyCardNoSetWCF = new MyCardNoSetWCF.Service1Client();
            MyCardNoSetWCF.ReturnValue result = new MyCardNoSetWCF.ReturnValue();
            ErrorReference.wsError ErrorLog = new ErrorReference.wsError();
            int Intreturnno;

            string InputValue = CardNo + "|" + Sn + "|" + ActIDQ + "|" + DateTime.Now;
            try
            {
                result = wsMyCardNoSetWCF.View_MyCard_SaveByCardBetweenQuery(CardNo, Sn, ActIDQ);
            }
            catch (Exception ex)
            {
                Intreturnno = ErrorLog.InsertErrorLog(Server.MapPath("Index"), "index查詢資料時發生錯誤：" + InputValue + "|" + ex.ToString(), CustIp);
                WorkLogTxt("index查詢資料時發生錯誤|" + InputValue + "|" + ex.ToString());
                ModelState.AddModelError("ErrorMessage", "發生錯誤:" + ex.ToString());
                ViewBag.Msg = "查詢時發生錯誤|" + ex.ToString();
                return View();
            }

            Session["Query"] = result.ReturnDataSet;

            //頁次/換頁計算
            int total = (result.ReturnDataSet.Tables[0].Rows.Count > 0 ? result.ReturnDataSet.Tables[0].Rows.Count : 0);  //總資料數
            int page = 0;  //目前頁次
            int rowS = 0;  //row起始位置
            int rowE = 0;  //row結束位置

            if (total > 0)
            {
                int pagerow = Convert.ToInt32(SimpleTest.Properties.Settings.Default.pagcount); //設定一頁幾筆資料
                newpage = (string.IsNullOrEmpty(Convert.ToString(newpage)).Equals(true) ? "1" : newpage);  //新頁次
                if (pagerow > 0)
                {
                    if (total % pagerow > 0)  //總資料筆數%一頁幾筆資料 (取餘數)
                        page = Convert.ToInt32(Math.Floor((double)total / pagerow) + 1);
                    else
                        page = Convert.ToInt32(Math.Floor((double)total / pagerow));

                    if (Convert.ToInt32(newpage) > page)
                        newpage = "1";

                    rowS = (pagerow * (Convert.ToInt32(newpage) - 1));  //資料起始位置

                    if ((rowS + pagerow) <= total) //資料結束位置
                        rowE = rowS + pagerow;
                    else
                        rowE = total;
                }

                for (int i = rowS; i < rowE; i++)
                {
                    DataList DataList = new Models.DataList();
                    DataList.Sn = Convert.ToString(result.ReturnDataSet.Tables[0].Rows[i]["Sn"]);
                    DataList.CreateDate = Convert.ToString(result.ReturnDataSet.Tables[0].Rows[i]["CreateDate"]);
                    DataList.Card_Start = Convert.ToString(result.ReturnDataSet.Tables[0].Rows[i]["Card_Start"]);
                    DataList.Card_End = Convert.ToString(result.ReturnDataSet.Tables[0].Rows[i]["Card_End"]);
                    DataList.CARD_POINT = Convert.ToString(result.ReturnDataSet.Tables[0].Rows[i]["CARD_POINT"]);
                    DataList.ActID = Convert.ToString(result.ReturnDataSet.Tables[0].Rows[i]["ActID"]);
                    DataList.ActTitle = Convert.ToString(result.ReturnDataSet.Tables[0].Rows[i]["ActTitle"]);
                    DataList.CreateUser = Convert.ToString(result.ReturnDataSet.Tables[0].Rows[i]["CreateUser"]);
                    QueryList.Add(DataList);
                }
            }

            ViewBag.page = page;
            ViewBag.newpage = newpage;
            ViewBag.Query = QueryList;

            if (QueryList.Count() <= 0)
                ViewBag.Msg = "查無建檔記錄！";
            else
                ViewBag.Msg = "查詢成功！";

            return View();
        }

        //匯出excel/寄mail
        public ActionResult Export(string stype)
        {
            if (Session["Query"] != null)
            {
                DataSet ds = (DataSet)Session["Query"];
                ds = ChangeDS(ds);
                var grid = new System.Web.UI.WebControls.GridView();

                #region 匯出excel (NPOI 2.0版 For .NET  4.0寫法)
                if (stype == "excel")
                {
                    #region 第一種寫法：NPOI
                    //HSSFWorkbook workbook = new HSSFWorkbook();                             //工作簿
                    //MemoryStream ms = new MemoryStream();                                         //記憶體位置
                    //HSSFSheet sheet_A = (HSSFSheet)workbook.CreateSheet("A");     //Sheet名稱     
                    //int rowIndex = 0;  //儲存格列數

                    //#region 儲存格樣式
                    ////建立第一列的儲存格格式
                    //NPOI.SS.UserModel.ICellStyle cs1 = workbook.CreateCellStyle();
                    ////字型為粗體黑字
                    //NPOI.SS.UserModel.IFont fWhiteBold = workbook.CreateFont();
                    ////fWhiteBold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;    //粗體
                    //fWhiteBold.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    //cs1.SetFont(fWhiteBold);

                    ////只需加框線的style
                    //HSSFCellStyle oStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                    //oStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    //oStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    //oStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    //oStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    //oStyle.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    //oStyle.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    //oStyle.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    //oStyle.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    ////oStyle.Alignment = HorizontalAlignment.Center;  //置中

                    //#endregion
                    //#region 表頭
                    ////產生第一列 (第一列:0, 第二列:1, ...類推)
                    //NPOI.HSSF.UserModel.HSSFRow r1 = (NPOI.HSSF.UserModel.HSSFRow)sheet_A.CreateRow(rowIndex);
                    ////建立第一列的第一格 (A1, 第一格:0, 第二格:1, ...類推)
                    //NPOI.HSSF.UserModel.HSSFCell c0 = (NPOI.HSSF.UserModel.HSSFCell)r1.CreateCell(0);
                    //NPOI.HSSF.UserModel.HSSFCell c1 = (NPOI.HSSF.UserModel.HSSFCell)r1.CreateCell(1);
                    //NPOI.HSSF.UserModel.HSSFCell c2 = (NPOI.HSSF.UserModel.HSSFCell)r1.CreateCell(2);
                    //NPOI.HSSF.UserModel.HSSFCell c3 = (NPOI.HSSF.UserModel.HSSFCell)r1.CreateCell(3);
                    //NPOI.HSSF.UserModel.HSSFCell c4 = (NPOI.HSSF.UserModel.HSSFCell)r1.CreateCell(4);
                    //NPOI.HSSF.UserModel.HSSFCell c5 = (NPOI.HSSF.UserModel.HSSFCell)r1.CreateCell(5);
                    //NPOI.HSSF.UserModel.HSSFCell c6 = (NPOI.HSSF.UserModel.HSSFCell)r1.CreateCell(6);

                    ////設定A1儲存格格式及內容值
                    //c0.CellStyle = oStyle;
                    //c0.SetCellValue("建檔編號");
                    //c1.CellStyle = oStyle;
                    //c1.SetCellValue("建立時間");
                    //c2.CellStyle = oStyle;
                    //c2.SetCellValue("起始卡號");
                    //c3.CellStyle = oStyle;
                    //c3.SetCellValue("結束卡號");
                    //c4.CellStyle = oStyle;
                    //c4.SetCellValue("點數面額");
                    //c5.CellStyle = oStyle;
                    //c5.SetCellValue("活動名稱");
                    //c6.CellStyle = oStyle;
                    //c6.SetCellValue("建檔人");
                    //#endregion

                    //rowIndex++;

                    ////建立內容值
                    //foreach (DataRow item in ds.Tables[0].Rows)
                    //{
                    //    NPOI.HSSF.UserModel.HSSFRow dataRow = (NPOI.HSSF.UserModel.HSSFRow)sheet_A.CreateRow(rowIndex);
                    //    dataRow.CreateCell(0).SetCellValue(item["建檔編號"].ToString());
                    //    dataRow.CreateCell(1).SetCellValue(item["建立時間"].ToString());
                    //    dataRow.CreateCell(2).SetCellValue(item["起始卡號"].ToString());
                    //    dataRow.CreateCell(3).SetCellValue(item["結束卡號"].ToString());
                    //    dataRow.CreateCell(4).SetCellValue(item["點數面額"].ToString());
                    //    dataRow.CreateCell(5).SetCellValue(item["活動名稱"].ToString());
                    //    dataRow.CreateCell(6).SetCellValue(item["建檔人"].ToString());

                    //    for (int ttI = 0; ttI < dataRow.Cells.Count; ttI++)
                    //    {
                    //        dataRow.Cells[ttI].CellStyle = oStyle;
                    //    }

                    //    //設定換行
                    //    //dataRow.Cells[0].CellStyle.WrapText = true;
                    //    rowIndex++;
                    //}

                    ////寫入 Memory
                    //workbook.Write(ms);
                    ////設定預設檔名
                    //Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode("會員儲值活動卡號區間設定.xls"));
                    ////傳送到 Client
                    //Response.BinaryWrite(ms.ToArray());
                    ////關閉 Workbook
                    //workbook = null;
                    ////關閉 Memory
                    //ms.Close();
                    //ms.Dispose();
                    #endregion

                    #region 第二種寫法：將GridView直接產出
                    grid.DataSource = ds;
                    grid.DataBind();
                    foreach (System.Web.UI.WebControls.GridViewRow dr in grid.Rows)
                    {
                        foreach (System.Web.UI.WebControls.TableCell tc in dr.Cells)
                            tc.Attributes.Add("class", "text");
                    }

               
                        Response.Clear();
                        Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode("會員儲值活動卡號區間設定.xls"));
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/vnd.xls";
                        Response.Charset = "UTF-8";
                        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
                        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                        grid.RenderControl(htmlWrite);
                        Response.Write("<style> .text { mso-number-format:\\@; } </style>" + stringWrite.ToString().Replace("<div>", "").Replace("</div>", ""));
                        Response.End();
                        Response.Clear();
                    #endregion
                }
                #endregion

                #region 寄送mail
                if (stype == "mail")
                {
                    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
                    grid.RenderControl(htmlWrite);

                    System.Net.Mail.MailMessage newMail = new System.Net.Mail.MailMessage();
                    System.Net.Mail.SmtpClient smtpMail = new System.Net.Mail.SmtpClient();

                    smtpMail.Host = SimpleTest.Properties.Settings.Default.MailHost;

                    try
                    {
                        //附檔
                        Attachment data = new Attachment(new System.IO.MemoryStream(System.Text.Encoding.Default.GetBytes("<style> .text { mso-number-format:\\@; } </style> " + stringWrite.ToString())), string.Format("{0}.xls", "會員儲值活動卡號區間設定"), "application/vnd.xls");
                        //================內文===================
                        string body;
                        body = "";

                        newMail.From = new System.Net.Mail.MailAddress("Service@mycard520.com.tw", "MyCard");  //寄件者
                        newMail.Body = body;   //'內文
                        newMail.Subject = "會員儲值活動卡號區間設定";//'主旨
                        newMail.Attachments.Add(data);
                        newMail.To.Add(new System.Net.Mail.MailAddress(UserEmail, "to"));
                        newMail.IsBodyHtml = true;                      //'是否為HTML格式
                        newMail.Priority = System.Net.Mail.MailPriority.Normal; //'優先權              
                        smtpMail.Send(newMail);
                        newMail.Dispose();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("Error_", "寄Email發生錯誤(101)：" + ex.Message);
                        ViewBag.Msg = "寄Email發生錯誤(101)";
                        return View("index");
                    }
                    ViewBag.Msg = "寄信成功！";
                    return View("index");
                }
                #endregion
            }
            else
            {
                ViewBag.Msg = "請先執行查詢！";
                return View("index");
            }

            ViewBag.Msg = "匯出失敗，無查詢內容。！";
            return View("index");

            //另開alert視窗
            //return View("show_Message", smsg("匯出失敗，無查詢內容。"));  
        }

        //匯出excel前重組ds，可只取需要匯出的欄位
        public DataSet ChangeDS(DataSet ds)
        {
            DataSet ChangeDS = new DataSet();
            ChangeDS.Tables.Add("ChangeDS");
            ChangeDS.Tables["ChangeDS"].Columns.Add("建檔編號");
            ChangeDS.Tables["ChangeDS"].Columns.Add("建立時間");
            ChangeDS.Tables["ChangeDS"].Columns.Add("起始卡號");
            ChangeDS.Tables["ChangeDS"].Columns.Add("結束卡號");
            ChangeDS.Tables["ChangeDS"].Columns.Add("點數面額");
            ChangeDS.Tables["ChangeDS"].Columns.Add("活動名稱");
            ChangeDS.Tables["ChangeDS"].Columns.Add("建檔人");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ChangeDS.Tables[0].NewRow();
                dr["建檔編號"] = ds.Tables[0].Rows[i]["Sn"].ToString();
                dr["建立時間"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreateDate"]).ToString("yyyy/MM/dd hh:mm:ss");
                dr["起始卡號"] = ds.Tables[0].Rows[i]["Card_Start"].ToString();
                dr["結束卡號"] = ds.Tables[0].Rows[i]["Card_End"].ToString();
                dr["點數面額"] = ds.Tables[0].Rows[i]["CARD_POINT"].ToString();
                dr["活動名稱"] = ds.Tables[0].Rows[i]["ActID"].ToString();
                dr["建檔人"] = ds.Tables[0].Rows[i]["CreateUser"].ToString();
                ChangeDS.Tables["ChangeDS"].Rows.Add(dr);
            }

            return ChangeDS;
        }
    }
}
