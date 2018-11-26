using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMSPL.Master
{
   public class PLGrade :PLCommon
    {
        public long GradeID { get; set; }
        public string GradeName { get; set; }
        public string GradeShortName { get; set; }       
        public long? DepartmentID { get; set; }
        public long? DesignationID { get; set; }
    }
}
