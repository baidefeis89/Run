using System;
using Tao.Sdl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    [Serializable]
    class Hardware
    {
        //set pantalla static
        short ancho;
        short alto;
        short bitsColor;
        static IntPtr pantalla;
        public static int TECLA_ESC = Sdl.SDLK_ESCAPE;
        public static int TECLA_ARR = Sdl.SDLK_UP;
        public static int TECLA_ABA = Sdl.SDLK_DOWN;
        public static int TECLA_DER = Sdl.SDLK_RIGHT;
        public static int TECLA_ESP = Sdl.SDLK_SPACE;
        public static int TECLA_ENT = Sdl.SDLK_RETURN;

        public Hardware(short ancho, short alto, short bits, bool pantallaCompleta)
        {
            this.ancho = ancho;
            this.alto = alto;
            bitsColor = bits;

            int flags = Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF | Sdl.SDL_ANYFORMAT;

            if (pantallaCompleta) flags = flags | Sdl.SDL_FULLSCREEN;

            Sdl.SDL_Init(Sdl.SDL_INIT_EVERYTHING);
            pantalla = Sdl.SDL_SetVideoMode(ancho, alto, bitsColor, flags);

            Sdl.SDL_Rect rect = new Sdl.SDL_Rect(0, 0, ancho, alto);

            Sdl.SDL_SetClipRect(pantalla, ref rect);

            SdlTtf.TTF_Init();//texto
        }

        ~Hardware()
        {
            Sdl.SDL_Quit();
        }

        public static void DibujarImagen(Sprite imagen)
        {
            Sdl.SDL_Rect origen = new Sdl.SDL_Rect(0, 0, imagen.GetAncho(), imagen.GetAlto());
            Sdl.SDL_Rect dest = new Sdl.SDL_Rect(imagen.GetX(), imagen.GetY(), imagen.GetAncho(), imagen.GetAlto());
            Sdl.SDL_BlitSurface(imagen.GetPuntero(), ref origen, pantalla, ref dest);
        }

        public void EscribirTexto(string texto, short x, short y)
        {
            IntPtr tipoLetra = SdlTtf.TTF_OpenFont("SEGOEPRB.TTF", 14);
            Sdl.SDL_Color color = new Sdl.SDL_Color(137,45,45);
            IntPtr textoComoImagen = SdlTtf.TTF_RenderText_Solid(tipoLetra, texto, color);
            if (textoComoImagen == IntPtr.Zero) Environment.Exit(5);
            Sdl.SDL_Rect origen = new Sdl.SDL_Rect(0, 0, ancho, alto);
            Sdl.SDL_Rect dest = new Sdl.SDL_Rect(x, y, ancho, alto);
            Sdl.SDL_BlitSurface(textoComoImagen, ref origen, pantalla, ref dest);
        }

        public void EscribirTexto(string texto, short x, short y,short size,byte r,byte g,byte b)
        {
            IntPtr tipoLetra = SdlTtf.TTF_OpenFont("SEGOEPRB.TTF", size);
            Sdl.SDL_Color color = new Sdl.SDL_Color(r, g, b);
            IntPtr textoComoImagen = SdlTtf.TTF_RenderText_Solid(tipoLetra, texto, color);
            if (textoComoImagen == IntPtr.Zero) Environment.Exit(5);
            Sdl.SDL_Rect origen = new Sdl.SDL_Rect(0, 0, ancho, alto);
            Sdl.SDL_Rect dest = new Sdl.SDL_Rect(x, y, ancho, alto);
            Sdl.SDL_BlitSurface(textoComoImagen, ref origen, pantalla, ref dest);
        }

        public void VisualizarPantalla()
        {
            Sdl.SDL_Flip(pantalla);
        }

        public bool TeclaPulsada(int c)
        {
            bool pulsada = false;
            Sdl.SDL_PumpEvents();
            Sdl.SDL_Event suceso;
            Sdl.SDL_PollEvent(out suceso);
            int numkeys;
            byte[] teclas = Sdl.SDL_GetKeyState(out numkeys);
            if (teclas[c] == 1)
            {
                pulsada = true;
            }
            return pulsada;
        }

        public void BorrarPantalla()
        {
            Sdl.SDL_Rect origen = new Sdl.SDL_Rect(0, 0, ancho, alto);
            Sdl.SDL_FillRect(pantalla, ref origen, 0);
        }
    }
}
