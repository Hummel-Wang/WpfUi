using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace WpfApp3.Common
{
  public  class BackgroundStory
    {
        public  Storyboard bgstoryboard = null;
        public  IList<string> ListImages = new List<string>();
        public  IList<Uri> ListImagesUri = new List<Uri>();
        public  string imgFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images");
        public  int ImageIndex = 1;
        public Grid myGrid;

        public BackgroundStory(Grid gridControl)
        {
            myGrid = gridControl;
            bgstoryboard = new Storyboard();
            bgstoryboard.AutoReverse = false;
            bgstoryboard.FillBehavior = FillBehavior.HoldEnd;
            bgstoryboard.RepeatBehavior = new RepeatBehavior(1);
            BgSwitchIni();
            ListImagesUri = GetImageList();
        }

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

        public void BgSwitch1(Uri imgPath)
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

        public void BgSwitchIni()
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
    }
}
