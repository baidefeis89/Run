using System;
using Tao.Sdl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    class Sprite
    {
        protected IntPtr imagen;
        protected short x, y;
        protected short ancho, alto;

        public Sprite(string nombreFichero, short ancho, short alto)
        {
            imagen = SdlImage.IMG_Load(nombreFichero);
            if (imagen == IntPtr.Zero)
            {
                System.Console.WriteLine("Imagen inexistente");
                Environment.Exit(1);
            }
            this.ancho = ancho;
            this.alto = alto;
        }
        public Sprite()
        {

        }

        public virtual void Desplazar(short x)
        {

        }

        public virtual void MoverA(short x, short y)
        {
            this.x = x;
            this.y = y;
        }

        public IntPtr GetPuntero()
        {
            return imagen;
        }

        public short GetX()
        {
            return x;
        }

        public short GetY()
        {
            return y;
        }

        public short GetAncho()
        {
            return ancho;
        }

        public short GetAlto()
        {
            return alto;
        }
    }
}
