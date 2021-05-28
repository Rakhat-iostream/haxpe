using System;
namespace Haxpe.V1.Common
{
    public interface ICallbackUrlService
    {
        string GetUrl(CallbackUrlModel model, object value);
    }
}
