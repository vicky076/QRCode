using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleTest.Models
{
    public class MultiSet
    {
        public string P_cCardStart { get; set; }
        public string P_cCardEnd { get; set; }
        public string P_cActID { get; set; }

        public string ErrorMessage { get; set; }
        public List<MutiDataList> MutiSetting { get; set; }
    }

    public class MutiDataList
    {
        public string P_cCardStart { get; set; }
        public string P_cCardEnd { get; set; }
        public string P_cActID { get; set; }
    }
}