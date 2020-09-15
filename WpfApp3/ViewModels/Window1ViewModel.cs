﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp3.Commands;
using WpfApp3.Models;
using WpfApp3.Views.Menu1;

namespace WpfApp3.ViewModels
{
   public class Window1ViewModel : ViewModelBase
    {
        #region 私有变量

        //private MenuTreeViewModel _demoItems;

        public List<Node> _nodes { get; set; }
        private DeletgateCommand<object> _selectItemCommand;
        #endregion

        #region 公有变量
        //public MenuTreeViewModel DemoItems
        //{
        //    get
        //    {
        //        return _demoItems;
        //    }
        //    set
        //    {
        //        _demoItems = value;
        //        OnPropertyChanged("_demoItems");
        //    }
        //}
        public List<Node> Nodes
        {
            get
            {
                return _nodes;
            }
            set
            {
                _nodes = value;
                OnPropertyChanged("Nodes");
            }
        }
        #endregion

        #region 公有命令
        public ICommand SelectItemCommand
        {
            get
            {
                if (_selectItemCommand == null)
                {
                    _selectItemCommand = new DeletgateCommand<object>(SelectItemFunction);
                }
                return _selectItemCommand;
            }
        }
        #endregion

        #region 私有方法【命令执行】
        private void SelectItemFunction(object value)
        {
            Node item = value as Node;
            Console.WriteLine(item.ID.ToString() + " *** " + item.Name);
        }


        #endregion

        #region 私有方法【类内辅助】

        /// <summary>
        /// 绑定树
        /// </summary>
        List<Node> Bind(List<Node> nodes)
        {
            List<Node> outputList = new List<Node>();
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].ParentID == -1)
                {
                    outputList.Add(nodes[i]);
                }
                else
                {
                    FindDownward(nodes, nodes[i].ParentID).Nodes.Add(nodes[i]);
                }
            }
            return outputList;
        }


        /// <summary>
        /// 递归向下查找
        /// </summary>
        Node FindDownward(List<Node> nodes, int id)
        {
            if (nodes == null) return null;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].ID == id)
                {
                    return nodes[i];
                }
                Node node = FindDownward(nodes[i].Nodes, id);
                if (node != null)
                {
                    return node;
                }
            }
            return null;
        }


        #endregion

        #region 公有方法

        #endregion

        #region 构造函数
        public Window1ViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            //if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));

            //DemoItems = new MenuTreeViewModel();

            //List<Node> nodes = new List<Node>()
            //{
            //    new Node { ID = 1, Name = "中国" },
            //    new Node { ID = 2, Name = "北京市", ParentID = 1 },
            //    new Node { ID = 3, Name = "吉林省", ParentID = 1 },
            //    new Node { ID = 4, Name = "上海市", ParentID = 1 },
            //    new Node { ID = 5, Name = "海淀区", ParentID = 2 },
            //    new Node { ID = 6, Name = "朝阳区", ParentID = 2 },
            //    new Node { ID = 7, Name = "大兴区", ParentID = 2 },
            //    new Node { ID = 8, Name = "白山市", ParentID = 3 },
            //    new Node { ID = 9, Name = "长春市", ParentID = 3 },
            //    new Node { ID = 10, Name = "抚松县", ParentID = 8 },
            //    new Node { ID = 11, Name = "靖宇县", ParentID = 8 },
            //    new Node { ID = 12, Name = "甘肃省", ParentID = 1 },
            //    new Node { ID = 13, Name = "张掖市", ParentID = 12 },
            //    new Node { ID = 14, Name = "山丹县", ParentID = 13 },
            //    new Node { ID = 15, Name = "位奇镇", ParentID = 14 },
            //    new Node { ID = 16, Name = "清泉镇", ParentID = 14 },
            //    new Node { ID = 17, Name = "汪庄村", ParentID = 15 },
            //    new Node { ID = 18, Name = "新开村", ParentID = 15 },
            //    new Node { ID = 19, Name = "一社", ParentID = 17 },
            //    new Node { ID = 20, Name = "金昌市", ParentID = 12 }
            //};

            //Nodes = Bind(nodes);



            
        }
        #endregion


    }


    public class Node
    {
        public Node()
        {
            this.Nodes = new List<Node>();
            this.ParentID = -1;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
        public List<Node> Nodes { get; set; }
    }
}
