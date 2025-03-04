using GIS_WinForms.Data.Math_utils;
using GIS_WinForms.Data.Primitives;
using GIS_WinForms.Data.Primitives.AUX_Classes;

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

        private List<Segment> roadBorders;

        public World(Graph graph, int roadWidth = 100, int roadRoundness = 10)
        {
            _graph = graph;
            _roadWidth = roadWidth;
            _roadRoundness = roadRoundness;
            _vertices = new Vertices();
            _envelopes = new List<Envelope>();
            _intersection = new List<MyPoints>();
            roadBorders = new List<Segment>();

            Generate();

        }

        public void DrawWorld(PaintEventArgs e)
        {
            PolyOptions options = new PolyOptions();
            foreach (var env in _envelopes)
            {
                int col = 200;
                options.Fill = Color.FromArgb(255, col, col, col); // BBB - > fill
                options.Stroke = "#BBB";
                options.LineWidth = 15;
                env.DrawEnvelope(e, options);
            }

            foreach (var seg in roadBorders)
            {
                //  seg.Draw(e, 4, "white");
            }
            //foreach (var inter in _intersection)
            //{
            //    _vertices.Draw(e, inter,8);
            //}
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

            //_intersection = Polygon.breakPolygon(_envelopes[0]._polygon,
            //                                     _envelopes[1]._polygon);


            // Polygon.multiBreak(_envelopes.Select(e => e._polygon).ToList());
            this.roadBorders = Polygon.Union(_envelopes.Select(e => e._polygon).ToList());
        }
    }
}
