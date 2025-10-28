using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.LoginApp.Entities
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public ServiceResponse(string message, bool success = false, T? data = default)
        {
            Message = message;
            Data = data;
            Success = success;
        }
    }
}
