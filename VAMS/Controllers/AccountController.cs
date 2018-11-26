using System;
using System.Web.Mvc;
using MCB = Kowni.Common.BusinessLogic;
using Newtonsoft.Json;
using System.Data;
using VAMS.Code;
using VAMS.Common;

namespace VAMS.Controllers
{
    public class AccountController : Controller
    {
        MCB.BLSingleSignOn objSingleSignOn = new MCB.BLSingleSignOn();
        MCB.BLAnnouncement objAnnouncement = new MCB.BLAnnouncement();
        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        public ActionResult Logindirect(string UserName, string Password)
        {
            try
            {
                string _res = string.Empty;
                int RoleID = 0;
                int UserID = 0;
                int CompanyID = 0;
                string CompanyName = string.Empty;
                int LocationID = 0;
                string LocationName = string.Empty;
                int GroupID = 0;
                int LanguageID = 0;
                string UserFName = string.Empty;
                string UserLName = string.Empty;
                string UserMailID = string.Empty;
                string ThemeFolderPath = string.Empty;
                if (!objSingleSignOn.Login(UserName, Password, 0, out RoleID, out UserID, out CompanyID, out CompanyName, out LocationID, out LocationName, out GroupID, out LanguageID, out UserFName, out UserLName, out UserMailID, out ThemeFolderPath))
                {
                    _res = JsonConvert.SerializeObject(new Model.StatusData() { Message = "Unauthorized Access.", Status = false }, Newtonsoft.Json.Formatting.Indented);
                }
                else
                {
                    UserSession.FromFramework = false;
                    UserSession.CountryID = 1;
                    UserSession.CountryIDCurrent = 1;
                    UserSession.RoleID = RoleID;

                    //Session.UserID = UserID;
                    //Session.GroupID = GroupID;

                    UserSession.UserID = UserID;
                    UserSession.GroupID = GroupID;


                    UserSession.UserID = UserID;
                    UserSession.CompanyIDUser = CompanyID;
                    UserSession.LocationIDUser = LocationID;
                    UserSession.CompanyName = CompanyName;
                    UserSession.CompanyIDCurrent = CompanyID;
                    UserSession.LocationName = LocationName;
                    UserSession.LocationIDCurrent = LocationID;
                    UserSession.GroupID = GroupID;
                    UserSession.GroupIDCurrent = GroupID;
                    UserSession.LanguageID = LanguageID;
                    UserSession.UserFirstName = UserFName;
                    UserSession.UserLastName = UserLName;
                    UserSession.ThemeFolderPath = ThemeFolderPath;
                    DataSet dsAnn = new DataSet();
                    dsAnn = objAnnouncement.GetAnnouncement(UserSession.CompanyIDCurrent, Convert.ToInt32(Kowni.Common.BusinessLogic.BLMenu.ToolID.MasterFramework));
                    if (dsAnn.Tables.Count > 0 && dsAnn.Tables[0].Rows.Count > 0)
                    {
                        if (dsAnn.Tables[0].Rows[0]["AnnouncementType"].ToString() == "1")
                        {
                            UserSession.Announcement = dsAnn.Tables[0].Rows[0]["Announcement"].ToString();
                        }
                    }
                    else
                    {
                        UserSession.Announcement = string.Empty;
                    }
                }
                var cmp = UserSession.CompanyName;
                return Json(new { result = cmp, message = string.Empty }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public ActionResult Logout()
        {
            var _page = string.Empty;
            if (!UserSession.FromFramework)
            {

                Session.Abandon();
                Session.Clear();
                bool isclear = UserSession.ClearSession;
                _page = "Login";
            }
            else
            {
                bool isclear = UserSession.ClearSession;
                Response.Redirect("http://" + Request.Url.Authority.ToLower() + "/", true);
            }
            return View(_page);
        }
  

    }
}
