using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;

namespace WQLTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            WqlConnectionManager wqlConnectionManager = new WqlConnectionManager();
            wqlConnectionManager.Connect("winsrv2016pri");
           var result = wqlConnectionManager.QueryProcessor.ExecuteQuery("Select * from SMS_ApplicationLatest");
            string t = "Hallo";

            var props = t.GetType().GetProperties();

            foreach (var prop in props)
            {
                Console.WriteLine(prop.Name);
            }
            Console.ReadKey(true);
        }

        private static int WaitForvalue()
        {
            System.Threading.Thread.Sleep(5000);
            return 1;
        }
        private static async Task<string[]> AsyncWQLStuff()
        {
            string[] erg =  await Task<string[]>.Run(
                ()=> {
                var list = new List<string>();
                WqlConnectionManager wqlConnectionManager = new WqlConnectionManager();
                    wqlConnectionManager.Connect("winsrv2016pri");
                    Console.WriteLine("In Sec Thread");
                    IResultObject result = wqlConnectionManager.QueryProcessor.ExecuteQuery("Select * from SMS_ApplicationLatest");
                    
                    foreach (IResultObject res in result)
                    {
                        var props = res.Properties;
                        var tmp = (string)props["LocalizedDisplayName"].ObjectValue;
                        list.Add(tmp);
                    }
                  //  System.Threading.Thread.Sleep(5000);
                    return list.ToArray();
                });
            return erg ;
        }
    }
}
