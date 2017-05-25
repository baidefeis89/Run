using System;
using System.Collections.Generic;
using Tao.Sdl;
using System.Threading;

/**
 * Clase con los distintos menús del juego
 * */

namespace Run
{
    class Menu:Sprite
    {
        Hardware h = new Hardware(800, 600, 24, false);
        Fondo fondo = new Fondo();
        Mapa mapa = new Mapa();
        Personaje pj = new Personaje();

        public Menu()
        {

            ancho = 800;
            alto = 600;
            imagen = SdlImage.IMG_Load("img/BG.png");
            x = 0;
            y = 0;
        }

        public bool Principal()
        {
            Hardware.DibujarImagen(fondo);
            mapa.DibujarMapa();
            h.EscribirTexto("Run!",340,150,40,0,0,0);
            h.EscribirTexto("Pulsa Enter para jugar", 140, 190, 40, 0, 0, 0);
            h.EscribirTexto("Controles:", 320, 500, 20, 250, 250, 250);
            h.EscribirTexto("Espacio -> Saltar", 320, 520, 20, 250, 250, 250);
            h.EscribirTexto("Escape -> Menú", 320, 540, 20, 250, 250, 250);

            h.VisualizarPantalla();

            do
            {
                if (h.TeclaPulsada(Hardware.TECLA_ENT)) return true;
                if (h.TeclaPulsada(Hardware.TECLA_ESC)) return false;
            } while (true);

        }

        public bool Secundario()
        {
            Hardware.DibujarImagen(fondo);
            mapa.DibujarMapa();
            h.EscribirTexto("Pulsa Escape para salir del juego", 100, 150, 40, 0, 0, 0);
            h.EscribirTexto("Pulsa Enter para empezar", 130, 190, 40, 0, 0, 0);
            h.EscribirTexto("Controles:", 320, 500, 20, 250, 250, 250);
            h.EscribirTexto("Espacio -> Saltar", 320, 520, 20, 250, 250, 250);
            h.EscribirTexto("Escape -> Menú", 320, 540, 20, 250, 250, 250);

            h.VisualizarPantalla();

            do
            {
                if (h.TeclaPulsada(Hardware.TECLA_ENT)) return true;
                if (h.TeclaPulsada(Hardware.TECLA_ESC)) return false;
            } while (true);

        }

        public bool MenuPausa()
        {
            Thread.Sleep(100);

            h.EscribirTexto("PAUSA",320,90,50,0,0,0);
            h.EscribirTexto("Pulsa Escape para salir del juego", 100, 150, 40, 0, 0, 0);
            h.EscribirTexto("Pulsa Enter para continuar", 130, 190, 40, 0, 0, 0);
            h.EscribirTexto("Controles:", 320, 500, 20, 250, 250, 250);
            h.EscribirTexto("Espacio -> Saltar", 320, 520, 20, 250, 250, 250);
            h.EscribirTexto("Escape -> Menú", 320, 540, 20, 250, 250, 250);

            h.VisualizarPantalla();

            do
            {
                if (h.TeclaPulsada(Hardware.TECLA_ENT)) return false;
                if (h.TeclaPulsada(Hardware.TECLA_ESC)) return true;
            } while (true);

        }

        /**
         * Mostramos un menú para que el usuario introduzca su nombre
         * devuelve un string con el nombre
         * */
        public string PedirNombre()
        {
            char[] letras = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '.', ' ' };
            List<char> nombre = new List<char>();
            string name="";
            short x =300;
            int i = 0;
            bool terminado = false;

            h.BorrarPantalla();
            do
            {
                x = 250;
                
                h.EscribirTexto("Introduce tu nombre", 300, 200);
                h.EscribirTexto("Usa las flechas ^v para cambiar entre letras",220,520);
                h.EscribirTexto("Usa la flecha -> para pasar a la siguiente letra", 210, 535);
                h.EscribirTexto("Pulsa Enter para finalizar", 280, 550);
                
                h.VisualizarPantalla();
                Thread.Sleep(50);
                foreach (char letra in nombre)
                {
                    h.EscribirTexto(Convert.ToString(letra), x, 250);
                    x += 15;
                }

                h.EscribirTexto(Convert.ToString(letras[i]), x, 250);

                if (h.TeclaPulsada(Hardware.TECLA_ABA))
                {
                    if (i < letras.Length - 1) i++;
                    else i = 0;
                    h.BorrarPantalla();
                }
                if (h.TeclaPulsada(Hardware.TECLA_ARR))
                {
                    if (i > 0) i--;
                    else i = letras.Length - 1;
                    h.BorrarPantalla();
                }
                if (h.TeclaPulsada(Hardware.TECLA_DER) && x < 400)
                {
                    x += 15;
                    nombre.Add(letras[i]);
                    h.BorrarPantalla();
                }
                if (h.TeclaPulsada(Hardware.TECLA_ENT))
                {
                    nombre.Add(letras[i]);
                    terminado = true;
                }
                
            } while (!terminado);

            foreach(char letra in nombre)
            {
                name += letra;
            }

            return name;
        }

        /**
         * Muestra en dos columnas los mejores resultados de
         * los jugadores (por distancia y por puntos)
         * */
        public void MostrarMarcas(Marcador marcador)
        {
            Thread.Sleep(200);
            bool salir = false;
            h.BorrarPantalla();
            h.EscribirTexto("Marcadores", 350, 0);
            marcador.OrdenarPorDistancia();
            marcador.OrdenarPorPuntuacion();
            h.EscribirTexto("Pulse Enter", 350, 580);
            h.VisualizarPantalla();
            do
            {
                if (h.TeclaPulsada(Hardware.TECLA_ENT)) salir = true;
            } while (!salir);
            
        }

    }
}
