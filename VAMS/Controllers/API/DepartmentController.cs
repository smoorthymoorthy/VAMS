using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using VAMSPL.Account;
using VAMSBL.Account;
using VAMS.Code;
using VAMS.Common;

namespace VAMS.Controllers.API
{
    public class DepartmentController : ApiController
    {

        PLDepartment objpl = new PLDepartment();
        BLDepartment objbl = new BLDepartment();

        [HttpPost]
        public HttpResponseMessage InsertUpdateDept(PLDepartment dept)
        {
            try
            {
                objpl.CUID = SessionHelper.Current.UserID;
                objpl.GroupID = SessionHelper.Current.GroupID;
                objpl.DepartmentID = dept.DepartmentID;
                objpl.DepartmentName = dept.DepartmentName;
                objpl.DepartmentShortName = dept.DepartmentShortName;
                objpl.CompanyID = dept.CompanyID;
                string[] Values = objbl.Insertupdatedept(objpl);
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
        public HttpResponseMessage GetAll(long ID,long CompanyID)
        {
            try
            {
                List<PLDepartment> LstDept = new List<PLDepartment>();
                objpl.CompanyID = CompanyID;
                objpl.DepartmentID = ID;
                objpl.GroupID = SessionHelper.Current.GroupID;
                LstDept = objbl.GetDeptDetails(objpl);
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
                List<PLDepartment> LstDept = new List<PLDepartment>();
                objpl.DepartmentID = ID;
                string result = objbl.DeleteDeptDetails(objpl);
                if (result != null)
                {
                    if (result.ToLower().Contains("already"))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,result);
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
