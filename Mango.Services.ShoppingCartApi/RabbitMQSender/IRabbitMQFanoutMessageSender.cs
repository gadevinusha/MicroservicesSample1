using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.RabbitMQSender
{
    public interface IRabbitMQFanoutMessageSender
    {
        public void SendMessage(BaseMessage message);
    }
}
