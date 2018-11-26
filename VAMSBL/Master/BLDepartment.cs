using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMSDL.Account;
using VAMSPL.Account;

namespace VAMSBL.Account
{
    public class BLDepartment
    {
        DLDepartment objdl = new DLDepartment();
        PLDepartment objpl = new PLDepartment();
        BLCommon objCommonBL = new BLCommon();
        public string[] Insertupdatedept(PLDepartment category)
        {
            string[] Value = objdl.Insertupdatedept(category);
            return Value;
        }
        public List<PLDepartment> GetDeptDetails(PLDepartment dept)
        {
            DataSet Ds = new DataSet();
            List<PLDepartment> LstCategory = new List<PLDepartment>();
            Ds = objdl.GetDeptDetails(dept);
            if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {

                LstCategory = objCommonBL.ConvertDataTable<PLDepartment>(Ds.Tables[0]);
            }
            return LstCategory;
        }

        public string DeleteDeptDetails(PLDepartment category)
        {
            string Value = objdl.DeleteDeptDetails(category);
            return Value;
        }


    }
}
