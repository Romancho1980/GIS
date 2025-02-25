using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpConsole.Sandbox
{
    internal class Viewport
    {
        public vert Mouse { get; set; }

        public Viewport()
        {
            Mouse = new vert();
            Mouse.X = 100;
            Mouse.Y = 100;
        }
        public override string ToString()
        {
            return $"\t mouse.x = {Mouse.X} : mouse.y = {Mouse.Y}";
        }

        public void Init(int x, int y)
        {
            vert tmpMouse= new vert();
            tmpMouse.X = x;
            tmpMouse.Y = y;

           // Mouse= tmpMouse.Copy();
            Mouse= tmpMouse;

        }

        public void Init1()
        {
            vert tmpMouse= new vert();
            tmpMouse.X = 666;
            tmpMouse.Y = 6666;
            Mouse= tmpMouse;
        }
    }
}
