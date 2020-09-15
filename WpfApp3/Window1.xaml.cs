using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp3.Common;
using WpfApp3.Models;
using WpfApp3.ViewModels;
using WpfApp3.Views;
using WpfApp3.Views.Menu1;
using WpfApp3.Views.Menu2;
using WpfApp3.Views.Menu3;

namespace WpfApp3
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        DispatcherTimer bgTimer;
        BackgroundStory story;

        public ContentPage contentPage;
        public static Snackbar Snackbar;

        private DoubleAnimation c_daListAnimation;
        private DoubleAnimation c_gsAnimation;
        public bool c_bState = true;//记录菜单栏状态 false隐藏 true显示
        public bool c_changeState = true;

        public Window1()
        {
            InitializeComponent();
            //Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(2500);
            //}).ContinueWith(t =>
            //{
            //    //note you can use the message queue from any thread, but just for the demo here we 
            //    //need to get the message queue from the snackbar, so need to be on the dispatcher
            //    MainSnackbar.MessageQueue.Enqueue("Welcome to Material Design In XAML Tookit");
            //}, TaskScheduler.FromCurrentSynchronizationContext());

            DataContext = new Window1ViewModel(null);


            //Snackbar = this.MainSnackbar;
        }

        private void Win1_Loaded(object sender, RoutedEventArgs e)
        {
            
            TreeMenuBt.Width = 1920 * 0.15;

            c_daListAnimation = new DoubleAnimation();
            c_daListAnimation.BeginTime = TimeSpan.FromSeconds(1);//获取或设置此 Timeline 将要开始的时间。
            c_daListAnimation.FillBehavior = FillBehavior.HoldEnd;//获取或设置一个值，该值指定 Timeline 在活动周期结束后的行为方式。
            c_daListAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));//获取或设置此时间线播放的时间长度，而不是计数重复。

            c_gsAnimation = new DoubleAnimation();
            c_gsAnimation.BeginTime = TimeSpan.FromSeconds(1);//获取或设置此 Timeline 将要开始的时间。
            c_gsAnimation.FillBehavior = FillBehavior.Stop;//获取或设置一个值，该值指定 Timeline 在活动周期结束后的行为方式。
            c_gsAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));//获取或设置此时间线播放的时间长度，而不是计数重复。
            c_gsAnimation.Completed += C_gsAnimation_Completed;

            story = new BackgroundStory(grdWorkbench);

            bgTimer = new DispatcherTimer();
            bgTimer.Interval = new TimeSpan(0, 0, 10);
            bgTimer.Tick += bgTimer_Tick1;
            bgTimer.Start();

            //currentTimer = new DispatcherTimer();
            //currentTimer.Interval = new TimeSpan(0, 0, 1);
            //currentTimer.Tick += currentTimer_Tick1;
            //currentTimer.Start();

            contentPage = new ContentPage();
            myContent.Children.Add(contentPage);
            //ShowHiddenMenu();

            var item0 = new ItemMenu("系统菜单", PackIconKind.ViewDashboard, new Menu2());

            var menuRegister = new List<SubItem>();
            menuRegister.Add(new SubItem("系统菜单", new Menu1_1()));
            menuRegister.Add(new SubItem("系统菜单", new Menu1_2()));
            menuRegister.Add(new SubItem("系统菜单", new Menu1_3()));
            menuRegister.Add(new SubItem("系统菜单", new Menu1_1()));
            var item1 = new ItemMenu("系统菜单", menuRegister, PackIconKind.Register);
           

            var menuSchedule = new List<SubItem>();
            menuSchedule.Add(new SubItem("系统菜单", new Menu1_2()));
            menuSchedule.Add(new SubItem("系统菜单", new Menu1_3()));
            menuSchedule.Add(new SubItem("系统菜单", new Menu1_1()));
            menuSchedule.Add(new SubItem("系统菜单", new Menu1_3()));
            var item2 = new ItemMenu("系统菜单", menuSchedule, PackIconKind.Schedule);
           

            menu.Children.Add(new UserControlMenuItem(item0));
            menu.Children.Add(new UserControlMenuItem(item1));
            menu.Children.Add(new UserControlMenuItem(item2));

            GlobalParams.myMain = this;



        }

        private void C_gsAnimation_Completed(object sender, EventArgs e)
        {
            if(!c_changeState)
            {
                myContent.Margin = new Thickness(-(1920 * 0.135)+5, 5, 5, 5);
            }
           else
            {
                myContent.Margin = new Thickness(5, 5, 5, 5);
            }
        }

        private void bgTimer_Tick1(object sender, EventArgs e)
        {
            if (story.ListImagesUri.Count > 0)
            {
                if (story.ImageIndex >= story.ListImagesUri.Count)
                {
                    story.ImageIndex = 0;
                }
                story.BgSwitch1(story.ListImagesUri[story.ImageIndex]);
                if (story.ImageIndex == 1 && story.ListImagesUri.Count == 1)
                {
                    bgTimer.Stop();
                }
                else
                {
                    story.bgstoryboard.Begin(this);
                    story.ImageIndex++;
                }
            }
        }

        void btnGrdSplitter_Click(object sender, RoutedEventArgs e)
        {
            ShowHiddenMenu();
        }

        public void ShowHiddenMenu()
        {
            c_daListAnimation.BeginTime = TimeSpan.FromSeconds(0.01);//设置动画将要开始的时间
            c_gsAnimation.BeginTime = TimeSpan.FromSeconds(0.01);//设置动画将要开始的时间

            if (c_changeState)
            {
                c_changeState = false;
                c_daListAnimation.From = 0;
                c_daListAnimation.To = -(1920 * 0.135);
               

                c_gsAnimation.From = 0;
                c_gsAnimation.To = -(1920 * 0.135);
                
                GridTranslateTransform.BeginAnimation(TranslateTransform.XProperty, c_daListAnimation);
                GroupTranslateTransform.BeginAnimation(TranslateTransform.XProperty, c_gsAnimation);
                btnIco.Kind = PackIconKind.ChevronDoubleRight;

                DoubleAnimation widthAnimation = new DoubleAnimation(0, 1920 * 0.98, new Duration(TimeSpan.FromSeconds(0.5)));
                widthAnimation.FillBehavior = FillBehavior.Stop;
                myContent.BeginAnimation(WidthProperty, widthAnimation, HandoffBehavior.Compose);
                Console.WriteLine("折叠：" + myContent.ActualWidth.ToString());
            }
            else
            {
                grdWorkbench.ColumnDefinitions[0].Width = new GridLength(0.15, GridUnitType.Star);
                grdWorkbench.ColumnDefinitions[1].Width = new GridLength(0.85, GridUnitType.Star);
                c_changeState = true;
                c_gsAnimation.From = -(1920 * 0.135);
                c_gsAnimation.To = 0;
              

                c_daListAnimation.From = -(1920 * 0.135);
                c_daListAnimation.To = 0;

                //myContent.Width = 1920 * 0.985;
                DoubleAnimation widthAnimation = new DoubleAnimation(0, 1920 *0.85, new Duration(TimeSpan.FromSeconds(0.5)));
                widthAnimation.FillBehavior = FillBehavior.Stop;
                myContent.BeginAnimation(WidthProperty, widthAnimation, HandoffBehavior.Compose);

                GridTranslateTransform.BeginAnimation(TranslateTransform.XProperty, c_daListAnimation);
                GroupTranslateTransform.BeginAnimation(TranslateTransform.XProperty, c_gsAnimation);
                btnIco.Kind = PackIconKind.ChevronDoubleLeft;
                Console.WriteLine("展开：" + myContent.ActualWidth.ToString());
            }
        }

        private void Directory_BT_Click(object sender, RoutedEventArgs e)
        {
            ShowHiddenMenu();
            //TreeMenuBt.IsOpen = TreeMenuBt.IsOpen ? false : true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if(radioButton.IsChecked.Value)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
