
using TmpConsole.Sandbox;
using TmpConsole.Services.Telnet;
using TmpConsole.Services.TelnetClient;
using TmpConsole.TmpServices;
using WinForm_ForTests.Data;

namespace TmpConsole
{
    internal class Program
    {
        public static void CallGraph()
        {
            graph gr = new graph();

            gr.PrintVert();
            gr.PrintEdges();

            gr.point[0].X = 1000;
            gr.PrintEdges();

            // var ed = new edge(gr.edges[0]);
            edge ed = new edge();
            //ed.P1_refer.X = gr.edges[0].P1_refer.X;
            //ed.P1_refer.Y = gr.edges[0].P1_refer.Y;

            //ed = (edge)gr.edges[0].Clone();
            ed.getValue(gr.edges[0]);

            ed.P1_refer.X = 8826;
            ed.P1_refer.Y = 886;
            gr.PrintEdges();
        }
        public static void CallTemplate()
        {
            var el1 = new Element(0, 0);
            var el2 = new Element(100, 100);
            var el3 = new Element(200, 200);

            DataTemplate dT1 = new DataTemplate();
            DataTemplate dT2 = new DataTemplate();
            var t1 = dT1.GetData_1(el2);
            el2 = el3;
            var t2 = dT2.GetData_1(el2);

            Console.WriteLine($"T1 : X = {t1.X} Y = {t1.Y}");
            Console.WriteLine($"T2 : X = {t2.X} Y = {t2.Y}");
        }

        public static void Main(string[] args)
        {
            //  CallGraph();
            CallTemplate();
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
