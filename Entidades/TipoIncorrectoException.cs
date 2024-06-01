using System;
using System.Text;

namespace Entidades
{
    public class TipoIncorrectoException : Exception //heredo de Exception
    {
        //atributos
        string nombreClase;
        string nombreMetodo;

        //propiedades
        public string NombreClase
        {
            get => this.nombreClase;
        }
        public string NombreMetodo
        {
            get => this.nombreMetodo;
        }
        //constructores
        public TipoIncorrectoException(string mensaje, string clase, string metodo, Exception innerException)
            : base(mensaje, innerException)
        {
            this.nombreMetodo = metodo;
            this.nombreClase = clase;
        }

        public TipoIncorrectoException(string mensaje, string clase, string metodo)
          : this(mensaje, clase, metodo, new Exception()) { }

        //metodo ToString que devuelve un string de la excepcion
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Excepción en el metodo {0} de la clase {1}\n", this.NombreMetodo, this.NombreClase);
            sb.AppendLine("Algo salió mal, revisa los detalles.");
            sb.AppendLine($"Detalles: {this.InnerException}");
            return sb.ToString();
        }
    }
}
