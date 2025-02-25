using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustTesting.Data
{
    public class Vertex
    {
        public List<MyPoints> points;
        public Vertex()
        {
            points = new List<MyPoints>();
        }


        // Mock метод
        public void TestMethod(MyPoints[] myPoints)
        {
            for (int i = 0; i < myPoints.Length; i++)
            {
                int tmp = myPoints[i].X;
            }
        }

        public void InitList()
        {
            Random random= new Random();
            int rndX,rndY;
            for (int i = 0; i < 50_000; i++) ;
            {
                rndX = random.Next();
                rndY = random.Next();
                points.Add(new MyPoints(rndX, rndY));
            }
        }

        public void MockMethod()
        {
            TestMethod(points.ToArray());
        }
    }
}
