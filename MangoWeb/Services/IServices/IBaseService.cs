using MangoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangoWeb.Services.IServices
{
    public interface IBaseService : IDisposable
    {
        public ResponseDto responseObject { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
