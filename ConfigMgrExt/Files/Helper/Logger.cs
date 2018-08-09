using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ConfigMgrExt.Helper
{
    /// <summary>
    /// Defines a Logger which operated through the whole Application. 
    /// This logger only rises the LogEntry-Event, so other Logger can register for this Event
    /// </summary>
    public class Logger : ILogger
    {
        public event EventHandler<LogEventArgs> LogEntry;

        public void WriteLog(string message)
        {
            var e = new LogEventArgs
            {
                LogMessage = message
            };
            LogEntry(this, e);
        }
    }
}
