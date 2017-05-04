using System;
using Tao.Sdl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    class Personaje:Sprite
    {
        private IntPtr[] correr = new IntPtr[10];
        private int contador = 0;
        bool saltar;

        public Personaje()
        {
            ancho = 60;
            alto = 73;

            x = 100;
            y = 400;

            saltar = false;
            
            for(int i = 1; i < 11; i++)
            {
                correr[i - 1] = SdlImage.IMG_Load("img/Walk-"+i+".png");
            }

            imagen = correr[contador];
            contador++;
        }

        public void Animar()
        {
            imagen = correr[contador];
            if (contador == 9)
            {
                contador = 0;
            }
            else
            {
                contador++;
            }

            if (saltar)
            {
                if (y == 400)
                {
                    y -= 20;
                }
                else if(y<400)
                {
                    y += 2;
                    if (y == 400) saltar = false;
                }

            }
            base.MoverA(x, y);

        }

        public void Saltar()
        {
            saltar = true;
            
        }

        /*public override void MoverA(short x, short y)
        {
            base.MoverA(x, y);
            Animar();
        }*/
    }
}
