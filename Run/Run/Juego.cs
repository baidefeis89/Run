using System;
using System.Threading;
/**
 * Clase principal del juego, desde aquí se gestiona la partida
 * y eventos que sucedan
 * */

namespace Run
{
    class Juego
    {
        private Hardware h = new Hardware(800, 600, 24, false);
        private Mapa mapa = new Mapa();
        private Menu menu = new Menu();
        private Personaje zombie = new Personaje();
        private Fondo fondo = new Fondo();
        private Bonus[] recompensas = new Bonus[6];
        private int puntuacion = 0;
        private Marcador marca = new Marcador();
        private Audio musica = new Audio();

        public Juego()
        {
            for(int i = 0; i < 6; i++)
            {
                recompensas[i] = new Bonus();
                recompensas[i].setImagen(i);
            }

            marca = Marcador.CargarPuntuaciones();
        }

        /**
         * Muestra el menú principal, llama al método Jugar, almacena
         * la puntuación conseguida y reinicia los parámetros del juego entre partidas
         * */
        public void Iniciar()
        {
            bool jugar = true;
            do
            {
                Thread.Sleep(200);
                if (menu.Principal())
                {
                    Jugar();
                    marca.AddPuntuacion(menu.PedirNombre(), puntuacion, mapa.GetDistancia());
                    menu.MostrarMarcas(marca);
                    ResetearParametros();
                }
                else
                {
                    Thread.Sleep(100);
                    jugar = menu.Secundario();
                }
            } while (jugar);

            // Cuando se sale del juego almacena las mejores puntuaciones en un archivo
            Marcador.GuardarPuntuaciones(marca);            

        }

        /**
         * Gestión de la lógica del juego y llamada a los distintos métodos
         * */
        private void Jugar()
        {
            musica.PlayMusica();
            bool terminado = false;
            zombie.SetMapa(mapa);
            string textoPuntuacion = "Puntuacion: " + puntuacion;
            string textoDistancia = "Distancia: " + mapa.GetDistancia() + "m";
            do
            {
                zombie.Animar();
                terminado = zombie.Morir();
                mapa.MoverMapa();
                foreach (Bonus recompensa in recompensas)
                {
                    recompensa.Desplazar(Convert.ToInt16(recompensa.GetX() - mapa.GetVelocidad()));
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
                h.EscribirTexto(textoDistancia, 650, 570);

                foreach (Bonus recompensa in recompensas)
                {
                    Hardware.DibujarImagen(recompensa);
                }
                h.VisualizarPantalla();

                if (h.TeclaPulsada(Hardware.TECLA_ESP)) zombie.Saltar();
                if (h.TeclaPulsada(Hardware.TECLA_ESC)) terminado = menu.MenuPausa();


                Thread.Sleep(20);
                if (terminado) Thread.Sleep(100);

            } while (!terminado);

            musica.StopMusica();
        }

        /**
         * Vuelve a crear los objetos para que se pongan sus parametros a cero
         * */
        private void ResetearParametros()
        {
            mapa = new Mapa();
            zombie = new Personaje();
            puntuacion = 0;
            for (int i = 0; i < 6; i++)
            {
                recompensas[i] = new Bonus();
                recompensas[i].setImagen(i);
            }
        }


    }
}
