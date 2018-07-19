using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt.CMInfoClass
{
    /// <summary>
    /// Represents the main informations of an SMS_Package Object
    /// </summary>
    public class CMPackage : CMInfo
    {
        #region Members
        public string Name { get; set; }
        public string PackageID { get; set; }
        public string PackageSize { get; set; }
        public string ObjectPath { get; set; }
        #endregion

        #region Constructors

        #endregion

        #region Methods
        #endregion
    }
}
