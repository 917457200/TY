using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    /// <summary>
    /// 文件辅助类
    /// </summary>
    public sealed class FileHelper
    {
        public static string VIR_PATH = "~/Upload/";

        public static readonly string CurrentBaseDir = string.Empty;

        static FileHelper()
        {
            CurrentBaseDir = AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 获取文件上传的路径
        /// </summary>
        public static string UploadPath
        {
            get
            {
                //VIR_PATH = System.Configuration.ConfigurationManager.AppSettings["UploadPath"] ?? VIR_PATH;
                var context = HttpContext.Current;
                //var dateTimeDir = DateTime.Now.ToString("yyyyMMdd");
                var dateYear = DateTime.Now.Year.ToString();
                var dateMonth = DateTime.Now.Month.ToString();
                var dateDay = DateTime.Now.Day.ToString();
                if (context != null)
                {
                    return System.IO.Path.Combine(context.Server.MapPath(VIR_PATH), dateYear, dateMonth, dateDay);
                }
                VIR_PATH = VIR_PATH.TrimStart('~').TrimStart('/');
                return System.IO.Path.Combine(CurrentBaseDir, VIR_PATH, dateYear, dateMonth, dateDay);
            }
        }

        /// <summary>
        /// 获取文件上传的基路径
        /// </summary>
        public static string UploadPathBase
        {
            get
            {
                //VIR_PATH = System.Configuration.ConfigurationManager.AppSettings["UploadPath"] ?? VIR_PATH;
                var context = HttpContext.Current;
                if (context != null)
                {
                    return context.Server.MapPath(VIR_PATH);
                }
                VIR_PATH = VIR_PATH.TrimStart('~').TrimStart('/');
                return VIR_PATH;
            }
        }


        ///// <summary>
        ///// 上传文件的相对路径
        ///// </summary>
        //public static string UploadPathRelative
        //{
        //    get
        //    {
        //        VIR_PATH = System.Configuration.ConfigurationManager.AppSettings["UploadPath"] ?? VIR_PATH;
        //        var context = HttpContext.Current;
        //        //var dateTimeDir = DateTime.Now.ToString("yyyyMMdd");
        //        var dateYear = DateTime.Now.Year.ToString();
        //        var dateMonth = DateTime.Now.Month.ToString();
        //        var dateDay = DateTime.Now.Day.ToString();
        //        if (context != null)
        //        {
        //            return System.IO.Path.Combine(context.Server.MapPath(VIR_PATH), dateYear, dateMonth, dateDay);
        //        }
        //        VIR_PATH = VIR_PATH.TrimStart('~').TrimStart('/');
        //        return System.IO.Path.Combine(VIR_PATH, dateYear, dateMonth, dateDay);
        //    }
        //}

        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <returns></returns>
        public static string MapPath(string relativePath)
        {
            relativePath = relativePath.TrimStart('/').TrimStart('\\').Replace(@"/", @"\");
            return Path.Combine(CurrentBaseDir, relativePath);
        }

        /// <summary>
        /// 获取文件的后缀名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetFileType(string ext)
        {
            ext = (ext ?? "").ToLower();
            string[] zip = new string[] { ".zip", ".rar" };
            string[] doc = new string[] { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".pdf", ".txt" };
            string[] video = new string[] { ".avi", ".mp4", ".rm", ".rmvb" };
            string[] audio = new string[] { ".mp3", ".wma" };
            string[] picture = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp" };
            //System.IO.Path.

            Dictionary<string, string[]> _t = new Dictionary<string, string[]>();
            _t.Add("压缩包", zip);
            _t.Add("文档", doc);
            _t.Add("视频", video);
            _t.Add("音频", audio);
            _t.Add("图片", picture);
            foreach (var k in _t)
            {
                if (k.Value.Contains(ext))
                {
                    return k.Key;
                }
            }
            return "其它";
        }

        /// <summary>
        /// 获取文件上传大小 
        /// </summary>
        /// <param name="fileName">物理文件名称</param>
        /// <returns></returns>
        public static int ComputeSize(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                throw new System.IO.FileNotFoundException(fileName);
            }
            using (var strem = System.IO.File.OpenRead(fileName))
            {
                return (int)strem.Length;
            }
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ComputeHashMd5(string filePath)
        {
            using (var stream = System.IO.File.OpenRead(filePath))
            {
                var md5 = System.Security.Cryptography.MD5.Create();
                byte[] data = md5.ComputeHash(stream);
                return BitConverter.ToString(data).Replace("-", "");
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool DownloadFile(string filePath, string fileName = "")
        {
            bool ret = true;
            HttpContext httpContext = HttpContext.Current;
            try
            {
                #region--验证：HttpMethod，请求的文件是否存在

                switch (httpContext.Request.HttpMethod.ToUpper())
                { //目前只支持GET和HEAD方法   
                    case "GET":
                    case "HEAD":
                        break;
                    default:
                        httpContext.Response.StatusCode = 501;
                        return false;
                }
                if (!System.IO.File.Exists(filePath))
                {
                    httpContext.Response.StatusCode = 404;
                    return false;
                }

                #endregion

                #region 定义局部变量
                long startBytes = 0;
                int packSize = 1024 * 10; //分块读取，每块10K bytes   
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = Path.GetFileName(filePath);
                }
                FileStream myFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                long fileLength = myFile.Length;

                //int sleep = (int)Math.Ceiling(1000.0 * packSize / speed);//毫秒数：读取下一数据块的时间间隔   
                string lastUpdateTiemStr = System.IO.File.GetLastWriteTimeUtc(filePath).ToString("r");
                string eTag = HttpUtility.UrlEncode(fileName, Encoding.UTF8) + lastUpdateTiemStr;//便于恢复下载时提取请求头;   
                #endregion

                #region--验证：文件是否太大，是否是续传，且在上次被请求的日期之后是否被修
                if (myFile.Length > Int32.MaxValue)
                {
                    //-------文件太大了-------   
                    httpContext.Response.StatusCode = 413;//请求实体太大   
                    return false;
                }

                if (httpContext.Request.Headers["If-Range"] != null)//对应响应头ETag：文件名+文件最后修改时间   
                {
                    //----------上次被请求的日期之后被修改过--------------   
                    if (httpContext.Request.Headers["If-Range"].Replace("\"", "") != eTag)
                    {//文件修改过   
                        httpContext.Response.StatusCode = 412;//预处理失败   
                        return false;
                    }
                }
                #endregion

                try
                {
                    #region -------添加重要响应头、解析请求头、相关验证-------------------
                    httpContext.Response.Clear();
                    httpContext.Response.Buffer = false;
                    httpContext.Response.AddHeader("Content-MD5", "");//用于验证文件   
                    httpContext.Response.AddHeader("Accept-Ranges", "bytes");//重要：续传必须   
                    httpContext.Response.AppendHeader("ETag", "\"" + eTag + "\"");//重要：续传必须   
                    httpContext.Response.AppendHeader("Last-Modified", lastUpdateTiemStr);//把最后修改日期写入响应                   
                    httpContext.Response.ContentType = "application/octet-stream";//MIME类型：匹配任意文件类型   
                    httpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).Replace("+", "%20"));
                    httpContext.Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    httpContext.Response.AddHeader("Connection", "Keep-Alive");
                    httpContext.Response.ContentEncoding = Encoding.UTF8;
                    if (httpContext.Request.Headers["Range"] != null)
                    {//------如果是续传请求，则获取续传的起始位置，即已经下载到客户端的字节数------   
                        httpContext.Response.StatusCode = 206;//重要：续传必须，表示局部范围响应。初始下载时默认为200   
                        string[] range = httpContext.Request.Headers["Range"].Split(new char[] { '=', '-' });//"bytes=1474560-"   
                        startBytes = Convert.ToInt64(range[1]);//已经下载的字节数，即本次下载的开始位置     
                        if (startBytes < 0 || startBytes >= fileLength)
                        {//无效的起始位置   
                            return false;
                        }
                    }
                    if (startBytes > 0)
                    {//------如果是续传请求，告诉客户端本次的开始字节数，总长度，以便客户端将续传数据追加到startBytes位置后----------   
                        httpContext.Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                    }
                    #endregion

                    #region -------向客户端发送数据块-------------------
                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Ceiling((fileLength - startBytes + 0.0) / packSize);//分块下载，剩余部分可分成的块数   
                    for (int i = 0; i < maxCount && httpContext.Response.IsClientConnected; i++)
                    {//客户端中断连接，则暂停   
                        httpContext.Response.BinaryWrite(br.ReadBytes(packSize));
                        httpContext.Response.Flush();
                        //if (sleep > 1) Thread.Sleep(sleep);
                    }
                    #endregion
                }
                catch
                {
                    ret = false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
    }
}
