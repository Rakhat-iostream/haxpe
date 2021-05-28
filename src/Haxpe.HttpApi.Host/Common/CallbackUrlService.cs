using System;
using System.Security.Policy;
using Haxpe.V1.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Haxpe.Common
{
    public class CallbackUrlService: ICallbackUrlService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly LinkGenerator _generator;

        public CallbackUrlService(IHttpContextAccessor accessor, LinkGenerator generator)
        {
            _accessor = accessor;
            _generator = generator;
        }

        public string GetUrl(CallbackUrlModel model, object value)
        {
            var callbackLink = _generator.GetUriByPage(_accessor.HttpContext,
               model.Path, values: value, scheme: model.Scheme);

            return callbackLink;
        }
    }
}
