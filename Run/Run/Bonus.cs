using System;
using Tao.Sdl;

namespace Run
{
    class Bonus:Sprite
    {
        private IntPtr[] imgPremio = new IntPtr[7];
        private bool bajar = true;
        private int puntos;
        private Random random = new Random();
        private static short pos = 800;

        public Bonus()
        {
            //Carga de las distintas imágenes de las recompensas
            imgPremio[0] = SdlImage.IMG_Load("img/Jelly1.png");
            imgPremio[1] = SdlImage.IMG_Load("img/Jelly2.png");
            imgPremio[2] = SdlImage.IMG_Load("img/Jelly3.png");
            imgPremio[3] = SdlImage.IMG_Load("img/Jelly4.png");
            imgPremio[4] = SdlImage.IMG_Load("img/Jelly5.png");
            imgPremio[5] = SdlImage.IMG_Load("img/Jelly6.png");
            imgPremio[6] = SdlImage.IMG_Load("img/Bloque4.png");
            imagen = imgPremio[1];
            ancho = 35;
            alto = 35;
            x = pos;
            //De esta forma evitamos que siempre salgan a la misma altura
            y = Convert.ToInt16(random.Next(280, 380));
            //Así evitamos que se solapen los objetos bonús
            pos += Convert.ToInt16(random.Next(800,2000));
        }

        /**
         * Desplaza lateralmente las recompensas así como
         * de forma vertical alternativamente
         * */
        public override void Desplazar(short x)
        {
            this.x = x;
            if (bajar)
            {
                y += 6;
                if (y > 380)
                {
                    bajar = false;
                }
            }
            else
            {
                y -= 6;
                if (y < 280)
                {
                    bajar = true;
                }
            }
        }

        /**
         * Asigna una imagen al objeto bonús
         * en función del valor del parametro recibido
         * */
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

        /**
         * Comprueba la colisión con los objetos bonus y devuelve a estos a una
         * posición avanzada cuando se pierden de vista.
         * 
         * @param int Devuelve los puntos de recompensa de ese objeto al detectar colision
         * */
        public int Desaparecer(Sprite objeto)
        {
            //Si la imagen desaparece de la pantalla la volvemos a ubicar para que vuelva a parecer
            if (x < -50)
            {
                x = Convert.ToInt16(random.Next(850, 20600));
                /*
                 * Hace el calculo inverso de los puntos del objeto para
                 * volver a asignarle la imagen correcta
                 */
                imagen = imgPremio[puntos / 10 - 1];
            }
            //Quitamos 10px en los laterales de los objetos bonus para compensar la parte de la imagen transparente dado que estos son redondos pero las imagenes son cuadradas
            if (x + ancho-10 >= objeto.GetX() && x+10 <= objeto.GetX() + objeto.GetAncho() && y + alto >= objeto.GetY() && y <= objeto.GetY() + objeto.GetAlto())
            {
                //Asigna imagen transparente en caso de colisión
                imagen = imgPremio[6];
                return puntos;
            }

            return 0;
            
        }



    }

}
