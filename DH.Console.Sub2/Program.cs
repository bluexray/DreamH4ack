using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DH.Core;
using DH.MQ;

namespace DH.Console.Sub2
{
    class Program
    {
        static void Main(string[] args)
        {


           // RMQProvider provider = new RMQProvider(new TextMessage());
            RMQProvider provider = new RMQProvider();

            System.Console.WriteLine("Receive a Message, start to handle  subsciber2");

            provider.OnListening();

            System.Console.ReadKey();

        }
    }
}
