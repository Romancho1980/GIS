
using TmpConsole.Services.Telnet;
using TmpConsole.Services.TelnetClient;
using TmpConsole.TmpServices;

namespace TmpConsole
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            tmpSegment tmp=new tmpSegment();

            return;
            TelnetServer server = new TelnetServer();
            try
            {
                await server.StartAsync("127.0.0.2", 23);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Server error" + ex.Message);
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
