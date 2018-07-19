using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ConfigMgrExt.CMInfoClass;
using ConfigMgrExt.Helper;

namespace ConfigMgrExt.Files.Helper
{
    /// <summary>
    /// Helper Class to create and manage CollectionView Objects for the Datagrid Itemsources
    /// </summary>
    /// <typeparam name="T">CMInfo derived Class</typeparam>
    public class CollectionViewHelper<T> where T : CMInfo
    {
        /// <summary>
        /// Creates a Collectionview for a given ItemSource.
        /// The Filter will apply to a given Propertyname and will use a specified Textbox to filter the Values
        /// </summary>
        /// <param name="itemSource">ItemSource of a Datagrid</param>
        /// <param name="propertyName">Property from CMInfo derived Class you want to filter</param>
        /// <param name="txtBox">Textbox Control which contains the Value you want to Filter </param>
        private static void CreateCollectionViewFilter(IEnumerable itemSource, string propertyName, System.Windows.Controls.TextBox txtBox)
        {
            ListCollectionView view = CollectionViewSource.GetDefaultView(itemSource) as ListCollectionView;
            view.Filter = cmInfo =>
            {
                var result = true;
                var tmp = cmInfo as T;
                Type test = cmInfo.GetType();
                if (test.GetProperty(propertyName)?.GetValue(cmInfo) is string prop)
                {
                    result = prop.ToLower().Contains(txtBox.Text.ToLower());
                }
                return result;
            };
        }

        public static void ApplyFilter(System.Windows.Controls.TextBox txtBox, System.Windows.Controls.DataGrid destGrid, string catagoryName, string propertyName, Logger logger)
        {
            logger.WriteLog($"Applied filter: \"{txtBox.Text}\" to Datagrid {catagoryName}");
            CreateCollectionViewFilter(destGrid.ItemsSource, propertyName, txtBox);
        }
    }
}
