using System;
using System.Collections.Generic;
using System.Security.Cryptography;

/*
Liga deportiva C#

El presidente de una asociación deportiva local nos ha solicitado el desarrollo de una aplicación para gestionar la información de su liga.
Actualmente, la liga presenta problemas en:
 - El control de jugadores habilitados para competir
 - La organización de equipos por categoría
 - La generación de listados confiables para control administrativo.
Por este motivo, requiere un sistema que permita administrar jugadores 
y equipos, validando la información 
y generando reportes útiles para la toma de decisiones.

Objetivos del sistema
Desarrollar una aplicación que permita:
 - Gestionar equipos y jugadores (ABM)
 - Validar datos ingresados por el usuario
 - Generar listados según distitntos criterios

Estructura de la liga
----------------------

La liga se organiza en las siguientes categorías:
 - Infantiles(h / 13)
 - Cadetes(13 ~16)
 - Juveniles(16~18)
 - Primera(+18)
 - Veteranos(+35)

Condiciones:
------------
- Un club puede tener múltiples equipos por categoría
los equipos se nombran automáticamente:
Ejemplo: “Club Norte A”, “Club Norte B”, etc.
- Cada equipo pertenece a una única categoría


Reglas:
-------
**No se permiten DNIs duplicados EN EL ALTA DE JUGADORES NO SE PERMITEN DNI'S DUPLICADOS - CONTROLADO OK
**La edad del jugador debe ser coherente con la categoría del equipo 
**Un jugador puede estar en más de un equipo dentro del mismo club siempre que la edad se lo permita
**No se permite eliminar un equipo que tenga jugadores asignados
**Un equipo debe tener al menos 9 jugadores (excepto categoría veteranos que el mínimo es 10)
**El sistema debe poder detectar y reportar equipos que no cumplen con este requisito

*/

namespace TP1
{
    internal class Program
    {
        /*Gestión de jugadores
         * --------------------
         * Cada jugador debe tener:
         *  - DNI(único)
         *  - Nombre
         *  - Apellido
         *  - Edad
         *  - Equipo/s asignado/s 
         *  - Seguro(Si/No)
         *  - Afiliación(Si/No)
         */

        struct Jugador
        {
            public int DNI;
            public string Nombre;
            public string Apellido;
            public int Edad;
            public string Categoria;
            public List<string> Equipos;
            public bool Seguro;
            public bool Afiliado;
        }

        static List<Jugador> jugadores = new List<Jugador>();

        struct Equipo
        {
            public string Nombre;
            public string Club;
            public string Categoria;
        }

        static List<Equipo> equipos = new List<Equipo>();

        static void Main(string[] args)
        {
            Console.WriteLine("- GRUPO 33 - TEMA 2");
            Console.WriteLine("- Integrantes:");
            Console.WriteLine("- Batalla Córdoba María Florencia");
            Console.WriteLine("- Morales María Rosa");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Bienvenido al sistema");
            Console.WriteLine("de gestión de liga deportiva");
            Console.WriteLine("-------------------------");
            MostrarMenuPrincipal();
        }
        static void MostrarMenuPrincipal()
        {
            while (true)
            {
                Console.WriteLine("1 - ABM Equipos");
                Console.WriteLine("2 - ABM Jugadores");
                Console.WriteLine("3 - Funcionalidades");
                Console.WriteLine("4 - Reportes");
                Console.WriteLine("5 - Salir");
                Console.WriteLine("-------------------------");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("Alta, baja y modificación de Equipos");
                        Console.WriteLine("-------------------------");
                        MenuABMEquipos();
                        break;
                    case "2":
                        Console.WriteLine("Alta, baja y modificación de Jugadores");
                        Console.WriteLine("-------------------------");
                        MenuABMJugadores();
                        break;
                    case "3":
                        MenuFuncionalidades();
                        break;
                    case "4":
                        Console.WriteLine("Reportes adicionales");
                        Console.WriteLine("-------------------------");
                        MenuReportes();
                        break;
                    case "5":

                        return;
                    default:
                        Console.WriteLine("Ingrese una opción válida");
                        break;
                }
            }
        }

