using ConfigMgrExt.CMInfoClass;
using ConfigMgrExt.ISE;
using System.Windows;
using System.Windows.Controls;

namespace ConfigMgrExt
{
    /// <summary>
    /// This partial Class contains all Eventhandlers for all the different Contextmenus. 
    /// </summary>
    public partial class ConfigMgrMain
    {
        private void MenuQuery_SiteCode(object sender, RoutedEventArgs e)
        {
            var tmpObject = sender as MenuItem;

            switch (tmpObject.Name)
            {
                case "CtxQuerySiteCodeWMI":
                    {
                        _logger.WriteLog("Query SiteCode WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"$siteCode = Get-WmiObject -Namespace root\\sms -Class SMS_ProviderLocation -ComputerName '{TxtSiteServer.Text}' -Filter \"ProviderForLocalSite=1\" |Select-Object -ExpandProperty SiteCode");
                        break;
                    }
                case "CtxQuerySiteCodeCmdlet":
                    {
                        _logger.WriteLog("Query SiteCode with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, "$siteCode = Get-CMSite | Select-Object -ExpandProperty SiteCode");
                        break;
                    }
            }

        }

        private void MenuConfigMgr_CmdLet(object sender, RoutedEventArgs e)
        {
            _logger.WriteLog("ConfigMgr Cmdlet Module Template was used");
            ISEHelper.AddLine(HostObject, "If(-not(Get-Module ConfigurationManager))\n{\n\tImport-Module \"$($ENV:SMS_ADMIN_UI_PATH)\\..\\ConfigurationManager.psd1\"\n}\nSet-Location \"" + txtblcSiteCode.Text + ":\"");
        }

        private void Query_Application(object sender, RoutedEventArgs e)
        {
            var tmpObject = sender as MenuItem;
            var application = (CMApplication)grdApplication.SelectedItem;

            switch (tmpObject.Name)
            {
                case "CtxQueryApplicationWMI_fast":
                    {
                        _logger.WriteLog("Query Application WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"$app = Get-WmiObject -Namespace root\\sms\\site_{txtblcSiteCode.Text} -Class SMS_ApplicationLatest -Filter \"LocalizedDisplayname='{application.LocalizedDisplayname}'\" -ComputerName {TxtSiteServer.Text}");
                        break;
                    }
                case "CtxQueryApplicationCmdlet_fast":
                    {
                        _logger.WriteLog("Query Application with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"$app = Get-CMApplication -Name '{application.LocalizedDisplayname}' -Fast");
                        break;
                    }
            }
        }

    }
}

