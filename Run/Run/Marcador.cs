﻿using System;
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
        List<Puntuacion> puntuaciones;
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
            puntuaciones = new List<Puntuacion>();
        }

        public void AddPuntuacion(string nombre,int puntuacion, int distancia)
        {
            Puntuacion marca = new Puntuacion();
            marca.nombre = nombre;
            marca.puntuacion = puntuacion;
            marca.distancia = distancia;
            puntuaciones.Add(marca);
        }

        public void OrdenarPorDistancia()
        {
            var ordenadas = from puntuacion in puntuaciones
                           orderby puntuacion.distancia descending
                           select puntuacion;

            int i = 1;
            byte y = 40;
            h.EscribirTexto("Mejores marcas por Distancia",50,y);
            foreach(var record in ordenadas)
            {
                y += 20;
                h.EscribirTexto(i + ".- " + record.nombre + " -> " + record.distancia + "m.", 50, y);
                i++;
            }
            h.VisualizarPantalla();
        }

        public void OrdenarPorPuntuacion()
        {
            var ordenadas = from puntuacion in puntuaciones
                            orderby puntuacion.puntuacion descending
                            select puntuacion;

            int i = 1;
            byte y = 40;
            h.EscribirTexto("Mejores marcas por Puntuacion", 400, y);
            foreach (var record in ordenadas)
            {
                y += 20;
                h.EscribirTexto(i + ".- " + record.nombre + " -> " + record.puntuacion + "pts.", 400, y);
                i++;
            }
            h.VisualizarPantalla();
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