using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Libro : Documento
    {
        //Atributos
        int numPaginas;
        //Constructores
        public Libro(string titulo, string autor, int anio, string numNormalizado, string barcode, int numPaginas)
        : base(titulo, autor, anio, numNormalizado, barcode)
        {
            this.numPaginas = numPaginas;
        }
        //Propiedades
        public int NumPaginas { get => numPaginas; }
        public string ISBN { get => NumNormalizado; }
        //Sobrecarga == que lo que hace es devolver true si cumple con las condiciones, si no devuelve false
        public static bool operator ==(Libro l1, Libro l2)
        {
            if (l1 is Libro && l2 is Libro)
            {
                bool mismoBarcode = l1.Barcode == l2.Barcode;
                bool mismoISBN = l1.ISBN == l2.ISBN;
                bool mismoTituloYAutor = l1.Titulo == l2.Titulo && l1.Autor == l2.Autor;
                return mismoBarcode || mismoISBN || mismoTituloYAutor;
            }
            else
            {
                return false;
            }
        }
        //La sobrecarga del != lo que hace es determinar si los dos objetos no son iguales, devuelve False.
        public static bool operator !=(Libro l1, Libro l2)
        {
            return !(l1 == l2);
        }
        //utilizo el ToString del Documento para agregarle el ISBN y el Numero de Paginas
        public override string ToString()
        {
            string stringBaseDoc = base.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(stringBaseDoc);
            sb.AppendLine($"ISBN: {this.ISBN}");
            sb.AppendLine($"Número de páginas: {this.numPaginas}.");
            return sb.ToString();
        }
    }
}
