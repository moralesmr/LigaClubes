using System.Collections.Generic;
using System;


/*
 TP1 UPSO
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

        /// <summary>
        /// Genera un identificador unico para un equipo
        /// </summary>
        /// <param name="e">Equipo del cual se obtendra el identificador</param>
        /// <returns>Retorna un string con el nombre y categoria del equipo</returns>
        static string ObtenerIdentificadorEquipo(Equipo e)
        {
            return e.Nombre + "|" + e.Categoria;
        }

        /// <summary>
        /// Funcion principal del programa
        /// Muestra el menu inicial del sistema
        /// </summary>
        /// <param name="args">Argumentos enviados por consola</param>
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

        /// <summary>
        /// Muestra el menu principal del sistema
        /// Permite acceder a equipos, jugadores, funcionalidades y reportes
        /// </summary>
        static void MostrarMenuPrincipal()
        {
            while (true)
            {
                Console.WriteLine("¿Que desea hacer?");
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
        ABM de Equipos
        * Alta(con asignación automática de nombre)
        * Baja
        * Modificación*/

        //-------------------------------------ABM de equipos

        /// <summary>
        /// Muestra el menú ABM de equipos
        /// Permite dar de alta, baja y modificar equipos
        /// </summary>
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

        /// <summary>
        /// Permite seleccionar una categoria
        /// </summary>
        /// <returns>Retorna la categoria seleccionada</returns>
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

        /// <summary>
        /// Valida que el nuevo nombre pertenezca al mismo club
        /// </summary>
        /// <param name="nombreActual">Nombre actual del equipo</param>
        /// <param name="nuevoNombre">Nuevo nombre ingresado</param>
        /// <returns>
        /// Retorna true si el equipo pertenece al mismo club
        /// Retorna false si el nombre es invalido
        /// </returns>
        static bool ValidarMismoClub(string nombreActual, string nuevoNombre)
        {
            if (!nombreActual.Contains(" ") || !nuevoNombre.Contains(" "))
            {
                Console.WriteLine("El formato del nombre del equipo es incorrecto.");
                Console.WriteLine("Debe ser mismo club y");
                Console.WriteLine("contener un espacio entre el club y la letra.");
                return false;
            }

            string clubActual = nombreActual.Substring(0, nombreActual.LastIndexOf(" "));
            string nuevoClub = nuevoNombre.Substring(0, nuevoNombre.LastIndexOf(" "));

            return clubActual.ToUpper() == nuevoClub.ToUpper();

        }

        /// <summary>
        /// Da de alta un nuevo equipo
        /// Genera automaticamente el nombre segun club y categoria
        /// </summary>
        static void AltaEquipos()
        {
            Console.WriteLine("Alta de equipos");
            Console.WriteLine("--------------------------------");
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

        /// <summary>
        /// Elimina un equipo del sistema
        /// Valida que no tenga jugadores asignados
        /// </summary>
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
                if (j.Equipos != null && j.Equipos.Contains(ObtenerIdentificadorEquipo(equipos[indice])))
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

        /// <summary>
        /// Modifica el nombre o categoria de un equipo
        /// Actualiza los jugadores asociados
        /// </summary>
        static void ModificarEquipos()
        {
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos cargados");
                Console.WriteLine("-------------------------");
                return;
            }

            Console.WriteLine("Equipos disponibles:");

            for (int i = 0; i < equipos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - Nombre: {equipos[i].Nombre} - Categoria: {equipos[i].Categoria}");
            }

            Console.WriteLine("-------------------------");
            Console.WriteLine("Ingrese el numero del equipo a modificar:");

            int opcion;

            while (!int.TryParse(Console.ReadLine(), out opcion) ||
                   opcion < 1 ||
                   opcion > equipos.Count)
            {
                Console.WriteLine("Ingrese una opcion valida:");
            }

            int index = opcion - 1;

            // COPIA DEL EQUIPO
            Equipo e = equipos[index];

            Console.WriteLine("Equipo seleccionado:");
            Console.WriteLine("Nombre actual: " + e.Nombre);
            Console.WriteLine("Categoria actual: " + e.Categoria);
            Console.WriteLine("-------------------------");

            Console.WriteLine("¿Desea modificar el nombre? S/N");
            string modificarNombre = Console.ReadLine().ToUpper();

            if (modificarNombre == "S")
            {
                while (true)
                {
                    Console.WriteLine("Ingrese el nuevo nombre:");
                    string nuevoNombre = Console.ReadLine();
                    Console.WriteLine("-------------------------");
                    bool mismoClub = ValidarMismoClub(e.Nombre, nuevoNombre);
                    if (mismoClub)
                    {
                        string nombreAnterior = ObtenerIdentificadorEquipo(e);
                        e.Nombre = nuevoNombre;
                        for (int i = 0; i < jugadores.Count; i++)
                        {
                            for (int k = 0; k < jugadores[i].Equipos.Count; k++)
                            {
                                if (jugadores[i].Equipos[k] == nombreAnterior)
                                {
                                    jugadores[i].Equipos[k] = nuevoNombre + "|" + e.Categoria;
                                }
                            }
                        }
                        equipos[index] = e;
                        Console.WriteLine("Nombre modificado correctamente");
                        Console.WriteLine("-------------------------");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("No se puede cambiar el nombre del club del equipo");
                        Console.WriteLine("-------------------------");
                    }
                }
            }
            else if (modificarNombre == "N")
            {
                Console.WriteLine("No se cambió el nombre");
                Console.WriteLine("-------------------------");
            }
            else
            {
                Console.WriteLine("Opcion invalida");
                Console.WriteLine("-------------------------");
            }

            Console.WriteLine("¿Desea modificar la categoría? S/N");
            string modificarCategoria = Console.ReadLine().ToUpper();

            if (modificarCategoria == "S")
            {
                string nuevaCategoria = SeleccionarCategoria();

                // VALIDAR JUGADORES DEL EQUIPO
                bool jugadoresValidos = true;

                foreach (var j in jugadores)
                {
                    if (j.Equipos != null && j.Equipos.Contains(ObtenerIdentificadorEquipo(e)))
                    {
                        string categoria = ClasificarCategoria(j.Edad);

                        if (categoria != nuevaCategoria)
                        {

                            Console.WriteLine($"El jugador {j.Nombre} {j.Apellido}");
                            Console.WriteLine($"No cumple la edad para {nuevaCategoria}");
                            Console.WriteLine("--------------------------------");
                            jugadoresValidos = false;
                        }
                    }
                }

                if (jugadoresValidos)
                {
                    e.Categoria = nuevaCategoria;

                    // GENERAR NUEVO NOMBRE AUTOMÁTICO
                    int contador = 0;

                    foreach (var equipo in equipos)
                    {
                        if (
                            equipo.Club.ToUpper() == e.Club.ToUpper() &&
                            equipo.Categoria.ToUpper() == e.Categoria.ToUpper() &&
                            equipo.Nombre != e.Nombre
                           )
                        {
                            contador++;
                        }
                    }

                    char letra = (char)('A' + contador);

                    string nombreAnterior = ObtenerIdentificadorEquipo(e);

                    e.Nombre = e.Club + " " + letra;

                    // ACTUALIZAR NOMBRE EN JUGADORES
                    for (int i = 0; i < jugadores.Count; i++)
                    {
                        for (int k = 0; k < jugadores[i].Equipos.Count; k++)
                        {
                            if (jugadores[i].Equipos[k] == nombreAnterior)
                            {
                                jugadores[i].Equipos[k] = ObtenerIdentificadorEquipo(e);
                            }
                        }
                    }

                    equipos[index] = e;

                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Equipo modificado correctamente");
                    Console.WriteLine("Nuevo nombre: " + e.Nombre);
                    Console.WriteLine("Nueva categoría: " + e.Categoria);
                    Console.WriteLine("--------------------------------");
                }
                else
                {
                    Console.WriteLine("No se puede modificar el equipo");
                    Console.WriteLine("Hay jugadores incompatibles con la categoria");
                    Console.WriteLine("-------------------------");

                }
            }
            else if (modificarCategoria == "N")
            {
                Console.WriteLine("No se cambió la categoría");
                Console.WriteLine("-------------------------");
            }
            else
            {
                Console.WriteLine("Opcion invalida");
                Console.WriteLine("-------------------------");
            }
        }


        /*
        - ABM de Jugadores
        * Alta (con validaciones)
        * Baja
        * Modificación*/

        //-------------------------------------ABM de Jugadores

        /// <summary>
        /// Muestra el menu ABM de jugadores
        /// Permite alta, baja y modificacion
        /// </summary>
        
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

        /// <summary>
        /// Asigna equipos a un jugador segun su categoria
        /// </summary>
        /// <param name="jugador">Jugador al que se asignaran equipos</param>
        static void AsignarEquipos(ref Jugador jugador)
        {
            jugador.Equipos = new List<string>();

            List<int> equiposDisponibles = new List<int>();

            Console.WriteLine("Equipos disponibles:");
            Console.WriteLine("-------------------");

            // BUSCAR EQUIPOS DE LA MISMA CATEGORIA
            for (int i = 0; i < equipos.Count; i++)
            {
                if (equipos[i].Categoria == jugador.Categoria)
                {
                    equiposDisponibles.Add(i);

                    Console.WriteLine($"{equiposDisponibles.Count} - Equipo: {equipos[i].Nombre} - Categoría: {equipos[i].Categoria}");
                    Console.WriteLine("-------------------");
                }
            }

            // VALIDAR SI HAY EQUIPOS
            if (equiposDisponibles.Count == 0)
            {
                Console.WriteLine("No hay equipos disponibles en su categoría.");
                Console.WriteLine("-------------------");
                return;
            }

            Console.WriteLine("¿A cuántos equipos lo asignará?");
            int cantidad;

            while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Ingrese un número válido:");
            }

            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine($"Ingrese el número del equipo {i + 1}:");

                int opcion;

                while (!int.TryParse(Console.ReadLine(), out opcion) ||
                       opcion < 1 ||
                       opcion > equiposDisponibles.Count)
                {
                    Console.WriteLine("Ingrese una opción válida:");
                }

                int indiceReal = equiposDisponibles[opcion - 1];

                string identificadorEquipo = ObtenerIdentificadorEquipo(equipos[indiceReal]);

                // EVITAR DUPLICADOS
                if (jugador.Equipos.Contains(identificadorEquipo))
                {
                    Console.WriteLine("El jugador ya está asignado a ese equipo.");
                }
                else
                {
                    jugador.Equipos.Add(identificadorEquipo);

                    Console.WriteLine("Equipo asignado correctamente");
                    Console.WriteLine("-------------------");
                }
            }

            Console.WriteLine("Equipos asignados:");
            Console.WriteLine("-------------------");

            for (int i = 0; i < jugador.Equipos.Count; i++)
            {
                string[] partes = jugador.Equipos[i].Split('|');

                Console.WriteLine($"{i + 1} - {partes[0]} - Categoría: {partes[1]}");
            }
        }

        /// <summary>
        /// Valida el DNI ingresado
        /// </summary>
        /// <returns>Retorna un DNI valido</returns>
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

        /// <summary>
        /// Da de alta un nuevo jugador
        /// Valida DNI, edad y asignacion de equipos
        /// </summary>
        static void AltaJugador()
        {
            Jugador jugador = new Jugador();

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
            while (!int.TryParse(Console.ReadLine(), out jugador.Edad) || jugador.Edad <= 0)
            {
                Console.WriteLine("Ingrese una edad válida:");
            }
            jugador.Categoria = ClasificarCategoria(jugador.Edad);

            Console.WriteLine($"Su categoría es: {jugador.Categoria}");
            Console.WriteLine("-------------------");

            //GUARDAR LOS INDICES VALIDOS
            //List<int> equiposDisponibles = new List<int>();

            AsignarEquipos(ref jugador);

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

        /// <summary>
        /// Clasifica una categoria según la edad
        /// </summary>
        /// <param name="edad">Edad del jugador</param>
        /// <returns>Retorna la categoría correspondiente</returns>
        static string ClasificarCategoria(int edad)
        {
            if (edad < 13)
                return "Infantiles";
            else if (edad >= 13 && edad <= 16)
                return "Cadetes";
            else if (edad > 16 && edad < 18)
                return "Juveniles";
            else if (edad >= 18 && edad < 35)
                return "Primera";
            else if (edad >= 35)
                return "Veteranos";

            return "";
        }

        /// <summary>
        /// Elimina un jugador del sistema mediante DNI
        /// </summary>
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

        /// <summary>
        /// Modifica los datos de un jugador
        /// Permite cambiar nombre, apellido, edad, equipos y estados
        /// </summary>
        static void ModificarJugador()
        {
            Console.WriteLine("Ingrese el DNI del jugador que desea modificar:");
            int dni = ValidarDNI();

            int index = -1;

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
                Console.WriteLine("--------------------------------");
                return;
            }

            Console.WriteLine("El DNI pertenece a: " + jugadores[index].Nombre + " " + jugadores[index].Apellido);
            Console.WriteLine("--------------------------------");

            Console.WriteLine("Desea modificarlo? S/N");
            string modificarlo = Console.ReadLine().ToUpper();

            if (modificarlo == "N")
            {
                Console.WriteLine("El jugador no se ha modificado");
                Console.WriteLine("--------------------------------");
                return;
            }

            if (modificarlo != "S")
            {
                Console.WriteLine("Ingrese S o N");
                Console.WriteLine("--------------------------------");
                return;
            }

            Jugador j = jugadores[index];

            Console.WriteLine("Usted ingresó a la modificación");
            Console.WriteLine("-------------------------");

            Console.WriteLine("Nombre actual: " + j.Nombre);
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Ingrese el nuevo nombre del jugador o ENTER si no desea modificarlo:");
            Console.Write("Nuevo nombre: ");
            string nuevoNombre = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevoNombre))
            {
                j.Nombre = nuevoNombre;
            }
            else
            {
                Console.WriteLine("El nombre no se ha modificado");
                Console.WriteLine("--------------------------------");
            }

            Console.WriteLine("Apellido actual: " + j.Apellido);
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Ingrese el nuevo apellido del jugador o ENTER si no desea modificarlo:");
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

            Console.WriteLine("Edad actual: " + j.Edad);
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Ingrese la nueva edad del jugador o ENTER si no desea modificarla:");
            Console.Write("Nueva edad: ");
            string nuevaEdad = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevaEdad))
            {
                int edadNueva;

                while (!int.TryParse(nuevaEdad, out edadNueva) || edadNueva <= 0)
                {
                    Console.WriteLine("Ingrese una edad válida:");
                    nuevaEdad = Console.ReadLine();
                }

                string categoriaNueva = ClasificarCategoria(edadNueva);

                if (categoriaNueva == "")
                {
                    Console.WriteLine("La edad ingresada no corresponde a ninguna categoría");
                    Console.WriteLine("--------------------------------");
                }
                else if (categoriaNueva != j.Categoria)
                {
                    Console.WriteLine("La nueva edad cambia la categoría del jugador.");
                    Console.WriteLine("Categoría anterior: " + j.Categoria);
                    Console.WriteLine("Nueva categoría: " + categoriaNueva);
                    Console.WriteLine("Deberá reasignar equipos compatibles.");
                    Console.WriteLine("--------------------------------");

                    j.Edad = edadNueva;
                    j.Categoria = categoriaNueva;
                    j.Equipos = new List<string>();

                    AsignarEquipos(ref j);
                }
                else
                {
                    j.Edad = edadNueva;
                    Console.WriteLine("Edad modificada correctamente");
                    Console.WriteLine("--------------------------------");
                }
            }
            else
            {
                Console.WriteLine("No se modificó la edad");
                Console.WriteLine("--------------------------------");
            }

            if (j.Equipos != null && j.Equipos.Count > 0)
            {
                Console.WriteLine("Equipos asignados:");

                for (int i = 0; i < j.Equipos.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {j.Equipos[i]}");
                }
            }
            else
            {
                Console.WriteLine("El jugador no tiene equipos asignados.");
            }

            Console.WriteLine("--------------------------------");
            Console.WriteLine("Desea modificar los equipos? S/N");
            string modificarEquipos = Console.ReadLine().ToUpper();

            if (modificarEquipos == "S")
            {
                j.Equipos = new List<string>();
                AsignarEquipos(ref j);
            }
            else if (modificarEquipos == "N")
            {
                Console.WriteLine("Los equipos del jugador no se han modificado");
                Console.WriteLine("--------------------------------");
            }
            else
            {
                Console.WriteLine("Ingrese S o N");
                Console.WriteLine("--------------------------------");
            }

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
                Console.WriteLine("La afiliación no se ha modificado");
                Console.WriteLine("--------------------------------");
            }
            else
            {
                Console.WriteLine("Ingrese S o N");
            }

            jugadores[index] = j;

            Console.WriteLine("Jugador modificado correctamente");
            Console.WriteLine("--------------------------------");
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

        /// <summary>
        /// Muestra el menu de funcionalidades adicionales
        /// </summary>
        static void MenuFuncionalidades()
        {
            while (true)
            {
                Console.WriteLine("Menú funcionalidades: ");
                Console.WriteLine("1 - Lista de jugadores afiliados");
                Console.WriteLine("2 - Lista de jugadores por equipos");
                Console.WriteLine("3 - Salir");
                Console.WriteLine("-------------------------");
                string opcionFuncionalidad = Console.ReadLine();

                switch (opcionFuncionalidad)
                {
                    case "1":
                        ListarPorAfiliados();
                        break;
                    case "2":
                        ListarPorEquipo();
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

        /// <summary>
        /// Lista todos los jugadores afiliados
        /// </summary>
        static void ListarPorAfiliados()
        {
            Console.WriteLine("Jugadores afiliados:");
            Console.WriteLine("-------------------------");

            int contador = 0;
            foreach (var j in jugadores)
            {
                if (j.Afiliado)
                {
                    contador++;
                    Console.WriteLine($"{contador} - {j.Nombre} {j.Apellido}");
                }
            }
            if (contador == 0)
            {
                Console.WriteLine("No hay jugadores afiliados cargados.");
            }
            Console.WriteLine("-------------------------");
        }

        /// <summary>
        /// Lista los jugadores pertenecientes a un equipo
        /// </summary>
        static void ListarPorEquipo()
        {
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos cargados");
                Console.WriteLine("-------------------------");
                return;
            }

            Console.WriteLine("-------------------------");
            Console.WriteLine("Equipos disponibles:");

            for (int i = 0; i < equipos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {equipos[i].Nombre} - Categoría: {equipos[i].Categoria}");
            }

            Console.WriteLine("-------------------------");
            Console.WriteLine("Ingrese el número del equipo:");

            int opcion;

            while (!int.TryParse(Console.ReadLine(), out opcion) ||
                   opcion < 1 ||
                   opcion > equipos.Count)
            {
                Console.WriteLine("Ingrese un número válido:");
            }

            Equipo equipoSeleccionado = equipos[opcion - 1];

            string identificadorEquipo = ObtenerIdentificadorEquipo(equipoSeleccionado);

            Console.WriteLine("-------------------------");
            Console.WriteLine("Jugadores del equipo: " + equipoSeleccionado.Nombre + " - " + equipoSeleccionado.Categoria);
            Console.WriteLine("-------------------------");

            int contador = 0;

            foreach (var j in jugadores)
            {
                if (j.Equipos != null && j.Equipos.Contains(identificadorEquipo))
                {
                    contador++;

                    Console.WriteLine($"{contador} - DNI: {j.DNI} - {j.Nombre} {j.Apellido} - Edad: {j.Edad} - Categoría: {j.Categoria}");
                }
            }

            if (contador == 0)
            {
                Console.WriteLine("Este equipo no tiene jugadores asignados.");
            }

            Console.WriteLine("-------------------------");
        }

        /*
         * Reportes adicionales
        - Cantidad de jugadores por equipo
        - Equipo con mayor cantidad de jugadores
        - Equipos que no alcanzan el cupo mínimo requerido
        - Equipos sin jugadores*/

        //-------------------------------------Reportes

        /// <summary>
        /// Muestra el menu de reportes del sistema
        /// </summary>
        static void MenuReportes()
        {
            while (true)
            {
                Console.WriteLine("¿Qué reporte necesita ver?");
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

        /// <summary>
        /// Muestra la cantidad de jugadores por equipo
        /// </summary>
        static void JugadoresPorEquipo()
        {
            foreach (var e in equipos)
            {
                int contador = 0;

                string identificadorEquipo = ObtenerIdentificadorEquipo(e);

                foreach (var j in jugadores)
                {
                    if (j.Equipos != null && j.Equipos.Contains(identificadorEquipo))
                    {
                        contador++;
                    }
                }

                Console.WriteLine($"{e.Nombre} - {e.Categoria}: {contador} jugadores");
            }

            Console.WriteLine("-------------------------");
        }

        /// <summary>
        /// Muestra el equipo con mayor cantidad de jugadores
        /// </summary>
        static void EquipoConMasCantidadDeJugadores()
        {
            string mejorEquipo = "";
            string mejorCategoria = "";

            int max = -1;

            foreach (var e in equipos)
            {
                int contador = 0;

                string identificadorEquipo = ObtenerIdentificadorEquipo(e);

                foreach (var j in jugadores)
                {
                    if (j.Equipos != null && j.Equipos.Contains(identificadorEquipo))
                    {
                        contador++;
                    }
                }

                if (contador > max)
                {
                    max = contador;
                    mejorEquipo = e.Nombre;
                    mejorCategoria = e.Categoria;
                }
            }

            Console.WriteLine($"El equipo con más jugadores es: {mejorEquipo} - {mejorCategoria} con {max} jugadores");
            Console.WriteLine("-------------------------");
        }

        /// <summary>
        /// Muestra los equipos que no alcanzan el cupo minimo requerido
        /// </summary>
        static void EquipoQueNoAlcanzanElCupo()
        {
            foreach (var e in equipos)
            {
                int contador = 0;

                string identificadorEquipo = ObtenerIdentificadorEquipo(e);

                foreach (var j in jugadores)
                {
                    if (j.Equipos != null && j.Equipos.Contains(identificadorEquipo))
                    {
                        contador++;
                    }
                }

                int minimo = (e.Categoria.ToUpper() == "VETERANOS") ? 10 : 9;

                if (contador < minimo)
                {
                    Console.WriteLine($"{e.Nombre} - {e.Categoria} NO cumple el mínimo ({contador}/{minimo})");
                    Console.WriteLine("-------------------------");
                }
            }
        }

        /// <summary>
        /// Muestra los equipos que no tienen jugadores asignados
        /// </summary>
        static void EquipoSinJugadores()
        {
            foreach (var e in equipos)
            {
                bool tieneJugadores = false;

                string identificadorEquipo = ObtenerIdentificadorEquipo(e);

                foreach (var j in jugadores)
                {
                    if (j.Equipos != null && j.Equipos.Contains(identificadorEquipo))
                    {
                        tieneJugadores = true;
                        break;
                    }
                }

                if (!tieneJugadores)
                {
                    Console.WriteLine($"El equipo: {e.Nombre} - {e.Categoria}, no tiene jugadores");
                    Console.WriteLine("-------------------------");
                }
            }
        }
    }
}