using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BusServiceApp.ReceiverMvcApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;

namespace BusServiceApp.ReceiverMvcApp
{
    public class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://userbusservice.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DoNJnlRCEO3tLKQUtMkeyf9a5DQRpZP0Z5b4jf+rF3Q=";
        const string QueueName = "userqueue";
        static IQueueClient queueClient;
        private  static readonly UserContext userContext = new UserContext();

        public static void Main(string[] args)
        {
            (new Thread(() => { CreateHostBuilder(args).Build().Run(); })).Start();
            
            MainAsync().GetAwaiter().GetResult();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        static async Task MainAsync()
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);


            RegisterOnMessageHandlerAndReceiveMessages();

            Console.ReadKey();

            await queueClient.CloseAsync();
        }

        static void RegisterOnMessageHandlerAndReceiveMessages()
        {

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,

                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            string textmessage = Encoding.UTF8.GetString(message.Body);
            User _user = JsonSerializer.Deserialize<User>(textmessage);
            userContext.Add(_user);
            userContext.SaveChanges();

            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            return Task.CompletedTask;
        }
    }

}
