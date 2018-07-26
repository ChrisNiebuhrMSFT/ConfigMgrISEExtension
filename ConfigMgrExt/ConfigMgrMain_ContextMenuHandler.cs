using ConfigMgrExt.CMInfoClass;
using ConfigMgrExt.ISE;
using System;
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
            var codeFormat = @"If(-not(Get-Module ConfigurationManager))
{{
    Import-Module  ""$($ENV:SMS_ADMIN_UI_PATH)\..\ConfigurationManager.psd1""
    Set-Location ""{0}:""
}}
";
            var code = string.Format(codeFormat, txtblcSiteCode.Text);
            ISEHelper.AddLine(HostObject, code);
        }

        private void Query_Application(object sender, RoutedEventArgs e)
        {
            var tmpObject = sender as MenuItem;
            var application = (CMApplication)grdApplication.SelectedItem;

            switch (tmpObject.Name)
            {
                case "CtxQueryApplicationWMI_fast":
                    {
                        _logger.WriteLog("Query Application fast WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"$app = Get-WmiObject -Namespace root\\sms\\site_{txtblcSiteCode.Text} -Class SMS_ApplicationLatest -Filter \"LocalizedDisplayname='{application.LocalizedDisplayname}'\" -ComputerName {TxtSiteServer.Text}");
                        break;
                    }
                case "CtxQueryApplicationWMI_full":
                    {
                        _logger.WriteLog("Query Application full WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"$app = Get-WmiObject -Namespace root\\sms\\site_{txtblcSiteCode.Text} -Class SMS_ApplicationLatest -Filter \"LocalizedDisplayname='{application.LocalizedDisplayname}'\" -ComputerName {TxtSiteServer.Text}\n$app.Get()");
                        break;
                    }
                case "CtxQueryApplicationCmdlet_fast":
                    {
                        _logger.WriteLog("Query Application fast with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"$app = Get-CMApplication -Name '{application.LocalizedDisplayname}' -Fast");
                        break;
                    }
                case "CtxQueryApplicationCmdlet_full":
                    {
                        _logger.WriteLog("Query Application full with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"$app = Get-CMApplication -Name '{application.LocalizedDisplayname}'");
                        break;
                    }
            }
        }


        private void Query_AllApplications(object sender, RoutedEventArgs e)
        {
            var tmpObject = sender as MenuItem;

            switch (tmpObject.Name)
            {
                case "CtxQueryAllApplicationsWMI_fast":
                    {
                        _logger.WriteLog("Query all Applications fast WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"$allApps = Get-WmiObject -Namespace root\\sms\\site_{txtblcSiteCode.Text} -Class SMS_ApplicationLatest -ComputerName {TxtSiteServer.Text}");
                        break;
                    }
                case "CtxQueryAllApplicationsWMI_full":
                    {
                        _logger.WriteLog("Query all Applications full WMI-Only Template was used");
                        var codeFormat = @"$allApps = Get-WmiObject -Namespace root\sms\site_{0} -Class SMS_ApplicationLatest -ComputerName {1}
$allApps | Foreach-Object {{$_.Get()}}";
                        var code = String.Format(codeFormat, txtblcSiteCode.Text, TxtSiteServer.Text);
                        ISEHelper.AddLine(HostObject, code);
                        break;
                    }
                case "CtxQueryAllApplicationsCmdlet_fast":
                    {
                        _logger.WriteLog("Query all Applications fast with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"$allApps = Get-CMApplication -Fast");
                        break;
                    }
                case "CtxQueryAllApplicationsCmdlet_full":
                    {
                        _logger.WriteLog("Query all Applications full with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"$allApps = Get-CMApplication");
                        break;
                    }
            }
        }

    }
}

