using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleTest.Models;
using System.Data;
using System.IO;
using System.Web.Routing;

namespace SimpleTest.Controllers
{
    public class baseController : Controller
    {
        //
        // GET: /base/

        //const
        //==============================================
        //因為MACT底下的專案一進去就會執行使用者驗證
        //所以這裡的baseController不去做使用者驗證不然會GG
        //還有要把TESTMODE設成True
        //=======================================
        private string APCODE = SimpleTest.Properties.Settings.Default.APCODE;
        public string UserName;
        public string UserAccount = "vicky076";
        public string UserEmail = "vicky076@soft-world.com.tw";
        public string CustIp;
        public string StringDept = "";
        public int Dept = 1;
        private mbase[] ButtonStruct;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.ServerVariables["HTTP_CLIENTIP"] != null && Request.ServerVariables["HTTP_CLIENTIP"] != "")
                CustIp = Request.ServerVariables["HTTP_CLIENTIP"];
            else if (Request.ServerVariables["REMOTE_ADDR"] != null && Request.ServerVariables["REMOTE_ADDR"] != "")
                CustIp = Request.ServerVariables["REMOTE_ADDR"];
            else if (Request.UserHostAddress != null && Request.UserHostAddress != "")
                CustIp = Request.UserHostAddress;
            else
                CustIp = "127.0.1.1";
            ////檢查是否有權限
            //使用者所有的資訊

            if (SimpleTest.Properties.Settings.Default.DebMode == "False")
            {
                string sAuthKey = "";


                if ((Request.Cookies[APCODE] != null))
                {
                    sAuthKey = Server.HtmlEncode(Request.Cookies[APCODE]["sAuthKey"]);
                    Session["sAuthKey"] = Server.HtmlEncode(Request.Cookies[APCODE]["sAuthKey"]);
                    Session["AuthKey"] = Server.HtmlEncode(Request.Cookies[APCODE]["sAuthKey"]);

                }
                else
                {
                    throw new Exception("驗證不過!請重新登入");
                }

                IsPortalValid();
                Userid();
                if (IsPortalValid() == false)
                    throw new Exception("驗證不過!請重新登入");

            }
            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            String Errormsg = String.Empty;

            //Exception unhandledException = Server.GetLastError();
            Exception unhandledException = filterContext.Exception;
            //Response.Clear();

            Exception httpException = unhandledException as Exception;

            Errormsg = "{3}發生例外網頁:{0}錯誤訊息:{1}堆疊內容:{2}";

            //if (httpException != null && !httpException.GetType().IsAssignableFrom(typeof(HttpException)))
            //{

            Errormsg = String.Format(Errormsg, Request.Path + Environment.NewLine,

                unhandledException.GetBaseException().Message + Environment.NewLine,
                unhandledException.StackTrace + Environment.NewLine,
                DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss") + Environment.NewLine + "User:" + UserName + Environment.NewLine + "IP:" + CustIp + Environment.NewLine);
            //寫入文字檔

            System.IO.File.AppendAllText(Server.MapPath("~/baseController.txt"), Errormsg);



            //寫入事件撿視器  

            //EventLog.WriteEntry("WebError", Errormsg, EventLogEntryType.Error);
            ErrorReference.wsError ws = new ErrorReference.wsError();
            //ws.InsertErrorLog(Request.Path, Errormsg, CustIp);

            Server.ClearError();
            //}
            base.OnException(filterContext);
            //ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            filterContext.Result = new ViewResult
            {
                ViewName = "Error"
            };
            filterContext.ExceptionHandled = true;
        }

