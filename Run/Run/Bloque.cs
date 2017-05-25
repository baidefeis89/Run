/**
 * Clase de los pequeños bloques de tierra que componen la pantalla por la que 
 * corre el personaje
 * */
using System;
using Tao.Sdl;

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

        /**
         * Recibe un enum y modifica la apariencia del suelo
         * también modifica el valor suelo para poder detectar los agujeros
         * */
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

        /**
         * Devuelve true si hay suelo
         * false si es un agujero
         * */
        public bool GetSuelo()
        {
            return suelo;
        }

        /**
         * Desplaza lateralmente el bloque
         * */
        public override void Desplazar(short x)
        {
            this.x = x;
        }

    }
}
