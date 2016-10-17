using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Totyu.WeixinSDK.Log
{
    public class Log : IDisposable
    {
        private static string m_logFilePath;

        private static string m_errorFilePath;

        private static FileStream m_fsLog;

        private static StreamWriter m_swLog;

        private static Log m_log;

        public static Log Instence
        {
            get
            {
                if (Log.m_log == null)
                {
                    Log.m_log = new Log();
                    Log.m_fsLog = new FileStream(GetRootPath() + "log.txt", FileMode.Create, FileAccess.Write);
                    Log.m_swLog = new StreamWriter(Log.m_fsLog, Encoding.UTF8);
                    Log.m_logFilePath = new FileInfo("log.txt").FullName;
                }
                return Log.m_log;
            }
            internal set
            {
                Log.m_log = value;
            }
        }

        public string LogFilePath
        {
            get
            {
                return Log.m_logFilePath;
            }
        }

        public string ErrorFilePath
        {
            get
            {
                return Log.m_errorFilePath;
            }
        }
        /// <summary>
        /// 取得网站根目录的物理路径
        /// </summary>
        /// <returns></returns>
        private static string GetRootPath()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            if (HttpCurrent != null)
            {
                AppPath = HttpCurrent.Server.MapPath("~");
            }
            else
            {
                AppPath = AppDomain.CurrentDomain.BaseDirectory;
                if (Regex.Match(AppPath, @"\\$", RegexOptions.Compiled).Success)
                    AppPath = AppPath.Substring(0, AppPath.Length - 1);
            }
            return AppPath;
        }
        public void LogWriteLine(string data)
        {
            Log.m_swLog.WriteLine(data);
            Log.m_swLog.Flush();
        }

        public void CloseFile()
        {
            if (Log.m_swLog != null)
            {
                Log.m_swLog.Close();
                Log.m_swLog = null;
            }
            if (Log.m_fsLog != null)
            {
                Log.m_fsLog.Close();
                Log.m_fsLog = null;
            }
        }

        public void Dispose()
        {
            Log.Instence.CloseFile();
            Log.Instence = null;
        }
    }
}