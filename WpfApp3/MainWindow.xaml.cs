using System;
using System.Collections.Generic;
using System.IO;
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
using WpfApp3.Domain;

namespace WpfApp3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer bgTimer;
        DispatcherTimer currentTimer;

        Storyboard bgstoryboard = null;
        IList<string> ListImages = new List<string>();
        IList<Uri> ListImagesUri = new List<Uri>();
        string imgFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images");
        int ImageIndex = 1;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            bgstoryboard = new Storyboard();
            bgstoryboard.AutoReverse = false;
            bgstoryboard.FillBehavior = FillBehavior.HoldEnd;
            bgstoryboard.RepeatBehavior = new RepeatBehavior(1);
            BgSwitchIni();
            ListImagesUri = GetImageList();

            bgTimer = new DispatcherTimer();
            bgTimer.Interval = new TimeSpan(0, 0, 10);
            bgTimer.Tick += bgTimer_Tick1;
            bgTimer.Start();

            currentTimer = new DispatcherTimer();
            currentTimer.Interval = new TimeSpan(0, 0, 1);
            currentTimer.Tick += currentTimer_Tick1;
            currentTimer.Start();

            userName.GotFocus += UserName_GotFocus;
            userName.LostFocus += UserName_LostFocus;
            pwd.GotFocus += Pwd_GotFocus;
            pwd.LostFocus += Pwd_LostFocus;
        }

        private void Pwd_LostFocus(object sender, RoutedEventArgs e)
        {
            pwdZone.BorderBrush = Brushes.White;
            userZone.BorderThickness = new Thickness(1);
        }

        private void Pwd_GotFocus(object sender, RoutedEventArgs e)
        {
            pwdZone.BorderBrush = Brushes.BlueViolet;
            pwdZone.BorderThickness = new Thickness(2);
        }

        private void UserName_LostFocus(object sender, RoutedEventArgs e)
        {
            userZone.BorderBrush = Brushes.White;
            userZone.BorderThickness = new Thickness(1);
        }

        private void UserName_GotFocus(object sender, RoutedEventArgs e)
        {
            userZone.BorderBrush = Brushes.BlueViolet;
            userZone.BorderThickness =new Thickness(2);
        }

        private void currentTimer_Tick1(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;
            switch (current.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    cuWeek.Text = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    cuWeek.Text = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    cuWeek.Text = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    cuWeek.Text = "星期四";
                    break;
                case DayOfWeek.Friday:
                    cuWeek.Text = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    cuWeek.Text = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    cuWeek.Text = "星期日";
                    break;
            }
            
            cuDate.Text = current.ToShortDateString();
            string time = current.ToLongTimeString();
            string[] timeArray = time.Split(':');
            if (timeArray[2].Equals("59"))
            {
                currentTimer.Interval = new TimeSpan(0, 1, 0);
            }
            cuTime.Text = timeArray[0] + ":" + timeArray[1];
        }


        private void MyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bgTimer.Tick -= bgTimer_Tick1;
            currentTimer.Tick -= currentTimer_Tick1;
            userName.GotFocus -= UserName_GotFocus;
            userName.LostFocus -= UserName_LostFocus;
            pwd.GotFocus -= Pwd_GotFocus;
            pwd.LostFocus-= Pwd_LostFocus;
        }

        private void bgTimer_Tick1(object sender, EventArgs e)
        {
            if (ListImagesUri.Count > 0)
            {
                if (ImageIndex >= ListImagesUri.Count)
                {
                    ImageIndex = 0;
                }
                BgSwitch1(ListImagesUri[ImageIndex]);
                if (ImageIndex == 1 && ListImagesUri.Count == 1)
                {
                    bgTimer.Stop();
                }
                else
                {
                    bgstoryboard.Begin(this);
                    ImageIndex++;
                }
            }
        }

        void bgTimer_Tick(object sender, EventArgs e)
        {
            if (ListImages.Count > 0)
            {
                if (ImageIndex >= ListImages.Count)
                {
                    ImageIndex = 0;
                }
                BgSwitch(ListImages[ImageIndex]);
                if (ImageIndex == 1 && ListImages.Count == 1)
                {
                    bgTimer.Stop();
                }
                else
                {
                    bgstoryboard.Begin(this);
                    ImageIndex++;
                }
                Console.WriteLine(DateTime.Now.ToString());
            }
        }

        private void ChargeBG_Click(object sender, RoutedEventArgs e)
        {

            //ListImages = GetImages(imgFolder);

            //ListImages = GetImages("pack://application:,,,/Images/");
            //if (ListImages.Count > 0)
            //{
            //    int index = new Random().Next(0,11);
            //    ImageBrush imageBrush = new ImageBrush();
            //    imageBrush.ImageSource = new BitmapImage(new Uri(ListImages[index]));
            //    myWindow.Background = imageBrush;
            //    Console.WriteLine(ListImages[index]);
            //}

        }

        /// <summary>
        /// 硬编码方式获取图片
        /// </summary>
        /// <returns></returns>
        private IList<Uri> GetImageList()
        {
            Uri[] dataJpg = new Uri[13];
            for (int i = 0; i < 13; i++)
            {
                dataJpg[i] = new Uri("pack://application:,,,/Images/bj_" + i.ToString() + ".jpg", UriKind.Absolute);
            }
            return ArraysToSingArray(dataJpg);
        }

        private IList<Uri> ArraysToSingArray(Uri[] array1)
        {
            Array array = Array.CreateInstance(typeof(Uri), array1.Length);

            array1.CopyTo(array, 0);

            return RandomSort1(((Uri[])array).ToList<Uri>());
        }

        private IList<Uri> RandomSort1(IList<Uri> listPhoto)
        {
            IList<Uri> lists = new List<Uri>();
            Random random = new Random();
            int index = 0;
            while (listPhoto.Count > 0)
            {
                if (listPhoto.Count == 1)
                {
                    lists.Add(listPhoto[0]);
                    listPhoto.Clear();
                }
                else
                {
                    index = random.Next(listPhoto.Count - 1);
                    lists.Add(listPhoto[index]);
                    listPhoto.RemoveAt(index);
                }
            }
            return lists;
        }

        private void BgSwitch1(Uri imgPath)
        {
            var obj = bgstoryboard.Children.FirstOrDefault(c => c is ObjectAnimationUsingKeyFrames);
            if (obj != null)
            {
                ObjectAnimationUsingKeyFrames oa = obj as ObjectAnimationUsingKeyFrames;
                if (oa.KeyFrames.Count > 0)
                {
                    oa.KeyFrames[0].Value = new BitmapImage(imgPath);
                }
            }
        }

        private IList<string> GetImages(string PhotoFlold)
        {

            string[] dataJpg = Directory.GetFiles(PhotoFlold, "*.jpg");
            //string[] dataPng = Directory.GetFiles(PhotoFlold, "*.png");
            //string[] dataGif = Directory.GetFiles(PhotoFlold, "*.gif");
            //string[] dataBmp = Directory.GetFiles(PhotoFlold, "*.bmp");
            //return ArraysToSingArray(dataJpg, dataPng, dataGif, dataBmp);
            return ArraysToSingArray(dataJpg);
        }

        private IList<string> ArraysToSingArray(string[] array1)
        {
            Array array = Array.CreateInstance(typeof(string), array1.Length);

            array1.CopyTo(array, 0);

            return RandomSort(((string[])array).ToList<string>());
        }

        //private IList<string> ArraysToSingArray(string[] array1, string[] array2, string[] array3, string[] array4)
        //{
        //    Array array = Array.CreateInstance(typeof(string), array1.Length +
        //         array2.Length + array3.Length + array4.Length);

        //    array1.CopyTo(array, 0);
        //    array2.CopyTo(array, array1.Length);
        //    array3.CopyTo(array, array1.Length + array2.Length);
        //    array4.CopyTo(array, array1.Length + array2.Length + array3.Length);

        //    return RandomSort(((string[])array).ToList<string>());
        //}

        private IList<string> RandomSort(IList<string> listPhoto)
        {
            IList<string> lists = new List<string>();
            Random random = new Random();
            int index = 0;
            while (listPhoto.Count > 0)
            {
                if (listPhoto.Count == 1)
                {
                    lists.Add(listPhoto[0]);
                    listPhoto.Clear();
                }
                else
                {
                    index = random.Next(listPhoto.Count - 1);
                    lists.Add(listPhoto[index]);
                    listPhoto.RemoveAt(index);
                }
            }
            return lists;
        }

        private void BgSwitchIni()
        {
            DoubleAnimationUsingKeyFrames da = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame sd = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500)));//前一张背景图片去白色mask时长
            da.KeyFrames.Add(sd);
            Storyboard.SetTargetName(da, myGrid.Name);
            DependencyProperty[] propertyChain = new DependencyProperty[]
            {
                    Panel.BackgroundProperty,
                    Brush.OpacityProperty
            };
            Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)", propertyChain));

            ObjectAnimationUsingKeyFrames oa = new ObjectAnimationUsingKeyFrames();
            DiscreteObjectKeyFrame diso = new DiscreteObjectKeyFrame(new BitmapImage(new Uri("pack://application:,,,/Images/bj_3.jpg", UriKind.Absolute)), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(10)));
            oa.KeyFrames.Add(diso);
            oa.BeginTime = new TimeSpan(0, 0, 0, 0, 510); //过渡动画时长
            Storyboard.SetTargetName(oa, myGrid.Name);
            DependencyProperty[] propertyChain2 = new DependencyProperty[]
            {
                    Panel.BackgroundProperty,
                    ImageBrush.ImageSourceProperty
            };
            Storyboard.SetTargetProperty(oa, new PropertyPath("(0).(1)", propertyChain2));

            DoubleAnimationUsingKeyFrames da2 = new DoubleAnimationUsingKeyFrames();
            da2.BeginTime = new TimeSpan(0, 0, 0, 0, 520);
            EasingDoubleKeyFrame sd2 = new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500)));//后一张背景图片去白色mask时长
            da2.KeyFrames.Add(sd2);
            Storyboard.SetTargetName(da2, myGrid.Name);
            Storyboard.SetTargetProperty(da2, new PropertyPath("(0).(1)", propertyChain));
            bgstoryboard.Children.Add(da);
            bgstoryboard.Children.Add(oa);
            bgstoryboard.Children.Add(da2);
        }

        private void BgSwitch(string imgPath)
        {
            var obj = bgstoryboard.Children.FirstOrDefault(c => c is ObjectAnimationUsingKeyFrames);
            if (obj != null)
            {
                ObjectAnimationUsingKeyFrames oa = obj as ObjectAnimationUsingKeyFrames;
                if (oa.KeyFrames.Count > 0)
                {
                    oa.KeyFrames[0].Value = new BitmapImage(new Uri(imgPath));
                }
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var story = (Storyboard)this.Resources["HideWindow"];
            if (story != null)
            {
                story.Completed += delegate { ShowBussinessWindow(); };
                story.Begin(this);
            }
               
        }

        private void ShowBussinessWindow()
        {
            this.Hide();
            Window1 window1 = new Window1();
            window1.Show();
        }
    }
}
