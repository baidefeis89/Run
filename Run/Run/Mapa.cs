using System;
using Tao.Sdl;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    class Mapa:Sprite
    {
        Bloque[] bloques = new Bloque[20];
        private int contador = 0;
        private short primerBloque = 0;
        private Random random = new Random();
        private bool agujero = false;



        public Mapa()
        {
            short x = 0;
            for(int i = 0; i < 20; i++)
            {
                bloques[i] = new Bloque();
                bloques[i].SetBloque(Bloque.parteBloque.MITAD);
                bloques[i].Desplazar(x);
                x += 128;
            }
        }

        public void MoverBloques()
        {
            short pos=0;
            int numRand;
            

            //Movimiento de los bloques a la izquierda
            for (int i=0;i<bloques.Length;i++)
            {
                pos = bloques[i].GetX();
                pos -= 8;
                bloques[i].Desplazar(pos);
            }

            //TODO Comprobar sincronización del suelo
            //Control bloques para situarlos al final de la pantalla una vez desaparecen de la vista
            if (contador == 16)
            {
                numRand = random.Next(20);

                if (numRand == 0) agujero = true;

                if (primerBloque == 0)
                {
                    pos = bloques[19].GetX();
                    pos += 128;
                    bloques[primerBloque].Desplazar(pos);

                    if (agujero)
                    {
                        bloques[19].SetBloque(Bloque.parteBloque.FINAL);
                        bloques[0].SetBloque(Bloque.parteBloque.VACIO);
                    }

                }
                else if (primerBloque > 0)
                {
                    pos = bloques[primerBloque - 1].GetX();
                    pos += 128;
                    bloques[primerBloque].Desplazar(pos);

                    if (agujero)
                    {
                        bloques[primerBloque - 1].SetBloque(Bloque.parteBloque.FINAL);
                        bloques[primerBloque].SetBloque(Bloque.parteBloque.VACIO);
                    }

                }

                if (primerBloque == 19) primerBloque = 0;
                else primerBloque++;

                if (agujero)
                {
                    bloques[primerBloque].SetBloque(Bloque.parteBloque.INICIO);
                    agujero = false;
                }
                else
                {
                    bloques[primerBloque].SetBloque(Bloque.parteBloque.MITAD);
                }

                contador = 0;
            }
            else contador++;
        }

        public void DibujarMapa()
        {
            foreach(Bloque x in bloques)
            {
                Hardware.DibujarImagen(x);
            }
        }
    }
}
