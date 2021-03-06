﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMgrExt_DevTest.Helper
{
    /// <summary>
    /// Represents the main informations of an SMS_Collection Object
    /// </summary>
    public class CMCollection : CMInfo
    {
        #region Members
        public string Name { get; set; }
        public string CollectionID { get; set; }
        public string CollectionType { get; set; }
        public string Membercount { get; set; }
        public string ObjectPath { get; set; }
        #endregion

        #region Constructors

        #endregion

        #region Methods
        #endregion
    }
}
