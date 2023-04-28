using Mango.Services.ShoppingCartApi.Models.Dto;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.RabbitMQConsumer
{
    public class RabbitMQDirectConsumer: BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly string Exchange = "DirectExchange";
        public RabbitMQDirectConsumer()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",//"100.81.164.155",//"localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672

        };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(Exchange, ExchangeType.Fanout, true, false, null);
            _channel.QueueDeclare(queue: "direct1", false, false, false, arguments: null);
            _channel.QueueDeclare(queue: "direct2", false, false, false, arguments: null);
            _channel.QueueBind("direct1", Exchange, "routekey1");
            _channel.QueueBind("direct2", Exchange, "routekey2");
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(content);
                HandleMessage(cartDto).GetAwaiter().GetResult();

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume("direct1", false, consumer);
            _channel.BasicConsume("direct2", false, consumer);

            return Task.CompletedTask;
            throw new NotImplementedException();
        }
        private async Task HandleMessage(CartDto cartDto)
        {

            Console.WriteLine(cartDto.Id);
            Console.WriteLine(cartDto.CartDetails.FirstOrDefault().CartDetailsId);
            Console.WriteLine(cartDto.CartDetails.FirstOrDefault().CartHeader);
            Console.WriteLine(cartDto.CartDetails.FirstOrDefault().Product.CategoryName);
            Console.WriteLine(cartDto.CartDetails.FirstOrDefault().Product.Name);
            Console.WriteLine(cartDto.CartDetails.FirstOrDefault().Product.Description);
            //return Task.CompletedTask;
        }
    }
}
