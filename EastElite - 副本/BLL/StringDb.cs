using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StringDb
    {
        public string Code = "12|2|1|3|4|5|6|7|8|9|10|11";
        public string oaName = "校务管理系统|班宣系统|班班通管理系统|LED大屏系统|物联网系统|无线数字广播系统|精品录课系统|校园电视台|图书管理系统|监控系统|教学仪器管理系统|停车库系统";
        public string Img = "校务管理系统.png|class.png|Digital.png|LED大屏系统.png|物联网系统.png|无线数字广播系统.png|精品录课系统.png|校园电视台系统.png|图书管理系统.png|监控系统.png|教学仪器管理系统.png|停车库管理系统.png";
        public string link = "www.baidu1.com|www.baidu1.com||www.baidu1.com|www.baidu1.com|www.baidu1.com|www.baidu1.com|www.baidu1.com|www.baidu1.com|www.baidu1.com|www.baidu1.com|www.baidu1.com";

        public List<Link> getLink() 
        {
            List<Link> Lin = new List<Link>();
            string[] CodeS= Code.Split('|');
            string[] oaNameS = oaName.Split('|');
            string[] ImgS = Img.Split('|');
            string[] linkS = link.Split('|');

            for (int i = 0; i < CodeS.Length; i++)
            {
                Link L =new Link();
                L.Code =int.Parse(CodeS[i]);
                L.oaName = oaNameS[i].ToString();
                L.img = ImgS[i].ToString();
                L.link = linkS[i].ToString();
                Lin.Add(L);
            }
            return Lin;
        }

        public partial class Link
        {
            public int Code { get; set; }
            public string oaName { get; set; }
            public string link { get; set; }
            public string img { get; set; }
        }

    }
}
