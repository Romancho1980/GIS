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
        public Vertices start;  // Координаты начала "перетаскивания", т.е. когда надата средняя кнопка мыши
        public Vertices end;    // Координаты конца перетаскивания, т.е. когда отпущена средняя кнопка мыши
        public Vertices offset; // Вычисляем вектор перемещения, т.е. dX и dY
        public bool active;     // Бит - нажата ли средняя кнопка мыши, т.е. происходит ли сейчас перетаскивание

        public Drag()
        {
            start = new Vertices(0, 0);
            end = new Vertices(0, 0);
            offset = new Vertices(0, 0);
            active = false;
        }

        public void ResetDraggingInfo()
        {
            start = new Vertices(0, 0);
            end = new Vertices(0, 0);
            offset = new Vertices(0, 0);
            active = false;
        }

    }
    public class Viewport
    {

        private Drag drag; // Информация о начале перетаскивания, т.е. начальная точка (когда нажата средняя кнопка) и конец - когда отпустили кнопку
        public Vertices Offset { get; set; } // Смещение верхнего левого угла Viewport относительно начала координат, т.е тоже верхнего левого угла
        public CustomPanel panel { get; set; }
        public float zoom { get; set; }//Масштаб
        public Vertices Center { get; set; }

        public Viewport(CustomPanel panel)
        {
            drag = new Drag();
            //Offset = new Vertices(0, 0);//
            Center = new(panel.Width / 2, panel.Height / 2);
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
            panel.MouseDown += HandleMiddleMouseDown;
            panel.MouseMove += HandleMouseMove;
            panel.MouseUp += HandleMiddleMouseUp;
        }

        private void HandleMiddleMouseUp(object? sender, MouseEventArgs e)
        {
            //Debug.WriteLine("Mouse button up");
            if (drag.active == true)
            {
                //drag.setOffset(Utils.Add(drag.end, drag.start).X,
                //                Utils.Add(drag.end, drag.start).Y); // Такой же вопрос и для этого метода

                // drag.active = false;

                // SetOffset();
                Offset = Utils.Add(Offset, drag.offset);
                drag.ResetDraggingInfo();

            }
        }

        private void HandleMouseMove(object? sender, MouseEventArgs e)
        {
            //Debug.WriteLine("Mouse Move");
            //Debug.WriteLine("Offset");
            //Debug.WriteLine($"{Offset.X} : {Offset.Y}");

            // Если происходит "перетаскивание" (dragging) , то запоминаем текущюю позицию мыши
            // И созраняем результат в drag.end
            if (drag.active == true)
            {
                drag.end = this.getMouse(e);
                drag.offset = Utils.Substract(drag.end, drag.start);

                //drag.setEnd(getMouse(e).X, getMouse(e).Y); // Интересно, будет два вызова метода getMouse или один?
                //drag.setOffset(Utils.Substract(drag.end,drag.start).X, 
                //                Utils.Substract(drag.end, drag.start).Y); // Такой же вопрос и для этого метода
            }
        }

        private void HandleMiddleMouseDown(object? sender, MouseEventArgs e)
        {
           // Debug.WriteLine("Mouse button Down");

            // Проверяем нажата ли средняя кнопка мыши
            if (e.Button == MouseButtons.Middle)
            {
                //Debug.WriteLine("Middle Mouse button Down");

                //Vertices MouseScaleCoord =new Vertices();
                //MouseScaleCoord=getMouse(e);
                //drag.setStart(MouseScaleCoord.X,
                //              MouseScaleCoord.Y);

                drag.start = this.getMouse(e);
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
            return Utils.Add(Offset, drag.offset);
            //Vertices tmp=new Vertices();
            //tmp = Utils.Add(Offset, drag.offset);
            //return new Vertices(Utils.Add(Offset, drag.offset).X, 
            //                    Utils.Add(Offset, drag.offset).Y);
        }


        // Перевод положения курсоры мыши 
        public Vertices getMouse(MouseEventArgs evt, bool SubtractDragOffset = false)
        {
            var tmpVert = new Vertices((Int32)((evt.X - Center.point.X) * zoom - Offset.point.X), 
                                       (Int32)((evt.Y - Center.point.Y) * zoom - Offset.point.Y));

            if (SubtractDragOffset == true) // Если перетаскивание включено, то делаем еще вычитание
            {
                return Utils.Substract(tmpVert, drag.offset);
            }

            return tmpVert;


            //return new Vertices(Convert.ToInt32((evt.X - Center.X) * zoom) - Offset.X,
            //                    Convert.ToInt32((evt.Y - Center.Y) * zoom) - Offset.Y);

            //return new Vertices((Int32)((evt.X - Center.X) * zoom - Offset.X), (Int32)((evt.Y - Center.Y) * zoom - Offset.Y));
        }
    }
}
