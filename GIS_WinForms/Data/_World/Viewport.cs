using GIS_WinForms.Data.Math_utils;
using GIS_WinForms.Data.Primitives;
using GIS_WinForms.ViewsElements;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GIS_WinForms.Data._World
{
    public struct Drag
    {
        public Vertices start;
        public Vertices end;
        public Vertices offset;
        public bool active;

        public Drag()
        {
            start = new Vertices(0, 0);
            end = new Vertices(0, 0);
            offset = new Vertices(0, 0);
            active = false;
        }

        public void setStart(int x,int y)
        {
            start.X = x;
            start.Y = y;
        }

        public void setEnd(int x,int y)
        {
            end.X = x;
            end.Y = y;
        }
        public void setOffset(int x,int y)
        {
            offset.X = x;
            offset.Y = y;
        }

        public void Reset()
        {
            start = new Vertices(0, 0);
            end = new Vertices(0, 0);
            offset = new Vertices(0, 0);
            active = false;
        }

    }
    public class Viewport
    {

        public Drag drag;
        public Vertices Offset; // Смещение относительно начала координат
        public CustomPanel panel { get; set; }
        public float zoom { get; set; }
        public Vertices Center { get; set; }

        public Viewport(CustomPanel panel)
        {
            drag = new Drag();
            //Offset = new Vertices(0, 0);//
            Center = new(panel.Width/2,panel.Height/2);
            Offset = Utils.Scale(Center, -1.0F);
            this.panel = panel;
            this.zoom = 1;

            addEventListener();
        }

        private void addEventListener()
        {
            // Обработка только для "перетаскивания" и масштабирования, т.е. если нажата средняя кнопка мыши.

            panel.MouseWheel += HandleMouseWheel; // Масштабирование

            // ************* Перетаскивание ***************
            panel.MouseDown += HandleMouseDown;
            panel.MouseMove += HandleMouseMove;
            panel.MouseUp += HandleMouseUp;
        }

        private void SetOffset()
        {
            Vertices tmpOffset = new();
            tmpOffset = Utils.Add(Offset, drag.offset);// Из-за того что ссылочные типы, приходится выполнять такие
                                                       // приседания;

            Offset.X = tmpOffset.X;
            Offset.Y = tmpOffset.Y;

        }
        private void HandleMouseUp(object? sender, MouseEventArgs e)
        {
            //Debug.WriteLine("Mouse button up");
            if (drag.active == true)
            {
                //drag.setOffset(Utils.Add(drag.end, drag.start).X,
                //                Utils.Add(drag.end, drag.start).Y); // Такой же вопрос и для этого метода

                // drag.active = false;

                SetOffset();

                drag.Reset();

            }
        }

        private void HandleMouseMove(object? sender, MouseEventArgs e)
        {
            //Debug.WriteLine("Mouse Move");
            Debug.WriteLine("Offset");
            Debug.WriteLine($"{Offset.X} : {Offset.Y}");

            // Если происходит "перетаскивание" (dragging) , то запоминаем текущюю позицию мыши
            // И созраняем результат в drag.end
            if (drag.active == true)
            {
                drag.setEnd(getMouse(e).X, getMouse(e).Y); // Интересно, будет два вызова метода getMouse или один?
                drag.setOffset(Utils.Substract(drag.end,drag.start).X, 
                                Utils.Substract(drag.end, drag.start).Y); // Такой же вопрос и для этого метода
            }
        }

        private void HandleMouseDown(object? sender, MouseEventArgs e)
        {
            Debug.WriteLine("Mouse button Down");

            // Проверяем нажата ли средняя кнопка мыши
            if (e.Button == MouseButtons.Middle)
            {
                Debug.WriteLine("Middle Mouse button Down");

                Vertices MouseScaleCoord =new Vertices();
                MouseScaleCoord=getMouse(e);
                drag.setStart(MouseScaleCoord.X,
                              MouseScaleCoord.Y);

                drag.active = true;
            }
        }

        private void HandleMouseWheel(object? sender, MouseEventArgs e)
        {
            var direction=Math.Sign(e.Delta);
            var step = 0.1F;
            zoom += direction * step;
            zoom = Math.Max(1,Math.Min(5,zoom));
            panel.Refresh();
        }

        public Vertices getOffset()
        {
            //Vertices tmp=new Vertices();
            //tmp = Utils.Add(Offset, drag.offset);
            return new Vertices(Utils.Add(Offset, drag.offset).X, 
                                Utils.Add(Offset, drag.offset).Y);
        }


        // Перевод положения курсоры мыши , 
        public Vertices getMouse(MouseEventArgs evt)
        {
            //return new Vertices(Convert.ToInt32((evt.X - Center.X) * zoom) - Offset.X,
            //                    Convert.ToInt32((evt.Y - Center.Y) * zoom) - Offset.Y);

            return new Vertices((Int32)(evt.X * zoom), (Int32)(evt.Y * zoom));
        }
    }
}
