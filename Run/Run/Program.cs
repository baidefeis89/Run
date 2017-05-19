using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Run
{
    class Program
    {
        static void Main(string[] args)
        {
            bool terminado = false;

            Hardware h = new Hardware(800, 600, 24, false);
            Personaje zombie = new Personaje();
            Fondo fondo = new Fondo();
            Mapa mapa = new Mapa();

            //TODO crear menu

            do
            {

                if (h.TeclaPulsada(Hardware.TECLA_ESP)) zombie.Saltar();
                if (h.TeclaPulsada(Hardware.TECLA_ESC)) terminado = true;

                zombie.Animar();
                mapa.MoverBloques();
                //fondo.MoverA(xmapa,0);

                h.BorrarPantalla();
                Hardware.DibujarImagen(fondo);
                mapa.DibujarMapa();
                Hardware.DibujarImagen(zombie);
                h.VisualizarPantalla();
                Thread.Sleep(20);

            } while (!terminado);
        }
    }
}
