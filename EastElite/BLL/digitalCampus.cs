using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace BLL
{
    public class digitalCampus
    {
        /// <summary>
        /// 数字校园的免除密码验证访问地址
        /// </summary>
        /// <param name="UsCd"></param>
        /// <param name="Ustp"></param>
        /// <returns></returns>
        public string getUrl(string Url, string UsCd, string Ustp)
        {
            //字符串拼接
            StringBuilder http = new StringBuilder();
            http.Append(Url + "?");
            //拼接UsCd
            http.Append("UsCd=" + UsCd);
            //拼接Ustp
            http.Append("&UsTp=" + Ustp);
            //计算ts 是从1970年1月1日（UTC/GMT的午夜）开始所经过的秒数
            DateTime TimeNow = DateTime.Now;
            DateTime TimeOld = Convert.ToDateTime("1970-1-1 00:00:00");
            TimeSpan TimeX = TimeNow - TimeOld;//计算时间差
            int ts = (int)TimeX.TotalSeconds;
            //拼接ts
            http.Append("&ts=" + ts);
            //计算Md 对于字符串UsCd+ts+secretKey计算MD5后，得到的字符，secretKey是我们协商加密字符串，约定为 furture#$sdf&
            string Md = MD5.Upper32(UsCd + ts + "furture#$sdf&");

            http.Append("&md5=" + Md);
            return http.ToString();

        }
        public string GetDianUrl(string Url, string UsCd, string Ustp)
        {
            string secretKey = "furture#$eccs&";
            string ts = DateTime.Now.Second.ToString();
            string userToken = MD5.Upper32(UsCd + ts + secretKey);
            string NavigateUrl = @"" + Url + "?code=" + UsCd + @"&type=" + Ustp + "&ts=" + ts + @"&userToken=" + userToken;
            return NavigateUrl;
        }
        public string GuangBo(string login,string password)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string url = "http://183.63.117.154:8010/v2/users/login";
            string param = string.Format("login={0}&password={1}&remember_me={2}", login, password, 0);

            string Json = HttpPost(url, param);
            retmessage message = jss.Deserialize<retmessage>(Json);
            if (message.code==200)
            {
                return "183.63.117.154:8010";
            }
            return "";
        }
        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            writer.Write(postDataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        }
        public class retmessage
        {

            public string message
            {
                get;
                set;
            }
            public int code
            {
                get;
                set;
            }
        }
    }
}
