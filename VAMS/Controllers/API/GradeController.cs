using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VAMS.Common;
using VAMSBL.Master;
using VAMSPL.Master;

namespace VAMS.Controllers.API
{
    public class GradeController : ApiController
    {
        PLGrade objpl = new PLGrade();
        BLGrade objbl = new BLGrade();

        [HttpPost]
        public HttpResponseMessage InsertUpdateGrade(PLGrade grade)
        {
            try
            {
                objpl.CUID = SessionHelper.Current.UserID;
                objpl.GroupID = SessionHelper.Current.GroupID;
                objpl.DepartmentID = grade.DepartmentID;
                objpl.DesignationID = grade.DesignationID;
                objpl.GradeID = grade.GradeID;
                objpl.GradeName = grade.GradeName;
                objpl.GradeShortName = grade.GradeShortName;
                objpl.CompanyID = grade.CompanyID;
                string[] Values = objbl.InsertUpdateGrade(objpl);
                if (Convert.ToInt32(Values[1]) == -1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Values[0]);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, 1);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, -1);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetAll(long ID, long CompanyID, long DepartmentID,long DesignationID)
        {
            try
            {
                List<PLGrade> LstDept = new List<PLGrade>();
                objpl.CompanyID = CompanyID;
                objpl.GradeID = ID;
                objpl.DesignationID = DesignationID;
                objpl.DepartmentID = DepartmentID;
                objpl.GroupID = SessionHelper.Current.GroupID;                
                LstDept = objbl.GetGradeDetails(objpl);
                return Request.CreateResponse(HttpStatusCode.OK, LstDept);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, -1);
            }
        }


        [HttpGet]
        public HttpResponseMessage Delete(long ID)
        {
            try
            {
                List<PLGrade> LstDesignation = new List<PLGrade>();
                objpl.GradeID = ID;
                string result = objbl.DeleteGradeDetails(objpl);
                if (result != null)
                {
                    if (result.ToLower().Contains("already"))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, 1);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, -1);
            }
        }
    }
}
