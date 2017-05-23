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
        private Mapa mapa;
        private IntPtr[] correr = new IntPtr[10];
        private int contador = 0;
        bool saltar,subiendo,bajando;


        public Personaje()
        {
            ancho = 60;
            alto = 73;

            x = 100;
            y = 400;

            saltar = false;
            subiendo = false;
            
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
                if (y == 400 || subiendo)
                {
                    y -= 12;
                    if (y < 340)
                    {
                        subiendo = false;
                        bajando = true;
                    }
                }
                if(y<340 || (y<400 && bajando) )
                {
                    subiendo = false;
                    y += 6;
                    if (y == 400)
                    {
                        saltar = false;
                        bajando = false;
                    }
                }

            }
            base.MoverA(x, y);

        }

        public bool Morir()
        {
            if (!saltar && !mapa.GetSuelo())
            {
                y += 30;
                return true;
            }
            else return false;
        }

        public void Saltar()
        {
            if (!bajando && !subiendo)
            {
                saltar = true;
                subiendo = true;
            }
            
        }

        public void SetMapa(Mapa mapa)
        {
            this.mapa = mapa;
        }

        /*public override void MoverA(short x, short y)
        {
            base.MoverA(x, y);
            Animar();
        }*/
    }
}
