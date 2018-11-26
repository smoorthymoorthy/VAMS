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
   public class DLDepartment
    {
        public string[] Insertupdatedept(PLDepartment dept)
        {
            string[] Values = new string[3];
            Database db = DatabaseFactory.CreateDatabase("DBVAMS");
            DbCommand cmd = db.GetStoredProcCommand("SP_InsertUpdate_Dept");
            db.AddInParameter(cmd, "@ID", DbType.Int64, dept.DepartmentID);
            db.AddInParameter(cmd, "@DepartmentName", DbType.String, dept.DepartmentName.Trim());
            db.AddInParameter(cmd, "@DepartmentShortName", DbType.String, dept.DepartmentShortName.Trim());
            db.AddInParameter(cmd, "@UID", DbType.Int64, dept.CUID);
            db.AddInParameter(cmd, "@GroupID", DbType.Int64, dept.GroupID);
            db.AddInParameter(cmd, "@CompanyID", DbType.Int64, dept.CompanyID);            
            db.AddOutParameter(cmd,"@ErrorMessage", DbType.String, 1000);
            db.AddOutParameter(cmd,"@Status", DbType.String, 10);
            db.ExecuteNonQuery(cmd);
            Values[0] = string.Format(CultureInfo.CurrentCulture, "{0}", db.GetParameterValue(cmd, "@ErrorMessage"));
            Values[1] = db.GetParameterValue(cmd, "@Status").ToString();
            return Values;
        }

        public DataSet GetDeptDetails(PLDepartment dept)
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("DBVAMS");
            DbCommand cmd = db.GetStoredProcCommand("SP_Get_DepartmentDetails");
            db.AddInParameter(cmd, "@ID", DbType.Int64, dept.DepartmentID);
            db.AddInParameter(cmd, "@CompanyID", DbType.Int64, dept.CompanyID);
            db.AddInParameter(cmd, "@GroupID", DbType.Int64, dept.GroupID);
            ds = db.ExecuteDataSet(cmd);
            return ds;
        }

        public string DeleteDeptDetails(PLDepartment dept)
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("DBVAMS");
            DbCommand cmd = db.GetStoredProcCommand("SP_Delete_DeptDetails");
            db.AddInParameter(cmd, "@ID", DbType.Int64, dept.DepartmentID);
            db.AddOutParameter(cmd, "@ErrorMessage", DbType.String, 1000);           
            db.ExecuteNonQuery(cmd);
            string results = string.Format(CultureInfo.CurrentCulture, "{0}",
                db.GetParameterValue(cmd, "@ErrorMessage"));
            return results;
        }

    }
}
