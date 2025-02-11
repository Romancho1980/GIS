
using TmpConsole.Sandbox;
using TmpConsole.Services.Telnet;
using TmpConsole.Services.TelnetClient;
using TmpConsole.TmpServices;

namespace TmpConsole
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            graph gr=new graph();

            gr.PrintVert();
            gr.PrintEdges();

            gr.point[0].X = 1000;
            gr.PrintEdges();

           // var ed = new edge(gr.edges[0]);
           edge ed=new edge();
            //ed.P1_refer.X = gr.edges[0].P1_refer.X;
            //ed.P1_refer.Y = gr.edges[0].P1_refer.Y;

            //ed = (edge)gr.edges[0].Clone();
            ed.getValue(gr.edges[0]);

            ed.P1_refer.X = 8826 ;
            ed.P1_refer.Y = 886 ;
            gr.PrintEdges();

        }

        //public static async Task Main(string[] args)
        //{
        //    tmpSegment tmp=new tmpSegment();

        //    return;
        //    TelnetServer server = new TelnetServer();
        //    try
        //    {
        //        await server.StartAsync("127.0.0.2", 23);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Server error" + ex.Message);
        //    }
        //    finally
        //    {
        //        server.Stop();
        //    }
        //}
    }
}
