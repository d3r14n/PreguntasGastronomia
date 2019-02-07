using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

/* Notas para el autor
 * Hay que revisar la parte de eliminar... ¿podemos dejarlo así?
 * Al cambiar las respuestas, puede que el usuario no ponga el inciso, es por lo mismo que esta parte es solo para el administrador
 * De cualquier manera, tarde o temprano se dará cuenta de su error
 * ¿Se podría arreglar? Sólo se me ocurre hacerlo con muchos if
 */

namespace PreguntasGastronomia
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declaración de variables
            string seleccionAdmin = "no";
            Random numero = new Random();
            string volver = "no";
            string ganador = "no";

            //Número de preguntas creadas
            StreamReader total = File.OpenText(@".\numeracion.txt");
            int maximo = int.Parse(total.ReadToEnd());
            total.Close();

            do
            {
                //Menú Inicial
                Console.WriteLine("Bienvenido");
                Console.WriteLine("1.- INICIAR");
                Console.WriteLine("2.- Configuracion");
                string seleccion = Console.ReadLine();
                Console.Clear();

                //Seccion de Juego
                if (seleccion == "1")
                {
                    //Bienvenida, Introducción
                    Console.WriteLine("Bienvenido, este juego consta de cinco preguntas sobre gastronomia, aquí demostraras tus conocimientos sobre esta ciencia");
                    Console.WriteLine("Presiona cualquier tecla para iniciar");
                    Console.ReadKey(true);
                    Console.Clear();

                    //Posibles preguntas a aparecer
                    StreamReader posibles = File.OpenText(@".\preguntas.txt");
                    string disponibles = posibles.ReadToEnd();
                    string[] preguntasDisponibles = disponibles.Split('\n');

                    //Variables, una para la pregunta actual y otra para la puntuación
                    int p = 1;
                    int puntuacion = 0;

                    //Mientras no sea la pregunta 5
                    while (p <= 5)
                    {
                        //Variable para determinar si la respuesta es correcta o incorrecta
                        string calificacion = null;

                        //Abrimos la pregunta y la dividimos
                        if (File.Exists(@".\Preguntas\" + numero.Next(1, maximo) + ".txt"))
                        {
                            StreamReader textoPregunta = File.OpenText(@".\Preguntas\" + numero.Next(1, maximo) + ".txt");
                            string elQuePregunta = textoPregunta.ReadToEnd();
                            string[] incisos = elQuePregunta.Split('\n');
                            Console.Clear();

                            //Escribimos la pregunta
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine(incisos[0]);
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(incisos[1]);
                            Console.WriteLine(incisos[2]);
                            Console.WriteLine(incisos[3]);
                            Console.WriteLine(incisos[4]);
                            Console.ResetColor();
                            Console.WriteLine();


                            //Interacción del usuario para responder la pregunta
                            string respuestaCorrecta = incisos[5].Substring(0, 1);
                            do
                            {
                                Console.WriteLine("Puntuación: {0}", puntuacion);
                                Console.WriteLine();

                                //Cuando la respuesta sea la respuesta correcta
                                string eleccionUsuario = Console.ReadLine();

                                if (eleccionUsuario == respuestaCorrecta)
                                {
                                    //La respuesta es correcta
                                    calificacion = "Correcto";
                                    Console.Beep(800, 300);
                                    p++;
                                    puntuacion = puntuacion + (puntuacion/2) + 100;
                                    Console.Clear();
                                    break;
                                }
                                else
                                {
                                    //La respuesta es incorrecta
                                    calificacion = "Incorrecto";
                                    Console.Beep(500, 300);
                                    puntuacion = puntuacion - puntuacion/4;

                                    //Coloración
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Incorrecto");
                                    Console.ResetColor();
                                }
                            }
                            while (calificacion == "Incorrecto");
                        }
                    }
                    //Puntuacion Final
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Puntuación final: {0}", puntuacion);
                    Console.ResetColor();
                    Console.ReadKey(true);
                    Console.Clear();
                    ganador = "si";
                }

                //Sección de Administrador
                if (seleccion == "2")
                {
                    Console.WriteLine("Configuración");
                    Console.WriteLine("1.- Añadir Preguntas");
                    Console.WriteLine("2.- Modificar Preguntas");
                    Console.WriteLine("3.- Eliminar Preguntas");
                    Console.WriteLine("4.- Revisar Preguntas");
                    Console.WriteLine("Para volver escribe \"volver\"");
                    seleccionAdmin = Console.ReadLine();
                    Console.Clear();

                    //Añadir Preguntas
                    if (seleccionAdmin == "1")
                    {

                        //Revisamos el maximo cada vez que se vaya a crear una nueva pregunta
                        StreamReader modificar = File.OpenText(@".\numeracion.txt");
                        maximo = int.Parse(modificar.ReadToEnd());
                        modificar.Close();

                        //Establecer el número de pregunta
                        StreamWriter numeracion = File.CreateText(@".\numeracion.txt");
                        numeracion.Write(maximo + 1);
                        numeracion.Close();

                        //Documento de texto que leerá las preguntas disponibles
                        StreamWriter sello = File.AppendText(@".\preguntas.txt");
                        sello.WriteLine(maximo + 1);
                        sello.Close();

                        //Crear la pregunta
                        StreamWriter aniadir = File.CreateText(@".\Preguntas\" + (maximo + 1) + ".txt");
                        Console.WriteLine("Escriba la pregunta:");
                        string pregunta = Console.ReadLine();
                        Console.WriteLine("Ahora escriba las opciones de respuesta");
                        Console.Write("a) ");
                        string nuevaPa = Console.ReadLine();
                        Console.Write("b) ");
                        string nuevaPb = Console.ReadLine();
                        Console.Write("c) ");
                        string nuevaPc = Console.ReadLine();
                        Console.Write("d) ");
                        string nuevaPd = Console.ReadLine();
                        Console.WriteLine("¿Cual es la respuesta correcta? Escriba la letra del inciso");
                        string nuevaRespuesta = Console.ReadLine();

                        //Guardar la pregunta
                        aniadir.WriteLine(pregunta);
                        aniadir.WriteLine("a) " + nuevaPa);
                        aniadir.WriteLine("b) " + nuevaPb);
                        aniadir.WriteLine("c) " + nuevaPc);
                        aniadir.WriteLine("d) " + nuevaPd);
                        aniadir.WriteLine(nuevaRespuesta);
                        aniadir.Close();

                        //Opcion para volver al menú
                        Console.WriteLine("¿Desea volver al menú?");
                        volver = Console.ReadLine();
                        Console.Clear();

                    }

                    //Modificar Preguntas
                    if (seleccionAdmin == "2")
                    {
                        //Se selecciona el archivo que corresponde a la pregunta a modificar
                        Console.WriteLine("Escriba el número de la pregunta a modificar");
                        string modificarNumero = Console.ReadLine();
                        StreamReader preguntaPModificar = File.OpenText(@".\Preguntas\" + modificarNumero + ".txt");

                        //Esta variable nos permitirá leer todo el texto
                        string leerPModificar = preguntaPModificar.ReadToEnd();

                        //Dividimos el texto en lineas
                        string[] linea = leerPModificar.Split('\n');

                        //Escribiremos en consola el texto dividido en lineas
                        for (int x = 0; x < 7 - 1; x++)
                        {
                            Console.WriteLine((x + 1) + ".- " + linea[x]);
                        }

                        //Se pregunta por la linea a modificar
                        Console.WriteLine("¿Que linea desea modificar?");
                        int lineaPModificar = int.Parse(Console.ReadLine());

                        //Se escribe la modificacion
                        Console.WriteLine("Escriba su modificacion");
                        string nuevaModificacion = Console.ReadLine();
                        preguntaPModificar.Close();

                        //El modificador se encargará de reescribir el texto con la modificación
                        StreamWriter modificador = File.CreateText(@".\Preguntas\" + modificarNumero + ".txt");
                        for (int x = 0; x < linea.Length; x++)
                        {
                            if (x == lineaPModificar - 1)
                            {
                                modificador.WriteLine(nuevaModificacion);
                            }
                            else
                            {
                                modificador.WriteLine(linea[x]);
                            }
                        }
                        modificador.Close();

                        //Volver al menú
                        Console.WriteLine("¿Desea volver al menú?");
                        volver = Console.ReadLine();
                        Console.Clear();
                    }


                    //Eliminar Preguntas
                    if (seleccionAdmin == "3")
                    {
                        string otraEliminacion = null;
                        do
                        {
                            //Se solicita la pregunta y elimina el archivo
                            Console.WriteLine("¿Que pregunta desea eliminar?");
                            string eliminar = Console.ReadLine();
                            File.Delete(@".\Preguntas\" + eliminar + ".txt");

                            //Se borra el número maximo de preguntas
                            StreamWriter numeracion = File.CreateText(@".\numeracion.txt");
                            numeracion.Write(maximo - 1);
                            numeracion.Close();

                            /*Se elimina del archivo de texto con todas las preguntas disponibles
                            StreamReader vacantes = File.OpenText(@"C:\Users\DerianJair\Desktop\Ing Software\Introducción a la Programación\Proyectos\PreguntasGastronomia\preguntas.txt");
                            string disponibilidad = vacantes.ReadToEnd();
                            string[] vacantesDisponibles = disponibilidad.Split('\n');
                            for (int x = 1; x <= vacantesDisponibles.Length; x++)
                            {
                                if (vacantesDisponibles[x] == eliminar)
                                {
                                    //Documento de texto que leerá las preguntas disponibles
                                    StreamWriter sello = File.AppendText(@"C:\Users\DerianJair\Desktop\Ing Software\Introducción a la Programación\Proyectos\PreguntasGastronomia\preguntas.txt");
                                    sello.WriteLine("1");
                                    sello.Close();
                                }
                            }*/
                            
                            Console.WriteLine("¿Desea Eliminar otro archivo?");
                            otraEliminacion = Console.ReadLine();
                        }
                        while (otraEliminacion.ToLower() == "si");

                        Console.WriteLine("¿Desea volver al menú?");
                        volver = Console.ReadLine();
                        Console.Clear();
                    }

                    //Revisar Preguntas
                    if (seleccionAdmin == "4")
                    {
                        string otraConsulta = null;
                        do
                        {
                            //Preguntamos por la pregunta a consultar
                            Console.WriteLine("¿Cuál número de pregunta deseas consultar?");
                            string consultaPregunta = Console.ReadLine();
                            Console.Clear();

                            //Se abre el archivo solicitado
                            StreamReader consulta = File.OpenText(@".\Preguntas\" + consultaPregunta + ".txt");
                            string verConsulta = consulta.ReadToEnd();
                            string[] preguntaConsultada = verConsulta.Split('\n');
                            Console.WriteLine(preguntaConsultada[0]);
                            Console.WriteLine(preguntaConsultada[1]);
                            Console.WriteLine(preguntaConsultada[2]);
                            Console.WriteLine(preguntaConsultada[3]);
                            Console.WriteLine(preguntaConsultada[4]);
                            Console.WriteLine(preguntaConsultada[5]);
                            Console.WriteLine();
                            
                            //Preguntamos si va a hacer otra consulta
                            Console.WriteLine("¿Desea hacer otra consulta?");
                            otraConsulta = Console.ReadLine();
                            Console.Clear();
                        }
                        while (otraConsulta == "si");
                        if (otraConsulta != "si")
                        {
                            volver = "si";
                        }
                    }

                }

            }
            while (volver.ToLower() == "si" || seleccionAdmin.ToLower() == "volver" || ganador == "si");
            
            //Despedida
            Console.WriteLine("Gracias por Jugar");
            Console.ReadKey(true);
        }
    }
}