using System;
using Tao.Sdl;


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
            ancho = 43;
            alto = 67;

            x = 100;
            y = 406;

            saltar = false;
            subiendo = false;
            
            for(int i = 1; i < 11; i++)
            {
                correr[i - 1] = SdlImage.IMG_Load("img/Walk-"+i+".png");
            }

            imagen = correr[contador];
            contador++;
        }

        /**
         * Animación y control del salto del personaje
         * */
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
                if (y == 406 || subiendo)
                {
                    y -= 20;
                    if (y < 300)
                    {
                        subiendo = false;
                        bajando = true;
                    }
                }
                if(y<300 || (y<406 && bajando) )
                {
                    subiendo = false;
                    y += 12;
                    if (y == 406)
                    {
                        saltar = false;
                        bajando = false;
                    }
                }

            }
            base.MoverA(x, y);

        }

        /**
         * Comprueba si el personaje cae en un agujero
         * */
        public bool Morir()
        {
            if (!saltar && !mapa.GetSuelo())
            {
                y += 50;
                return true;
            }
            else return false;
        }

        /**
         * Modifica los booleanos en caso de salto
         * */
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
    }
}
