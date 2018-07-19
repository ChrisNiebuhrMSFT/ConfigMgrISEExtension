using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt_DevTest.Helper
{
    /// <summary>
    /// Delegate for the ILogger Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Logging(object sender, LogEventArgs e);
    /// <summary>
    /// This interface defines the event for LogEntries.
    /// Concrete Logger like TextboxLogger & CMTraceLogger(and more..) can register for this event
    /// </summary>
    public interface ILogger
    {
        event Logging LogEntry;
    }
}
