using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
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
            /*TipoDoc tipoDeDocumento;
            if (d is Libro)
            {
                tipoDeDocumento = TipoDoc.libro;
            }
            else
            {
                tipoDeDocumento = TipoDoc.mapa;
            }*/
            TipoDoc tipoDeDocumento = d is Libro ? TipoDoc.libro : TipoDoc.mapa;//esta linea es lo mismo que hacer la las lineas comentadas de arriba
            if (e.tipo == tipoDeDocumento)
            {
                if (e.listaDocumentos.Count == 0)
                {
                    return false;
                }
                else
                {
                    foreach (Documento item in e.ListaDocumentos)
                    {
                        if ((d is Mapa && (Mapa)d == (Mapa)item) ||
                        (d is Libro && (Libro)d == (Libro)item))
                        {
                            throw new TipoIncorrectoException("Este escáner no acepta este tipo de documento", "Entidades", "sobrecarga del operador ==");
                        }
                    }
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
                if (e != d && d.Estado == Documento.Paso.Inicio)
                {
                    if (d.GetType() == typeof(Libro) && e.Locacion == Departamento.procesosTecnicos)
                    {
                        d.AvanzarEstado();
                        e.ListaDocumentos.Add(d);
                        return true;

                    }
                    else if (d.GetType() == typeof(Mapa) && e.locacion == Departamento.mapoteca)
                    {
                        d.AvanzarEstado();
                        e.ListaDocumentos.Add(d);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else 
                {
                    return false;
                }
            }
            catch (TipoIncorrectoException ex)
            {
                throw new TipoIncorrectoException("El documento no se pudo añadir a la lista", "Entidades", "sobrecarga del operador +", ex);
            }
        }
        public static bool operator !=(Escaner e, Documento d)
        {
                return !(e == d);
        }
        
    }
}