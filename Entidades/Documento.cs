using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entidades
{   
    public abstract class Documento
    {
        //Declaro los Atributos
        int anio;
        string autor;
        string barcode;
        string numNormalizado;
        string titulo;
        Paso estado;
        //Declaro los Constructores
        public Documento(string titulo, string autor, int anio, string numNormalizado, string barcode)
        {
            this.anio = anio;
            this.autor = autor;
            this.barcode = barcode;
            this.numNormalizado = numNormalizado;
            this.titulo = titulo;
            this.estado = Paso.Inicio;
        }
        //Declaro las Propiedades
        public int Anio { get => anio;}
        public string Autor { get => autor;}
        public string Barcode { get => barcode;}
        protected string NumNormalizado {  get => numNormalizado;}
        public string Titulo { get => titulo;}
        public Paso Estado { get => estado;}
        // Declaro los enumerados de inicio hasta el terminado
        public enum Paso
        {
            Inicio, Distribuido, EnEscaner, EnRevicion, Terminado
        }
        // El metodo AvanzarEstado verifica en que estado esta el libro o el mapa y avanza segun en donde deste
        public bool AvanzarEstado()
        {
            if (this.estado == Paso.Inicio)
            {
                this.estado = Paso.Distribuido;
                return true;
            }
            else if (this.estado == Paso.Distribuido)
            {
                this.estado = Paso.EnEscaner;
                return true;
            }
            else if (this.estado == Paso.EnEscaner)
            {
                this.estado = Paso.EnRevicion;
                return true;
            }
            else if (this.estado == Paso.EnRevicion)
            {
                this.estado = Paso.Terminado;
                return true;
            }
            else if (this.estado == Paso.Terminado)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        // utilizo el metodo ToString para generar un stringBuilder y retornarlo con los datos
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine($"Titulo: {this.titulo}");
            sb.AppendLine($"Autor: {this.autor}");
            sb.AppendLine($"Año: {this.anio}");
            sb.AppendLine($"Cód. de barras: {this.barcode}");

            return sb.ToString();
        }
    }
}
