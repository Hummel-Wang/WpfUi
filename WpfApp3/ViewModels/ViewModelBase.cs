using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            //PropertyChangedEventHandler handler = PropertyChanged;

            //if (handler != null)
            //{
            //    handler(this, new PropertyChangedEventArgs(propertyName));
            //}
        }

        #region 私有变量

        #endregion

        #region 公有变量

        #endregion

        #region 公有命令

        #endregion

        #region 私有方法

        #endregion

        #region 公有方法

        #endregion

        #region 构造函数

        #endregion

    }
}
