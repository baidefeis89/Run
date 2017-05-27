using System;

/**
 * Gestión de los bloques que componen el mapa, velocidad
 * de movimiento y frecuencia de agujeros
 * */

namespace Run
{
    class Mapa
    {
        private Bloque[] bloques = new Bloque[20];
        private int contador = 0;
        private short ultimoBloque=18;
        private Random random = new Random();
        private short velocidad = 18;
        //frecuenciaAgujeros menor número == mayor frecuencia
        private short frecuenciaAgujeros = 16;
        private short contadorVelocidad = 0;
        private int distancia = 0;


        /**
         * Crea todos los bloques y les asigna su posicion
         * inicial y la imagen por defecto
         * */
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

        /**
         * Devuelve true si el bloque que está debajo del 
         * personaje es suelo y false si es un agujero
         * */
        public bool GetSuelo()
        {
            foreach(Bloque bloque in bloques)
            {
                if(bloque.GetX()<=130 && bloque.GetX() + 128 >= 140)
                {
                    return bloque.GetSuelo();
                }
            }
            return true;
        }

        public void MoverMapa()
        { 
            MoverBloques();
            ControlVelocidad();
        }

        /**
         * Da el formato correspondiente a cada uno de los bloques
         * en funcion de la posición de los agujeros
         * */
        private void FormatBloques()
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
        private void ControlAgujeros()
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
        private void MoverBloques()
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

                    distancia += 1;
                }
            }

        }

        /**
         * Aumento progresivo de la velocidad 
         * 
         */
        private void ControlVelocidad()
        {
            if (contadorVelocidad == 200)
            {
                velocidad++;
                contadorVelocidad = 0;
                //Aumentamos la frecuencia de los agujeros cada 2 aumentos de velocidad
                if (frecuenciaAgujeros > 1 && velocidad % 2 == 0) frecuenciaAgujeros--;
            }
            else contadorVelocidad++;
        }

        public void DibujarMapa()
        {
            foreach(Bloque x in bloques)
            {
                Hardware.DibujarImagen(x);
            }
        }

        public short GetVelocidad()
        {
            return velocidad;
        }

        public int GetDistancia()
        {
            return distancia;
        }
    }
}
