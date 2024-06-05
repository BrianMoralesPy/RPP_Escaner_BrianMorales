/*
 * Sobre la puntuación provisional después del testing de hoy:
--> 8 - Los que sacaron más de 35 en el testing - puede añadir +2 si se añadió un excepción
--> 6 - Los que sacaron entre 30 y 34 (incluidos) - puede añadir +2 si se añadió un excepción
--> 4 - los que sacaron entre 25 y 29 (incluidos) - está para aprobar pero no para promocionar. 
--> 2 - Los que sacaron menos de 25, código con warnings, que lanza excepciones no controladas o directamente no compila - Desaprobado.

Puntuación provisional después de que yo revise el código:
   Los que tienen 6 u 8 pueden ver que su nota baje en los siguientes casos:
--> baja a 2 - El código no compila, manda warnings, lanza excepciones no controladas, tiene un commit tardío o el código tiene errores MUY groseros.
--> baja a 4 ó 6 - Si el código tiene errores de concepto (dependerá de la gravedad).
 Hoja de testing de la profe 
 */
using Entidades;
namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // LIBROS
            Libro l1 = new Libro("Yerma", "García Lorca, Federico", 1995, "11111", "22222", 100);
            Libro l2 = new Libro("Bodas de sangre", "García Lorca, Federico", 1997, "11112", "22223", 200);
            Libro l3 = new Libro("Codebar repetido", "García Lorca, Federico", 2003, "11113", "22222", 3);
            Libro l4 = new Libro("ISBN repetido", "García Lorca, Federico", 2003, "11112", "22224", 2);
            Libro l5 = new Libro("Yerma", "García Lorca, Federico", 2003, "55555", "66666", 1);

            // MAPAS 
            Mapa m1 = new Mapa("Buenos Aires", "Instituto Geográfico de Buenos Aires", 2005, "", "99999", 30, 15); //450
            Mapa m2 = new Mapa("Mendoza", "Instituto Geográfico de Mendoza", 2008, "", "99990", 100, 30); //300
            Mapa m3 = new Mapa("Santa Fe", "Instituto Geográfico de Santa Fe", 2010, "", "99991", 80, 30); //2400
            Mapa m4 = new Mapa("Corrientes", "Instituto Geográfico de Corrientes", 2013, "", "99992", 50, 25); //1250
            Mapa m5 = new Mapa("Barcode repetido", "Instituto Geográfico", 2015, "", "99999", 40, 15); //600
            Mapa m6 = new Mapa("Buenos Aires", "Instituto Geográfico de Buenos Aires", 2005, "", "99993", 30, 15); //200

            // ESCANERS
            Escaner l = new Escaner("HP", Escaner.TipoDoc.libro);
            Escaner m = new Escaner("HP", Escaner.TipoDoc.mapa);

            bool pudo = l + l1;
            pudo = l + l2;
            pudo = l + l3;
            pudo = l + l4;
            pudo = l + l5;
            pudo = m + m1;
            pudo = m + m2;
            pudo = m + m3;
            pudo = m + m4;
            pudo = m + m5;
            pudo = m + m6;
            // Cambiar los estados
            l1.AvanzarEstado(); // En Revisión
            l1.AvanzarEstado(); // En Revisión
            l2.AvanzarEstado(); // En Revisión
            l2.AvanzarEstado(); // En Revisión
            m2.AvanzarEstado(); // En Escáner
            m3.AvanzarEstado(); // Terminado
            m3.AvanzarEstado(); // Terminado
            m3.AvanzarEstado(); // Terminado
            m4.AvanzarEstado(); // Terminado
            m4.AvanzarEstado(); // Terminado
            m4.AvanzarEstado(); // Terminado
            m4.AvanzarEstado(); // Terminado
            m4.AvanzarEstado(); // Terminado

            // Generar informes
            Informes.MostrarDistribuidos(l, out int extensionLibroDistr, out int cantidadLibroDistr, out string resumenLibroDistr);
            Informes.MostrarEnEscaner(l, out int extensionLibroEnEsc, out int cantidadLibroEnEsc, out string resumenLibroEnEsc);
            Informes.MostrarEnRevision(l, out int extensionLibroEnRev, out int cantidadLibroEnRev, out string resumenLibroEnRev);
            Informes.MostrarTerminados(l, out int extensionLibroTerminado, out int cantidadLibroTerminado, out string resumenLibroTerminado);

            Informes.MostrarDistribuidos(m, out int extensionMapaDistr, out int cantidadMapaDistr, out string resumenMapaDistr);
            Informes.MostrarEnEscaner(m, out int extensionMapaEnEsc, out int cantidadMapaEnEsc, out string resumenMapaEnEsc);
            Informes.MostrarEnRevision(m, out int extensionMapaEnRev, out int cantidadMapaEnRev, out string resumenMapaEnRev);
            Informes.MostrarTerminados(m, out int extensionMapaTerminado, out int cantidadMapaTerminado, out string resumenMapaTerminado);
            
            try
            {
                pudo = m + l1; // Debería fallar y lanzar excepción
                pudo = l + m1; // Debería fallar y lanzar excepción
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            int puntos = 0;

            if (extensionLibroDistr == 0) { puntos += 3; }
            if (cantidadLibroDistr == 0) { puntos += 1; }
            if (resumenLibroDistr == "") { puntos += 1; }

            if (extensionLibroEnEsc == 0) { puntos += 3; }
            if (cantidadLibroEnEsc == 0) { puntos += 1; }
            if (resumenLibroEnEsc == "") { puntos += 1; }

            if (extensionLibroEnRev == l1.NumPaginas + l2.NumPaginas) { puntos += 3; }
            if (cantidadLibroEnRev == 2) { puntos += 1; }
            if (resumenLibroEnRev == l1.ToString() + l2.ToString()) { puntos += 1; }

            if (extensionLibroTerminado == 0) { puntos += 3; }
            if (cantidadLibroTerminado == 0) { puntos += 1; }
            if (resumenLibroTerminado == "") { puntos += 1; }

            if (extensionMapaDistr == m1.Superficie) { puntos += 3; }
            if (cantidadMapaDistr == 1) { puntos += 1; }
            if (resumenMapaDistr == m1.ToString()) { puntos += 1; }

            if (extensionMapaEnEsc == m2.Superficie) { puntos += 3; }
            if (cantidadMapaEnEsc == 1) { puntos += 1; }
            if (resumenMapaEnEsc == m2.ToString()) { puntos += 1; }

            if (extensionMapaEnRev == 0) { puntos += 3; }
            if (cantidadMapaEnRev == 0) { puntos += 1; }
            if (resumenMapaEnRev == "") { puntos += 1; }

            if (extensionMapaTerminado == m3.Superficie + m4.Superficie) { puntos += 3; }
            if (cantidadMapaTerminado == 2) { puntos += 1; }
            if (resumenMapaTerminado == m3.ToString() + m4.ToString()) { puntos += 1; }

            Console.WriteLine($"Puntos: {puntos} / 40");

            Console.WriteLine("LIBROS DISTRIBUIDOS");
            Console.WriteLine($"Cantidad de libros ya distribuidos: {cantidadLibroDistr}.");
            Console.WriteLine($"Cantidad de páginas ya distribuidas: {extensionLibroDistr}.");
            Console.WriteLine(resumenLibroDistr);
            Console.WriteLine("---------------------");

            Console.WriteLine("LIBROS EN ESCANER");
            Console.WriteLine($"Cantidad de libros en el escáner: {cantidadLibroEnEsc}.");
            Console.WriteLine($"Cantidad de páginas en el escáner: {extensionLibroEnEsc}.");
            Console.WriteLine(resumenLibroEnEsc);
            Console.WriteLine("---------------------");

            Console.WriteLine("LIBROS EN REVISIÓN");
            Console.WriteLine($"Cantidad de libros en el escáner: {cantidadLibroEnRev}.");
            Console.WriteLine($"Cantidad de páginas en el escáner: {extensionLibroEnRev}.");
            Console.WriteLine(resumenLibroEnRev);
            Console.WriteLine("---------------------");

            Console.WriteLine("LIBROS TERMINADOS");
            Console.WriteLine($"Cantidad de mapas en el escáner: {cantidadLibroTerminado}.");
            Console.WriteLine($"Cantidad de cm2 en el escáner: {extensionLibroTerminado}.");
            Console.WriteLine(resumenLibroTerminado);
            Console.WriteLine("---------------------");

            Console.WriteLine("MAPAS DISTRIBUIDOS");
            Console.WriteLine($"Cantidad de mapas ya distribuidos: {cantidadMapaDistr}.");
            Console.WriteLine($"Cantidad de cm2 ya distribuidos: {extensionMapaDistr}.");
            Console.WriteLine(resumenMapaDistr);
            Console.WriteLine("---------------------");

            Console.WriteLine("MAPAS EN ESCANER");
            Console.WriteLine($"Cantidad de mapas en el escáner: {cantidadMapaEnEsc}.");
            Console.WriteLine($"Cantidad de cm2 en el escáner: {extensionMapaEnEsc}.");
            Console.WriteLine(resumenMapaEnEsc);
            Console.WriteLine("---------------------");

            Console.WriteLine("MAPAS EN REVISIÓN");
            Console.WriteLine($"Cantidad de mapas en el escáner: {cantidadMapaEnRev}.");
            Console.WriteLine($"Cantidad de cm2 en el escáner: {extensionMapaEnRev}.");
            Console.WriteLine(resumenMapaEnRev);
            Console.WriteLine("---------------------");

            Console.WriteLine("MAPAS TERMINADOS");
            Console.WriteLine($"Cantidad de mapas en el escáner: {cantidadMapaTerminado}.");
            Console.WriteLine($"Cantidad de cm2 en el escáner: {extensionMapaTerminado}.");
            Console.WriteLine(resumenMapaTerminado);
            Console.WriteLine("---------------------");

            Console.ReadKey();
        }
    }
}
