using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace Run
{
    [Serializable]
    class Marcador
    {
        private List<Puntuacion> puntuacionesPuntos;
        private List<Puntuacion> puntuacionesDistancia;
        private Hardware h = new Hardware(800, 600, 24, false);

        [Serializable]
        public struct Puntuacion
        {
            public string nombre;
            public int puntuacion;
            public int distancia;
        }

        public Marcador()
        {
            puntuacionesPuntos = new List<Puntuacion>();
            puntuacionesDistancia = new List<Puntuacion>();
        }

        /**
         * Añade las puntuaciones a las dos listas de puntuaciones
         * */
        public void AddPuntuacion(string nombre,int puntuacion, int distancia)
        {
            Puntuacion marca = new Puntuacion();
            marca.nombre = nombre;
            marca.puntuacion = puntuacion;
            marca.distancia = distancia;
            puntuacionesPuntos.Add(marca);
            puntuacionesDistancia.Add(marca);
        }

        /**
         * Ordena y muestra las puntuaciones por mayor distancia
         * elimina las ultimas puntuaciones
         * */
        public void OrdenarPorDistancia()
        {
            var ordenadas = from puntuacion in puntuacionesDistancia
                           orderby puntuacion.distancia descending
                           select puntuacion;

            int i = 1;
            short y = 40;
            h.EscribirTexto("Mejores marcas por Distancia",50,y);
            foreach(var record in ordenadas)
            {
                if (y < 560)
                {
                    y += 20;
                    h.EscribirTexto(i + ".- " + record.nombre + " -> " + record.distancia + "m. (" + record.puntuacion + "pts.)", 50, y);
                    i++;
                }
                else
                {
                    puntuacionesDistancia.Remove(record);
                }
            }
        }

        /**
         * Ordena y muestra las puntuaciones por mayor puntuacion
         * elimina las ultimas puntuaciones
         * */
        public void OrdenarPorPuntuacion()
        {
            var ordenadas = from puntuacion in puntuacionesPuntos
                            orderby puntuacion.puntuacion descending
                            select puntuacion;

            int i = 1;
            short y = 40;
            h.EscribirTexto("Mejores marcas por Puntuacion", 500, y);
            foreach (var record in ordenadas)
            {
                if (y < 560)
                {
                    y += 20;
                    h.EscribirTexto(i + ".- " + record.nombre + " -> " + record.puntuacion + "pts. ("+record.distancia+"m.)", 500, y);
                    i++;
                }
                else
                {
                    puntuacionesPuntos.Remove(record);
                }
            }
        }

        /**
         * Guarda es estado de la clase marcador en un archivo binario
         * */
        public static void GuardarPuntuaciones(Marcador x)
        {
            
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("scores", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, x);
            stream.Close();
        }

        /**
         * Devuelve el estado guardado de la clase marcador
         * */
        public static Marcador CargarPuntuaciones()
        {
            if (File.Exists("scores"))
            {
                Marcador x;
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("scores", FileMode.Open, FileAccess.Read, FileShare.Read);
                try
                {
                    x = (Marcador)formatter.Deserialize(stream);
                }
                catch
                {
                    x = new Marcador();
                }
                
                stream.Close();
                return x;
            }
            return new Marcador();
        }
    }
}
