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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp4.ViewModel;

namespace WpfApp4
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //这里这个bj变量是用于换图片
        int bj = 0;
        //在WPF中没有Timer控件只能new一个，其实写法用法都一样
        DispatcherTimer Dshtp = new DispatcherTimer();
        Storyboard stdStart;
        public MainWindow()
        {
            InitializeComponent();
            //image1.ImageSource = new BitmapImage(new Uri("Images/bj_6.jpg", UriKind.Relative));

            //stdStart = (Storyboard)this.Resources["Tpjb"];

            ////new TimeSpan(100000000 )设置10秒换一次图片
            //Dshtp.Tick += new EventHandler(Dshtp_Tick);
            //Dshtp.Interval = new TimeSpan(50000000);
            //Dshtp.Start();

            this.DataContext = new MainWindowViewModel();
        }

        void Dshtp_Tick(object sender, EventArgs e)
        {
            //if (bj < 10)
            //{
            //    bj = bj + 1;
            //}
            //else
            //{
            //    bj = 0;
            //}
            //image1.ImageSource = new BitmapImage(new Uri("Images/bj_" + bj.ToString() + ".jpg", UriKind.Relative));

            ////启动淡入淡出效果        
            //stdStart.Begin();
        }

        private void InOut_Click(object sender, RoutedEventArgs e)
        {
            //DoubleAnimation widthAnimation = new DoubleAnimation(0, 200, new Duration(TimeSpan.FromSeconds(0.5)));
            //inOut.BeginAnimation(WidthProperty, widthAnimation, HandoffBehavior.Compose);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var sd = sender as TreeView;
            if(sd != null)
            {
                var node = sd.SelectedItem as Node;
                Console.WriteLine(node.Name);
            }
        }
    }
}
