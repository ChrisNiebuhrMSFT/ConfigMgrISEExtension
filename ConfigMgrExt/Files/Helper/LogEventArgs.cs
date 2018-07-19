using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt.Helper
{
    /// <summary>
    /// Defines a Type for the LogEvent Eventargument.
    /// It allows you to provide a LogMessage to the Event
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        public string LogMessage { get; set; }
    }
}
