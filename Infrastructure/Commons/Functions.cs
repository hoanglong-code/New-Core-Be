using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Commons
{
    public class Functions
    {
        public static string NumberToText(decimal inputNumber, bool suffix = true)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;
            string result = " ";
            // -12345678.3445435 => "-12345678"
            if (inputNumber == 0)
            {
                return "0" + (suffix ? " đồng chẵn" : "");
            }
            string sNumber = inputNumber.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }

            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if (ones > 0 || tens > 0 || hundreds > 0 || placeValue == 3)
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if (ones == 1 && tens > 1)
                        result = "một " + result;
                    else
                    {
                        if (ones == 5 && tens > 0)
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if (tens == 0 && ones > 0) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if (hundreds > 0 || tens > 0 || ones > 0)
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (isNegative) result = "Âm " + result;
            return result + (suffix ? " đồng chẵn" : "");
        }

        public static DateTime ConvertDateTimeToString(string dateString, string format = "yyyyMMdd")
        {
            try
            {
                return DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static string ConvertDateToViewFormat(string dateString)
        {
            try
            {
                var date = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture);
                return date.ToString("dd/MM/yyyy");
            }
            catch
            {
                return "";
            }
        }

        public static string ConvertDateWithDBFormat(string dateString)
        {
            try
            {
                var date = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return date.ToString("yyyyMMdd");
            }
            catch
            {
                return "";
            }
        }

        public static string GetMD5Hash(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int GetWeekInYear(DateTime time)
        {
            CultureInfo myCI = CultureInfo.CurrentCulture;
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            return myCal.GetWeekOfYear(time, myCWR, myFirstDOW);
        }

        public static string ColumnAdress(int col)
        {
            if (col <= 26)
            {
                return Convert.ToChar(col + 64).ToString();
            }
            int div = col / 26;
            int mod = col % 26;
            if (mod == 0) { mod = 26; div--; }
            return ColumnAdress(div) + ColumnAdress(mod);
        }

        public static string ConvertRomanNumber(int number)
        {
            if (number < 0 || number > 3999) throw new ArgumentOutOfRangeException("Value must be between 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ConvertRomanNumber(number - 1000);
            if (number >= 900) return "CM" + ConvertRomanNumber(number - 900); //EDIT: i've typed 400 instead 900
            if (number >= 500) return "D" + ConvertRomanNumber(number - 500);
            if (number >= 400) return "CD" + ConvertRomanNumber(number - 400);
            if (number >= 100) return "C" + ConvertRomanNumber(number - 100);
            if (number >= 90) return "XC" + ConvertRomanNumber(number - 90);
            if (number >= 50) return "L" + ConvertRomanNumber(number - 50);
            if (number >= 40) return "XL" + ConvertRomanNumber(number - 40);
            if (number >= 10) return "X" + ConvertRomanNumber(number - 10);
            if (number >= 9) return "IX" + ConvertRomanNumber(number - 9);
            if (number >= 5) return "V" + ConvertRomanNumber(number - 5);
            if (number >= 4) return "IV" + ConvertRomanNumber(number - 4);
            if (number >= 1) return "I" + ConvertRomanNumber(number - 1);
            throw new ArgumentOutOfRangeException("Value must be between 1 and 3999");
        }

        public static int GetWeekOrderInYear(DateTime time)
        {
            CultureInfo myCI = CultureInfo.CurrentCulture;
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            return myCal.GetWeekOfYear(time, myCWR, myFirstDOW);
        }

        public static int GetQuarter(int month)
        {
            int quarter = 1;
            if (month >= 1 && month <= 3)
                quarter = 1;
            if (month >= 4 && month <= 6)
                quarter = 2;
            if (month >= 7 && month <= 9)
                quarter = 3;
            if (month >= 10 && month <= 12)
                quarter = 4;
            return quarter;
        }

        public static DateTime ConvertDateDDMMYYYYStart(string datetime)
        {
            DateTime res = new DateTime(1990, 1, 1);
            string[] td1 = datetime.Split(' ');
            string[] td = td1[0].Split('-');
            try
            {
                res = new DateTime(int.Parse(td[0]), int.Parse(td[1]), int.Parse(td[2]), 0, 0, 0);
            }
            catch
            {
                res = new DateTime(1990, 1, 1);
            }


            return res;
        }

        public static DateTime ConvertDateDDMMYYYYEnd(string datetime)
        {
            DateTime res = new DateTime(1990, 1, 1);
            string[] td1 = datetime.Split(' ');
            string[] td = td1[0].Split('-');
            try
            {
                res = new DateTime(int.Parse(td[0]), int.Parse(td[1]), int.Parse(td[2]), 23, 59, 59);
            }
            catch
            {
                res = new DateTime(1990, 1, 1);
            }


            return res;
        }

        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }

        public static bool checkVideoFile(string extension)
        {
            bool res = false;
            IList<string> AllowedVideo = new List<string> { ".3gp", ".3g2", ".asf", ".avi", ".f4v", ".flv", ".ismv", ".m4v", ".mkv", ".mov", ".mp4", ".mpeg", ".ogv", ".wmv", ".webm" };
            if (AllowedVideo.Contains(extension))
            {
                res = true;
            }
            return res;

        }

        public static bool checkImageFile(string extension)
        {
            bool res = false;
            IList<string> AllowedImage = new List<string> { ".bmp", ".exr", ".ico", ".jpg", ".jpeg", ".gif", ".pbm", ".pcx", ".pgm", ".png", ".ppm", ".psd", ".tif", ".tiff", ".tga", ".wbmp", ".heic" };
            if (AllowedImage.Contains(extension))
            {
                res = true;
            }
            return res;

        }

        public static DateTime ConvertStringToDate(string strDate)
        {
            DateTime dateTime = new DateTime(1890, 1, 1);
            try
            {
                dateTime = DateTime.FromOADate(double.Parse(strDate));
            }
            catch
            {
                try
                {
                    //dateTime = DateTime.Parse(strDate);
                    dateTime = DateTime.ParseExact(strDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    try
                    {
                        string[] strT = strDate.Split(' ');
                        if (strT.Length > 1)
                        {
                            string[] str1 = strT[0].Split('/');
                            string[] str2 = strT[1].Split(':');
                            if (str1.Length == 3)
                            {
                                dateTime = new DateTime(int.Parse(str1[2]), int.Parse(str1[1]), int.Parse(str1[0]), int.Parse(str2[0]), int.Parse(str2[1]), int.Parse(str2[2]));
                            }

                        }
                        else
                        {
                            string[] str = strDate.Split('/');
                            if (str.Length == 3)
                            {
                                dateTime = new DateTime(int.Parse(str[2]), int.Parse(str[1]), int.Parse(str[0]));
                            }
                        }
                    }
                    catch
                    {
                        string[] str = strDate.Split('/');
                        if (str.Length == 3)
                        {
                            dateTime = new DateTime(int.Parse(str[2]), int.Parse(str[1]), int.Parse(str[0]));
                        }
                    }
                }
            }


            return dateTime;
        }

        public static string ConvertPhone(string phoneIn)
        {
            string phoneOut = "";
            if (phoneIn.Length >= 9)
            {
                string phoneInNew = phoneIn;
                phoneOut = "84" + phoneInNew.Substring(phoneIn.Length - 9, 9);
            }
            return phoneOut;
        }

        public static bool CheckPhone(string phoneIn, string phoneOn)
        {
            bool check = false;
            if (phoneIn != null && phoneOn != null)
            {
                if (phoneIn.Length >= 9 && phoneOn.Length >= 9)
                {
                    if (phoneIn == phoneOn) check = true;
                    else
                    {
                        string phoneInNew = phoneIn;
                        string phoneNew = "84" + phoneInNew.Substring(phoneIn.Length - 9, 9);
                        if (phoneNew == phoneOn) check = true;
                        else
                        {
                            string phoneOnNew = phoneOn;
                            string phoneNew2 = "84" + phoneOnNew.Substring(phoneOn.Length - 9, 9);
                            if (phoneNew2 == phoneNew) check = true;
                            else check = false;
                        }
                    }
                }
            }
            return check;
        }

        public static string RandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string ConvertContents(string Contents, string domain)
        {
            string result = "";
            string link = domain + "uploads/images/";
            if (Contents != null && Contents != "")
            {
                result = Contents.Replace("/uploads/images/", link);
            }
            return result;
        }

        public static string NonUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                "đ",
                "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                "í","ì","ỉ","ĩ","ị",
                "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                "d",
                "e","e","e","e","e","e","e","e","e","e","e",
                "i","i","i","i","i",
                "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                "u","u","u","u","u","u","u","u","u","u","u",
                "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }

            return Regex.Replace(text.ToLower().Replace(@"'", string.Empty), @"[^\w]+", "-").Replace("\"", "-").Replace(":", "-").ToLower();
        }

        public static int ConvertInt(string customerId)
        {
            int res = 0;
            try
            {
                res = int.Parse(customerId);
            }
            catch
            {
                res = 0;
            }
            return res;
        }

        public static String randomString(int len)
        {
            Random rnd = new Random();
            int number = rnd.Next(10000, 99999);
            string text = md5(number.ToString());
            text = text.ToUpper();
            text = text.Substring(0, len);
            return text;
        }

        public static string md5(String data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }

        public static byte[] encryptData(String data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static async Task<bool> SendEmail(string serverAddress, string serverDisplayName, string emailPassword, string host, int port, string clientAddress, string subject, string body, List<IFormFile>? files = null)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var email = new MimeMessage();
                var sender = new MailboxAddress(Encoding.UTF8, serverDisplayName, serverAddress);
                email.Sender = sender;
                email.From.Add(sender);
                email.To.Add(MailboxAddress.Parse(clientAddress.Trim()));
                email.Subject = subject;
                var builder = new BodyBuilder();
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            byte[] fileBytes;
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }
                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Timeout = 30000;
                smtp.Connect(host, port, true);
                smtp.Authenticate(serverAddress, emailPassword);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi xảy ra: {ex.Message}");
                return false;
            }
        }

        public static string TrimObjectToString(object value)
        {
            return value.ToString().Trim();
        }

        public static string? CheckValidDateTime(DateTime? dateTime, string formatDateTime = "yyyy-MM-dd hh:ss")
        {
            DateTime result;

            if (dateTime == null) return null;

            return DateTime.TryParseExact(dateTime.Value.ToString(), formatDateTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out result) ? dateTime.ToString() : null;
        }

        public static DateTime UnixTimeToDateTime(long unixTime)
        {
            // Chuyển từ Unix time (mili giây) sang DateTimeOffset (lưu trữ cả múi giờ)
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTime);

            // Chuyển về dạng DateTime theo giờ địa phương của máy chủ
            DateTime localDateTime = dateTimeOffset.LocalDateTime;

            return localDateTime;
        }
    }
}
