using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Mapa : Documento
    {
        //Atributos
        int alto;
        int ancho;
        //Constructores
        public Mapa(string titulo, string autor, int anio, string numNormalizado, string barcode, int ancho, int alto) : base(titulo, autor, anio, numNormalizado, barcode)
        {
            this.alto = alto;
            this.ancho = ancho;
        }
        //Propiedades
        public int Alto { get => alto; }
        public int Ancho { get => ancho; }
        public int Superficie { get => ancho * alto; }
        //Sobrecarga == que lo que hace es devolver true si cumple con las condiciones, si no devuelve false
        public static bool operator ==(Mapa m1, Mapa m2)
        {
            if (m1 is Mapa && m2 is Mapa)
            {
                bool mismoBarcode = m1.Barcode == m2.Barcode;
                bool mismoTitulo = m1.Titulo == m2.Titulo;
                bool mismoAutor = m1.Autor == m2.Autor;
                bool mismoAnio = m1.Anio == m2.Anio;
                bool mismaSuperficie = m1.Superficie == m2.Superficie;
                bool mismosDetalles = mismoTitulo && mismoAutor && mismoAnio && mismaSuperficie;
                return mismoBarcode || mismosDetalles; 
            }
            else
            {
                return false;
            }
        }
        //La sobrecarga del != lo que hace es determinar si los dos objetos no son iguales, devuelve False.
        public static bool operator !=(Mapa m1, Mapa m2)
        {
            return !(m1.Barcode == m2.Barcode);
        }
        //utilizo el ToString del Documento para agregarle la superficie
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.AppendLine($"Superficie: {this.alto} * {this.ancho} = {this.Superficie} cm2.");

            return sb.ToString();
        }
    }
}
