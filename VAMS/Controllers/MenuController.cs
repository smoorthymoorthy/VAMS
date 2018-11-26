using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VAMS.Code;
using VAMS.Common;
using VAMSPL;
using KB = Kowni.BusinessLogic;
using MCB = Kowni.Common.BusinessLogic;

namespace VAMS.Controllers
{
    public class MenuController : Controller
    {

        MCB.BLMenu obj = new MCB.BLMenu();
        KB.BLRoles.BLRoleCompany objRoleComp = new KB.BLRoles.BLRoleCompany();
        KB.BLRoles.BLRoleLocation objRoleLoc = new KB.BLRoles.BLRoleLocation();
        KB.BLCompany objCompany = new KB.BLCompany();
        KB.BLLocation objLocation = new KB.BLLocation();
        KB.BLUser.BLUserLocation objuserLocation1 = new KB.BLUser.BLUserLocation();
        KB.BLUser.BLUserCompany objuserCompany = new KB.BLUser.BLUserCompany();
        KB.BLCompanyMainForms objMainForms = new KB.BLCompanyMainForms();
        //KB.BLCompanySubForms objSubForms = new KB.BLCompanySubForms();
        KB.BLSubForms objSubForms = new KB.BLSubForms();
        KB.BLTools objtools = new KB.BLTools();

