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
using System.Windows.Interop;
using MahApps.Metro.Controls;
using System.Threading;

namespace all_in_one
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var logins = new Logins();
            logins.ParentWindow = this;
            mainContent.Children.Add(logins);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

        }

        public void ToDataList(string userId){
            this.Dispatcher.BeginInvoke(new Action(
                () =>
                {
                    var listpage = new dataList();
                    mainContent.Children.Add(listpage);
                    listpage.UserId = userId;

                    Thread.Sleep(1000);
                    mainContent.Children.Remove(listpage);
                }
                ));
            
        }

    }
}
