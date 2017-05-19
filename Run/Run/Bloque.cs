﻿using System;
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
        public enum parteBloque { INICIO, MITAD, FINAL, VACIO };

        public Bloque()
        {
            seccion[0] = SdlImage.IMG_Load("img/Bloque1.png");
            seccion[1] = SdlImage.IMG_Load("img/Bloque2.png");
            seccion[2] = SdlImage.IMG_Load("img/Bloque3.png");
            seccion[3] = SdlImage.IMG_Load("img/Bloque4.png");
            ancho = 128;
            alto = 128;
            y = 470;
        }

        public void SetBloque(parteBloque parte)
        {
            if (parte == parteBloque.INICIO) imagen = seccion[0];
            if (parte == parteBloque.MITAD) imagen = seccion[1];
            if (parte == parteBloque.FINAL) imagen = seccion[2];
            if (parte == parteBloque.VACIO) imagen = seccion[3];
        }

        public override void Desplazar(short x)
        {
            this.x = x;
        }
    }
}
