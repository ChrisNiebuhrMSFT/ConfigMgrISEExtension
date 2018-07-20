#define TRACELOG
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.PowerShell.Host.ISE;
using ConfigMgrExt.Helper;
using ConfigMgrExt.CMInfoClass;
using System.Windows.Forms;
using System.Windows.Data;
using ConfigMgrExt.Files.Helper;

namespace ConfigMgrExt
{
    /// <summary>
    /// Interaction logic for ConfigMgrMain.xaml
    /// </summary>
    public partial class ConfigMgrMain : System.Windows.Controls.UserControl, IAddOnToolHostObject
    {
        private Logger _logger;
#if (TRACELOG)
        private CMTraceLog _cmtrace;
#endif
        private TextBoxLogger _txtLogger;
        private const int MAX_SOFTWAREUPDATECOUNT = 5000; //MAX Number of Softwarepdates for the Softwareupdate Grid
        private string _connectedSite; //stores the current connected Site
        public ObjectModelRoot HostObject { get; set; } //Access ISE Hostobject

        public ConfigMgrMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Connects to the SMSProvider and gathers some informations regarding Applications, Collections, Softwareupdates,
        /// Drivers, Tasksequences , Packages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtSiteServer.Text))
            {
                _logger.WriteLog("Siteserver Textbox is empty. Please provide a valid Siteserver name");
                System.Windows.Forms.MessageBox.Show("Please enter a SiteServer Name!", "Information", MessageBoxButtons.OK);
            }
            else
            {
                var siteServer = TxtSiteServer.Text;
                _logger.WriteLog($"Siteserver={siteServer}");
                //Find SiteCode Information for given SiteServer
                string siteCode = await CMInfoCollector.GetSiteCodeAsync(siteServer);
                if (!String.IsNullOrEmpty(siteCode))
                {
                    txtblcSiteCode.Text = siteCode;
                    _logger.WriteLog($"SiteCode = {siteCode}");
                    _connectedSite = siteServer;
                    SetConnectButton("Connected", false);
                    TabMenu.IsEnabled = true;
                }
                else
                {
                    _logger.WriteLog($"SiteCode could not be found");
                    TabMenu.IsEnabled = false;
                    _connectedSite = siteServer;
                    return;
                }
                using (WQLHelper helper = new WQLHelper(siteServer))
                {
                    await GatherInformation(helper);
                }
            }
        }

        /// <summary>
        /// Gathers informations for each CMInfo derived Class and puts them to a related Grid
        /// </summary>
        /// <param name="wqlHelper"></param>
        /// <returns></returns>
        private async Task GatherInformation(WQLHelper wqlHelper)
        {
            _logger.WriteLog("Loading Applicationinformation");
            List<CMApplication> apps = await CMInfoCollector.GetSMSObjectInformation<CMApplication>(wqlHelper, "SMS_Applicationlatest");
            grdApplication.ItemsSource = apps.ToArray();
            _logger.WriteLog("Applicationinformation successfully loaded");

            _logger.WriteLog("Loading Collectioninformation");
            List<CMCollection> colls = await CMInfoCollector.GetSMSObjectInformation<CMCollection>(wqlHelper, "SMS_Collection");
            grdCollection.ItemsSource = colls.ToArray();
            _logger.WriteLog("Collectioninformation successfully loaded");

            _logger.WriteLog("Loading Packageinformation");
            List<CMPackage> pkgs = await CMInfoCollector.GetSMSObjectInformation<CMPackage>(wqlHelper, "SMS_Package");
            grdPackage.ItemsSource = pkgs.ToArray();
            _logger.WriteLog("Packageinformation successfully loaded");


            _logger.WriteLog("Loading Softwareupdateinformation");
            List<CMSoftwareupdate> sus = await CMInfoCollector.GetSMSObjectInformation<CMSoftwareupdate>(wqlHelper, "SMS_Softwareupdate");
            grdSoftwareupdate.ItemsSource = sus.Take(MAX_SOFTWAREUPDATECOUNT)
                                                .ToArray();
            _logger.WriteLog("Softwareupdateinformation successfully loaded");

            _logger.WriteLog("Loading Tasksequenceinformation");
            List<CMTasksequence> ts = await CMInfoCollector.GetSMSObjectInformation<CMTasksequence>(wqlHelper, "SMS_TaskSequencePackage");
            grdTasksequence.ItemsSource = ts.ToArray();
            _logger.WriteLog("Tasksequenceinformation successfully loaded");
        }

        /// <summary>
        /// Triggers a Connect Button Click when the Enter-Key is pressed. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSiteServer_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnConnect_Click(sender, e);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Do some preparation work when the Form is loaded
        /// Enable logging mechanisms and test if ConfigMgr Console is installed on the current system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Loaded(object sender, RoutedEventArgs e)
        {
            _logger = new Logger();
            //Create and Register a Textboxlogger + CmTrace Logger for this application
#if (TRACELOG)
            _cmtrace = new CMTraceLog(Environment.CurrentDirectory, "Test.log", "ConfigMgrEasy");
            _logger.LogEntry += _cmtrace.WriteLog;
#endif
            _txtLogger = new TextBoxLogger(txtLogging);
            _logger.LogEntry += _txtLogger.WriteLog;
            CheckForConsoleInstallation();
        }

        /// <summary>
        /// Check if the ConfigMgr Console is installed on the currrent System
        /// </summary>
        private void CheckForConsoleInstallation()
        {
            if (Environment.GetEnvironmentVariable("SMS_ADMIN_UI_PATH") != null)
            {
                _logger.WriteLog($"ConfigMgr Console installed on System {Environment.GetEnvironmentVariable("Computername")}.");
                //_isConsoleInstalled = true;
            }
            else
            {
                _logger.WriteLog($"Console not installed on System {Environment.GetEnvironmentVariable("Computername")}.");
                // _isConsoleInstalled = false;
            }
        }

        /// <summary>
        /// TextChanged Event changes the Status of the Connect Button when the  Textvalue of the SiteServer is different
        /// to _connectedSite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSiteServer_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_connectedSite != null)
            {
                if (!TxtSiteServer.Text.Equals(_connectedSite))
                {
                    SetConnectButton("Connect", true);
                }
                else
                {
                    SetConnectButton("Connected", false);
                }
            }
        }

        /// <summary>
        /// Changes the Status and Conent Property of the Connect Button
        /// </summary>
        /// <param name="status"></param>
        /// <param name="isEnabled"></param>
        private void SetConnectButton(string status, bool isEnabled)
        {
            BtnConnect.IsEnabled = isEnabled;
            BtnConnect.Content = status;
        }

        /// <summary>
        /// Applies the CollectionFilterview to a specific Datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            var s = (System.Windows.Controls.Button)sender; //The Button determines which Filter will be Applied 

            switch (s.Name)
            {
                case "BtnApplyCollectionFilter":
                    CollectionViewHelper<CMCollection>.ApplyFilter(txtCollectionFilter, grdCollection, "Collections", "Name", _logger);
                    break;
                case "BtnApplyApplicationFilter":
                    CollectionViewHelper<CMApplication>.ApplyFilter(txtApplicationFilter, grdApplication, "Applications", "LocalizedDisplayname", _logger);
                    break;
                case "BtnApplySoftwareupdateFilter":
                    CollectionViewHelper<CMSoftwareupdate>.ApplyFilter(txtSoftwareupdateFilter, grdSoftwareupdate, "Software Updates", "LocalizedDisplayname", _logger);
                    break;
            }
        }
    }
}
