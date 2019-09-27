using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server{
    public class Server{
        int port = 100;
        IPEndPoint iPEndPoint;
        Socket listenSocket;
        string ipAdress;
        string File = @"C:\Users\Сергей\Documents\Учеба\ИрГУПС\Курганская\Конструирование ПО\KPO_laba3_4\Server\shekspir.txt";
        public Server(string ip_add, int p){
            port = p;
            ipAdress = ip_add;
            iPEndPoint = new IPEndPoint(IPAddress.Parse(ipAdress), port);
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void LaunchServer(){
            listenSocket.Bind(iPEndPoint);
            listenSocket.Listen(10);
            Console.WriteLine("Сервер запущен. Ожидание подключений...");
        }

        public void ClientListen(){
            while (true)
            {
                Socket handler = listenSocket.Accept();
                string text = "";
                string [] sonets; 
                byte[] data = new byte[1024];
                if (System.IO.File.Exists(File)) {
                    StreamReader sr = new StreamReader(File, Encoding.Default);
                    try {
                        using (sr){
                            text = sr.ReadToEnd();
                            sonets = text.Split(new char[] { '*' }, StringSplitOptions.None);
                            Random rnd = new Random();
                            int i = rnd.Next(0, sonets.Length);
                            text = sonets[i];
                        }
                    } catch (Exception e) {
                        Console.WriteLine(e.Message);
                    } finally {
                        try {
                            sr.Close();
                        } catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                        }            
                    }
                } else {
                    Console.WriteLine("Файл не существует!");
                }
                data = Encoding.Unicode.GetBytes(text);
                handler.Send(data);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
    }
}