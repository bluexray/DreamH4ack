using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DH.Core.Dependency;
using System.Reflection;
using DH.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using DH.Common;



namespace DH.MQ
{
    public class RMQProvider : IProvider
    {
        private  XMLConfig cs;
        private readonly object _loadLock = new object();

        private RabbitMQContext context = new RabbitMQContext();

        public BaseMessage basemessage;


        /// <summary>
        /// 指定处理消息的子类
        /// </summary>
        /// <param name="message"></param>
        public void InitMessage(BaseMessage message)
        {
            basemessage = message;
        }


        public RMQProvider()
        {
        }

        public void OnListening()
        {
            try
            {
                Task.Factory.StartNew(ListenInit);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ListenInit()
        {

            context.ListenConnection =RabbitMQInit.CreateConnection();
            context.ListenConnection.ConnectionShutdown += (v, e) =>
             {
                 //write logs
             };
            context.ListenChannel = RabbitMQInit.CreateChannel(context.ListenConnection);

            if (string.IsNullOrEmpty(RabbitMQInit.QUEUENAME))
            {
                RabbitMQInit.QUEUENAME = context.ListenChannel.QueueDeclare().QueueName;
            }


            //var queueName = context.ListenChannel.QueueDeclare().QueueName;

            context.ListenChannel.QueueDeclare(RabbitMQInit.QUEUENAME, true, false, false, null);

            context.ListenChannel.QueueBind(RabbitMQInit.QUEUENAME, RabbitMQInit.EXCHANGE, RabbitMQInit.RoutingKey);

            var consumer = new EventingBasicConsumer(context.ListenChannel); //事件驱动模型

            consumer.Received += Received;

            context.ListenChannel.BasicQos(0, 1, false); //每个队列一次处理一条消息，公平分发模式
            context.ListenChannel.BasicConsume(RabbitMQInit.QUEUENAME, false, consumer);

        }


        /// <summary>
        /// 接收消息处理消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Received(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                var result = basemessage.BuildMessageResult(e.Body);
  
                if (result==null)
                {
                    //讲未能消费的消息放回队列中
                    context.ListenChannel.BasicReject(e.DeliveryTag, true);
                }
                else if (context.ListenChannel.IsOpen)//需要注意
                {
                    context.ListenChannel.BasicAck(e.DeliveryTag, false);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exChange"></param>
        /// <param name="queue"></param>
        /// <param name="IsDurability"></param>
        public  void SendMessage(BaseMessage message,string exChange,string queue,bool IsDurability=true)
        {
            context.SendConnection = RabbitMQInit.CreateConnection();

            using (context.SendConnection)
            {
                context.SendChannel = RabbitMQInit.CreateChannel(context.SendConnection);


                //binding
                //context.SendChannel.QueueBind(queue, exChange, queue);

                const Byte deliveryMode = 2;//持久化

                using (context.SendChannel)
                {
                    var pop = context.SendChannel.CreateBasicProperties();
                    
                    var msg = SerializationHelper.ToByteArray(message);

                    pop.DeliveryMode = deliveryMode;

                    context.SendChannel.BasicPublish(exChange, queue, pop, msg);
                }

            }
        }
    }
}
