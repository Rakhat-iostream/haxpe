using System;
using System.Threading.Tasks;

namespace Haxpe.V1.Google
{
    public interface IGoogleProvider
    {
        Task<GoogleCustomerInfo> GetCustomerInfo(GoogleCredentials credentials);
    }
}
