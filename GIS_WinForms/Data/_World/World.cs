using GIS_WinForms.Data.Math_utils;
using GIS_WinForms.Data.Primitives;
using GIS_WinForms.Services.Algorythm;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data._World
{
    public class World
    {
        private Graph _graph;
        private int _roadWidth;
        private int _roadRoundness;
        private List<Envelope> _envelopes;
        private List<MyPoints> _intersection; // Пересечения полигонов
        private Vertices _vertices;


        public World(Graph graph, int roadWidth = 100, int roadRoundness = 3)
        {
            _graph = graph;
            _roadWidth = roadWidth;
            _roadRoundness = roadRoundness;
            _vertices = new Vertices();
            _envelopes = new List<Envelope>();
            _intersection = new List<MyPoints>();

            Generate();

        }

        public void DrawWorld(PaintEventArgs e)
        {
            foreach (var env in _envelopes)
            {
                env.DrawEnvelope(e);
            }

            foreach (var inter in _intersection)
            {
                _vertices.Draw(e, inter,8);
            }
        }

        public void Generate()
        {
            _envelopes.Clear();

            foreach (var seg in _graph.segments) 
            {
                _envelopes.Add(new Envelope(seg, _roadWidth, _roadRoundness));
            }

            _intersection.Clear();
            //_envelopes[0]._polygon.ConvertListToPoint()
            //ConvertListToPoint(_points);


            //_intersection = Polygon.breakPolygon(_envelopes[0]._polygon,
            //                                     _envelopes[1]._polygon);

            _intersection = Polygon.breakPolygon(_envelopes[0]._polygon,
                                                 _envelopes[1]._polygon);
        }
    }
}
