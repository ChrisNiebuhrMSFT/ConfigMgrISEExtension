using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt_DevTest.Helper
{
    /// <summary>
    /// Represents the main informations of an SMS_Softwareupdate Object
    /// </summary>
    public class CMSoftwareupdate : CMInfo
    {
        #region Members
        public string LocalizedDisplayname { get; set; }
        public string ArticleID { get; set; }
        public string CI_ID { get; set; }
        public string IsSuperseded { get; set; }
        public string IsExpired { get; set; }
        #endregion

        #region Constructors

        #endregion

        #region Methods
        #endregion
    }
}
