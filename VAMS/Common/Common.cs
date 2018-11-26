using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace VAMS.Common
{
    public class Common
    {
        public enum ErrorType
        {
            Error,
            Exclamation,
            Information,
            None,
            Question,
            Stop,
            Warning
        }
        public enum ButtonStatus
        {
            New = 0,
            Edit = 1,
            Delete = 2,
            Cancel = 3,
            View = 4,
            Save = 5,
            Views = 6
        }

        public enum CommonRoles
        {
            Controller = 1,
            SuperAdmin = 2,
            Admin = 3,
            User = 4,
        }


        public static string ToTitleCase(string mText)
        {

            string rText = "";
            try
            {
                System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Globalization.TextInfo TextInfo = cultureInfo.TextInfo;
                rText = TextInfo.ToTitleCase(mText);
            }
            catch
            {
                rText = mText;
            }
            return rText;
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

        private const string _GroupIDCurrent = "GroupIDCurrent";
        public static int GroupIDCurrent
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_GroupIDCurrent] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session.Contents[_GroupIDCurrent]);
            }
            set
            {
                HttpContext.Current.Session.Contents[_GroupIDCurrent] = value;
            }
        }



        public static bool DDVal(DropDownList dd, out int val)
        {
            val = 0;
            if (dd.SelectedIndex >= 0)
            {
                if (!Int32.TryParse(dd.SelectedItem.Value, out val))
                    return false;
                else
                    return true;
            }
            return false;
        }

        internal static void DDVal(object dFacility, out int locationID)
        {
            throw new NotImplementedException();
        }

        public static string formatPrice(object val)
        {
            return String.Format("{0:n2}", val);
        }

        public static void DecodeDDVal(object sender, EventArgs e)
        {
            foreach (ListItem item in ((DropDownList)sender).Items)
            {
                item.Text = System.Web.HttpUtility.HtmlDecode(item.Text);
            }
        }

        public static string Encode(string val)
        {
            val = System.Web.HttpUtility.HtmlEncode(val);
            return val;
        }

        public static string Decode(string val)
        {
            val = System.Web.HttpUtility.HtmlDecode(val);
            return val;
        }

        public static bool RDOVal(RadioButtonList dd, out int val)
        {
            val = 0;
            if (dd.SelectedIndex >= 0)
            {
                if (!Int32.TryParse(dd.SelectedItem.Value, out val))
                    return false;
                else
                    return true;
            }
            return false;
        }

        private const string _IsFramework = "Yes";
        public static string IsFramework
        {
            get
            {
                if (HttpContext.Current.Session.Contents[_IsFramework] == null)
                    return "Yes";
                else
                    return HttpContext.Current.Session.Contents[_IsFramework].ToString();
            }
            set
            {
                HttpContext.Current.Session.Contents[_IsFramework] = value;
            }
        }


        public static string GetMMDDYYYYTODDMMYYYY(string Date)
        {
            string[] Value = Date.Split('/');
            string ConvertDate = Value[1] + "/" + Value[0] + "/" + Value[2];
            return ConvertDate;
        }

        public static bool SplitDates(string twoDateTime, out DateTime val)
        {
            val = new DateTime();
            if (twoDateTime.Contains("-"))
            {
                if (DateTime.TryParse(twoDateTime.Split('-')[0].Trim(), out val))
                    return true;
                else
                    return false;
            }
            else if (DateTime.TryParse(twoDateTime.Trim(), out val))
            {
                return true;
            }
            return false;
        }



        public static DateTime GetCurrentZonalTime(string Min)
        {
            DateTime myTime = new DateTime();
            try
            {


                string Sing = Min.Substring(0, 1);
                int Mins = 0;
                if (Min.Length >= 4)
                {
                    Mins = Convert.ToInt32(Min.Substring(1, 3));
                }
                else
                {
                    Mins = 0;
                }
                if (Sing == "+")
                    myTime = DateTime.Now.AddMinutes(+Mins);
                else
                    myTime = DateTime.Now.AddMinutes(-Mins);
                string myMin = myTime.Hour.ToString();

                return myTime;
            }
            catch { return myTime; }
        }

        public static DateTime GetCurrentZonalTimeByDate(DateTime Dt, string Min)
        {
            DateTime myTime = new DateTime();
            try
            {

                string Sing = Min.Substring(0, 1);
                int Mins = 0;
                if (Min.Length >= 4)
                {
                    Mins = Convert.ToInt32(Min.Substring(1, 3));
                }
                else
                {
                    Mins = 0;
                }

                if (Sing == "+")
                    myTime = Dt.AddMinutes(+Mins);
                else
                    myTime = Dt.AddMinutes(-Mins);
                string myMin = myTime.Hour.ToString();

                return myTime;
            }
            catch { return myTime; }
        }

        public static DateTime GetCurrentZonalTimeByDate_Globel(DateTime Dt, string Min)
        {
            DateTime myTime = new DateTime();
            try
            {

                //string Sing = Min.Substring(0, 1);
                //int Mins = 0;
                //if (Min.Length >= 4)
                //{
                //    Mins = Convert.ToInt32(Min.Substring(1, 3));
                //}
                //else
                //{
                //    Mins = 0;
                //}

                //if (Sing == "+")
                //    myTime = Dt.AddMinutes(+Mins);
                //else
                //    
                //string myMin = myTime.Hour.ToString();
                int ihour = Convert.ToInt16(Min);
                myTime = Dt.AddMinutes(ihour);

                return myTime;
            }
            catch { return myTime; }
        }

        public static string Encryption(string inputStr)
        {
            try
            {
                byte[] encData_byte = new byte[inputStr.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(inputStr);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Encryption" + ex.Message);
            }
        }

        public static string Decryption(string inputStr)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(inputStr);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length); char[] decoded_char = new char[charCount]; utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Decryption" + ex.Message);
            }
        }


        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            System.Reflection.PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        public static bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }

        }


        public static bool SendMail(string ToEmail, string Subject, string Body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(WebConfigurationManager.AppSettings["SmtpServer"].ToString());
            mail.From = new MailAddress(WebConfigurationManager.AppSettings["FromUserEmail"].ToString());
            mail.To.Add(ToEmail);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["FromUserName"], WebConfigurationManager.AppSettings["FromUserEmailPassword"]);
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            return true;

        }


        public static string ToXml(DataSet ds)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(DataSet));
                    xmlSerializer.Serialize(streamWriter, ds);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }

    }
}