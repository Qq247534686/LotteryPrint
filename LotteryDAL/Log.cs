using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
namespace Lottery.DAL
{
     public static class Log
    {
         /// <summary>
         /// 日志文件夹路径
         /// </summary>
         private static readonly string logPath = ConfigurationManager.AppSettings["LogPath"].ToString();
        
         /// <summary>
         /// 日志创建
         /// </summary>
         private static void ExistDirectory()
         {
             if (!string.IsNullOrWhiteSpace(logPath))
             {
                 if (!Directory.Exists(logPath))
                 {
                     Directory.CreateDirectory(logPath);
                 }
                 if (!Directory.Exists(logPath+"\\Error"))
                 {
                     Directory.CreateDirectory(logPath + "\\Error");
                 }
                 if (!Directory.Exists(logPath + "\\Info"))
                 {
                     Directory.CreateDirectory(logPath + "\\Info");
                 }
                 if (!File.Exists(logPath + "\\Error" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt"))
                 {
                     File.Create(logPath + "\\Error" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt").Close();
                 }
                 if (!File.Exists(logPath + "\\Info" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt"))
                 {
                     File.Create(logPath + "\\Info" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt").Close();
                 }
             }
         }
         /// <summary>
         /// 日志处理_1
         /// </summary>
         /// <param name="logString">自定义错误</param>
         public static void WriteLog(string logString, string ErrorOrInfo)
        {
            ExistDirectory();
            string txt=string.Empty;
            if (ErrorOrInfo == "Error")
            {
                txt = logPath + "\\Error\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }
            else
            {
                txt = logPath + "\\Info\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }
            using (StreamWriter streamWriterLog = new StreamWriter(txt, true, Encoding.UTF8))
            {
                streamWriterLog.WriteLine(string.Format(DateTime.Now.ToString() + "\t{0}", logString));
            }
        }
         /// <summary>
        /// 日志处理_2
         /// </summary>
        /// <param name="error">异常参数</param>
        public static void WriteLog(Exception error, string ErrorOrInfo)
        {
            ExistDirectory();
            string txt = string.Empty;
            if (ErrorOrInfo == "Error")
            {
                txt = logPath + "\\Error\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }
            else
            {
                txt = logPath + "\\Info\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }
            using (StreamWriter streamWriterLog = new StreamWriter(txt, true, Encoding.UTF8))
            {
                streamWriterLog.WriteLine(string.Format(DateTime.Now.ToString() + "\t{0}", error));
            }
        }
         /// <summary>
        /// 日志处理_3
         /// </summary>
         /// <param name="logString">自定义错误</param>
         /// <param name="error">异常参数</param>
        public static void WriteLog(string logString, Exception error, string ErrorOrInfo)
        {
            ExistDirectory();
            string txt = string.Empty;
            if (ErrorOrInfo == "Error")
            {
                txt = logPath + "\\Error\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }
            else
            {
                txt = logPath + "\\Info\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }
            using (StreamWriter streamWriterLog = new StreamWriter(txt, true, Encoding.UTF8))
            {
                streamWriterLog.WriteLine(string.Format(DateTime.Now.ToString() + "\t{0}>>{1}", logString, error));
            }
        
        }
    }
}
