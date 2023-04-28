using Mango.Services.ShoppingCartApi.Models.Dto;
using Mango.Services.ShoppingCartApi.RabbitMQSender;
using Mango.Services.ShoppingCartApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.Controllers
{
    [Route("api/cart")]

    [ApiController]
    public class CartApiController : Controller
    {
        private readonly ICartRepository _cartRepository;
        protected ResponseDto _response;
        private readonly IRabbitMQCartMessageSender _rabbitMQCartMessageSender;
        private readonly IRabbitMQFanoutMessageSender _rabbitMQFanoutMessageSender;
        private readonly IRabbitMQDirectMessageSender _rabbitMQDirectMessageSender;
        public CartApiController(ICartRepository cartRepository, IRabbitMQCartMessageSender rabbitMQCartMessageSender, 
            IRabbitMQFanoutMessageSender rabbitMQFanoutMessageSender, IRabbitMQDirectMessageSender rabbitMQDirectMessageSender)
        {
            _cartRepository = cartRepository;
            _response = new ResponseDto();
            _rabbitMQCartMessageSender = rabbitMQCartMessageSender;
            _rabbitMQFanoutMessageSender = rabbitMQFanoutMessageSender;
            _rabbitMQDirectMessageSender = rabbitMQDirectMessageSender;
        }
        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                var cartDto=await _cartRepository.GetCartByUserId(userId);
                _response.Result = cartDto;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto cartDto)
        {
            try
            {
                CartDto cartDt = await _cartRepository.CreateUpdateCart(cartDto);
                //_rabbitMQCartMessageSender.SendMessage(cartDto, "shoppingcartqueue");
                //_rabbitMQFanoutMessageSender.SendMessage(cartDto);
                _rabbitMQDirectMessageSender.SendMessage(cartDto);
                _response.Result = cartDt;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            try
            {
                CartDto cartDt = await _cartRepository.CreateUpdateCart(cartDto);
                _response.Result = cartDt;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody] int cartId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveFromCart(cartId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
