using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt.Helper
{
    /// <summary>
    /// This interface defines the event for LogEntries.
    /// Concrete Logger like TextboxLogger & CMTraceLogger(and more..) can register for this event
    /// </summary>
    public interface ILogger
    {
        event EventHandler<LogEventArgs> LogEntry;
    }
}
