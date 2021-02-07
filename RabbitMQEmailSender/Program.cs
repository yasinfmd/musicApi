using Microsoft.Extensions.Configuration;
using MusicApp.Entity.ParameterModels;
using MusicApp.Helper.Abstract;
using MusicApp.Helper.Concrate;
using MusicApp.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace RabbitMQEmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            async void SendMail(string email,string subject,string message) 
            {
                SendGridMailService sendGridMailService = new SendGridMailService();
                await sendGridMailService.SendEmail(email, subject, message);
            }
            try
            {
                RabbitMQService _rabbitMQService = new RabbitMQService();
                using (var connection = _rabbitMQService.GetRabbitMQConnection())
                {
                        //kanal olusturma
                        using (var channel = connection.CreateModel())
                        {
                            //kuyruk olusturma 
                            channel.QueueDeclare("mailQueque", false, false, false, null);
                          var consumer = new EventingBasicConsumer(channel);
                        channel.BasicConsume("mailQueque", true, consumer);
                        consumer.Received += (s, e) =>
                        {
                            string serializeJson=Encoding.UTF8.GetString(e.Body.Span);
                            UserEmailModel user= JsonSerializer.Deserialize<UserEmailModel>(serializeJson);
                            SendMail(user.Email, user.Title, user.Message);
                            Console.WriteLine($"Email Başarıyla {user.Email} gönderildi");
                        };
                        Console.ReadLine();


                        //rabbitmq gönderme
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Hata Oldu {e.Message}");
            }
    

            Console.Read();
        }
    }
}
