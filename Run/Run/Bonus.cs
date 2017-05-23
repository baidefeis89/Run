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
        private IntPtr[] imgPremio = new IntPtr[7];
        private bool bajar = true;
        private int puntos;
        private Random random = new Random();

        public Bonus()
        {
            //imagen = SdlImage.IMG_Load("img/object1.png");
            imgPremio[0] = SdlImage.IMG_Load("img/Jelly1.png");
            imgPremio[1] = SdlImage.IMG_Load("img/Jelly2.png");
            imgPremio[2] = SdlImage.IMG_Load("img/Jelly3.png");
            imgPremio[3] = SdlImage.IMG_Load("img/Jelly4.png");
            imgPremio[4] = SdlImage.IMG_Load("img/Jelly5.png");
            imgPremio[5] = SdlImage.IMG_Load("img/Jelly6.png");
            imgPremio[6] = SdlImage.IMG_Load("img/Bloque4.png");
            imagen = imgPremio[1];
            ancho = 50;
            alto = 50;
            x = Convert.ToInt16(random.Next(800,4600));
            y = 330;
        }

        public override void Desplazar(short x)
        {
            this.x = x;
            if (bajar)
            {
                y += 2;
                if (y > 380)
                {
                    bajar = false;
                }
            }
            else
            {
                y -= 2;
                if (y < 320)
                {
                    bajar = true;
                }
            }

            
        }

        public void setImagen(int i)
        {
            try
            {
                imagen = imgPremio[i];
                puntos = (i + 1) * 10;
            }
            catch(Exception e)
            {
                imagen = imgPremio[1];
                puntos = 20;
            }
        }


        public int Desaparecer(Sprite objeto)
        {
            //Si la imagen desaparece de la pantalla la volvemos a ubicar para que vuelva a parecer
            if (x < -50)
            {
                x = Convert.ToInt16(random.Next(850, 20600));
                imagen = imgPremio[puntos / 10 - 1];
            }

            if (x + ancho >= objeto.GetX() && x <= objeto.GetX() + objeto.GetAncho() && y + alto >= objeto.GetY() && y <= objeto.GetY() + objeto.GetAlto())
            {
                imagen = imgPremio[6];
                return puntos;
            }

            return 0;
            
        }



    }

}
