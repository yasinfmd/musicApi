using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.RabbitMQ
{
    public class Publisher
    {
     
            private readonly RabbitMQService _rabbitMQService;

            public Publisher(string queueName, string message)
            {
                _rabbitMQService = new RabbitMQService();

                using (var connection = _rabbitMQService.GetRabbitMQConnection())
                {
                //kanal olusturma
                    using (var channel = connection.CreateModel())
                    {
                    //kuyruk olusturma 
                        channel.QueueDeclare(queueName,false, false, false, null);
                            //rabbitmq gönderme
                        channel.BasicPublish("", queueName, body: Encoding.UTF8.GetBytes(message));

                        Console.WriteLine("{0} queue'su üzerine, \"{1}\" mesajı yazıldı.", queueName, message);
                    }
                }
            }

    }
}
