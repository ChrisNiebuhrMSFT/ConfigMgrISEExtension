using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt.CMInfoClass
{
    /// <summary>
    /// Represents the main informations of an SMS_CombinedDeviceResources Object
    /// </summary>
    public class CMDevice : CMInfo
    {
        #region Members
        public string Name { get; set; }
        public string ResourceID { get; set; }
        public string ClientVersion { get; set; }
        public string Domain { get; set; }
        #endregion

        #region Constructors
     
        #endregion

        #region Methods
        #endregion
    }
}



