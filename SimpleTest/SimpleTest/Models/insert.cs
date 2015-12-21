using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleTest.Models
{
    public class insert
    {
        [Required(ErrorMessage = "*")]
        [StringLength(16, ErrorMessage = "長度不符")]
        [RegularExpression(@"^[0-9a-zA-Z]*", ErrorMessage = "格式錯誤")]
        public string P_cCardStart { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(16, ErrorMessage = "長度不符")]
        [RegularExpression(@"^[0-9a-zA-Z]*", ErrorMessage = "格式錯誤")]
        public string P_cCardEnd { get; set; }

        [StringLength(15, ErrorMessage = "長度不符")]
        [RegularExpression(@"^[0-9a-zA-Z]*", ErrorMessage = "格式錯誤")]
        public string P_cActID { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(500, ErrorMessage = "長度不符")]
        [RegularExpression(@"^[0-9a-zA-Z一-龜(),-/+._]*", ErrorMessage = "格式錯誤")]
        public string P_cProcDesc { get; set; }

        public string P_cCreateUser { get; set; }
        public string P_iSn { get; set; }
        public string P_iStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
}