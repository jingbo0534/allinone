using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;

namespace MyWebSocketDemo
{
    class Server1
    {

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
                    


                return msg;
            
            }
    }
}
