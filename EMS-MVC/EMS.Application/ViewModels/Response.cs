using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.ViewModels
{
    public class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public List<string> Messages { get; set; } = new List<string>();
        public bool Success { get; set; }
        public T Content { get; set; }

    }
}
