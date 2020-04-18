﻿using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BusServiceApp.EF.Models;
using Microsoft.Azure.ServiceBus;

namespace BusServiceApp.SenderApp
{
    class Program
    {
        private static ApplicationContext db = new ApplicationContext();

        const string ServiceBusConnectionString = "Endpoint=sb://userbusservice.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DoNJnlRCEO3tLKQUtMkeyf9a5DQRpZP0Z5b4jf+rF3Q=";
        const string QueueName = "userqueue";
        static IQueueClient queueClient;
        public static async Task Main(string[] args)
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            // Send messages.
            await SendMessagesAsync();

            Console.ReadKey();

            await queueClient.CloseAsync();
        }

        static async Task SendMessagesAsync()
        {
            var users = db.Users.ToList();
            try
            {
                foreach (var u in users)
                {
                    var json = JsonSerializer.Serialize(u);
                    var message = new Message(Encoding.UTF8.GetBytes(json));
                    await queueClient.SendAsync(message);
                    Console.WriteLine(json);
                    Console.WriteLine("Done!");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}