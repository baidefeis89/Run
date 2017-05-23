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
        //Bonus bonus = new Bonus();
        private int contador = 0;
        private short ultimoBloque=18;
        private Random random = new Random();
        private bool agujero = false;
        private short velocidad = 18;
        //frecuenciaAgujeros menor número == mayor frecuencia
        private short frecuenciaAgujeros = 16;
        private short contadorVelocidad = 0;



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

        public void MoverMapa()
        { 
            MoverBloques();
            //MoverRecompensas();
            ControlVelocidad();
            
            //ControlRecompensas();//TODO
        }

        /**
         * Da el formato correspondiente a cada uno de los bloques
         * en funcion de la posición de los agujeros
         * */
        public void FormatBloques()
        {
            
            if (!bloques[ultimoBloque].GetSuelo())
            {
                switch (ultimoBloque)
                {
                    case 0:
                        bloques[19].SetBloque(Bloque.parteBloque.FINAL);
                        bloques[1].SetBloque(Bloque.parteBloque.INICIO);
                        break;
                    case 19:
                        bloques[18].SetBloque(Bloque.parteBloque.FINAL);
                        bloques[0].SetBloque(Bloque.parteBloque.INICIO);
                        break;
                    default:
                        bloques[ultimoBloque - 1].SetBloque(Bloque.parteBloque.FINAL);
                        bloques[ultimoBloque + 1].SetBloque(Bloque.parteBloque.INICIO);
                        break;
                }
            }
            else
            {
                switch (ultimoBloque)
                {
                    case 0:
                        bloques[1].SetBloque(Bloque.parteBloque.MITAD);
                        break;
                    case 19:
                        bloques[0].SetBloque(Bloque.parteBloque.MITAD);
                        break;
                    default:
                        bloques[ultimoBloque + 1].SetBloque(Bloque.parteBloque.MITAD);
                        break;
                }
            }
        }

        /**
         * Crea agujeros aleatoriamente evitando que se creen
         * dos consecutivos o con menos de 2 bloques de tierra entre medias
         * */
        public void ControlAgujeros()
        {
            short numRand;

            numRand = Convert.ToInt16(random.Next(frecuenciaAgujeros));

            if (numRand == 0 && bloques[ultimoBloque].GetEstado() == Bloque.parteBloque.MITAD)
            {
                if ( ultimoBloque > 0 && ultimoBloque < 19 && bloques[ultimoBloque -1].GetEstado() != Bloque.parteBloque.INICIO)
                {
                    bloques[ultimoBloque].SetBloque(Bloque.parteBloque.VACIO);
                }
                else if (ultimoBloque == 0 && bloques[19].GetEstado() != Bloque.parteBloque.INICIO)
                {
                    bloques[ultimoBloque].SetBloque(Bloque.parteBloque.VACIO);
                }
                else if(ultimoBloque == 19 && bloques[ultimoBloque -1].GetEstado() != Bloque.parteBloque.INICIO)
                {
                    bloques[ultimoBloque].SetBloque(Bloque.parteBloque.VACIO);
                }
            }
            
        }

        /**
         * Mueve los bloques a la izquierda y los vuelve a poner al final
         * cuando desaparecen de la pantalla
         * 
         */
        public void MoverBloques()
        {
            short pos=0;

            //Movimiento de los bloques a la izquierda y marcado del ultimo de ellos
            for (short i=0;i<bloques.Length;i++)
            {
                pos = bloques[i].GetX();
                pos -= velocidad;
                bloques[i].Desplazar(pos);
                if (bloques[i].GetX() < -250)
                {
                    if (i == 0)
                    {
                        pos = bloques[19].GetX();
                        pos += Convert.ToInt16(128-velocidad);
                        bloques[i].Desplazar(pos);
                    }
                    else
                    {
                        pos = bloques[i - 1].GetX();
                        pos += 128;
                        bloques[i].Desplazar(pos);                   
                    }

                    ultimoBloque = i;
                    
                    //Cada vez que se mueve un bloque al final
                    ControlAgujeros();
                    FormatBloques();


                }
            }

        }

        /**
         * Aumento progresivo de la velocidad 
         * 
         */
        public void ControlVelocidad()
        {
            if (contadorVelocidad == 200)
            {
                velocidad++;
                contadorVelocidad = 0;
            }
            else contadorVelocidad++;
        }

        public void DibujarMapa()
        {
            foreach(Bloque x in bloques)
            {
                Hardware.DibujarImagen(x);
            }
            //Hardware.DibujarImagen(bonus);
        }

        public short GetVelocidad()
        {
            return velocidad;
        }
    }
}
