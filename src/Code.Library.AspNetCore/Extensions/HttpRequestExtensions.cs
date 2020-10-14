using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Code.Library.AspNetCore.Extensions
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Return true if HTTP request contain Prefer header with value 'return=representation'
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool HasPreferHeaderWithReturnRepresentation(this HttpRequest request) => request.Headers.TryGetValue("Prefer", out var header) && header.Any(h => IsReturnRepresentation(h));

        private static bool IsReturnRepresentation(string value) => value.Split(';').Any(v => v.Equals("return=representation"));
        
        /// <summary>
        /// get user agent
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetUserAgent(this HttpRequest request)
        {
            return request.Headers["User-Agent"].ToString();
        }

        /// <summary>
        /// get user ip
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static IPAddress GetUserIp(this HttpRequest request)
        {
            if (request.Headers["X-Forwarded-For"].FirstOrDefault() != null)
            {
                return request.Headers["X-Forwarded-For"].FirstOrDefault().GetIpAddresses().FirstOrDefault();
            }

            return request.HttpContext.Connection.RemoteIpAddress.MapToIPv4();
        }
        
         /// <summary>
        /// get ip addresses
        /// </summary>
        /// <param name="ips"></param>
        /// <param name="separator">ips separator(default is ',')</param>
        /// <returns></returns>
        public static List<IPAddress> GetIpAddresses(this string ips, string separator = ",")
        {
            var ipAddresses = new List<IPAddress>();
            var ipList = ips.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            if (ipList.Length == 0)
                return default;

            foreach (var ip in ipList)
            {
                if (IPAddress.TryParse(ip.Trim(), out var ipAddress))
                    ipAddresses.Add(ipAddress);
            }

            return ipAddresses;
        }
    }
}
