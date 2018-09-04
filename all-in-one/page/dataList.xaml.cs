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
using all_in_one.Model;
using System.Threading;
using Newtonsoft.Json;


namespace all_in_one
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class dataList : UserControl
    {
        private string _userId;
        public string UserId
        {
            get { return _userId ;}
            set{ _userId=value ;}
        }
        
        public dataList()
        {
            InitializeComponent();
            showList(10);
            this.size.Content = _userId;
            
        }
        public void showList(int c)
        {
            List<MailList> list = new List<MailList>();
            receiveList.ItemsSource = list;
            Task t = new Task(new Action(
                () =>
                {
                    for (int i = 0; i < c; i++)
                    {
                        list.Add(new MailList(i + "", "sdf", "sdf", "sdfw"));
                    }
                    showData(list);
                    //Thread.Sleep(500);
                    
                }
                ));
            t.Start();
        }

        private void getData(){

            string rs=HttpHelper.doGet("http://qys.gansu-gw.cn/qysWeb/mode/modeList?", "memberId=1256");
            MailList ml= JsonConvert.DeserializeObject<MailList>(rs);
            
        
        }

        private void showData(List<MailList> list){
            //this.Dispatcher.BeginInvoke(new Action(() => { receiveList.ItemsSource = list; }));
           this.Dispatcher.BeginInvoke(new Action(() => { this.size.Content = this._userId; }));
        }

    }
}
