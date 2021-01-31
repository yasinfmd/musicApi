using MusicApp.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;

namespace RabbitMQTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQService _rabbitMQService = new RabbitMQService();
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                //kanal olusturma
                using (var channel = connection.CreateModel())
                {
                    //kuyruk olusturma 
                    channel.QueueDeclare("test", false, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume("test", true, consumer);
                    consumer.Received += (s, e) =>
                    {
                        Thread.Sleep(5000);
                        Console.WriteLine("test123123");
                    };
                    Console.ReadLine();


                    //rabbitmq gönderme
                }
                Console.Read();
                Console.WriteLine("Hello World!");
            }
        }
    }
}
