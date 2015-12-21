using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace SimpleTest.Models
{
    public class update
    {
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "長度不符")]
        public string P_cProcDesc { get; set; }

        public string P_iStatus { get; set; }
        public string P_cCreateUser { get; set; }
        public string P_iSn { get; set; }
        public string ErrorMessage { get; set; }
    }
}