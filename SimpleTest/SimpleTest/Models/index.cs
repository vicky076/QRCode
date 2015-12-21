using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleTest.Models
{
    public class index
    {
        [DisplayName("儲值卡號")]
        [StringLength(16, ErrorMessage = "長度不符")]
        [RegularExpression(@"^[0-9a-zA-Z]*", ErrorMessage = "格式錯誤")]
        public string CardNo { get; set; }

        [DisplayName("建檔編號")]
        public string Sn { get; set; }

        [DisplayName("活動名稱")]
        [StringLength(16, ErrorMessage = "長度不符")]
        [RegularExpression(@"^[0-9a-zA-Z]*", ErrorMessage = "格式錯誤")]
        public string ActID { get; set; }

        [DisplayName("領獎專區活動ID")]
        public string ActIDQ { get; set; }

        [DisplayName("建立時間")]
        public string CreateDate { get; set; }

        [DisplayName("起始卡號")]
        public string Card_Start { get; set; }

        [DisplayName("結束卡號")]
        public string Card_End { get; set; }

        [DisplayName("點數面額")]
        public string CARD_POINT { get; set; }

        [DisplayName("建檔人")]
        public string CreateUser { get; set; }

        public string ActTitle { get; set; }
        public string ErrorMessage { get; set; }
        public string newpage { get; set; }
    }

    public class DataList
    {
        public string Sn { get; set; }
        public string CreateDate { get; set; }
        public string Card_Start { get; set; }
        public string Card_End { get; set; }
        public string CARD_POINT { get; set; }
        public string CreateUser { get; set; }
        public string ActID { get; set; }
        public string ActTitle { get; set; }
    }

    //public class Message
    //{
    //    public string message { get; set; }
    //}
}