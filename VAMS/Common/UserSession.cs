using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace VAMS.Common
{
    public class UserSession
    {
        public static bool ClearSession
        {
            get
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                return true;
            }
        }
        
        private const string _companyIDCurrent = "CompanyIDCurrent";
        public static int CompanyIDCurrent
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_companyIDCurrent] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_companyIDCurrent]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_companyIDCurrent] = value;
            }
        }


        private const string _isRegistration = "IsRegistration";
        public static long IsRegistration
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_companyIDCurrent] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_isRegistration]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_isRegistration] = value;
            }
        }


        private const string _lang = "Lang";
        public static string Lang
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_lang] == null)
                    return "en";
                else
                    return Convert.ToString(HttpContext.Current.Session.Contents[_lang]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_lang] = value;
            }
        }


        private const string _email = "Email";
        public static string Email
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_email] == null)
                    return "";
                else
                    return Convert.ToString(HttpContext.Current.Session.Contents[_email]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_email] = value;
            }
        }



        private const string _invoiceIDCurrent = "InvoiceIDCurrent";
        public static int InvoiceIDCurrent
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_invoiceIDCurrent] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_invoiceIDCurrent]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_invoiceIDCurrent] = value;
            }
        }


        private const string _countryIDCurrent = "CountryIDCurrent";
        public static int CountryIDCurrent
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_countryIDCurrent] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_countryIDCurrent]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_countryIDCurrent] = value;
            }
        }


        //*****************************

        private const string _SearchTicket = "Search";
        public static bool SearchTicket
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_SearchTicket] == null)
                    return false;
                else
                    return Convert.ToBoolean(HttpContext.Current.Session.Contents[_SearchTicket]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_SearchTicket] = value;
            }
        }

        private const string _isSE = "ServiceEngineer";
        public static bool ServiceEngineer
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_isSE] == null)
                    return false;
                else
                    return Convert.ToBoolean(HttpContext.Current.Session.Contents[_isSE]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_isSE] = value;
            }
        }

        private const string _isController = "Controller";
        public static bool Controller
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_isController] == null)
                    return false;
                else
                    return Convert.ToBoolean(HttpContext.Current.Session.Contents[_isController]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_isController] = value;
            }
        }

        private const string _fromFramework = "FromFramework";
        public static bool FromFramework
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_fromFramework] == null)
                    return false;
                else
                    return Convert.ToBoolean(HttpContext.Current.Session.Contents[_fromFramework]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_fromFramework] = value;
            }
        }

        public static class Settings
        {
            private const string _gaugeHeight = "GaugeHeight";
            public static int GaugeHeight
            {
                get
                {
                    if (HttpContext.Current.Session.Contents[_gaugeHeight] == null)
                        return 250;
                    else
                        return Convert.ToInt32(HttpContext.Current.Session.Contents[_gaugeHeight]);
                }
                set
                {
                    HttpContext.Current.Session.Contents[_gaugeHeight] = value;
                }
            }

            private const string _gaugeWidth = "GaugeWidth";
            public static int GaugeWidth
            {
                get
                {
                    if (HttpContext.Current.Session.Contents[_gaugeWidth] == null)
                        return 500;
                    else
                        return Convert.ToInt32(HttpContext.Current.Session.Contents[_gaugeWidth]);
                }
                set
                {
                    HttpContext.Current.Session.Contents[_gaugeWidth] = value;
                }


            }

            public static class Images
            {
                private const string _showCompanyImage = "ShowCompanyImage";
                public static bool ShowCompanyImage
                {
                    get
                    {
                        if (HttpContext.Current.Session.Contents[_showCompanyImage] == null)
                            return false;
                        else
                            return Convert.ToBoolean(HttpContext.Current.Session.Contents[_showCompanyImage]);
                    }
                    set
                    {
                        HttpContext.Current.Session.Contents[_showCompanyImage] = value;
                    }
                }

                private const string _companyImage = "CompanyImage";
                public static string CompanyImage
                {
                    get
                    {
                        string url = ConfigurationManager.AppSettings["ThemesDomainName"].ToString().TrimEnd('/').Trim() + "/" + ConfigurationManager.AppSettings["StaticFolderPathName"].ToString().TrimEnd('/').Trim() + "/" + ConfigurationManager.AppSettings["CompanyImageFolderName"].ToString().TrimEnd('/').Trim() + "/";
                        if (ShowCompanyImage)
                            return url + CompanyIDCurrent.ToString().Trim() + ".png";
                        else
                            return url + "default.png";
                    }
                }

                private const string _groupImage = "GroupImage";
                public static string GroupImage
                {
                    get
                    {
                        string url = ConfigurationManager.AppSettings["ThemesDomainName"].ToString().TrimEnd('/').Trim() + "/" + ConfigurationManager.AppSettings["StaticFolderPathName"].ToString().TrimEnd('/').Trim() + "/" + ConfigurationManager.AppSettings["GroupImageFolderName"].ToString().TrimEnd('/').Trim() + "/";
                        return url + GroupID.ToString().Trim() + ".png";
                    }
                }
            }
        }

        #region "Others"
        private const string _pageSize = "PageSize";
        public static int PageSize
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_pageSize] == null)
                    return 10;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_pageSize]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_pageSize] = value;
            }
        }
        #endregion
        #region "Company Details"
        private const string _groupID = "GroupID";
        public static int GroupID
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_groupID] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_groupID]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_groupID] = value;
            }
        }

        private const string _groupIDCurrent = "GroupIDCurrent";
        public static long GroupIDCurrent
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_groupIDCurrent] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_groupIDCurrent]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_groupIDCurrent] = value;
            }
        }

        private const string _mailSmsTempalteIDCurrent = "MailSmsTempalteIDCurren";
        public static int MailSmsTempalteIDCurrent
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_mailSmsTempalteIDCurrent] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_mailSmsTempalteIDCurrent]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_mailSmsTempalteIDCurrent] = value;
            }
        }
        private const string _companyIDUser = "CompanyIDUser";
        public static int CompanyIDUser
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_companyIDUser] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_companyIDUser]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_companyIDUser] = value;
            }
        }

        private const string _TimeZone = "TimeZoneName";
        public static string TimeZoneName
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_TimeZone] == null)
                    return string.Empty;
                else
                    return HttpContext.Current.Session.Contents[_TimeZone].ToString();
            }
            set
            {
                HttpContext.Current.Session.Contents[_TimeZone] = value;
            }
        }

        private const string _TimeZoneTime = "TimeZoneTime";
        public static string TimeZoneTime
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_TimeZoneTime] == null)
                    return string.Empty;
                else
                    return HttpContext.Current.Session.Contents[_TimeZoneTime].ToString();
            }
            set
            {
                HttpContext.Current.Session.Contents[_TimeZoneTime] = value;
            }
        }

        private const string _locationIDUser = "LocationIDUser";
        public static int LocationIDUser
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_locationIDUser] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_locationIDUser]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_locationIDUser] = value;
            }
        }

        private const string _locationIDCurrent = "LocationIDCurrent";
        public static int LocationIDCurrent
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_locationIDCurrent] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_locationIDCurrent]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_locationIDCurrent] = value;
            }
        }
        private const string _companyName = "CompanyName";
        public static string CompanyName
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_companyName] == null)
                    return string.Empty;
                else
                    return HttpContext.Current.Session.Contents[_companyName].ToString();
            }
            set
            {
                HttpContext.Current.Session.Contents[_companyName] = value;
            }
        }
        private const string _groupName = "GroupName";
        public static string GroupName
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_groupName] == null)
                    return string.Empty;
                else
                    return HttpContext.Current.Session.Contents[_groupName].ToString();
            }
            set
            {
                HttpContext.Current.Session.Contents[_groupName] = value;
            }
        }
        private const string _EmailID = "EmailID";
        public static string EmailID
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_EmailID] == null)
                    return string.Empty;
                else
                    return HttpContext.Current.Session.Contents[_EmailID].ToString();
            }
            set
            {
                HttpContext.Current.Session.Contents[_EmailID] = value;
            }
        }

        private const string _FirstName = "FirstName";
        public static string FirstName
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_FirstName] == null)
                    return string.Empty;
                else
                    return HttpContext.Current.Session.Contents[_FirstName].ToString();
            }
            set
            {
                HttpContext.Current.Session.Contents[_FirstName] = value;
            }
        }
        private const string _MobileNo = "MoblileNo";
        public static string MobileNo
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_MobileNo] == null)
                    return string.Empty;
                else
                    return HttpContext.Current.Session.Contents[_MobileNo].ToString();
            }
            set
            {
                HttpContext.Current.Session.Contents[_MobileNo] = value;
            }
        }

        private const string _UserProfilePic = "UserProfilePic";
        public static string UserProfilePic
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_UserProfilePic] == null)
                    return string.Empty;
                else
                    return HttpContext.Current.Session.Contents[_UserProfilePic].ToString();
            }
            set
            {
                HttpContext.Current.Session.Contents[_UserProfilePic] = value;
            }
        }

        private const string _locationName = "LocationName";
        public static string LocationName
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_locationName] == null)
                    return string.Empty;
                else
                    return HttpContext.Current.Session.Contents[_locationName].ToString();
            }
            set
            {
                HttpContext.Current.Session.Contents[_locationName] = value;
            }
        }
        private const string _countryID = "CountryID";
        public static int CountryID
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_countryID] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_countryID]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_countryID] = value;
            }
        }
        #endregion
        #region "User Details"
        private const string _roleID = "RoleID";
        public static int RoleID
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_roleID] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_roleID]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_roleID] = value;
            }
        }

        private const string _isAdmin = "isAdmin";
        public static bool isAdmin
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_isAdmin] == null)
                    return false;
                else
                    return Convert.ToBoolean(HttpContext.Current.Session.Contents[_isAdmin]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_isAdmin] = value;
            }
        }
        private const string _userID = "UserID";
        public static int UserID
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_userID] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_userID]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_userID] = value;


            }
        }

        private const string _userFirstName = "UserFirstName";
        public static string UserFirstName
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_userFirstName] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return HttpContext.Current.Session.Contents[_userFirstName].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session.Contents[_userFirstName] = value;
            }
        }

        private const string _userLastName = "UserLastName";
        public static string UserLastName
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_userLastName] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return HttpContext.Current.Session.Contents[_userLastName].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session.Contents[_userLastName] = value;
            }
        }


        private const string _themeFolderPath = "ThemeFolderPath";
        public static string ThemeFolderPath
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_themeFolderPath] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return HttpContext.Current.Session.Contents[_themeFolderPath].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session.Contents[_themeFolderPath] = value;
            }
        }
        private const string _announcement = "Announcement";
        public static string Announcement
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_announcement] == null)
                    return string.Empty;
                else
                    return HttpContext.Current.Session.Contents[_announcement].ToString();
            }
            set
            {
                HttpContext.Current.Session.Contents[_announcement] = value;
            }
        }
        private const string _companyLogoName = "CompanyLogoName";
        public static string CompanyLogoName
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_companyLogoName] == null)
                    return "default.png";
                else
                    return HttpContext.Current.Session.Contents[_companyLogoName].ToString();
            }
            set
            {
                if (value.Trim() == string.Empty)
                    HttpContext.Current.Session.Contents[_companyLogoName] = "default.png";
                else
                    HttpContext.Current.Session.Contents[_companyLogoName] = value;
            }
        }
        #endregion
        private const string _languageID = "LanguageID";
        public static int LanguageID
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_languageID] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_languageID]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_languageID] = value;
            }
        }
        private const string _rowCount = "RowCount";
        public static int RowCount
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_rowCount] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_rowCount]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_rowCount] = value;
            }
        }
        private const string _menuDetails = "MenuDetails";
        public static DataSet MenuDetails
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_menuDetails] == null)
                    return null;
                else
                    return (DataSet)(HttpContext.Current.Session.Contents[_menuDetails]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_menuDetails] = value;
            }
        }
        
        private const string _currentpageSize = "CurrentPageSize";
        public static int CurrentPageSize
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_currentpageSize] == null)
                    return 1;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_currentpageSize]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_currentpageSize] = value;
            }
        }

        private const string _currentrowcount = "CurrentRowcount";
        public static int CurrentRowcount
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_currentrowcount] == null)
                    return 10;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_currentrowcount]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_currentrowcount] = value;
            }
        }

        private const string IsReqType = "ReqType";
        public static bool RequestType
        {
            get
            {
                if (HttpContext.Current.Session.Contents[IsReqType] == null)
                    return false;
                else
                    return Convert.ToBoolean(HttpContext.Current.Session.Contents[IsReqType]);
            }
            set
            {
                HttpContext.Current.Session.Contents[IsReqType] = value;
            }
        }

        private const string _isemployee = "IsEmployee";
        public static bool IsEmployee
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_isemployee] == null)
                    return false;
                else
                    return Convert.ToBoolean(HttpContext.Current.Session.Contents[_isemployee]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_isemployee] = value;
            }
        }

        private const string _ResolutionReq = "ResolutionReq";
        public static bool IsResolutionReq
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_ResolutionReq] == null)
                    return false;
                else
                    return Convert.ToBoolean(HttpContext.Current.Session.Contents[_ResolutionReq]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_ResolutionReq] = value;
            }
        }
        

        #region "Request Assign - New changes"
        private const string _RequestIssueId = "RequestIssueId";
        public static int RequestIssueID
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_RequestIssueId] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_RequestIssueId]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_RequestIssueId] = value;
            }
        }
        #endregion

    }
}