        /*Funcionalidades Base(obligatorias)
        -----------------------------------
        */

        /*- ABM de Equipos
        * Alta(con asignación automática de nombre)
        * Baja
        * Modificación*/

        //-------------------------------------ABM de equipos
        static void MenuABMEquipos()
        {
            while (true)
            {
                Console.WriteLine("¿Que desea hacer?");
                Console.WriteLine("1 - Alta de equipos");
                Console.WriteLine("2 - Baja de equipos");
                Console.WriteLine("3 - Modificacion de equipos");
                Console.WriteLine("4 - Salir");
                Console.WriteLine("-------------------------");
                string opcionAMB = Console.ReadLine();

                switch (opcionAMB)
                {
                    case "1":
                        Console.WriteLine("-------------------------");
                        AltaEquipos();
                        break;
                    case "2":
                        Console.WriteLine("-------------------------");
                        BajaEquipos();
                        break;
                    case "3":
                        Console.WriteLine("-------------------------");
                        ModificarEquipos();
                        break;
                    case "4":
                        Console.WriteLine("Usted salió del menú equipos");
                        return;
                    default:
                        Console.WriteLine("Ingrese una opción válida");
                        break;
                }

            }
        }
        static string SeleccionarCategoria()
        {
            while (true)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Seleccione una categoria:");
                Console.WriteLine("1 - Infantiles");
                Console.WriteLine("2 - Cadetes");
                Console.WriteLine("3 - Juveniles");
                Console.WriteLine("4 - Primera");
                Console.WriteLine("5 - Veteranos");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        return "Infantiles";

                    case "2":
                        return "Cadetes";

                    case "3":
                        return "Juveniles";

                    case "4":
                        return "Primera";

                    case "5":
                        return "Veteranos";

                    default:
                        Console.WriteLine("Ingrese una opción válida");
                        break;
                }
            }
        }
        static void AltaEquipos()
        {
            Console.WriteLine("Alta de equipos");
            Equipo equipo;

            //CLUB
            Console.WriteLine("Ingrese el nombre del club:");
            equipo.Club = Console.ReadLine();

            //CATEGORIA
            while (true)
            {
                equipo.Categoria = SeleccionarCategoria();

                //GENERAR NOMBRE AUTOMATICO
                int contador = 0;

                foreach (var e in equipos)
                {
                    if (
                        e.Club.ToUpper() == equipo.Club.ToUpper() &&
                        e.Categoria.ToUpper() == equipo.Categoria.ToUpper()
                       )
                    {
                        contador++;
                    }
                }

                char letra = (char)('A' + contador);

                equipo.Nombre = equipo.Club + " " + letra;

                equipos.Add(equipo);

                Console.WriteLine("Equipo agregado correctamente");
                Console.WriteLine("-------------------------");
                Console.WriteLine("Nombre generado: " + equipo.Nombre);
                Console.WriteLine("Categoría: " + equipo.Categoria);
                Console.WriteLine("-------------------------");
                break;
            }
        }
        static void BajaEquipos()
        {
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos cargados aún");
                return;
            }
            Console.WriteLine("Estos son los equipos disponibles: ");
            for (int i = 0; i < equipos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - Club: {equipos[i].Nombre} - Categoria: {equipos[i].Categoria}");
                Console.WriteLine("-------------------");
            }

            Console.WriteLine("Ingrese el numero del equipo a eliminar: ");
            int opcion;

            while (!int.TryParse(Console.ReadLine(), out opcion) ||
                   opcion < 1 ||
                   opcion > equipos.Count)
            {
                Console.WriteLine("Ingrese un numero valido:");
                Console.WriteLine("-------------------");
            }

            int indice = opcion - 1;

            //VALIDAR SI TIENE JUGADORES
            bool tieneJugadores = false;

            foreach (var j in jugadores)
            {
                if (j.Equipos.Contains(equipos[indice].Nombre))
                {
                    tieneJugadores = true;
                    break;
                }
            }

            if (tieneJugadores)
            {
                Console.WriteLine("No se puede eliminar porque el equipo tiene jugadores");
                Console.WriteLine("-------------------");
            }
            else
            {
                Console.WriteLine("¿Está realmente seguro? S/N");
                string seguro = Console.ReadLine();

                if (seguro.ToUpper() == "S")
                {
                    equipos.RemoveAt(indice);
                    Console.WriteLine("-------------------");
                    Console.WriteLine("Equipo eliminado correctamente");
                    Console.WriteLine("-------------------");
                }
                else if (seguro.ToUpper() == "N")
                {
                    Console.WriteLine("-------------------");
                    Console.WriteLine("El equipo no se eliminó");
                    Console.WriteLine("-------------------");
                }
                else
                {
                    Console.WriteLine($"{seguro} - No es un ingreso válido");
                    Console.WriteLine("-------------------");
                }

            }
        }

        static void ModificarEquipos()
        {
            int index = -1;
            Equipo e = equipos[index];
            Console.WriteLine("Equipos disponibles:");
            for (int i = 0; i < equipos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - Nombre:{e.Nombre[i]} - Categoría: {e.Categoria[i]}");
            }
            Console.WriteLine("-------------------------");
            Console.WriteLine("Ingrese el numero del equipo a modificar:");
            int opcion;
            /*if(!int.TryParse(Console.ReadLine(), out opcion)
                {
                }*/


            // COPIA DEL EQUIPO

            Console.WriteLine("Categoria actual: " + e.Categoria);

            Console.WriteLine("Desea modificar la categoria? S/N");
            string modificar = Console.ReadLine().ToUpper();

            if (modificar == "S")
            {
                e.Categoria = SeleccionarCategoria();
            }

            // GUARDAR CAMBIOS
            equipos[index] = e;

            Console.WriteLine("Equipo modificado correctamente");
            Console.WriteLine("-------------------------");

        }

        /*
        - ABM de Jugadores
        * Alta (con validaciones)
        * Baja
        * Modificación*/

        //-------------------------------------ABM de Jugadores
        static void MenuABMJugadores()
        {
            while (true)
            {
                Console.WriteLine("¿Que desea hacer?");
                Console.WriteLine("1 - Alta de jugadores");
                Console.WriteLine("2 - Baja de jugadores");
                Console.WriteLine("3 - Modificacion de jugadores");
                Console.WriteLine("4 - Salir");
                Console.WriteLine("-------------------------");
                string opcionAMB = Console.ReadLine();

                switch (opcionAMB)
                {
                    case "1":
                        Console.WriteLine("-------------------------");
                        AltaJugador();
                        break;
                    case "2":
                        Console.WriteLine("-------------------------");
                        BajaJugador();
                        break;
                    case "3":
                        Console.WriteLine("-------------------------");
                        ModificarJugador();
                        break;
                    case "4":
                        Console.WriteLine("Usted salió del sistema de modificación de jugadores");
                        return;
                    default:
                        Console.WriteLine("Ingrese una opción válida");
                        break;
                }

            }
        }

        static void AsignarEquipos(ref Jugador jugador)
        {
            Console.WriteLine("¿A cuantos equipos lo asignará?:");
            int cantidad;
            while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Ingrese un número válido:");
                Console.WriteLine("-------------------");
            }
            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine($"Ingrese el numero del equipo {i + 1} a asignar:");
                int opcion;
                while (!int.TryParse(Console.ReadLine(), out opcion)
                    || opcion < 1
                    || opcion > equipos.Count
                    || equipos[opcion - 1].Categoria.ToUpper() != jugador.Categoria.ToUpper())
                {
                    Console.WriteLine("Ingrese un número válido:");
                    Console.WriteLine("-------------------");
                }
                jugador.Equipos.Add(equipos[opcion - 1].Nombre);
            }

            Console.WriteLine("Equipos asignados correctamente");
            Console.WriteLine("-------------------");
            for (int i = 0; i < jugador.Equipos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - Equipo:{equipos[i].Nombre}");
            }
        }
        static int ValidarDNI()
        {
            int dni;
            while (true)
            {
                Console.WriteLine("Ingrese el DNI del jugador:");
                while (!int.TryParse(Console.ReadLine(), out dni))
                {
                    Console.WriteLine("Ingrese solo números:");
                }
                if (dni <= 0)
                {
                    Console.WriteLine("El DNI debe ser un número positivo");
                }
                else if (dni < 10000000 || dni > 99999999)
                {
                    Console.WriteLine("El DNI debe tener entre 8 dígitos");
                }
                else
                {
                    return dni;
                }
            }
        }
        static void AltaJugador()
        {
            Jugador jugador = new Jugador();
            Equipo equipo = new Equipo();

            jugador.DNI = ValidarDNI();

            //VALIDACIÓN DE DNI
            bool existe = false;
            foreach (var j in jugadores)
            {
                if (j.DNI == jugador.DNI)
                {
                    existe = true;
                    break;
                }
            }
            if (existe)
            {
                Console.WriteLine("El DNI ya está cargado");
                Console.WriteLine("--------------------------------");
                return;
            }

            //INGRESO DE NOMBRE
            Console.WriteLine("Ingrese el nombre del jugador:");
            jugador.Nombre = Console.ReadLine();

            //INGRESO DE APELLIDO
            Console.WriteLine("Ingrese el apellido del jugador:");
            jugador.Apellido = Console.ReadLine();

            //INGRESO DE EDAD
            Console.WriteLine("Ingrese la edad del jugador:");
            //Para que no se rompa si el usuario pone letra pusimos el TryParse
            while (!int.TryParse(Console.ReadLine(), out jugador.Edad))
            {
                Console.WriteLine("Ingrese una edad válida:");
            }
            jugador.Categoria = ClasificarCategoria(jugador.Edad);

            Console.WriteLine($"Su categoría es: {jugador.Categoria}");
            Console.WriteLine("-------------------");

            //GUARDAR LOS INDICES VALIDOS
            List<int> equiposDisponibles = new List<int>();

            if (equipos.Count > 0)
            {
                Console.WriteLine("Equipos disponibles en su categoría:");
                for (int i = 0; i < equipos.Count; i++)
                {
                    if (equipos[i].Categoria == jugador.Categoria)
                    {
                        Console.WriteLine($"{i + 1} - Equipo:{equipos[i].Nombre}");
                        equiposDisponibles.Add(i);
                    }
                }
                jugador.Equipos = new List<string>();
                //INICIALIZAR LISTA DE EQUIPOS 
                AsignarEquipos(ref jugador);
            }

            //SEGURO
            while (true)
            {
                Console.Write("Tiene seguro? S/N: ");
                string seguro = Console.ReadLine().ToUpper();

                if (seguro == "S")
                {
                    jugador.Seguro = true;
                    break;
                }
                else if (seguro == "N")
                {
                    jugador.Seguro = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Ingrese S o N");
                }
            }


            //AFILIADO
            while (true)
            {
                Console.Write("Está afiliado? S/N: ");
                string afiliado = Console.ReadLine().ToUpper();

                if (afiliado == "S")
                {
                    jugador.Afiliado = true;
                    break;
                }
                else if (afiliado == "N")
                {
                    jugador.Afiliado = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Ingrese S o N");
                }
            }

            jugadores.Add(jugador);

            Console.WriteLine("-------------------------");
            Console.WriteLine("Jugador agregado correctamente");
            Console.WriteLine("-------------------------");
        }

        static string ClasificarCategoria(int edad)
        {
            if (edad >= 2 && edad < 13)
            {
                return "Infantiles";
            }
            else if (edad >= 13 && edad < 16)
            {
                return "Cadetes";
            }
            else if (edad >= 16 && edad < 18)
            {
                return "Juveniles";
            }
            else if (edad >= 18 && edad < 35)
            {
                return "Primera";
            }
            else if (edad >= 35 && edad < 100)
            {
                return "Veteranos";
            }

            return "";
        }

        static void BajaJugador()
        {
            //ACA VA BAJA DE JUGADORES
            Console.WriteLine("Baja de jugadores");
            Console.WriteLine("Ingrese el DNI del jugador a eliminar :");
            int dni = int.Parse(Console.ReadLine());

            int index = -1;

            //BUSCAMOS JUGADOR
            for (int i = 0; i < jugadores.Count; i++)
            {
                if (jugadores[i].DNI == dni)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("No se encontró ningun jugador con ese DNI");
                Console.WriteLine("-------------------------");
            }
            else
            {
                Console.WriteLine("Jugador: " + jugadores[index].Nombre + " " + jugadores[index].Apellido);
                Console.Write("¿Seguro que querés eliminarlo? (S/N): ");
                string confirmacion = Console.ReadLine().ToUpper();

                if (confirmacion == "S")
                {
                    jugadores.RemoveAt(index);
                    Console.WriteLine("Jugador eliminado correctamente");
                }
                else
                {
                    Console.WriteLine("Operación cancelada");
                }
            }

            Console.WriteLine("-------------------------");
        }
        static void ModificarJugador()
        {
            //ACA VA MODIFICACIÓN DE JUGADORES
            Console.WriteLine("Ingrese el DNI del jugador que desea modificar:");
            int dni = int.Parse(Console.ReadLine());

            int index = -1;

            //VALIDACIÓN POOR DNI                            
            for (int i = 0; i < jugadores.Count; i++)
            {
                if (jugadores[i].DNI == dni)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                Console.WriteLine("El DNI no existe en el sistema");

            }
            else
            {
                Console.WriteLine("El DNI pertenece a : " + jugadores[index].Nombre + " " + jugadores[index].Apellido);
                Console.WriteLine("--------------------------------");

                Console.WriteLine("Desea modificarlo? S/N");
                string modificarlo = Console.ReadLine().ToUpper();

                if (modificarlo == "S")
                {
                    //Esto lohicimos por que el struct no nos deja modificar directamente por lo que nos toca hacer copia del jugador a modificar
                    Jugador j = jugadores[index];

                    Console.WriteLine("Usted ingresó a la modificación");
                    Console.WriteLine("-------------------------");

                    //INGRESO MODIFICACION DE NOMBRE
                    Console.WriteLine("Nombre actual: " + j.Nombre);
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Ingrese el nuevo nombre del jugador o enter si no desea modificarlo:");
                    Console.Write("Nuevo nombre: ");
                    string nuevoNombre = Console.ReadLine();

                    if (!string.IsNullOrEmpty(nuevoNombre))
                    {
                        j.Nombre = nuevoNombre; ;
                    }
                    else
                    {
                        Console.WriteLine("El nombre no se ha modificado");
                        Console.WriteLine("--------------------------------");
                    }

                    //INGRESO MODIFICACION DE APELLIDO
                    Console.WriteLine("Apellido actual: " + j.Apellido);
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Ingrese el nuevo apellido del jugador o enter si no desea modificarlo:");
                    Console.Write("Nuevo apellido: ");
                    string nuevoApellido = Console.ReadLine();

                    if (!string.IsNullOrEmpty(nuevoApellido))
                    {
                        j.Apellido = nuevoApellido;
                    }
                    else
                    {
                        Console.WriteLine("El apellido no se ha modificado");
                        Console.WriteLine("--------------------------------");
                    }

                    //INGRESO MODIFICACION DE EDAD
                    Console.WriteLine("Edad actual: " + j.Edad);
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Ingrese la nueva edad del jugador o ENTER si no desea modificarla:");
                    Console.Write("Nueva Edad: ");
                    string nuevaEdad = Console.ReadLine();
                    Console.WriteLine("--------------------------------");

                    if (!string.IsNullOrEmpty(nuevaEdad))
                    {
                        j.Edad = int.Parse(nuevaEdad);
                    }
                    else
                    {
                        Console.WriteLine("No se modificó la edad");
                        Console.WriteLine("--------------------------------");
                    }

                    //INGRESO A MODIFICACIÓN DE EQUIPOS
                    Console.WriteLine("Equipos asignados:");
                    for (int i = 0; i < j.Equipos.Count; i++)
                    {
                        Console.WriteLine("Equipo " + (i + 1) + ": " + j.Equipos[i]);
                    }
                    Console.WriteLine("--------------------------------");

                    Console.WriteLine("Desea modificarlos? S/N");
                    string modificar = Console.ReadLine().ToUpper();

                    if (modificar == "S")
                    {
                        //Esto reemplaza la lista
                        j.Equipos = new List<string>();

                        Console.WriteLine("Cantidad de equipos a asignar: ");
                        int cantidad = int.Parse(Console.ReadLine());

                        Console.WriteLine("--------------------------------");
                    }
                    else if (modificar == "N")
                    {
                        Console.WriteLine("Los equipos del jugador, no se han modificado");
                        Console.WriteLine("--------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("Ingrese S o N");
                    }

                    //SEGURO
                    Console.WriteLine("Seguro actual: " + j.Seguro);
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Desea modificar el seguro? S/N:");
                    string nuevoSeguro = Console.ReadLine().ToUpper();

                    if (nuevoSeguro == "S")
                    {
                        j.Seguro = !j.Seguro;
                        Console.WriteLine("Seguro actual: " + j.Seguro);
                        Console.WriteLine("--------------------------------");

                    }
                    else if (nuevoSeguro == "N")
                    {
                        Console.WriteLine("El seguro no se ha modificado");
                        Console.WriteLine("--------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("Ingrese S o N");
                    }

                    //AFILIADO
                    Console.WriteLine("Afiliación actual: " + j.Afiliado);
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Desea modificar la afiliación? S/N:");
                    string nuevaAfiliacion = Console.ReadLine().ToUpper();

                    if (nuevaAfiliacion == "S")
                    {
                        j.Afiliado = !j.Afiliado;
                        Console.WriteLine("Afiliación actual: " + j.Afiliado);
                        Console.WriteLine("--------------------------------");

                    }
                    else if (nuevaAfiliacion == "N")
                    {
                        Console.WriteLine("La afiliacion no se ha modificado");
                        Console.WriteLine("--------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("Ingrese S o N");
                    }
                    jugadores[index] = j;
                }
                else if (modificarlo == "N")
                {
                    Console.WriteLine("El jugador, no se han modificado");
                    Console.WriteLine("--------------------------------");

                }
                else
                {
                    Console.WriteLine("Ingrese S o N - No sea tonto ");
                }
            }
        }


        /*Tema B – Control Organizativo de Equipos
        ------------------------------------------
        El cliente necesita evaluar la estructura de sus equipos.

        * Funcionalidades
        - Listar jugadores afiliados
        - Listar jugadores por:
        * Equipo o
        * Club*/

        //-------------------------------------Funcionalidades
        static void MenuFuncionalidades()
        {
            while (true)
            {
                Console.WriteLine("Menú funcionalidades");
                Console.WriteLine("1 - Lista de jugadores afiliados");
                Console.WriteLine("2 - Lista de jugadores por equipos");
                Console.WriteLine("3 - Salir");
                Console.WriteLine("-------------------------");
                string opcionFuncionalidad = Console.ReadLine();

                switch (opcionFuncionalidad)
                {
                    case "1":
                        ListarPorAfiliados();
                        //Console.WriteLine("-------------------------");
                        break;
                    case "2":
                        ListarPorEquipo();
                        //Console.WriteLine("-------------------------");
                        break;
                    case "3":
                        Console.WriteLine("Usted salió del sistema de funcionalidades");
                        return;
                    default:
                        Console.WriteLine("Ingrese una opción válida");
                        break;
                }

            }
        }
        static void ListarPorAfiliados()
        {
            Console.WriteLine("Jugadores afiliados:");

            int i = 1;
            foreach (var j in jugadores)
            {
                if (j.Afiliado)
                {
                    Console.WriteLine($"{i} - {j.Nombre} {j.Apellido}");
                    i++;
                }
            }
            Console.WriteLine("-------------------------");

        }
        static void ListarPorEquipo()
        {
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos cargados");
            }
            else
            {
                foreach (var equipoListado in equipos)
                {
                    Console.WriteLine("-------------------");
                    Console.WriteLine("Nombre: " + equipoListado.Nombre);
                    Console.WriteLine("Club: " + equipoListado.Club);
                    Console.WriteLine("Categoria: " + equipoListado.Categoria);

                }
            }
        }

        /*
         * Reportes adicionales
        - Cantidad de jugadores por equipo
        - Equipo con mayor cantidad de jugadores
        - Equipos que no alcanzan el cupo mínimo requerido
        - Equipos sin jugadores*/

        //-------------------------------------Reportes
        static void MenuReportes()
        {
            while (true)
            {
                Console.WriteLine("1 - Cantidad de jugadores por equipo");
                Console.WriteLine("2 - Equipo con mayor cantidad de jugadores");
                Console.WriteLine("3 - Equipos que no alcanzan el cupo mínimo requerido");
                Console.WriteLine("4 - Equipos sin jugadores");
                Console.WriteLine("5 - Salir");
                Console.WriteLine("-------------------------");
                string opcionReportes = Console.ReadLine();

                switch (opcionReportes)
                {
                    case "1":
                        JugadoresPorEquipo();
                        break;
                    case "2":
                        EquipoConMasCantidadDeJugadores();
                        break;
                    case "3":
                        EquipoQueNoAlcanzanElCupo();
                        break;
                    case "4":
                        EquipoSinJugadores();
                        break;
                    case "5":
                        Console.WriteLine("Usted salió del sistema de reportes");
                        return;
                    default:
                        Console.WriteLine("Ingrese una opción válida");
                        break;
                }

            }
        }
        static void JugadoresPorEquipo()
        {
            foreach (var e in equipos)
            {
                int contador = 0;
                foreach (var j in jugadores)
                {
                    if (j.Equipos.Contains(e.Nombre))
                    {
                        contador++;
                    }

                }
                Console.WriteLine($"{e.Nombre}: {contador} jugadores");
            }
            Console.WriteLine("-------------------------");

        }
        static void EquipoConMasCantidadDeJugadores()
        {
            string mejorEquipo = "";
            int max = 0;
            foreach (var e in equipos)
            {
                int contador = 0;
                foreach (var j in jugadores)
                {
                    if (j.Equipos.Contains(e.Nombre))
                    {
                        contador++;
                    }
                }
                if (contador > max)
                {
                    max = contador;
                    mejorEquipo = e.Nombre;
                }
            }
            Console.WriteLine($"El equipo con más jugadores es: {mejorEquipo} con {max} jugadores");
            Console.WriteLine("-------------------------");
        }
        static void EquipoQueNoAlcanzanElCupo()
        {
            foreach (var e in equipos)
            {
                int contador = 0;
                foreach (var j in jugadores)
                {
                    if (j.Equipos.Contains(e.Nombre))
                    {
                        contador++;
                    }
                }
                int minimo = (e.Categoria.ToUpper() == "VETERANOS") ? 10 : 9;

                if (contador < minimo)
                {
                    Console.WriteLine($"{e.Nombre} NO cumple el mínimo ({contador}/{minimo})");
                    Console.WriteLine("-------------------------");

                }
            }
        }
        static void EquipoSinJugadores()
        {
            foreach (var e in equipos)
            {
                bool tieneJugadores = false;
                foreach (var j in jugadores)
                {
                    if (j.Equipos.Contains(e.Nombre))
                    {
                        tieneJugadores = true;
                        break;
                    }
                }
                if (!tieneJugadores)
                {
                    Console.WriteLine($"El equipo: {e.Nombre}, no tiene jugadores");
                    Console.WriteLine("-------------------------");

                }
            }
        }
    }
}