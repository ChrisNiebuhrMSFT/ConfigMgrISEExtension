using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigMgrExt;
using Microsoft.PowerShell.Host.ISE;

namespace ConfigMgrExt.ISE
{
    /// <summary>
    /// Helper-Class to simplyfy the Access to the ObjectModelRoot-Object.
    /// </summary>
    public static class ISEHelper
    {
        /// <summary>
        /// Adds a given command to the current File of the ISE
        /// </summary>
        /// <param name="hostObject"></param>
        /// <param name="command"></param>
        public static void AddLine(ObjectModelRoot hostObject, string command)
        {
            try
            {
                var lineRow = hostObject.CurrentPowerShellTab.Files.SelectedFile.Editor.CaretLine;
                var lineColumn = hostObject.CurrentPowerShellTab.Files.SelectedFile.Editor.CaretColumn;
                var lineCountStart = hostObject.CurrentPowerShellTab.Files.SelectedFile.Editor.LineCount;
              
                hostObject.CurrentPowerShellTab.Files.SelectedFile.Editor.SetCaretPosition(lineRow, 1);
                hostObject.CurrentPowerShellTab.Files.SelectedFile.Editor.InsertText( command + "\n");
                var lineCountEnd = hostObject.CurrentPowerShellTab.Files.SelectedFile.Editor.LineCount;
                var offset = lineCountEnd - lineCountStart;
                hostObject.CurrentPowerShellTab.Files.SelectedFile.Editor.SetCaretPosition(lineRow + offset, 1);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
