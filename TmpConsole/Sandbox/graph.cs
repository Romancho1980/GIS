using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpConsole.Sandbox
{
    internal class graph
    {
        public List<edge> edges=new List<edge>();
        public List<vert> point=new List<vert>();
        public graph()
        {
            point.Add(new vert(0, 0));
            point.Add(new vert(10, 10));
            point.Add(new vert(20, 20));
            point.Add(new vert(30, 10));

            edges.Add(new edge(point[0], point[1]));
            edges.Add(new edge(point[1], point[2]));
            edges.Add(new edge(point[2], point[3]));
            edges.Add(new edge(point[3], point[0]));
            edges.Add(new edge(point[1], point[3]));

        }


        public void PrintEdges()
        {
            Console.WriteLine("Segments .....");
            for (int i = 0; i < edges.Count; i++)
            {
                Console.WriteLine($"\tSegment Index:{i}");
                edges[i].Print();

            }
        }
        public void PrintVert()
        {
            Console.WriteLine("Points .....");
            foreach (vert v in point)
            {
                Console.WriteLine($"\tX= {v.X}: Y= {v.Y}");
            }
        }
    }
}
