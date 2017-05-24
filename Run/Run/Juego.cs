﻿using System;
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
            string textoPuntuacion = "Puntuacion: " + puntuacion;
            string textoDistancia = "Distancia: " + mapa.GetDistancia()+"m";

            do
            {

                zombie.Animar();
                terminado = zombie.Morir();
                mapa.MoverMapa();
                foreach(Bonus recompensa in recompensas)
                {
                    recompensa.Desplazar(Convert.ToInt16(recompensa.GetX()-mapa.GetVelocidad()));
                    //Comprobamos la colision
                    puntuacion += recompensa.Desaparecer(zombie);
                }

                textoPuntuacion = "Puntuación: " + puntuacion;
                textoDistancia = "Distancia: " + mapa.GetDistancia() + "m";

                h.BorrarPantalla();
                Hardware.DibujarImagen(fondo);
                mapa.DibujarMapa();
                Hardware.DibujarImagen(zombie);
                h.EscribirTexto(textoPuntuacion, 650, 550);
                h.EscribirTexto(textoDistancia,650,570);

                foreach(Bonus recompensa in recompensas)
                {
                    Hardware.DibujarImagen(recompensa);
                }
                h.VisualizarPantalla();

                if (h.TeclaPulsada(Hardware.TECLA_ESP)) zombie.Saltar();
                if (h.TeclaPulsada(Hardware.TECLA_ESC)) terminado = true;


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
