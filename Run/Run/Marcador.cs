using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    [Serializable]
    class Marcador
    {
        List<Puntuacion> puntuacionesPuntos;
        List<Puntuacion> puntuacionesDistancia;
        Hardware h = new Hardware(800, 600, 24, false);

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

        public void AddPuntuacion(string nombre,int puntuacion, int distancia)
        {
            Puntuacion marca = new Puntuacion();
            marca.nombre = nombre;
            marca.puntuacion = puntuacion;
            marca.distancia = distancia;
            puntuacionesPuntos.Add(marca);
            puntuacionesDistancia.Add(marca);
        }

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

        public void OrdenarPorPuntuacion()
        {
            var ordenadas = from puntuacion in puntuacionesPuntos
                            orderby puntuacion.puntuacion descending
                            select puntuacion;

            int i = 1;
            short y = 40;
            h.EscribirTexto("Mejores marcas por Puntuacion", 550, y);
            foreach (var record in ordenadas)
            {
                if (y < 560)
                {
                    y += 20;
                    h.EscribirTexto(i + ".- " + record.nombre + " -> " + record.puntuacion + "pts. ("+record.distancia+"m.)", 550, y);
                    i++;
                }
                else
                {
                    puntuacionesPuntos.Remove(record);
                }
            }
        }

        public static void GuardarPuntuaciones(Marcador x)
        {
            
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("scores", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, x);
            stream.Close();
        }

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
