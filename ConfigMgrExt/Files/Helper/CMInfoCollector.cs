using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConfigMgrExt.CMInfoClass;

namespace ConfigMgrExt.Helper
{
    /// <summary>
    /// Gathers informations of different SMS WMI-Classes
    /// </summary>
    public class CMInfoCollector
    {
        /// <summary>
        /// Gathers informations for a specified SMS WMI-Class and converts the Result to a list of CMInfo derived Types 
        /// e.g. CMApplication, CMCollection, etc. 
        /// </summary>
        /// <typeparam name="T">Type of CMInfo derived Class</typeparam>
        /// <param name="helper">WQL Helper object </param>
        /// <param name="SMS_Class">SMS WMI-Class</param>
        /// <returns>List of type T</returns>
        public static async Task<List<T>> GetSMSObjectInformation<T>(WQLHelper helper, string SMS_Class)
                                                                    where T: CMInfo, new()
        {
            var result = await Task.Run(() =>
            {
                var res = helper.QueryWmi($"Select * From {SMS_Class}");
                List<T> objects = new List<T>();

                foreach (WqlResultObject r in res)
                { 
                    var obj = r.ManagedObject;
                    objects.Add(CMConverter<T>.ConvertFrom(obj));
                }
                return objects;
            });
            return result;
        }

        /// <summary>
        /// Returns the SiteCode of the SMS Provider where the WQLConnectionManager is connected to
        /// </summary>
        /// <param name="siteServer">Name of the SMS Provider</param>
        /// <returns>Sitecode of the connected Site</returns>
        public static async Task<string> GetSiteCodeAsync(string siteServer)
        {
            var result = await Task.Run(() =>
            {
                try
                {
                    using (WQLHelper wql = new WQLHelper(siteServer))
                    {
                        return wql.GetSiteCode();
                    }
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    System.Windows.Forms.MessageBox.Show($"{ex.GetType().Name} : {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                catch (System.Management.ManagementException ex)
                {
                    System.Windows.Forms.MessageBox.Show($"{ex.GetType().Name} : {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                catch (SmsConnectionException ex)
                {
                    System.Windows.Forms.MessageBox.Show($"{ex.GetType().Name} : {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show($"{ex.GetType().Name} : {ex.GetType().Namespace} :No SMS_Provider found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            });
            return result;

        }
    }
}
