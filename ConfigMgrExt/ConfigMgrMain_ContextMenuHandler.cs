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
            var code = string.Format(codeFormat, TxtblcSiteCode.Text);
            ISEHelper.AddLine(HostObject, code);
        }

        private void Query_Application(object sender, RoutedEventArgs e)
        {
            var tmpObject = sender as MenuItem;
            var application = (CMApplication)GrdApplication.SelectedItem;

            switch (tmpObject.Name)
            {
                case "CtxQueryApplicationWMI_fast":
                    {
                        _logger.WriteLog("Query Application fast WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"$app = Get-WmiObject -Namespace root\\sms\\site_{TxtblcSiteCode.Text} -Class SMS_ApplicationLatest -Filter \"LocalizedDisplayname='{application.LocalizedDisplayname}'\" -ComputerName {TxtSiteServer.Text}");
                        break;
                    }
                case "CtxQueryApplicationWMI_full":
                    {
                        _logger.WriteLog("Query Application full WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"$app = Get-WmiObject -Namespace root\\sms\\site_{TxtblcSiteCode.Text} -Class SMS_ApplicationLatest -Filter \"LocalizedDisplayname='{application.LocalizedDisplayname}'\" -ComputerName {TxtSiteServer.Text}\n$app.Get()");
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
                        ISEHelper.AddLine(HostObject, $"$allApps = Get-WmiObject -Namespace root\\sms\\site_{TxtblcSiteCode.Text} -Class SMS_ApplicationLatest -ComputerName {TxtSiteServer.Text}");
                        break;
                    }
                case "CtxQueryAllApplicationsWMI_full":
                    {
                        _logger.WriteLog("Query all Applications full WMI-Only Template was used");
                        var codeFormat = @"$allApps = Get-WmiObject -Namespace root\sms\site_{0} -Class SMS_ApplicationLatest -ComputerName {1}
$allApps | Foreach-Object {{$_.Get()}}";
                        var code = String.Format(codeFormat, TxtblcSiteCode.Text, TxtSiteServer.Text);
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

        private void Query_AllCollections(object sender, RoutedEventArgs e)
        {
            var tmpObject = sender as MenuItem;

            switch (tmpObject.Name)
            {
                case "CtxQueryAllCollectionsWMI":
                    {
                        _logger.WriteLog("Query all Collections WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"$allColls = Get-WmiObject -Namespace root\\sms\\site_{TxtblcSiteCode.Text} -Class SMS_Collection  -ComputerName {TxtSiteServer.Text}");
                        break;
                    }
                case "CtxQueryAllCollectionsCmdlet":
                    {
                        _logger.WriteLog("Query all Collections with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"$allColls = Get-CMCollection");
                        break;
                    }
                case "CtxQueryAllDeviceCollectionsWMI":
                    {
                        _logger.WriteLog("Query all Device-Collections WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"$allDeviceColls = Get-WmiObject -Namespace root\\sms\\site_{TxtblcSiteCode.Text} -Class SMS_Collection -Filter 'CollectionType=2' -ComputerName {TxtSiteServer.Text}");
                        break;
                    }
                case "CtxQueryAllDeviceCollectionsCmdlet":
                    {
                        _logger.WriteLog("Query all Device-Collections with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"$allDeviceColls = Get-CMDeviceCollection");
                        break;
                    }
                case "CtxQueryAllUserCollectionsWMI":
                    {
                        _logger.WriteLog("Query all User-Collections WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"$allUserColls = Get-WmiObject -Namespace root\\sms\\site_{TxtblcSiteCode.Text} -Class SMS_Collection -Filter 'CollectionType=1' -ComputerName {TxtSiteServer.Text}");
                        break;
                    }
                case "CtxQueryAllUserCollectionsCmdlet":
                    {
                        _logger.WriteLog("Query all User-Collections with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"$allUserColls = Get-CMUserCollection");
                        break;
                    }
            }
        }

        private void Query_Collection(object sender, RoutedEventArgs e)
        {
            var tmpObject = sender as MenuItem;
            var collection = (CMCollection)GrdCollection.SelectedItem;

            switch (tmpObject.Name)
            {
                case "CtxQueryCollectionWMI":
                    {
                        _logger.WriteLog("Query Collection WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"$coll = Get-WmiObject -Namespace root\\sms\\site_{TxtblcSiteCode.Text} -Class SMS_Collection -Filter \"Name='{collection.Name}'\" -Computername {TxtSiteServer.Text}");
                        break;
                    }
                case "CtxQueryCollectionCmdlet":
                    {
                        _logger.WriteLog("Query Collection  with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"$coll = Get-CMCollection -Name '{collection.Name}'");
                        break;
                    }
            }
        }

        private void Remove_AllAppRevisions(object sender, RoutedEventArgs e)
        {
            var tmpObject = sender as MenuItem;

            switch (tmpObject.Name)
            {
                case "CtxRemoveAllAppRevisionsWMI":
                    {
                        _logger.WriteLog("Remove all AppRevisions WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"Get-WmiObject -Namespace root\\sms\\site_{TxtblcSiteCode.Text} -Class SMS_Application -Filter 'IsLatest = 0' -ComputerName {TxtSiteServer.Text} | Remove-WmiObject -Verbose");
                        break;
                    }
                case "CtxRemoveAllAppRevisionsCmdlet":
                    {
                        _logger.WriteLog("Remove all AppRevisions with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"Get-CMApplication -Fast | ForEach-Object {{Get-CMApplicationRevisionHistory -Name $_.LocalizedDisplayname | Remove-CMApplicationRevisionHistory -Force}}");
                        break;
                    }
            }
        }

        private void Query_AllDevices(object sender, RoutedEventArgs e)
        {
            var tmpObject = sender as MenuItem;
            switch(tmpObject.Name)
            {
                case "CtxQueryAllDevicesWMI":
                    {
                        _logger.WriteLog("Query all Devices WMI-Only Template was used");
                        ISEHelper.AddLine(HostObject, $"$allDevices = Get-WmiObject -Namespace root\\sms\\site_{TxtblcSiteCode.Text} -Class SMS_CombinedDeviceResources -ComputerName {TxtSiteServer.Text}");
                        break;
                    }
                case "CtxQueryAllDevicesCmdlet":
                    {
                        _logger.WriteLog("Query all Devices with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"$allDevices = Get-CMDevice");
                        break;
                    }
            }
        }

        private void ImportDeviceInformation(object sender, RoutedEventArgs e)
        {
            var tmpObject = sender as MenuItem;
            switch (tmpObject.Name)
            {
                case "CtxImportDeviceInformationWMI_SMBIOSGUID":
                    {
                        _logger.WriteLog("Import Device Information via SMBIOSGUID WMI-Only Template was used");
                        var codeFormat = @"$SMSSite = [WMICLASS]""\\{0}\root\sms\Site_{1}:SMS_SITE""
$params = $SMSSite.GetMethodParameters(""ImportMachineEntry"")
$params.NetBiosName = ""Pleaser enter NetbiosName here""
$params.SMBIOSGUID = ""Please enter SMBIOSGUID here""
$SMSSite.InvokeMethod(""ImportMachineEntry"", $params, $null)
";
                        var code = string.Format(codeFormat, TxtSiteServer.Text, TxtblcSiteCode.Text);
                        ISEHelper.AddLine(HostObject, code);
                        break;
                    }
                case "CtxImportDeviceInformationWMI_MACAddress":
                    {
                        _logger.WriteLog("Import Device Information via MAC-Address WMI-Only Template was used");
                        var codeFormat = @"$SMSSite = [WMICLASS]""\\{0}\root\sms\Site_{1}:SMS_SITE""
$params = $SMSSite.GetMethodParameters(""ImportMachineEntry"")
$params.NetBiosName = ""Pleaser enter Netbiosname here""
$params.MACAddress = ""Please enter MACAdddress here""
$SMSSite.InvokeMethod(""ImportMachineEntry"", $params, $null)
";
                        var code = string.Format(codeFormat, TxtSiteServer.Text, TxtblcSiteCode.Text);
                        ISEHelper.AddLine(HostObject, code);
                        break;
                    }
                case "CtxImportDeviceInformationCmdlet_MACAddress":
                    {
                        _logger.WriteLog("Import Device Information via MAC-Address with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"Import-CMComputerInformation -ComputerName \"Enter NetbiosName here\" -MacAddress \"Enter MAC-Address here\"");
                        break;
                    }
                case "CtxImportDeviceInformationCmdlet_SMBIOSGUID":
                    {
                        _logger.WriteLog("Import Device Information via SMBIOSGUID with ConfigMgr Cmdlet Template was used");
                        ISEHelper.AddLine(HostObject, $"Import-CMComputerInformation -ComputerName \"Enter NetbiosName here\" -SMBiosGuid \"Enter SMBIOSGUID here\"");
                        break;
                    }
            }
        }

        private void Add_DeviceToCollection(object sender, RoutedEventArgs e)
        {
            var tmpObject = sender as MenuItem;
            var device = (CMDevice)GrdDevices.SelectedItem;

            switch(tmpObject.Name)
            {
                case "CtxAddDeviceToCollectionDMCmdlet":
                    {
                        _logger.WriteLog("Add Device to Collection via Direct-Membership with ConfigMgr Cmdlet Template was used");
                        var codeFormat = @"$deviceToAdd = Get-CMDevice -Name {0}
Get-CMCollection -Name ""Enter Collectionname here"" | Add-CMDeviceCollectionDirectMembershipRule -ResourceId $deviceToAdd.ResourceID
";
                        var code = string.Format(codeFormat, device.Name);
                        ISEHelper.AddLine(HostObject,code);
                        break;
                    }

            }

        }
    }
}

