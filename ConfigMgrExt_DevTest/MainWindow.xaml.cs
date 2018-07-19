using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;
using ConfigMgrExt_DevTest.Helper;

namespace ConfigMgrExt_DevTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logger _logger;
        private CMTraceLog _cmtrace;
        private TextBoxLogger _txtLogger;
        private const int MAX_SOFTWAREUPDATECOUNT = 500;

        public MainWindow()
        {
            InitializeComponent();

        }
        /// <summary>
        /// Connects to the SMSProvider and gathers some informations regarding Applications, Collections, Softwareupdates,
        /// Drivers, Tasksequences , Packaages
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
                }
                else
                {
                    _logger.WriteLog($"SiteCode could not be found");
                    return;
                }

                WQLHelper helper = new WQLHelper(siteServer);
                await GatherInformation(helper);
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
            GrdApplication.ItemsSource = apps;
            _logger.WriteLog("Applicationinformation successfully loaded");

            _logger.WriteLog("Loading Collectioninformation");
            List<CMCollection> colls = await CMInfoCollector.GetSMSObjectInformation<CMCollection>(wqlHelper, "SMS_Collection");
            GrdCollection.ItemsSource = colls;
            _logger.WriteLog("Collectioninformation successfully loaded");

            _logger.WriteLog("Loading Packageinformation");
            List<CMPackage> pkgs = await CMInfoCollector.GetSMSObjectInformation<CMPackage>(wqlHelper, "SMS_Package");
            GrdPackage.ItemsSource = pkgs;
            _logger.WriteLog("Packageinformation successfully loaded");


            _logger.WriteLog("Loading Softwareupdateinformation");
            List<CMSoftwareupdate> sus = await CMInfoCollector.GetSMSObjectInformation<CMSoftwareupdate>(wqlHelper, "SMS_Softwareupdate");
            GrdSoftwareupdate.ItemsSource = sus.Take(MAX_SOFTWAREUPDATECOUNT);
            _logger.WriteLog("Softwareupdateinformation successfully loaded");

            _logger.WriteLog("Loading Tasksequenceinformation");
            List<CMTasksequence> ts = await CMInfoCollector.GetSMSObjectInformation<CMTasksequence>(wqlHelper, "SMS_TaskSequencePackage");
            GrdTasksequence.ItemsSource = ts;
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
            _logger    = new Logger(); 
            //Create and Register a Textboxlogger + CmTrace Logger for this application
            _cmtrace   = new CMTraceLog(Environment.CurrentDirectory, "Test.log", "ConfigMgrEasy");
            _txtLogger = new TextBoxLogger(txtLogging); 
            // Both will register for the OnLogEvent
            _logger.LogEntry += _txtLogger.WriteLog;
            _logger.LogEntry +=  _cmtrace.WriteLog; 
            if (Environment.GetEnvironmentVariable("SMS_ADMIN_UI_PATH") != null)
            {
                _logger.WriteLog($"ConfigMgr Console installed on System {Environment.GetEnvironmentVariable("Computername")}.");
            }
            else
            {
                _logger.WriteLog($"Console not installed on System {Environment.GetEnvironmentVariable("Computername")}.");
            }
        }
    }
}
