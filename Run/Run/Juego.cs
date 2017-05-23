using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Run
{
    class Juego
    {
        private Hardware h = new Hardware(800, 600, 24, false);
        private Mapa mapa = new Mapa();
        private Personaje zombie = new Personaje();
        private Fondo fondo = new Fondo();
        private Bonus[] recompensas = new Bonus[6];
        private int puntuacion = 0;

        public Juego()
        {
            for(int i = 0; i < 6; i++)
            {
                recompensas[i] = new Bonus();
                recompensas[i].setImagen(i);
                
            }
        }

        public void Iniciar()
        {
            bool terminado = false;
            zombie.SetMapa(mapa);

            do
            {

                if (h.TeclaPulsada(Hardware.TECLA_ESP)) zombie.Saltar();
                if (h.TeclaPulsada(Hardware.TECLA_ESC)) terminado = true;

                zombie.Animar();
                terminado = zombie.Morir();
                mapa.MoverMapa();
                foreach(Bonus recompensa in recompensas)
                {
                    Console.WriteLine(recompensa.GetX());
                    recompensa.Desplazar(Convert.ToInt16(recompensa.GetX()-mapa.GetVelocidad()));
                    //Comprobamos la colision
                    puntuacion += recompensa.Desaparecer(zombie);
                }
                                

                


                h.BorrarPantalla();
                Hardware.DibujarImagen(fondo);
                mapa.DibujarMapa();
                Hardware.DibujarImagen(zombie);
                foreach(Bonus recompensa in recompensas)
                {
                    Hardware.DibujarImagen(recompensa);
                }
                h.VisualizarPantalla();
                Thread.Sleep(20);

            } while (!terminado);
        }

        //TODO
        public void Caer()
        {
            if (!mapa.GetSuelo())
            {

            }
        }


    }
}
