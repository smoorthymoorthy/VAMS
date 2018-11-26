using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMSPL.Account;

namespace VAMSDL.Account
{
    public class DLDesignation
    {
        public string[] InsertUpdateDesignation(PLDesignation Designation)
        {
            string[] Values = new string[3];
            Database db = DatabaseFactory.CreateDatabase("DBVAMS");
            DbCommand cmd = db.GetStoredProcCommand("SP_InsertUpdate_Designation");
            db.AddInParameter(cmd, "@ID", DbType.Int64, Designation.DesignationID);
            db.AddInParameter(cmd, "@DepartmentID", DbType.Int64, Designation.DepartmentID);
            db.AddInParameter(cmd, "@DesignationName", DbType.String, Designation.DesignationName.Trim());
            db.AddInParameter(cmd, "@DesignationShortName", DbType.String, Designation.DesignationShortName.Trim());
            db.AddInParameter(cmd, "@UID", DbType.Int64, Designation.CUID);
            db.AddInParameter(cmd, "@GroupID", DbType.Int64, Designation.GroupID);
            db.AddInParameter(cmd, "@CompanyID", DbType.Int64, Designation.CompanyID);
            db.AddOutParameter(cmd, "@ErrorMessage", DbType.String, 1000);
            db.AddOutParameter(cmd, "@Status", DbType.String, 10);
            db.ExecuteNonQuery(cmd);
            Values[0] = string.Format(CultureInfo.CurrentCulture, "{0}", db.GetParameterValue(cmd, "@ErrorMessage"));
            Values[1] = db.GetParameterValue(cmd, "@Status").ToString();
            return Values;
        }

        public DataSet GetDesignationDetails(PLDesignation Designation)
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("DBVAMS");
            DbCommand cmd = db.GetStoredProcCommand("SP_Get_DesignationDetails");
            db.AddInParameter(cmd, "@ID", DbType.Int64, Designation.DesignationID);
            db.AddInParameter(cmd, "@DepartmentID", DbType.Int64, Designation.DepartmentID);
            db.AddInParameter(cmd, "@CompanyID", DbType.Int64, Designation.CompanyID);
            db.AddInParameter(cmd, "@GroupID", DbType.Int64, Designation.GroupID);
            ds = db.ExecuteDataSet(cmd);
            return ds;
        }

        public string DeleteDesignationDetails(PLDesignation Designation)
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("DBVAMS");
            DbCommand cmd = db.GetStoredProcCommand("SP_Delete_DesignationDetails");
            db.AddInParameter(cmd, "@ID", DbType.Int64, Designation.DesignationID);
            db.AddOutParameter(cmd, "@ErrorMessage", DbType.String, 1000);
            db.ExecuteNonQuery(cmd);
            string results = string.Format(CultureInfo.CurrentCulture, "{0}",db.GetParameterValue(cmd, "@ErrorMessage"));
            return results;
        }
    }
}
