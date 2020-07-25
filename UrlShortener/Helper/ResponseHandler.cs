using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace UrlShortener.Helper
{
    public class ResponseHandler
    {

        public ResponseHandler() { }

        public ResponseHandler(int code, string message)
        {
            this.Message = message;
            this.StatusCode = code;
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }



        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
