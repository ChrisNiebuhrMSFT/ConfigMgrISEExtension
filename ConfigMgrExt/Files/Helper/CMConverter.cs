using System;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using ConfigMgrExt.CMInfoClass;

namespace ConfigMgrExt.Helper
{
    /// <summary>
    /// Static Class to convert a WMI-Managementobject to a CMInfo derived Object 
    /// </summary>
    /// <typeparam name="T">Name of CMInfo derived Class</typeparam>
    public static class CMConverter<T> where T:CMInfo, new()
    {
        /// <summary>
        /// Converts a WMI-Managementobject to a CMInfo derived object
        /// </summary>
        /// <param name="wmiObject">Object you like to convert</param>
        /// <returns>Converted Object of type T</returns>
        public static T ConvertFrom(ManagementBaseObject wmiObject)
        {
            T tmp = new T();
            try
            {
                var props = tmp.GetType().GetProperties()
                                         .Where(p => wmiObject.GetPropertyValue(p.Name) != null);
                foreach (var prop in props)
                {
                    tmp.GetType().GetProperty(prop.Name).SetValue(tmp, wmiObject[prop.Name].ToString());
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"{ex.GetType().Name} : {ex.GetType().Namespace} :{ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tmp;
        }
    }
}

