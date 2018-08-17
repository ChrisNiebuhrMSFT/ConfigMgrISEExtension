using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt.CMInfoClass
{
    /// <summary>
    /// Represents the main informations of an SMS_Application Object
    /// </summary>
    public class CMApplication : CMInfo
    {
        #region Members
        public string LocalizedDisplayname { get; set; }
        public string CIVersion { get; set; }
        public string ObjectPath { get; set; }
        public string CreatedBy{ get; set; }
        public string IsDeployed { get; set; }
        public string IsSuperseded{ get; set; }
        public string IsSuperseding{ get; set; }
        public string Manufacturer { get; set; }
        public string ModelName { get; set; }
        #endregion

        #region Constructors

        #endregion

        #region Methods
        #endregion
    }
}



