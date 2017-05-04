using System;
using Tao.Sdl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    class Fondo:Sprite
    {

        public Fondo()
        {
            ancho = 2000;
            alto = 600;
            imagen = SdlImage.IMG_Load("img/BG.png");
            x = -600;
            y = 0;
        }

        //TODO Animar el fondo y unir el principio con el final de la imagen

        
    }
}
