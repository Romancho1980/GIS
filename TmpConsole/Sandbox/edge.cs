using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpConsole.Sandbox
{
    internal class edge:ICloneable
    {

        public int P1 {  get; set; }

        public int P2 {  get; set; }

        public vert P1_refer { get; set; }
        public vert P2_refer { get; set; }

        public vert P1_value { get; set; }
        public vert P2_value { get; set; }


        public edge(edge other):this()
        {
            P1_refer = other.P1_refer;
            P2_refer = other.P2_refer;
        }

        public edge()
        {
            P1_refer = new vert();
            P2_refer = new vert();

            P1_value = new vert();
            P2_value = new vert();
        }
        public edge(vert point1, vert point2): this()
        {
            P1_refer = (vert)point1;//.Clone();
            P2_refer = (vert)point2;//.Clone() ;
        }

        public edge(int index1,int index2):this()
        {
            P1=index1;
            P2=index2;
        }

        public object Clone()
        {
            return new edge{ P1_refer = this.P1_refer, P2_refer = this.P2_refer };
        }

        public void getValue(edge _edge)
        {
            this.P1_refer.X=_edge.P1_refer.X;
            this.P1_refer.Y=_edge.P1_refer.Y;

            this.P2_refer.X=_edge.P2_refer.X;
            this.P2_refer.Y=_edge.P2_refer.Y;


        }
        public override string ToString()
        {
            return $"{P1_refer.ToString()} : {P2_refer.ToString()}";
        }
        public void Print()
        {
            Console.WriteLine(P1_refer.ToString());
            Console.WriteLine(P2_refer.ToString());
        }

    }
}
