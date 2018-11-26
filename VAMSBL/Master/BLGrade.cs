using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMSDL.Master;
using VAMSPL.Master;

namespace VAMSBL.Master
{
   public class BLGrade
    {
        DLGrade objdl = new DLGrade();
        PLGrade objpl = new PLGrade();
        BLCommon objCommonBL = new BLCommon();
        public string[] InsertUpdateGrade(PLGrade grade)
        {
            string[] Value = objdl.InsertUpdateGrade(grade);
            return Value;
        }

        public List<PLGrade> GetGradeDetails(PLGrade grade)
        {
            DataSet Ds = new DataSet();
            List<PLGrade> Lstdesignation = new List<PLGrade>();
            Ds = objdl.GetGradeDetails(grade);
            if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {

                Lstdesignation = objCommonBL.ConvertDataTable<PLGrade>(Ds.Tables[0]);
            }
            return Lstdesignation;
        }

        public string DeleteGradeDetails(PLGrade grade)
        {
            string Value = objdl.DeleteGradeDetails(grade);
            return Value;
        }
    }
}
