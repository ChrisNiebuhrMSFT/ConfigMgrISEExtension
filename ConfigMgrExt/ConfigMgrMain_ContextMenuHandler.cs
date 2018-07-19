using ConfigMgrExt.ISE;
using System.Windows;

namespace ConfigMgrExt
{
    /// <summary>
    /// This partial Class contains all Eventhandlers for all the different Contextmenus. 
    /// </summary>
    public partial class ConfigMgrMain
    {
        private void MenuSiteCode_WMIOnly(object sender, RoutedEventArgs e)
        {
            _logger.WriteLog("Query SiteCode WMI-Only Template was used");
            ISEHelper.AddLine(HostObject, $"$siteCode = Get-WmiObject -Namespace root\\sms -Class SMS_ProviderLocation -ComputerName '{TxtSiteServer.Text}' -Filter \"ProviderForLocalSite=1\" |Select-Object -ExpandProperty SiteCode");
        }

        private void MenuConfigMgrCmdLet(object sender, RoutedEventArgs e)
        {
            _logger.WriteLog("ConfigMgr Cmdlet Module Template was used");
            ISEHelper.AddLine(HostObject, "If(-not(Get-Module ConfigurationManager))\n{\n\tImport-Module \"$($ENV:SMS_ADMIN_UI_PATH)\\..\\ConfigurationManager.psd1\"\n}\nSet-Location \"" + txtblcSiteCode.Text + ":\"");
        }
    }
}
