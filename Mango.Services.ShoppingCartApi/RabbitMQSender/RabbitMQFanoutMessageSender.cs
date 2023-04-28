using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.RabbitMQSender
{
    public class RabbitMQFanoutMessageSender : IRabbitMQFanoutMessageSender
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;
        private readonly string Exchange = "DirectExchange";

        public RabbitMQFanoutMessageSender()
        {
            _hostname = "localhost";// "100.81.164.155"; //"localhost";
            _password = "guest";
            _username = "guest";
        }
        public void SendMessage(BaseMessage message)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.ExchangeDeclare(Exchange, ExchangeType.Fanout, true, false, null);
                channel.QueueDeclare("direct1", true, false, false, arguments: null);
                channel.QueueDeclare("direct2", true, false, false, arguments: null);
                channel.QueueBind("direct1", Exchange, "routekey1", null);
                channel.QueueBind("direct2", Exchange, "routekey2", null);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: Exchange, "routekey1", basicProperties: null, body: body);
                channel.BasicPublish(exchange: Exchange, "routekey2", basicProperties: null, body: body);
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password,
                    Port = 5672

            };
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                //log exception
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }
            CreateConnection();
            return _connection != null;
        }
    }
}
