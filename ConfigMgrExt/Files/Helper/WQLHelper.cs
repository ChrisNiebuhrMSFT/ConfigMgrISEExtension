using System;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;


namespace ConfigMgrExt.Helper
{
    /// <summary>
    /// Helperclass to simplify the WQL Queries against the SMS Provider
    /// </summary>
    public sealed class WQLHelper : IDisposable
    {
        #region Members
        private WqlConnectionManager _wqlConnectionManager;
        private readonly SmsNamedValuesDictionary _smsValues;
        #endregion

        #region Constructors
        /// <summary>
        /// Overloaded Constructor to establish a connection to a specified SMS-Provider (servername)
        /// </summary>
        /// <param name="serverName">Name of the SMS-Provider</param>
        public WQLHelper(string serverName)
        {
                _smsValues = new SmsNamedValuesDictionary();
                _wqlConnectionManager = new WqlConnectionManager(_smsValues);
                _wqlConnectionManager.Connect(serverName);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the SiteCode from the connected SMS-Provider
        /// This Method is used in another Helperclass
        /// <see cref="ConfigMgrExt_DevTest.Helper.CMInfoCollector.GetSiteCodeAsync(string)"/>
        /// to provide a async Mechanism. 
        /// </summary>
        /// <returns>SiteCode of connected SMS-Provider</returns>
        public string GetSiteCode()
        {
            return (string)_smsValues["ConnectedSiteCode"];
        }

        /// <summary>
        /// Queries against the SMS-Provider
        /// </summary>
        /// <param name="query">WQL QueryString</param>
        /// <returns>Queryresult of Type IResultObject</returns>
        public IResultObject QueryWmi(string query)
        {
            return _wqlConnectionManager.QueryProcessor.ExecuteQuery(query);
        }

        /// <summary>
        /// Implementing the Dispose Method to fullfill the IDisposable Interface.
        /// This allows the use of the using-Clause 
        /// </summary>
        public void Dispose()
        {
            _wqlConnectionManager.Dispose();
        }
        #endregion
    }
}
