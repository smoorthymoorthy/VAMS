using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace VAMS.Code
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
        public static string ConverDateDDMMToMMDD(string val)
        {
            try
            {
                string UrDate = val;
                System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                dateInfo.ShortDatePattern = "dd/MM/yyyy";
                DateTime validDate = Convert.ToDateTime(UrDate, dateInfo);
                if (validDate.ToString() == "01/01/0001" || validDate.ToString() == "01/01/1900")
                {
                    return string.Empty;
                }
                else
                    return validDate.ToString();
            }
            catch { }
            return "";
        }

        public static string ConverDateMMDDToDDMM(string val)
        {
            //string oldstr1 = "1/1/2011";
            //string strDate = DateTime.ParseExact(oldstr1, "MM/dd/yyyy", null).ToString("dd/MM/yyyy");

            try
            {
                DateTime temp = Convert.ToDateTime(val);
                string oldstr = temp.ToString("MM/dd/yyyy");
                string date = DateTime.ParseExact(oldstr, "MM/dd/yyyy", null).ToString("dd/MM/yyyy");

                if (date == "01/01/0001" || date == "01/01/1900")
                {
                    return string.Empty;
                }
                else
                    return date;

            }
            catch { }
            return "";
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



    }
}