        // GET: Menu
        [HttpGet]
        public ActionResult Loadmenu()
        {
            DataSet Ds = new DataSet();
            DataSet Ds1 = new DataSet();
            string Path = string.Empty;
            int MainFormD = 0;
            try
            {
                Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                System.IO.FileInfo Info = new System.IO.FileInfo(Path);
                Path = Info.Name;
            }
            catch { }
            Ds = objMainForms.GetCompanyMainForms(SessionHelper.Current.CompanyIDCurrent, Convert.ToInt32(ConfigurationManager.AppSettings["toolid"].ToString()), 1);

            for (int k = 0; k < Ds.Tables[0].Rows.Count; k++)
            {
                string MainPageName = string.Empty;
                string[] MainPageIDValue = new string[3];
                try
                {
                    MainPageName = Ds.Tables[0].Rows[k]["MainFormsPageName"].ToString();
                    MainPageIDValue = MainPageName.Split('.');
                }
                catch { }
                DataRow[] dr = new DataRow[10];
                Ds1 = objSubForms.GetSubForms(Convert.ToInt32(Ds.Tables[0].Rows[k]["MainFormsID"].ToString()));
                if (Ds1.Tables[0].Rows.Count > 0)
                {
                    dr = Ds1.Tables[0].Select("SubFormsPageName='" + Path + "'");

                }
                int result1 = dr.Count(s => s != null);
                if (result1 > 0)
                {
                    MainFormD = Convert.ToInt32(Ds.Tables[0].Rows[k]["MainFormsID"].ToString());
                    break;
                }
            }
            Ds = new DataSet();
            Ds = objMainForms.GetCompanyMainForms(SessionHelper.Current.CompanyIDCurrent, Convert.ToInt32(ConfigurationManager.AppSettings["toolid"].ToString()), 1);


            StringBuilder objBuilder = new StringBuilder();
            for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
            {
                string MainPageID = string.Empty;
                string[] MainPageIDValue = new string[3];
                try
                {
                    MainPageID = Ds.Tables[0].Rows[i]["MainFormsPageName"].ToString();
                    MainPageIDValue = MainPageID.Split('.');
                }
                catch { }
                if (Convert.ToInt32(Ds.Tables[0].Rows[i]["MainFormsID"]) == MainFormD)
                {
                    objBuilder.Append("<li class=\"dropdown profile-drop\" id=\"p" + MainPageIDValue[0].ToString() + "\"><a class=\"dropdown-toggle\"");
                }
                else
                {
                    objBuilder.Append("<li class=\"dropdown profile-drop\" id=\"p" + MainPageIDValue[0].ToString() + "\"><a class=\"dropdown-toggle\"");
                }
                objBuilder.Append("data-toggle=\"dropdown\" title=\"" + Ds.Tables[0].Rows[i]["MainFormsPageText"].ToString() + "\"><span class=\"overlay-label red\">");
                objBuilder.Append("</span>");
                objBuilder.Append("<div id=\"p" + MainPageIDValue[0].ToString() + "\" class=\"Capatilize\">" + Ds.Tables[0].Rows[i]["MainFormsPageText"].ToString() + "<i class=\"fa fa-angle-down \"></i></div></a>");
                objBuilder.Append("<ul class=\"dropdown-menu\">");
                Ds1 = new DataSet();
                Ds1 = objSubForms.GetSubForms(Convert.ToInt32(Ds.Tables[0].Rows[i]["MainFormsID"].ToString()));
                if (Ds1.Tables[0].Rows.Count > 0)
                {
                    DataRow[] Dr;
                    Dr = Ds1.Tables[0].Select("IsVisible='True'");
                    for (int j = 0; j < Dr.Length; j++)
                    {
                        string SubPageID = string.Empty;
                        string[] SubPageIDValue = new string[3];
                        try
                        {
                            SubPageID = Ds.Tables[0].Rows[i]["MainFormsPageName"].ToString();
                            SubPageIDValue = SubPageID.Split('.');
                        }
                        catch { }
                        if (Path == Ds1.Tables[0].Rows[j]["SubFormsPageName"].ToString())
                            objBuilder.Append("<li class=\"clsAdmin\"><a class=\"Capatilize\" id=\"" + Dr[j]["SubFormsID"].ToString() + "\" title=\"" + Dr[j]["SubFormsPageText"].ToString() + "\" href=\"" + Dr[j]["SubFormsPageName"].ToString() + "\">");
                        else
                            objBuilder.Append("<li class=\"clsAdmin\"><a class=\"Capatilize\" id=\"" + Dr[j]["SubFormsID"].ToString() + "\" title=\"" + Dr[j]["SubFormsPageText"].ToString() + "\" href=\"" + Dr[j]["SubFormsPageName"].ToString() + "\">");
                        objBuilder.Append("<i class=\"fa fa-sitemap\"></i>" + " " + Dr[j]["SubFormsPageText"].ToString() + "</a></li>");
                    }
                }

                objBuilder.Append("</ul>");
                objBuilder.Append("</li>");
            }
            var mforms = objBuilder.ToString();
            //ltrlMenu.Text = objBuilder.ToString();
            return Json(new { divs = mforms }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult dropdowncompany()
        {
            try
            {
                var companycurrentid = SessionHelper.Current.CompanyIDCurrent;
                DataSet ds = new DataSet();
                ds = objuserCompany.GetUserCompany(SessionHelper.Current.UserID);
                List<PLCommon> listdropdowncom = new List<PLCommon>();
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                listdropdowncom = (from DataRow dr in dt.Rows
                                   select new PLCommon()
                                   {
                                       CompanyID = Convert.ToInt32(dr["CompanyID"]),
                                       CompanyName = dr["CompanyName"].ToString(),
                                   }).ToList();
                return Json(new { result = listdropdowncom, value = companycurrentid, message = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = string.Empty, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult dropdownlocation(int CompanyID)
        {
            try
            {
                SessionHelper.Current.CompanyIDCurrent = CompanyID;
                DataSet ds = new DataSet();
                ds = objuserLocation1.GetUserLocation(SessionHelper.Current.UserID, SessionHelper.Current.CompanyIDCurrent);
                List<PLCommon> listdropdownloc = new List<PLCommon>();
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                listdropdownloc = (from DataRow dr in dt.Rows
                                   select new PLCommon()
                                   {
                                       LocationID = Convert.ToInt32(dr["LocationID"]),
                                       LocationName = dr["LocationName"].ToString(),
                                   }).ToList();
                return Json(new { result = listdropdownloc, message = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = string.Empty, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}