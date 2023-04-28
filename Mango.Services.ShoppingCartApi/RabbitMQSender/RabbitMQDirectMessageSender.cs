using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.RabbitMQSender
{
    public class RabbitMQDirectMessageSender : IRabbitMQDirectMessageSender
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;
        private readonly string Exchange = "DirectExchange";

        public RabbitMQDirectMessageSender()
        {
            _hostname = "localhost";// "100.81.164.155";// "localhost";
            _password = "guest";
            _username = "guest";
        }
        public void SendMessage(BaseMessage message)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.ExchangeDeclare(Exchange, ExchangeType.Fanout, true, false, null);
                //channel.QueueDeclare("queue1", true, false, false, arguments: null);
                //channel.QueueDeclare("queue2", true, false, false, arguments: null);
                //channel.QueueBind("queue1", Exchange, "", null);
                //channel.QueueBind("queue2", Exchange, "", null);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: Exchange, "", basicProperties: null, body: body);
                //channel.BasicPublish(exchange: "", "", basicProperties: null, body: body);
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
                     Port = 5672,
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
