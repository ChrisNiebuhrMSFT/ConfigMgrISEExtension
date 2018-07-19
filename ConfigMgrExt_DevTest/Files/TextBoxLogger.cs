using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ConfigMgrExt_DevTest.Helper
{
    /// <summary>
    /// Helperclass to provide "In-Application" -Logging
    /// </summary>
    public class TextBoxLogger
    {
        private TextBox _destination;

        /// <summary>
        /// Creates a TextBoxLogger with a specific Textbox Control for the Loggingoutput
        /// </summary>
        /// <param name="destination"></param>
        public TextBoxLogger(TextBox destination)
        {
            _destination = destination;
        }

        /// <summary>
        /// Method to register for a Logger-Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WriteLog(object sender, LogEventArgs e)
        {
            var d = DateTime.Now;
            var format = String.Format("{0:yyyy/dd/MM}-{1:hh:mm:ss.fff}: {2}\n", d, d, e.LogMessage);
            _destination.AppendText(format);
        }
    }
}