        public void InsertErrorLog(string Method, string Message, string IP)
        {
            System.IO.File.AppendAllText(Server.MapPath(String.Format("~/ErrorLog-{0}.txt",
                DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss"))), Message);

            ErrorReference.wsError ws = new ErrorReference.wsError();
            ws.InsertErrorLog(Method, "USERNAME:" + UserName + Environment.NewLine + Message, CustIp);

        }
#if DEBUG

#else

#endif
        public void ChkButton(string caseName)
        {

            if (SimpleTest.Properties.Settings.Default.DebMode == "False")
            {
                DataTable oDT = (DataTable)Session["UserInfo"];
                UserName = oDT.Rows[0]["User_UName"].ToString();
                UserAccount = oDT.Rows[0]["User_UAccount"].ToString();
                UserEmail = oDT.Rows[0]["User_Email"].ToString();
                CheckButtonWLog(CustIp, UserName, caseName);
                Session["UserName"] = (UserName == null ? "TEST" : UserName);
                Session["UserAccount"] = (UserAccount == null ? "TEST" : UserAccount);
                Session["UserEmail"] = (UserEmail == null ? "TEST" : UserEmail);
                Session["qButton"] = (!CheckButton(mbase.EnumButtonCode.Query) ? "false" : "true");
                //Session["iButton"] = (!CheckButton(mbase.EnumButtonCode.Add) ? "false" : "true");
                //Session["eButton"] = (!CheckButton(mbase.EnumButtonCode.Edit) ? "false" : "true");
                //Session["dButton"] = (!CheckButton(mbase.EnumButtonCode.Delete ) ? "false" : "true");
            }
            else
            {
                UserName = "admin";
                Session["UserName"] = UserName;
                Session["UserAccount"] = "vicky076";
                Session["UserEmail"] = "vicky076@soft-world.com.tw";
                Session["qButton"] = "true";
                //Session["iButton"] = "true";
                //Session["eButton"] = "true";
                //Session["dButton"] = "true";
            }
        }

        private void Userid()
        {
            string sAuthKey = "";

            if ((Request.Cookies[APCODE] != null))
            {
                sAuthKey = Server.HtmlEncode(Request.Cookies[APCODE]["sAuthKey"]);
                Session["sAuthKey"] = Server.HtmlEncode(Request.Cookies[APCODE]["sAuthKey"]);
            }
            else
            {
                throw new Exception("驗證不過!請重新登入");
            }

            PortalReference.wsAppFunctions oAppFun = new PortalReference.wsAppFunctions();

            DataSet dsReturnData = default(DataSet);
            dsReturnData = oAppFun.APLogin(sAuthKey, APCODE);
            if (dsReturnData.Tables["header"].Rows[0]["ReturnCode"].ToString() == "Success")
            {
                //顯示錯誤頁
                //紀錄user資料

                //使用者所有的資訊
                Session["UserInfo"] = dsReturnData.Tables["datatxt"];
                //這個使用者所有可使用的頁面跟按鈕
                Session["PageInfo"] = dsReturnData.Tables["datapage"];
                Session["AuthKey"] = sAuthKey;
            }
            else
            {
                //顯示錯誤頁
                throw new Exception(dsReturnData.Tables["header"].Rows[0]["ReturnDesc"].ToString());
            }
        }

        protected void CheckButtonWLog(string userIp, string user, string caseName)
        {
            //驗證頁面跟按鈕是否有權限
            PortalLogReference.wsWorkLog Worklog = new PortalLogReference.wsWorkLog();
            string sCurrentUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf(Request.ApplicationPath) + Request.ApplicationPath.Length);
            string ReturnNom = "";
            DataTable dtPageInfo = (DataTable)Session["PageInfo"];
            try
            {
                ReturnNom = Worklog.InsertWorkLog(user, userIp, sCurrentUrl, dtPageInfo, caseName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            string[] arysPageButtons = ReturnNom.Split(new char[] { ',' });

            //呼叫這一隻是要用來顯示頁面上的ｂｕｔｔｏｎ
            RetrieveButtons(arysPageButtons);
        }

        private bool IsPortalValid()
        {
            //要判段驗證碼還在不在，不在就要重登
            PortalReference.wsAppFunctions oPortal = new PortalReference.wsAppFunctions();
            if (Session["AuthKey"] == null)
            {
                return false;
            }


            bool eResult = oPortal.CheckAuthIsValid(Session["AuthKey"].ToString(), APCODE);


            return eResult;

        }

        private void RetrieveButtons(string[] arysPageButtons)
        {
            ButtonStruct = new mbase[arysPageButtons.Length];

            for (int i = 0; i <= arysPageButtons.Length - 1; i++)
            {
                ButtonStruct[i] = new mbase(arysPageButtons[i]);
            }
        }

        private bool CheckButton(mbase.EnumButtonCode enButton)
        {
            try
            {
                if (Session["UserInfo"] == null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("撿查按鈕ex錯誤:" + enButton.ToString());
            }


            bool bResult = false;
            int i = 0;
            try
            {
                for (i = 0; i <= ButtonStruct.Length - 1; i++)
                {
                    //Response.Write(ButtonStruct(i).intButtonCode)
                    if (ButtonStruct[i].intButtonCode == enButton)
                    {
                        bResult = true;
                        break; // TODO: might not be correct. Was : Exit For
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(i + "撿查按鈕群組ex錯誤:" + ButtonStruct.ToString());
            }


            return bResult;
        }


        public void WorkLogTxt(string Message)
        {
            if (SimpleTest.Properties.Settings.Default.DebMode.ToUpper() == "False")
                System.IO.File.WriteAllText(Server.MapPath("~/ErrLogTxt.txt"), Message + Environment.NewLine);
            else
                System.IO.File.AppendAllText(Server.MapPath("~/ErrLogTxt.txt"), Message + Environment.NewLine);
        }

        //public Message smsg(string msg)
        //{
        //    InsertErrorLog("baseController.cs/smsg", "msg=" + msg, CustIp);
        //    Message mm = new Message();
        //    mm.message = msg;

        //    InsertErrorLog("baseController.cs/smsg", "mm.message=" + mm.message, CustIp);
        //    return mm;
        //}
    }
}
