using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using Microsoft.Extensions.Hosting;

namespace RabbitMq
{
    public class MessageBus : IEventBus
    {
        //How we can create event bus..We need to have our object as singleton + it needs to have notification for consumers
        IHostApplicationLifetime lifetime;
        public MessageBus()
        {

        }

        public MessageBus(IHostApplicationLifetime _lifetime)
        {
            this.lifetime = _lifetime;
        }
        public void publish(string msg)
        {
            //communicate with RabbitMq server
            msg = "Payment passed";
            var factory = new ConnectionFactory{
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672 //Default port of RabbitMq
            };

            //Creating the socket & within the socket creating the Channel
            using(var connection = factory.CreateConnection()){
                using(var channel = connection.CreateModel())
                {
                    //Creating queue within the channel
                    channel.QueueDeclare("Surinder Channel Queue",false,false,false,null);
                    var byteMsg = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish("","Surinder Channel Queue",body:byteMsg);
                    Console.WriteLine("Message Published:" + byteMsg);
                    

                }
            }
        }

        public void subscribe()//Subscribe will call SubscribeProcess
        {
            lifetime.ApplicationStarted.Register(()=>{
                subscribeProcess();
            });
        }

        public void subscribeProcess()
        {
            var factory = new ConnectionFactory{
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672 //Default port of RabbitMq
            };

            //Creating the socket & within the socket creating the Channel
            using(var connection = factory.CreateConnection()){
                using(var channel = connection.CreateModel())
                {
                    //Creating queue within the channel
                    channel.QueueDeclare("Surinder Channel Queue",false,false,false,null);
                    
                    //Now we have to consume data from this channel
                    var consumer = new EventingBasicConsumer(channel);//Important

                    //Event Hub
                    //Event bus comes under publisher & Subscriber
                    //Postman=Even bus...
                    //RabbitMq is creating communication
                    consumer.Received += ((s, e) =>
                    {
                        var body = e.Body.ToArray();
                        var mess = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Message subscribed {0}", mess);
                    });

                    channel.BasicConsume("Surinder Channel Queue", autoAck:true, consumer:consumer);
                }
            }
        }
    

    
    }
}