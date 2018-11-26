using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VAMS.Common;
using VAMSBL.Account;
using VAMSPL.Account;

namespace VAMS.Controllers.API
{
    public class DesignationController : ApiController
    {

        PLDesignation objpl = new PLDesignation();
        BLDesignation objbl = new BLDesignation();

        [HttpPost]
        public HttpResponseMessage InsertUpdateDesignation(PLDesignation dept)
        {
            try
            {
                objpl.CUID = SessionHelper.Current.UserID;
                objpl.GroupID = SessionHelper.Current.GroupID;
                objpl.DepartmentID = dept.DepartmentID;
                objpl.DesignationID = dept.DesignationID;
                objpl.DesignationName = dept.DesignationName;
                objpl.DesignationShortName = dept.DesignationShortName;
                objpl.CompanyID = dept.CompanyID;
                string[] Values = objbl.InsertUpdateDesignation(objpl);
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
        public HttpResponseMessage GetAll(long ID, long CompanyID,long DepartmentID)
        {
            try
            {
                List<PLDesignation> LstDept = new List<PLDesignation>();
                objpl.CompanyID = CompanyID;
                objpl.DesignationID = ID;
                objpl.DepartmentID = DepartmentID;
                objpl.GroupID = SessionHelper.Current.GroupID;
                LstDept = objbl.GetDesignationDetails(objpl);
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
                List<PLDesignation> LstDesignation = new List<PLDesignation>();
                objpl.DesignationID = ID;
                string result = objbl.DeleteDesignationDetails(objpl);
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
