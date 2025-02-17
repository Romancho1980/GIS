using GIS_WinForms.Data.Math_utils;
using GIS_WinForms.Data.Primitives;
using GIS_WinForms.ViewsElements;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data._World
{
    public class GraphEditor
    {
        Graph graph;                                // Сам граф
        CustomPanel customPanel;
        Viewport viewport;

        public Vertices? selected { get; set; }     // Выбранная вершина
        public Vertices? hovered { get; set; }      // Курсор мыши наведён на вершину графа

        bool dragging = false;                              // Перетаскиваем ?

        public Vertices MouseCoord { get; set; }

        public GraphEditor(CustomPanel panel,Viewport viewport, Graph graph, int width, int height)
        {
            this.customPanel = panel;
            this.graph = graph;
            this.viewport = viewport;

            selected = null;
            hovered = new();
            MouseCoord = new();

            customPanel.MouseDown += CustomPanel_MouseDown;
            customPanel.MouseMove += CustomPanel_MouseMove;
            customPanel.MouseUp   += CustomPanel_MouseUp;
        }


        // Обработчик события от мыши, когда кнопка отжата
        // т.е dragging - закончился 
        private void CustomPanel_MouseUp(object? sender, MouseEventArgs e)
        {
            dragging = false;
            customPanel.Refresh();
        }


        // Обработчик события мыши, когда она перемещается по экрану
        private void CustomPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            //MouseCoord.Y = e.Y;
            //MouseCoord.X = e.X;

            Vertices tmp_offset= new Vertices();
            tmp_offset = viewport.getOffset();

            //Debug.WriteLine($"{tmp_offset.X} : {tmp_offset.Y}");
            //Debug.WriteLine($"{e.X} : {e.Y}");

            MouseCoord.X = Convert.ToInt32(e.X * viewport.zoom);
            MouseCoord.Y = Convert.ToInt32(e.Y * viewport.zoom);//new                                                                              

            //Debug.WriteLine("Mouse Moved");
            Vertices mouse = new Vertices(e.X, e.Y);
            //Vertices mouse = new Vertices();
            //viewport.getMouse(e);


            Vertices Scaledmouse = new Vertices();
            Scaledmouse = viewport.getMouse(e);
            //Vertices mouse = new Vertices(e.X, e.Y);
            mouse = Scaledmouse;



            hovered = Math_utils.Utils.getNearestPoint(mouse, graph.vertices,900); // 900 - это не настоящее расстояние,
                                                                                   // потому что в методе не извлекается корень
                                                                                   // для увеличения скорости алгоритма
                                                                                   // Гипотенуза^2 = dx^2+dy^2

            // если "перетаскиваем", т.е. нажата кнопка мыши
            // то мы меняем координаты выбранной вершины, на текущие координаты мыши
            if (dragging == true)
            {
                if (selected!=null)
                {
                    selected.X = mouse.X;
                    selected.Y = mouse.Y;
                }
            }
            customPanel.Refresh();
        }


        /// <summary>
        /// Логика при нажатии кнопки мыши
        /// Левая - добавление или перемещение вершины
        /// Правая - удаление вершины и ребер, котоорые связаны с этой вершиной
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            MouseCoord.X = Convert.ToInt32(e.X * viewport.zoom);
            MouseCoord.Y = Convert.ToInt32(e.Y * viewport.zoom) ;

            Debug.WriteLine($"Mouse coodr X{MouseCoord.X} : Y {MouseCoord.Y}");
            Debug.WriteLine("Mouse Down on Panel");

            // if right button clicked
            // И если указатель мыши наведён на вершине, то 
            // Мы удалим вершину (Vertics) из списка вершин
            if (e.Button == MouseButtons.Right) // Правая кнопка
            {
                if (selected != null)
                    selected = null;
                else
                // Удаляем вершину, если нажата правая кнопка мыши, Hover - вершина, на которую наведенена мышь (hovered)
                if (hovered != null)
                {
                    RemoveVectices(hovered); // Удаляем
                }
                //else
                //// А если нажата правая кнопка вне вершины, то "снимаем" выбранную вершину
                //{
                //    selected = null;
                //}
            }

            if (e.Button == MouseButtons.Left) // Левая кнопка
            {
                Vertices Scaledmouse = new Vertices();
                Scaledmouse = viewport.getMouse(e);
                Vertices mouse = new Vertices(e.X,e.Y);
                mouse = Scaledmouse;
              //  hovered = Math_utils.Utils.getNearestPoint(mouse, graph.vertices,20);
                if (hovered != null) 
                    {
                    // соединения выбранных вершин в сегмент
                    if (selected != null)
                    {
                        //Создание сегмента из двух вершин- выбранной вершины (selected) и на которую наведенена мышь (hovered)
                        graph.TryAddSegment(new Segment(selected,hovered));   
                    }
                        selected=hovered;
                        dragging = true;
                        customPanel.Refresh();
                       // Debug.WriteLine($"Selected {selected.X} :{selected.Y}");
                        return;
                    }
              // graph.AddPoint(new Vertices(mouse.X, mouse.Y));

                // Создаём ребро, если существует пред. точка
                    graph.AddPoint(mouse);
                if (selected != null)
                {
                   // graph.AddPoint(new Vertices(mouse.X, mouse.Y));
                    graph.TryAddSegment(new Segment(selected, mouse));
                    //graph.TryAddSegment(new Segment(new Vertices(selected.X,selected.Y), 
                    //                                new Vertices(mouse.X,mouse.Y)));
                }
                selected = mouse;
                hovered = mouse;
            }
//                Debug.WriteLine($"Selected {selected.X} :{selected.Y}");
                customPanel.Refresh();
        }

        private void RemoveVectices(Vertices vert)
        {
            graph.RemoveVectices(vert); // Удалить
            hovered = null;             // Стереть инфу о выбранных и "наведённых" вершинах

            //Если удаляем выбранную вершину, то очищаем инфу
            if (selected==vert)
                selected = null;            //
        }

        //private Vertices getNearestPoint(Vertices mouse, List<Vertices> vertices)
        //{
        //}

        internal void display(PaintEventArgs e)
        {
            graph.Draw(e);
            if (hovered != null)
            {
                hovered.Draw(e, 10, "Red", false);
            }
            if (selected != null)
            {
                Vertices intent = new();

                if (hovered != null)
                    intent.getValue(hovered);
                else
                    intent.getValue(MouseCoord);

                Debug.WriteLine($" viewport.Center {viewport.Center.ToString()}");
                Debug.WriteLine($" viewport.Offset {viewport.Offset.ToString()}");
                Debug.WriteLine($" Intent   {intent.ToString()}");
                Debug.WriteLine($" Selected {selected.ToString()}");
                //intent = Utils.Substract(intent, viewport.Center);
                intent.X *= (Int32) viewport.zoom;
                intent.Y *= (Int32) viewport.zoom;
                Segment tmp = new Segment(selected, intent);
                //tmp = Utils.Substract(tmp, viewport.Center);
                Debug.WriteLine($"{tmp.ToString()}");
                tmp.Draw(e, 2, "black", true); // Рисуем пунктирную линию

                selected.Draw(e,16,"Red",true);
            }
        }
    }
}
