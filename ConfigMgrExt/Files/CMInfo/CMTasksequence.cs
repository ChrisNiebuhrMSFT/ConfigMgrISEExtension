using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt.CMInfoClass
{
    /// <summary>
    /// Represents the main informations of an SMS_TasksequencePackage Object
    /// </summary>
    public class CMTasksequence : CMInfo
    {
        public string Name { get; set; }
        public string PackageID { get; set; }
        public string Description { get; set; }
    }
}
