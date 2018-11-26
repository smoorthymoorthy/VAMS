using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMSPL.Master;

namespace VAMSDL.Master
{
   public class DLGrade
    {
        public string[] InsertUpdateGrade(PLGrade grade)
        {
            string[] Values = new string[3];
            Database db = DatabaseFactory.CreateDatabase("DBVAMS");
            DbCommand cmd = db.GetStoredProcCommand("SP_InsertUpdate_Grade");
            db.AddInParameter(cmd, "@ID", DbType.Int64, grade.GradeID);
            db.AddInParameter(cmd, "@DepartmentID", DbType.Int64, grade.DepartmentID);
            db.AddInParameter(cmd, "@DesignationID", DbType.Int64, grade.DesignationID);
            db.AddInParameter(cmd, "@GradeName", DbType.String, grade.GradeName.Trim());
            db.AddInParameter(cmd, "@GradeShortName", DbType.String, grade.GradeShortName.Trim());
            db.AddInParameter(cmd, "@UID", DbType.Int64, grade.CUID);
            db.AddInParameter(cmd, "@GroupID", DbType.Int64, grade.GroupID);
            db.AddInParameter(cmd, "@CompanyID", DbType.Int64, grade.CompanyID);
            db.AddOutParameter(cmd, "@ErrorMessage", DbType.String, 1000);
            db.AddOutParameter(cmd, "@Status", DbType.String, 10);
            db.ExecuteNonQuery(cmd);
            Values[0] = string.Format(CultureInfo.CurrentCulture, "{0}", db.GetParameterValue(cmd, "@ErrorMessage"));
            Values[1] = db.GetParameterValue(cmd, "@Status").ToString();
            return Values;
        }
        public DataSet GetGradeDetails(PLGrade grade)
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("DBVAMS");
            DbCommand cmd = db.GetStoredProcCommand("SP_Get_GradeDetails");
            db.AddInParameter(cmd, "@ID", DbType.Int64, grade.GradeID);
            db.AddInParameter(cmd, "@DepartmentID", DbType.Int64, grade.DepartmentID);
            db.AddInParameter(cmd, "@DesignationID", DbType.Int64, grade.DesignationID);
            db.AddInParameter(cmd, "@CompanyID", DbType.Int64, grade.CompanyID);
            db.AddInParameter(cmd, "@GroupID", DbType.Int64, grade.GroupID);
            ds = db.ExecuteDataSet(cmd);
            return ds;
        }
        public string DeleteGradeDetails(PLGrade grade)
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("DBVAMS");
            DbCommand cmd = db.GetStoredProcCommand("SP_Delete_GradeDetails");
            db.AddInParameter(cmd, "@ID", DbType.Int64, grade.GradeID);
            db.AddOutParameter(cmd, "@ErrorMessage", DbType.String, 1000);
            db.ExecuteNonQuery(cmd);
            string results = string.Format(CultureInfo.CurrentCulture, "{0}", db.GetParameterValue(cmd, "@ErrorMessage"));
            return results;
        }
    }
}
