using System;
using Tao.Sdl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    class Bonus:Sprite
    {
        public Bonus()
        {
            imagen = SdlImage.IMG_Load("img/object1.png");
            ancho = 128;
            alto = 128;
            x = 800;
            y = 330;
        }

        public override void Desplazar(short x)
        {
            this.x = x;
        }
    }

}
