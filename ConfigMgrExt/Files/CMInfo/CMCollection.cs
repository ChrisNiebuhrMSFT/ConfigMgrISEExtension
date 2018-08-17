using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt.CMInfoClass
{
    /// <summary>
    /// Represents the main informations of an SMS_Collection Object
    /// </summary>
    public class CMCollection : CMInfo
    {
        #region Members
        public string Name { get; set; }
        public string CollectionID { get; set; }
        public string LimitToCollectionName { get; set; }
        public string ObjectPath { get; set; }
        public string MemberCount { get; set; }
        //Private Field for Collectiontype
        private string _CollectionType;
        public string CollectionType
        {
            get { return this._CollectionType; }
            set
            {
                switch (value)
                {
                    case "2":
                        _CollectionType = "Device";
                        break;
                    case "1":
                        _CollectionType = "User";
                        break;
                    default:
                        _CollectionType = "Other";
                        break;
                }
            }
        }
        public string RefreshType { get; set; }
        #endregion

        #region Constructors

        #endregion

        #region Methods
        #endregion
    }
}
