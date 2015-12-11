using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DH.Core;
using DH.MQ;



namespace DH.Console.Sub
{
    class Program
    {

        public static string str = "";
        static void Main(string[] args)
        {
           

            //RMQProvider provider = new RMQProvider(new TextMessage());

            RMQProvider provider = new RMQProvider();


            System.Console.WriteLine("Receive a Message, start to handle subsciber1");

            provider.OnListening();

            System.Console.ReadKey();

            //provider.Connected();

            //using (var bus =provider.CreateBus())
            //{
            //    bus.Subscribe<TextMessage>("test", HandleTextMessage2);

            //    //bus.Subscribe<dynamic>("test1", x => { str = x;});

             
            //    System.Console.WriteLine("Got message: str"+ str);

            //    System.Console.WriteLine("Listening for messages. Hit <return> to quit.");
            //    System.Console.ReadLine();
            //}
            
        }


        static void HandleTextMessage(TextMessage textMessage)
        {

            str = textMessage.Text;
            
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Got message: {0}", str);
            System.Console.ResetColor();
        }


        static void HandleTextMessage2(dynamic textMessage)
        {

            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Got message: {0}", textMessage.Text);
            System.Console.ResetColor();
        }


        public void Test()
        {
               
        }

    }
}
