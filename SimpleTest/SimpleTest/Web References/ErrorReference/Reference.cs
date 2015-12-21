﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.18408
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 原始程式碼已由 Microsoft.VSDesigner 自動產生，版本 4.0.30319.18408。
// 
#pragma warning disable 1591

namespace SimpleTest.ErrorReference {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsErrorSoap", Namespace="http://tempuri.org/")]
    public partial class wsError : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback InsertErrorLogOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsError() {
            this.Url = global::SimpleTest.Properties.Settings.Default.SimpleTest_ErrorReference_wsError;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event InsertErrorLogCompletedEventHandler InsertErrorLogCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InsertErrorLog", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int InsertErrorLog(string sErrorPage, string sErrorMsg, string sCreateIp) {
            object[] results = this.Invoke("InsertErrorLog", new object[] {
                        sErrorPage,
                        sErrorMsg,
                        sCreateIp});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void InsertErrorLogAsync(string sErrorPage, string sErrorMsg, string sCreateIp) {
            this.InsertErrorLogAsync(sErrorPage, sErrorMsg, sCreateIp, null);
        }
        
        /// <remarks/>
        public void InsertErrorLogAsync(string sErrorPage, string sErrorMsg, string sCreateIp, object userState) {
            if ((this.InsertErrorLogOperationCompleted == null)) {
                this.InsertErrorLogOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInsertErrorLogOperationCompleted);
            }
            this.InvokeAsync("InsertErrorLog", new object[] {
                        sErrorPage,
                        sErrorMsg,
                        sCreateIp}, this.InsertErrorLogOperationCompleted, userState);
        }
        
        private void OnInsertErrorLogOperationCompleted(object arg) {
            if ((this.InsertErrorLogCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InsertErrorLogCompleted(this, new InsertErrorLogCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void InsertErrorLogCompletedEventHandler(object sender, InsertErrorLogCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class InsertErrorLogCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal InsertErrorLogCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591