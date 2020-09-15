using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.Common;
using WpfApp3.Models;

namespace WpfApp3.ViewModels
{
    /// <summary>
    /// UserControlMenuItem.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        public ItemMenu myItems;
        public UserControlMenuItem(ItemMenu itemMenu)
        {
            InitializeComponent();
            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;
            this.DataContext = itemMenu;
            myItems = itemMenu;
        }

        private void ListViewItemMenu_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GlobalParams.myMain.myContent.Children.Clear();
            GlobalParams.myMain.myContent.Children.Add(myItems.Screen);
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = sender as ListView;
            SubItem sub = lv.SelectedItem as SubItem;
            GlobalParams.myMain.myContent.Children.Clear();
            GlobalParams.myMain.myContent.Children.Add(sub.Screen);

        }
    }
}
