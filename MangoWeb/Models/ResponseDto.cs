using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangoWeb.Models
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage { set; get; } = "";
        public List<string> ErrorMessages { set; get; }
    }
}
