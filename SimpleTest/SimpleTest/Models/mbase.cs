using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleTest.Models
{
    public class mbase
    {
        public mbase(string sButtonCode)
        {
            switch (sButtonCode.ToUpper().Trim())
            {
                //case "A":
                //    intButtonCode = EnumButtonCode.Add;
                //    break;
                //case "E":
                //    intButtonCode = EnumButtonCode.Edit;
                //    break;
                case "Q":
                    intButtonCode = EnumButtonCode.Query;
                    break;
                //case "D":
                //    intButtonCode = EnumButtonCode.Delete;
                //    break;
                default:
                    intButtonCode = EnumButtonCode.None;
                    break;
            }
        }

        public EnumButtonCode intButtonCode;
        public enum EnumButtonCode
        {
            None,
            Query
            //查詢
            //無
            // Add,
            //新增
            // Edit,
            //修改
            //Delete
            //刪除
        }
    }
}