using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaClubes
{
    internal class Program

    {
        //hago arrays para todos
        static int[] dniJugadores = new int[100];
        static string[] nombresJugadores = new string[100];
        static string[] apellidosJugadores = new string[100];
        static int[] edadesJugadores = new int[100];
        static string[] equipoJugador = new string[100];
        static bool[] seguroJugadores = new bool[100];
        static bool[] afiliadosJugadores = new bool[100];

        static string[] nombresEquipos = new string[50];
        static string[] categoriasEquipos = new string[50];
        static int[] jugadoresPorEquipo = new int[50];

        static void Main(string[] args)
        {

            int opcion;

            do
            {
                Console.WriteLine("LA LIGA");
                Console.WriteLine("1- Alta equipo");
                Console.WriteLine("2- Listar equipos");
                Console.WriteLine("3- Alta jugador");
                Console.WriteLine("4- Listar jugadores");
                Console.WriteLine("5- Jugadores afiliados");
                Console.WriteLine("6- Cantidad jugadores por equipo");
                Console.WriteLine("7- Equipos sin jugadores");
                Console.WriteLine("0- Salir");

                Console.Write("Opcion: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AltaEquipo();
                        break;

                    case 2:
                        ListarEquipos();
                        break;

                    case 3:
                        AltaJugador();
                        break;

                    case 4:
                        ListarJugadores();
                        break;

                    case 5:
                        ListarAfiliados();
                        break;

                    case 6:
                        CantidadJugadoresPorEquipo();
                        break;

                    case 7:
                        EquiposSinJugadores();
                        break;
                }

            } while (opcion != 0);
        }


    }
}

