using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt_DevTest.Helper
{
    #region Enums
    public enum LogType
    {
        Info = 1,
        Warning,
        Error
    }
    #endregion

    /// <summary>
    /// Helperclass to write CMTrace compatible Logs. 
    /// </summary>
    public class CMTraceLog
    {
        #region Public Members
        public string Path { get; set; }
        public string Component { get; set; }
        public string Filename { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CMTraceLog()
        {
            Path = Environment.CurrentDirectory;
            Filename = "Logfile.log";
            Component = "CMTraceLogger";
        }

        /// <summary>
        /// Overloaded Debugger
        /// </summary>
        /// <param name="path">Path of the CmTrace Logfile</param>
        /// <param name="filename">Filename of the CmTrace Logfile</param>
        /// <param name="component">Component which will appear in the CmTrace Component Row</param>
        public CMTraceLog(string path, string filename, string component)
        {
            Path = path;
            Filename = filename;
            Component = component;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Writes a CMtrace compatible Log (async)
        /// </summary>
        /// <param name="message">Message to write to the Logfile</param>
        /// <param name="logtype">Type of Logmessage (Info/Warning/Error)</param>
        public async void WriteCMTraceLog(string message, LogType logtype = LogType.Info)
        {
            var logTime = String.Format("{0:HH:mm:ss.fff}", DateTime.Now);
            var logDate = String.Format("{0:MM-dd-yyyy}", DateTime.Now);
            var timeZoneBias = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).TotalMinutes;
            var logTimePlusBias = String.Format("{0}-{1}", logTime, timeZoneBias);

            var output = String.Format("<![LOG[{0}]LOG]!><time=\"{1}\" date=\"{2}\" component=\"{3}\" context=\"\" type=\"{4}\" thread=\"{5}\" file=\"{6}\">", message, logTimePlusBias, logDate, Component, ((int)logtype), (Process.GetCurrentProcess().Id), Filename);
            try
            {
                using (StreamWriter sw = new StreamWriter($"{Path}\\{Filename}", true, Encoding.UTF8))
                {
                    await sw.WriteLineAsync(output);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Method to register for a Logger-Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WriteLog(object sender, LogEventArgs e)
        {
            WriteCMTraceLog(e.LogMessage);
        }
        #endregion
    }
}
