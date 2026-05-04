using System;
using System.Collections.Generic;

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

Gestión de jugadores
--------------------
Cada jugador debe tener:
 - DNI(único)
 - Nombre
 - Apellido
 - Edad
 - Equipo/s asignado/s 
 - Seguro (Si/No)
 - Afiliación (Si/No)

Reglas:
-------
**No se permiten DNIs duplicados EN EL ALTA DE JUGADORES NO SE PERMITEN DNI'S DUPLICADOS
**La edad del jugador debe ser coherente con la categoría del equipo
**Un jugador puede estar en más de un equipo dentro del mismo club siempre que la edad se lo permita
**No se permite eliminar un equipo que tenga jugadores asignados
**Un equipo debe tener al menos 9 jugadores (excepto categoría veteranos que el mínimo es 10)
**El sistema debe poder detectar y reportar equipos que no cumplen con este requisito

Funcionalidades Base (obligatorias)
-----------------------------------
- ABM de Equipos
        * Alta (con asignación automática de nombre)
        * Baja
        * Modificación
        
- ABM de Jugadores
        * Alta (con validaciones)
        * Baja
        * Modificación

Tema B – Control Organizativo de Equipos
------------------------------------------
El cliente necesita evaluar la estructura de sus equipos.

* Funcionalidades
    - Listar jugadores afiliados
    - Listar jugadores por:
        * Equipo o
        * Club

* Reportes adicionales
    - Cantidad de jugadores por equipo
    - Equipo con mayor cantidad de jugadores
    - Equipos que no alcanzan el cupo mínimo requerido
    - Equipos sin jugadores
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
            public List<string> Equipos;
            public bool Seguro;
            public bool Afiliado;
        }

        static List<Jugador> jugadores = new List<Jugador>();

        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido al sistema de gestión de liga deportiva del GRUPO 33");
            while (true)
            {
                Console.WriteLine("--> Por favor elija una opción:");
                Console.WriteLine("1 - ABM - Equipos");
                Console.WriteLine("2 - ABM - Jugadores");
                Console.WriteLine("3 - Funcionalidades");
                Console.WriteLine("4 - Reportes adicionales");
                Console.WriteLine("5 - Salir");
                Console.WriteLine("-------------------------");
                var opcion = Console.ReadLine();
                Console.WriteLine("Usted ingresó: " + opcion);
                Console.WriteLine("-------------------------");

                if (opcion == "1")
                {
                    Console.WriteLine("Alta, baja y modificación de Equipos");
                    //ACA VA ALTA - BAJA - MODIFICACIÓN DE EQIOPOS
                    Console.WriteLine("-------------------------");





                }
                else if (opcion == "2")
                {
                    Console.WriteLine("'Alta, baja y modificación de Jugadores'");
                    //ACA VA ALTA - BAJA - MODIFICACIÓN DE JUGADORES
                    Console.WriteLine("-------------------------");
                    while (true)
                    {
                        Console.WriteLine("Elija una opción:");
                        Console.WriteLine("1 - Dar de alta ");
                        Console.WriteLine("2 - Dar de baja ");
                        Console.WriteLine("3 - Modificar ");
                        Console.WriteLine("4 - Salir ");
                        Console.WriteLine("-------------------------");
                        var ambjugador = Console.ReadLine();
                        Console.WriteLine("Usted ingresó: " + ambjugador);
                        Console.WriteLine("-------------------------");

                        if (ambjugador == "1")
                        {
                            //ACA VA ALTA DE JUGADORES
                            Console.WriteLine("Alta de jugadores");
                            Jugador jugador;

                            int dni;
                            Console.WriteLine("Ingrese el DNI del jugador:");
                            while (!int.TryParse(Console.ReadLine(), out dni))
                            {
                                Console.WriteLine("Ingrese solo números:");
                            }
                            jugador.DNI = dni;

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
                                continue;
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

                            //INICIALIZAR LISTA PARA LOS EQUIPOS
                            jugador.Equipos = new List<string>();

                            Console.WriteLine("Cantidad de equipos asignados:");
                            int cantidad = int.Parse(Console.ReadLine());

                            for (int i = 0; i < cantidad; i++)
                            {
                                Console.Write("Ingrese el nombre del ");
                                Console.Write("equipo " + (i) + " :");
                                string equipo = Console.ReadLine();
                                jugador.Equipos.Add(equipo);
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
                        else if (ambjugador == "2")
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
                                Console.WriteLine("Jugador: " + jugadores[index].Nombre + jugadores[index].Apellido);
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
                        else if (ambjugador == "3")
                        {
                            //ACA VA MODIFICACIÓN DE JUGADORES
                            Console.WriteLine("Modificación de jugadores");
                            Console.WriteLine("-------------------------");
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
                                        Console.WriteLine("No se modificó la edad, ingrese un numero");
                                        Console.WriteLine("--------------------------------");
                                    }

                                    //INGRESO A MODIFICACIÓN DE EQUIPOS
                                    Console.WriteLine("Equipos asignados:");
                                    for (int i = 0; i < j.Equipos.Count; i++)

                                    {
                                        Console.WriteLine("Equipo " + i + 1 + ": " + j.Equipos[i]);
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

                                        for (int i = 0; i < cantidad; i++)
                                        {
                                            Console.Write("Ingrese el nombre del ");
                                            Console.Write("equipo " + (i) + " :");
                                            string equipo = Console.ReadLine();
                                            j.Equipos.Add(equipo);
                                        }
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
                                    continue;
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
                        else if (ambjugador == "4")
                        {
                            Console.WriteLine("Usted eligió salir de ABM de jugadores");
                            Console.WriteLine("-------------------------");
                            break;

                        }
                        else
                        {
                            Console.WriteLine(ambjugador + " , no es un ingreso válido!.");
                            Console.WriteLine("-------------------------");
                        }
                        Console.WriteLine("-------------------------");
                    }
                }
                else if (opcion == "3")
                {
                    Console.WriteLine("Funcionalidades");
                    //ACA VAN LAS FUNCIONALIDADES
                    Console.WriteLine("-------------------------");




                }
                else if (opcion == "4")
                {
                    Console.WriteLine("4 - Reportes adicionales");
                    //ACA VAN LOS REPORTES ADICIONALES
                    Console.WriteLine("-------------------------");

                }
                else if (opcion == "5")
                {
                    Console.WriteLine("Usted salió del sistema. Vuelva pronto y ponganos un 10!");
                    Console.WriteLine("Vuelva pronto y ponganos un 10!");
                    Console.WriteLine("Adios!.");
                    Console.WriteLine("-------------------------");
                    break;
                }
                else
                {
                    Console.WriteLine(opcion + " ,no es un ingreso válido.");
                    Console.WriteLine("-------------------------");
                }

            }

        }

    }
}
