using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Escaner
    {
        //atributos, tenemos una lista , dos enumerados y un string
        List<Documento> listaDocumentos;
        Departamento locacion;
        string marca;
        TipoDoc tipo;
        //constructor
        public Escaner(string marca, TipoDoc tipo)
        {
            this.marca = marca;
            this.tipo = tipo;
            listaDocumentos = new List<Documento>();
            if (this.tipo == TipoDoc.libro)
            {
                this.locacion = Departamento.procesosTecnicos;
            }
            else
            {
                this.locacion = Departamento.mapoteca;
            }
        }
        //propiedades
        public List<Documento> ListaDocumentos { get => listaDocumentos; }
        public Departamento Locacion { get => locacion; }
        public string Marca { get => marca; }
        public TipoDoc Tipo { get => tipo; }
        //enumerados
        public enum Departamento
        {
            nulo, mapoteca, procesosTecnicos
        }

        public enum TipoDoc
        {
            libro, mapa
        }
        //este metodo lo que hace es iterar los documentos y verificar si los documentos son libros o mapas y cambiar el estado del documento dentro de la lista
        public bool CambiarEstadoDocumento(Documento d)
        {
            foreach (Documento doc in listaDocumentos)
            {
                if (d is Libro libro && ((Libro)d == (Libro)doc))
                {
                    return doc.AvanzarEstado();
                }
                else if (d is Mapa mapa && doc is Mapa mapaExistente && mapa == mapaExistente)
                {
                    return doc.AvanzarEstado();
                }
            }

            return false;
        }
        //la sobrecarga del operador == se fija si hay un documento igual en la lista, devuelve true si hay y false si no ademas de que si no
        // ademas se agrega de que si el escaner no tiene su documento correspondiente se lanza la excepcion y es controlada en la sobrecarga del +
        public static bool operator ==(Escaner e, Documento d)
        {
            foreach (Documento doc in e.listaDocumentos)
            {
                if ((d is Libro && doc is Libro && ((Libro)d) == ((Libro)doc)) ||
                (d is Mapa && doc is Mapa && ((Mapa)d) == ((Mapa)doc)))
                {
                    return true;
                }
                else if ((d is Libro && doc is Libro && ((Libro)d) != ((Libro)doc)) ||
                (d is Mapa && doc is Mapa && ((Mapa)d) != ((Mapa)doc)))
                {
                    return false;
                }
                else
                {
                    throw new TipoIncorrectoException("Este escáner no acepta este tipo de documento", "Escaner.cs", "Validación ==");
                }
            }
            return false;
        }
        //la sobrecarga del operador + intenta añadir el documento a la lista de documentos, debe añadirlo solo si esta en estado inicio , se añade a la lista
        // y ademas cambia el estado a distribuido luego controla la excepcion generada en la sobrecarga del == para generar otra excepcion y controlarla
        public static bool operator +(Escaner e, Documento d)
        {
            try
            {
                if ((d is Libro && e.locacion == Departamento.procesosTecnicos && e.tipo == TipoDoc.libro) ||
                    (d is Mapa && e.locacion == Departamento.mapoteca && e.tipo == TipoDoc.mapa))
                {
                    if (d.Estado == Documento.Paso.Inicio)
                    {
                        if (e != d)
                        {
                            d.AvanzarEstado();
                            e.listaDocumentos.Add(d);
                            return true;
                        }
                    }
                }
                else
                {
                    // Verificación usando el operador ==
                    if (e != d)
                    {

                        return false;
                    }
                }
            }
            catch (TipoIncorrectoException ex)
            {
                throw new TipoIncorrectoException("El documento no se pudo añadir a la lista", "Escaner.cs", "sobrecarga +", ex);
            }

            return false;
        }
        public static bool operator !=(Escaner e, Documento d)
        {
            return !(e == d);
        }
        
    }
}