using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entidades.Documento;

namespace Entidades
{
    public static class Informes
    {
        //este metodo privado y statico lo que hace es verificar en el estado que esta el libro o el mapa para
        //devolver sus caracteristas y poder mostrarlas en el testing, se itera los documentos para ver si es un libro o un mapa
        //y asignarle la extension, la cantidad y el resumen
        //luego este metodo se usara para mostrar los docmuentos de cada estado con sus respectivas extensiones cantidades y resumenes
        private static void MostrarDocumentos(Escaner e, Paso estado, out int extension, out int cantidad, out string resumen)
        {
            extension = 0;
            cantidad = 0;
            resumen = "";
            List<Documento> listaDistribuidos = new List<Documento>();
            foreach (Documento doc in e.ListaDocumentos)
            {
                if (doc.Estado == estado)
                {
                    if (doc is Libro)
                    {
                        Libro libro = (Libro)doc;
                        extension += libro.NumPaginas;
                    }
                    else
                    {
                        if (doc is Mapa mapa)
                        {
                            //Mapa mapa = (Mapa)doc;
                            extension += mapa.Superficie;
                        }
                    }
                    cantidad++;
                    listaDistribuidos.Add(doc);
                    resumen += doc.ToString();
                }
            }

            if (listaDistribuidos.Count == 0)
            {
                resumen = "";
            }

        }
        public static void MostrarDistribuidos(Escaner e, out int extension, out int cantidad, out string resumen)
        {
            MostrarDocumentos(e, Paso.Distribuido, out extension, out cantidad, out resumen);
        }
        public static void MostrarEnRevision(Escaner e, out int extension, out int cantidad, out string resumen)
        {
            MostrarDocumentos(e, Paso.EnRevicion, out extension, out cantidad, out resumen);
        }
        public static void MostrarEnEscaner(Escaner e, out int extension, out int cantidad, out string resumen)
        {
            MostrarDocumentos(e, Paso.EnEscaner, out extension, out cantidad, out resumen);
        }
        public static void MostrarTerminados(Escaner e, out int extension, out int cantidad, out string resumen)
        {
            MostrarDocumentos(e, Paso.Terminado, out extension, out cantidad, out resumen);
        }
        
    }
}

