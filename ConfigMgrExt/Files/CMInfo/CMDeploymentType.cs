using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt.CMInfoClass
{
    /// <summary>
    /// Represents the main informations of an SMS_Deploymenttype Object
    /// </summary>
    public class CMDeploymenttype : CMInfo
    {
        #region Members
        public string LocalizedDisplayname { get; set; }
        public string CIVersion { get; set; }
        public string ContentId { get; set; }
        public string IsLatest { get; set; }
        public string CreatedBy { get; set; }
        public string AppModelName{ get; set; }
        public string ModelName { get; set; }
        #endregion

        #region Constructors

        #endregion

        #region Methods
        #endregion
    }
}



