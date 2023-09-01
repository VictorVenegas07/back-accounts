using System.Net.Sockets;
using System.Net;
namespace Persistence.Helpers
{
    public static class IpHelpers
    {
        public static string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return String.Empty;
        }
    }
}
