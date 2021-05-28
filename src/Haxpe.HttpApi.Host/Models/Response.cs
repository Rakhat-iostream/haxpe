using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haxpe.Models
{
    public class Response
    {
        public ResponseStatus Status { get; set; }

        public string ErrorCode { get; set; }

        public static Response Ok()
        {
            return new Response
            {
                Status = ResponseStatus.Success
            };
        }

        public static Response Error(string errorCode)
        {
            return new Response
            {
                Status = ResponseStatus.Error,
                ErrorCode = errorCode
            };
        }

    }

    public class Response<T> : Response
    {
        public T Body { get; set; }

        public static Response<T> Ok<T>(T res)
        {
            return new Response<T>
            {
                Status = ResponseStatus.Success,
                Body = res
            };
        }

        public static Response<T> Error<T>(string errorCode)
        {
            return new Response<T>
            {
                Status = ResponseStatus.Error,
                ErrorCode = errorCode
            };
        }
    }
}
