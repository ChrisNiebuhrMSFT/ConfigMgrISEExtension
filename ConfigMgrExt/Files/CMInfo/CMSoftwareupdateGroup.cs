using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt.CMInfoClass
{
    /// <summary>
    /// Represents the main informations of an SMS_Softwareupdate Object
    /// </summary>
    public class CMSoftwareupdateGroup : CMInfo
    {
        #region Members
        public string LocalizedDisplayname { get; set; }
        public string CI_ID { get; set; }
        public string CreatedBy { get; set; }
        public string IsDeployed { get; set; }
        public string IsExpired { get; set; }
        public string NumberOfCollectionsDeployed {get; set;}
        public string NumberOfExpiredUpdates { get; set;}
        public string NumberOfUpdates { get; set; }

        #endregion

        #region Constructors

        #endregion

        #region Methods
        #endregion
    }
}
