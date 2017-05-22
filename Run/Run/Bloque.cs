using System;
using Tao.Sdl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    class Bloque:Sprite
    {
        private IntPtr[] seccion = new IntPtr[4];
        private bool suelo;
        private parteBloque estado;
        public enum parteBloque { INICIO, MITAD, FINAL, VACIO };

        public Bloque()
        {
            seccion[0] = SdlImage.IMG_Load("img/Bloque1.png");
            seccion[1] = SdlImage.IMG_Load("img/Bloque2.png");
            seccion[2] = SdlImage.IMG_Load("img/Bloque3.png");
            seccion[3] = SdlImage.IMG_Load("img/Bloque4.png");
            suelo = true;
            ancho = 128;
            alto = 128;
            y = 470;
        }

        public void SetBloque(parteBloque parte)
        {
            switch (parte)
            {
                case parteBloque.INICIO:
                    imagen = seccion[0];
                    estado = parte;
                    suelo = true;
                    break;
                case parteBloque.MITAD:
                    imagen = seccion[1];
                    estado = parte;
                    suelo = true;
                    break;
                case parteBloque.FINAL:
                    imagen = seccion[2];
                    estado = parte;
                    suelo = true;
                    break;
                case parteBloque.VACIO:
                    imagen = seccion[3];
                    estado = parte;
                    suelo = false;
                    break;
                default:
                    break;
            }
        }

        public parteBloque GetEstado()
        {
            return estado;
        }


        public bool GetSuelo()
        {
            return suelo;
        }

        public override void Desplazar(short x)
        {
            this.x = x;
        }

    }
}
