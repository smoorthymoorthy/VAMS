using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMSPL.Account
{
    public class PLDesignation : PLCommon
    {
        public long DesignationID { get; set; }
        public long? DepartmentID { get; set; }
        public string DesignationName { get; set; }
        public string DesignationShortName { get; set; }
      
    }
}
