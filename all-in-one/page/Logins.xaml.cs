using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using all_in_one.QRCode;
using System;
using System.Threading.Tasks;

namespace all_in_one
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Logins : UserControl
    {
        private MainWindow _parentWin;
        public MainWindow ParentWindow
        {
            get { return _parentWin; }
            set { _parentWin = value; }
        }
        
        public Logins()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void toQrClick(object sender, RoutedEventArgs e)
        {
            
            App.type = "qrcode";
            changeType(false);
            set_qrBtn_content( "启动扫描器！");
            new Task(new Action(doQrcode)).Start();

           
            changeType(true);
        }

        private void toFingerClick(object sender, RoutedEventArgs e)
        {

        }

        //点击了身份证识别
        private void toPidClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void changeType(bool enable)
        {
            enable = true;
            qrBtn.IsEnabled = enable;
            fingerBtn.IsEnabled = enable;
            faceBtn.IsEnabled = enable;
            pidBtn.IsEnabled = enable;
        }
        
        public void set_qrBtn_content(string msg){
            this.Dispatcher.BeginInvoke(new Action(() => this.qrBtn.Content = msg)); 
        }


        // 启动二维码扫描器 
        public void doQrcode(){
            byte[] rsBytes;
            VbarApi v=new VbarApi();
            if (v.openDevice("127.0.0.1")){
                if (v.Scan(out rsBytes))
                {
                    string userInfo=System.Text.Encoding.Default.GetString(rsBytes);
                    MessageBox.Show(userInfo);
                    long userStmp = Convert.ToInt64(userInfo.Substring(0, 8));

                    App.userId = userInfo.Substring(8);

                    DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
                    long timeStmp = (long)(DateTime.Now.AddSeconds(10) - startTime).TotalSeconds; // 相差秒数
                    //设置10 s 过期时间
                    if (userStmp + 10 < timeStmp)
                    {
                        //二维码失效了 ，提示用户刷新二维码

                    }
                    else
                    { 
                        //跳转到列表页面
                        set_qrBtn_content( "跳转到用户信息列表！");
                    }
                }
                else
                {
                    set_qrBtn_content("扫码失败！");
                }
            }
            else
            {
                set_qrBtn_content("设备初始化失败！");
            }
            _parentWin.ToDataList("123456");

        }


    }
}
