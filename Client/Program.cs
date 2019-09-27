using System;

namespace Client
{
    class Program
    {
        static int port = 100;
        static string adress = "127.0.0.1";
        
        static void Main(string[] args)
        {
            string message;

            try {
                Client client = new Client(adress, port);
                client.ConnectServer();

                message = client.SendMessage();
                Console.WriteLine("Ответ сервера: " + message);

            } catch (Exception e) {
                Console.WriteLine(e.Message);        
            }
            Console.Read();
        }
    }
}
