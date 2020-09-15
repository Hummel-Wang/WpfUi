using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp3.Models
{
   public class ItemMenu
    {
        public ItemMenu(string header, List<SubItem> subItems, PackIconKind icon)
        {
            Header = header;
            SubItems = subItems;
            Icon = icon;
        }

        public ItemMenu(string header,PackIconKind icon, UserControl userControl)
        {
            Header = header;
            Icon = icon;
            Screen = userControl;
        }


        public string Header { get; set; }
        public UserControl Screen { get; set; }
        public PackIconKind Icon { get; set; }
        public List<SubItem> SubItems { get; set; }
    }

    public class SubItem
    {
        public SubItem(string name, UserControl userControl = null)
        {
            Name = name;
            Screen = userControl;
        }


        public string Name { get; set; }
        public UserControl Screen { get; set; }
    }
}
