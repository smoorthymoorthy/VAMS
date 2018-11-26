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
    public class BLDesignation
    {
        DLDesignation objdl = new DLDesignation();
        PLDesignation objpl = new PLDesignation();
        BLCommon objCommonBL = new BLCommon();
        public string[] InsertUpdateDesignation(PLDesignation designation)
        {
            string[] Value = objdl.InsertUpdateDesignation(designation);
            return Value;
        }

        public List<PLDesignation> GetDesignationDetails(PLDesignation designation)
        {
            DataSet Ds = new DataSet();
            List<PLDesignation> Lstdesignation = new List<PLDesignation>();
            Ds = objdl.GetDesignationDetails(designation);
            if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {

                Lstdesignation = objCommonBL.ConvertDataTable<PLDesignation>(Ds.Tables[0]);
            }
            return Lstdesignation;
        }

        public string DeleteDesignationDetails(PLDesignation designation)
        {
            string Value = objdl.DeleteDesignationDetails(designation);
            return Value;
        }

    }
}
