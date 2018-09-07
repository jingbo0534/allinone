using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;
using TaxCardX;
using System.Windows;

namespace MyWebSocketDemo
{
    class Server1
    {
        private static GoldTax GT = new GoldTax();
        private static List<IWebSocketConnection> allSockets;
        private static WebSocketServer server;
        
        public  void startWsFleck() 
            {
                allSockets = new List<IWebSocketConnection>();
                server = new WebSocketServer("ws://0.0.0.0:1818");
                server.Start(socket =>
                {
                    socket.OnOpen = () =>
                    {
                        Console.WriteLine("Open!");
                        allSockets.Add(socket);
                    };
                    socket.OnClose = () =>
                    {
                        Console.WriteLine("Close!");
                        allSockets.Remove(socket);
                    };
                    socket.OnMessage = message =>
                    {
                        Console.WriteLine(message);
                        String msg = doService(message);
                        socket.Send(msg);
                    };
                });
            }
            
            public  void close(){
             
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send("socketClose");
                }
                server.Dispose();
            }

            //服务判断
            public  string doService(string msg){
                    invok(msg);


                return msg;
            
            }

            public void invok(string msg)
            {
                String GS = GT.RetCode.ToString();
                if (GS.Equals("1011"))
                {
                    GT.InvInfoInit();//初始化
                    GT.InfoKind = 2;
                    GT.SellerAddress = "2";
                    // GT.InfoTypeCode = "6500143320"; //发票类别
                    GT.GetInfo();


                    //textBox2.Text = GT.InfoTypeCode;

                    //textBox1.Text = GT.InfoNumber.ToString();

                    GT.InfoClientName = "开票企业名称";  //购方名称
                    GT.InfoClientTaxCode = "650105999990003"; //购方税号
                    GT.InfoClientBankAccount = "246234524";//购方开户账号
                    GT.InfoClientAddressPhone = "新疆";//购方地址
                    //GT.InfoSellerBankAccount = ""; //销方开户行
                    GT.InfoSellerAddressPhone = "";//销方地址
                    GT.InfoTaxRate = 3; //税率
                    GT.InfoNotes = "新A79972";  //备注
                    GT.InfoInvoicer = "admin";//开票人
                    GT.InfoChecker = "崔杰";  //复核人
                    GT.InfoCashier = "崔浩";//收款人
                    //GT.InfoListName = "详见销货清单";
                    //明细数据\
                    GT.ClearInvList();

                    GT.InvListInit();//初始化明细

                    //if ("是否含税" == "含税")
                    //    {
                    //        GT.ListPriceKind = 1;
                    //    }
                    //    else
                    //    {
                    //        GT.ListPriceKind = 0;
                    //    }
                    GT.ListPriceKind = 1;

                    GT.ListGoodsName = "细绒棉";//名称
                    GT.ListStandard = "3128";//规格
                    GT.ListUnit = "KG";//规格

                    GT.ListNumber = 2;//数量
                    GT.ListPrice = 100;//单价
                    GT.ListAmount = 200;//金额


                    GT.AddInvList();

                    GT.Invoice();//发票开具

                    //textBox3.Text = GT.InfoNumber.ToString();

                    if (GT.RetCode.ToString() == "4001")// 传入发票数据不合法
                    {
                       //"传入发票数据不合法", "信息提示";
                    }
                    else if (GT.RetCode.ToString() == "4002")//开票前金税卡状态错误
                    {
                       // MessageBox.Show("开票前金税卡状态错误", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //保存发票ToolStripMenuItem.Enabled = false;
                    }
                    else if (GT.RetCode.ToString() == "4003")//金税卡开票调用错误 
                    {
                      //  MessageBox.Show("金税卡开票调用错误,请确认组件版本号！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //保存发票ToolStripMenuItem.Enabled = false;
                    }
                    else if (GT.RetCode.ToString() == "4004")//开票后金税卡状态错误
                    {
                       // MessageBox.Show("开票后金税卡状态错误", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //保存发票ToolStripMenuItem.Enabled = false;
                    }
                    else if (GT.RetCode.ToString() == "4011")//开票成功
                    {

                        GT.GoodsListFlag = 0;
                        GT.PrintInv();

                        if (GT.RetCode == 5001)
                        {
                          //  MessageBox.Show("发票打印失败，失败原因：未找到发票，请先保存发票", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (GT.RetCode == 5011)
                        {
                          //  MessageBox.Show("发票打印成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //if (GetFPStyle == "发票")
                            //{
                            //    toolStripButton2_Click(null, null);
                            //}
                        }
                        else if (GT.RetCode == 5012)
                        {
                           // MessageBox.Show("发票打印失败，失败原因：未打印", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (GT.RetCode == 5013)
                        {
                           // MessageBox.Show("发票打印失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

            }        
                   
                

            

    }
}
