using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMSPL.Account
{
   public class PLDepartment : PLCommon
    {
        public long DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentShortName { get; set; }
    }
}
