using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MFA_Main
{
    public static class Misc
    {
        public static string SHA1HashStringForUTF8String(string s)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);

            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(bytes);
                return HexStringFromBytes(hashBytes);
            }
        }

        private static string HexStringFromBytes(byte[] hashBytes)
        {
            var sb = new System.Text.StringBuilder();
            foreach (byte b in hashBytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        public static int GenerateOTP(int length)
        {
            Random r = new Random();
            string otp = "";
            for (int i = 0; i < length; i++)
            {
                otp += r.Next(9).ToString();
            }
            return Convert.ToInt32(otp);
        }

        public static async Task SendText(string phone, string message)
        {
            string username = "7ca9d7d13deb58b97bc058b0f2b86f43";
            string password = "180060eb3921fb8f8f7ade5ff260cc17";
            string uri = "https://jusibe.com/smsapi/send_sms/";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", username + ":" + password);

            var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("token", password),
                    new KeyValuePair<string, string>("key", username),
                    new KeyValuePair<string, string>("to", phone), 
                    new KeyValuePair<string, string>("from", "MFA-Bank"),
                    new KeyValuePair<string, string>("message", message)
                });

            var result = client.PostAsync(uri, formContent);
        }

        public static string AccNoForText(int accNo, int num)
        {
            string s = "";
            string ss = accNo.ToString();
            for (int i = 0; i < ss.Length - (num + 1); i++)
            {
                s += "*";
            }
            for (int i = ss.Length - (num + 1); i < ss.Length; i++){
                s += ss.ElementAt(i);
            }
            return s;
        }
    }
}
