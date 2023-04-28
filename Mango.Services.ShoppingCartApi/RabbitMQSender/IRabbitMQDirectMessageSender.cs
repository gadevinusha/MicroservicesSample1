using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.RabbitMQSender
{
    public interface IRabbitMQDirectMessageSender
    {
        public void SendMessage(BaseMessage message);
    }
}
