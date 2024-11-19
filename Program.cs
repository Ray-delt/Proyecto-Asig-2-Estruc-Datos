using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_teorica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0,ser=0; bool control = false;
            int real = -1;
            //pila para nuestro balanceo de cauchos
            Pila pila = new Pila();
            // numero de membresia en uno por ya haber 1 agregado
            int membresia = 1;
            //nuestros cupos para nuestras colas de servicios autolavado SC1, aspirado SC2 y balanceo SC3
            //como se cambio el servicio 1 en clases el cupo de 10 se va a ajustar con las colas lavado y secado siendo estas dos sin limite
            //lo que va a definir el limite es la primera aspirado de tal forma que si no sale nadie de aspirado no pueden entrar mas
            int SC1 = 10, SC2 = 5, SC3 = 5;
            //nuestras colas para espera,servicios (s1,s2,s3) y cola auxiliar
            Cola<Cliente> colaespera = new Cola<Cliente>();
            Cola<Cliente> colaS2 = new Cola<Cliente>();
            Cola<Cliente> colaS3 = new Cola<Cliente>();
            Cola<Cliente> colaS1 = new Cola<Cliente>();
            Cola<Cliente> Caux = new Cola<Cliente>();
            //colas para colaS1 
            Cola<Cliente> lav = new Cola<Cliente>();
            Cola<Cliente> sec = new Cola<Cliente>();
            //unico clint simpre presente en balance
            Cola<Cliente> pilas= new Cola<Cliente>();
         

            // Agregar personas a la cola
            Console.WriteLine("Ejemplo: aqui los datos del primero cliente y se le va a asignar numero de menbresia \n");
            //Nombre, Apellido, CI, tipo de vehículo (camioneta,auto), modelo vehículo, placa.
           
            colaespera.encola(new Cliente("raimundo", "Atienza", "30488983", "auto", "sensacion", "XYS-RS3", membresia));
            Console.WriteLine("primera persona en la cola de espera : " + colaespera.Peek());//muestra la primera persona en la cola de espera
            Console.ReadKey();
            #region MENU
            do
            {
                
                // Mostrar el menú
                Console.Clear();
                Console.WriteLine("\n                 --------------------------PROYECTO AUTOLAVADO------------------------");
                Console.WriteLine("                 -- 1. Agregar Clientes a la lista de espera                        --");
                Console.WriteLine("                 -- 2. Mover el cliente a su servicio deseado                       --");
                Console.WriteLine("                 -- 3. Cancelar en cola de espera o servicio                        --");//dentro opcion de eliminar
                Console.WriteLine("                 -- 4. Modificar datos del cliente de la membresia ingresada        --");
                Console.WriteLine("                 -- 5. Atender el servicio seleccionado                             --");
                Console.WriteLine("                 -- 6. Atender balanceo o lavado/secado                             --");
                Console.WriteLine("                 -- 7. Listar cola de espera                                        --");
                Console.WriteLine("                 -- 0. Salir                                                        --");
                Console.Write("                 --------------------------PROYECTO AUTOLAVADO------------------------\n                 Ingrese su opción: ");

                // Validamos la entrada
                string entrada = Console.ReadLine();
                if (!int.TryParse(entrada, out opcion))
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                    continue; // Regresar al inicio del bucle util para no pasar por switchhh
                }

                // Evaluar la opción seleccionada
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("¿Cuántas personas deseas agregar a la cola de espera?");//Agregar limite para la cola de espera no es necesario
                        int cantidad = int.Parse(Console.ReadLine());
                        #region peticion de datos
                        for (int i = 0; i < cantidad; i++)
                        {
                            Console.Clear();
                            membresia++;//sera 2 porque ya hay uno entonces escala n cantidad(no se repite)
                            Console.WriteLine($"\nIngrese los datos para la persona {i + 1}:");

                            Console.Write("Nombre: ");
                            string nombre = Console.ReadLine();

                            Console.Write("Apellido: ");
                            string apellido = Console.ReadLine();

                            Console.Write("Cédula de Identidad (CI): ");
                            string ci = Console.ReadLine();

                            Console.Write("Tipo de Vehículo (camioneta/auto): ");
                            string tipoVehiculo = Console.ReadLine();

                            Console.Write("Modelo de Vehículo: ");
                            string modeloVehiculo = Console.ReadLine();

                            Console.Write("Placa del Vehículo: ");
                            string placa = Console.ReadLine();

                            // Crear una nueva persona y agregarla a la cola
                            Cliente persona = new Cliente(nombre, apellido, ci, tipoVehiculo, modeloVehiculo, placa, membresia);
                            colaespera.encola(persona);
                            Console.WriteLine("Cliente agregado exitosamente.");
                            Console.ReadKey();
                        }
                        #endregion//DATOS
                        
                      
                        break;

                    case 2:
                        #region seleccion
                        Console.WriteLine("\nPrimer persona en la cola de espera:");
                        Console.WriteLine(colaespera.Peek());
                        Console.WriteLine("\nQue servicio desea usar? 1(autolavado),2(cambio de aceite) y 3(balanceo) ");
                        string sere=Console.ReadLine();
                        if (!int.TryParse(sere, out ser))
                        {
                            Console.WriteLine("Por favor, ingrese un número válido no se pudo validar su cupo.");
                            Console.ReadKey();
                            continue; // Regresar al inicio del bucle util para no pasar por switchhh

                        }
                        if (ser == 2)
                        {
                            if (SC2 != 0)
                            {
                                colaS2.encola(colaespera.atendida());
                                SC2--;//se resta el cupo
                            }
                            else
                            {
                                Console.WriteLine("\nNo hay cupo para el servicio 2");
                            }
                           
                        }
                        if (ser == 3)
                        {
                            if (SC2 != 0)
                            {
                                colaS3.encola(colaespera.atendida());
                                SC3--;//se resta el cupo
                            }
                            else
                            {
                                Console.WriteLine("\nNo hay cupo para el servicio 3");
                            }

                        }
                        if (ser == 1)
                        {
                            if (SC1 != 0)
                            {
                                colaS1.encola(colaespera.atendida());
                                SC1--;//se resta el cupo
                            }
                            else
                            {
                                Console.WriteLine("\nNo hay cupo para el servicio 3");
                            }

                        }
                        Console.WriteLine("Proceso terminado...");
                        Console.ReadKey();
                        #endregion

                        break;

                    case 3:
                        #region cancelar o eliminar
                        Console.WriteLine("Se cancela o elimina por numero de membresia, desea cancelar o eliminar? (1/2): ");
                        string e = Console.ReadLine();
                        if (!int.TryParse(e, out ser))
                        {
                            Console.WriteLine("Por favor, ingrese un número válido no se pudo ejecutar su eleccion.");
                            Console.ReadKey();
                            continue; // Regresar al inicio del bucle util para no pasar por switchh
                        }
                        if (ser<1||ser>2)
                        {
                            Console.WriteLine("Por favor, ingrese un número válido no se pudo ejecutar su eleccion.");
                            Console.ReadKey();
                            continue;

                        }
                        else
                        {
                            if (ser == 1)
                            {
                                bool a = false;
                                do
                                {
                                   a=false;
                                    Console.WriteLine("Desea cancelar un cliente de que servicio? ");
                                    Console.WriteLine("Estan los servicios (1,2,3): ");
                                        string d = Console.ReadLine();
                                        if(!int.TryParse(d, out ser)||ser<0||ser>3)
                                        {
                                            Console.WriteLine("Por favor, ingrese un número válido no se pudo ejecutar su eleccion.");
                                            Console.ReadKey();
                                            a = true;
                                            continue;
                                        }
                                        if(ser == 1)
                                        {
                                            Console.WriteLine("Toda la cola en cambio de solo aspirado(ultimo chance de cancelar): \n");
                                            colaS1.imprimir();
                                            // Preguntar al usuario si desea eliminar a alguien de una posición específica
                                            Console.WriteLine("Ingrese el numero de menbresia del que quiere cancelar: ");
                                            int eli = int.Parse(Console.ReadLine());
                                            #region eliminar
                                            while (!colaS1.EsVacia())
                                            {
                                                Cliente persona = colaS1.atendida();
                                                if (persona.ObtenerMen() == eli)
                                                {
                                                    Console.WriteLine("La persona " + persona + " cancelo su servicio");
                                                    colaespera.encola(persona);
                                                    control = true;
                                                    Console.ReadKey();
                                                }
                                                else
                                                {
                                                    Caux.encola(persona);
                                                }

                                            }
                                            int R = Caux.Count();
                                            for (int i = 0; i < R; i++)
                                            {
                                                colaS1.encola(Caux.atendida());
                                            }
                                            if (control == false)
                                            {
                                                Console.WriteLine("No esta la membresia indicada aqui");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Aqui esta la cola reajustada: \n");
                                                colaS1.imprimir();
                                            }
                                            #endregion

                                        }
                                        if (ser == 2)
                                        {
                                            Console.WriteLine("Toda la cola en cambio de aceite: ");
                                            colaS2.imprimir();
                                            // Preguntar al usuario si desea eliminar a alguien de una posición específica
                                            Console.WriteLine("Ingrese el numero de menbresia del que quiere cancelar: ");
                                            int eli = int.Parse(Console.ReadLine());
                                            #region eliminar
                                            while (!colaS2.EsVacia())
                                            {
                                                Cliente persona = colaS2.atendida();
                                                if (persona.ObtenerMen() == eli)
                                                {
                                                    Console.WriteLine("La persona " + persona + " cancelo su servicio vuelve a la espera");
                                                    colaespera.encola(persona);
                                                    control = true;
                                                    Console.ReadKey();
                                                }
                                                else
                                                {
                                                    Caux.encola(persona);
                                                }

                                            }
                                            int R = Caux.Count();
                                            for (int i = 0; i < R; i++)
                                            {
                                                colaS2.encola(Caux.atendida());
                                            }
                                            if (control == false)
                                            {
                                                Console.WriteLine("No esta la membresia indicada aqui");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Aqui esta la cola reajustada: \n");
                                                colaS2.imprimir();
                                            }
                                            #endregion

                                        }
                                        if (ser == 3)
                                        {
                                            Console.WriteLine("Toda la cola en cambio de balanceo: ");
                                            colaS3.imprimir();
                                            // Preguntar al usuario si desea eliminar a alguien de una posición específica
                                            Console.WriteLine("Ingrese el numero de menbresia del que quiere cancelar: ");
                                            int eli = int.Parse(Console.ReadLine());
                                            #region eliminar
                                            
                                            while (!colaS3.EsVacia())
                                            {
                                                Cliente persona = colaS3.atendida();
                                                if (persona.ObtenerMen() == eli)
                                                {
                                                    Console.WriteLine("La persona " + persona + " cancelo su servicio vuelve a la espera");
                                                    colaespera.encola(persona);
                                                    Console.ReadKey();
                                                    control = true;  
                                                }
                                                else
                                                {
                                                    Caux.encola(persona);
                                                }

                                            }
                                            int R = Caux.Count();
                                            for (int i = 0; i < R; i++)
                                            {
                                                colaS3.encola(Caux.atendida());
                                            }
                                            if (control == false)
                                            {
                                                Console.WriteLine("No esta la membresia indicada aqui");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Aqui esta la cola reajustada: \n");
                                                colaS3.imprimir();
                                            }
                                            
                                            #endregion
                                        }
                                    
                                   

                                } while (a == true);


                            }
                            else// eliminar completamente
                            {
                                bool a = false;
                                do
                                {
                                    a = false;
                                    Console.WriteLine("Desea eliminar un cliente de que servicio? ");
                                    Console.WriteLine("Estan los servicios (1,2,3,4) el cuatro para espera : ");
                                    string d = Console.ReadLine();
                                    if (!int.TryParse(d, out ser) || ser < 0 || ser > 3)
                                    {
                                        Console.WriteLine("Por favor, ingrese un número válido no se pudo ejecutar su eleccion.");
                                        Console.ReadKey();
                                        a = true;
                                        continue;
                                    }
                                    if (ser == 1)
                                    {
                                        Console.WriteLine("Toda la cola en solo aspirado puede ser eliminado: \n");
                                        colaS1.imprimir();
                                        // Preguntar al usuario si desea eliminar a alguien de una posición específica
                                        Console.WriteLine("Ingrese el numero de menbresia del que quiere eliminar: ");
                                        int eli = int.Parse(Console.ReadLine());
                                        #region eliminar
                                        while (!colaS1.EsVacia())
                                        {
                                            Cliente persona = colaS1.atendida();
                                            if (persona.ObtenerMen() == eli)
                                            {
                                                Console.WriteLine("La persona " + persona + " fue eliminado ");

                                                control = true;
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Caux.encola(persona);
                                            }

                                        }
                                        int R = Caux.Count();
                                        for (int i = 0; i < R; i++)
                                        {
                                            colaS1.encola(Caux.atendida());
                                        }
                                        if (control == false)
                                        {
                                            Console.WriteLine("No esta la membresia indicada aqui");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Aqui esta la cola reajustada: \n");
                                            colaS1.imprimir();
                                        }
                                        #endregion

                                    }
                                    if (ser == 2)
                                    {
                                        Console.WriteLine("Toda la cola en cambio de aceite: ");
                                        colaS2.imprimir();
                                        // Preguntar al usuario si desea eliminar a alguien de una posición específica
                                        Console.WriteLine("Ingrese el numero de menbresia del que quiere cancelar: ");
                                        int eli = int.Parse(Console.ReadLine());
                                        #region eliminar
                                        while (!colaS2.EsVacia())
                                        {
                                            Cliente persona = colaS2.atendida();
                                            if (persona.ObtenerMen() == eli)
                                            {
                                                Console.WriteLine("La persona " + persona + " fue eliminado ");

                                                control = true;
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Caux.encola(persona);
                                            }

                                        }
                                        int R = Caux.Count();
                                        for (int i = 0; i < R; i++)
                                        {
                                            colaS2.encola(Caux.atendida());
                                        }
                                        if (control == false)
                                        {
                                            Console.WriteLine("No esta la membresia indicada aqui");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Aqui esta la cola reajustada: \n");
                                            colaS2.imprimir();
                                        }
                                        #endregion

                                    }
                                    if (ser == 3)
                                    {
                                        Console.WriteLine("Toda la cola en balanceo: ");
                                        colaS3.imprimir();
                                        // Preguntar al usuario si desea eliminar a alguien de una posición específica
                                        Console.WriteLine("Ingrese el numero de menbresia del que quiere cancelar: ");
                                        int eli = int.Parse(Console.ReadLine());
                                        #region eliminar

                                        while (!colaS3.EsVacia())
                                        {
                                            Cliente persona = colaS3.atendida();
                                            if (persona.ObtenerMen() == eli)
                                            {
                                                Console.WriteLine("La persona " + persona + " fue eliminado ");
                                                Console.ReadKey();
                                                control = true;
                                            }
                                            else
                                            {
                                                Caux.encola(persona);
                                            }

                                        }
                                        int R = Caux.Count();
                                        for (int i = 0; i < R; i++)
                                        {
                                            colaS3.encola(Caux.atendida());
                                        }
                                        if (control == false)
                                        {
                                            Console.WriteLine("No esta la membresia indicada aqui");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Aqui esta la cola reajustada: \n");
                                            colaS3.imprimir();
                                        }

                                        #endregion
                                    }
                                    if (ser == 4)
                                    {
                                        Console.WriteLine("Toda la cola de espera: ");
                                        colaespera.imprimir();
                                        // Preguntar al usuario si desea eliminar a alguien de una posición específica
                                        Console.WriteLine("Ingrese el numero de menbresia del que quiere cancelar: ");
                                        int eli = int.Parse(Console.ReadLine());
                                        #region eliminar

                                        while (!colaespera.EsVacia())
                                        {
                                            Cliente persona = colaespera.atendida();
                                            if (persona.ObtenerMen() == eli)
                                            {
                                                Console.WriteLine("La persona " + persona + " fue eliminado ");
                                                Console.ReadKey();
                                                control = true;
                                            }
                                            else
                                            {
                                                Caux.encola(persona);
                                            }

                                        }
                                        int R = Caux.Count();
                                        for (int i = 0; i < R; i++)
                                        {
                                            colaespera.encola(Caux.atendida());
                                        }
                                        if (control == false)
                                        {
                                            Console.WriteLine("No esta la membresia indicada aqui");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Aqui esta la cola reajustada: \n");
                                            colaespera.imprimir();
                                        }

                                        #endregion
                                    }


                                } while (a == true);
                            }
                        }
                        #endregion
                        break;
                    case 4:
                        #region Modificar
                        Console.WriteLine("Se modifica por numero de membresia desea modificar servicio o espera? (1/2): ");
                        string z = Console.ReadLine();
                        if (!int.TryParse(z, out ser))
                        {
                            Console.WriteLine("Por favor, ingrese un número válido no se pudo ejecutar su eleccion.");
                            Console.ReadKey();
                            continue; // Regresar al inicio del bucle util para no pasar por switchh
                        }
                        if (ser < 1 || ser > 2)
                        {
                            Console.WriteLine("Por favor, ingrese un número válido no se pudo ejecutar su eleccion.");
                            Console.ReadKey();
                            continue;

                        }
                        else
                        {
                            if (ser == 1)
                            {
                                bool a = false;
                                do
                                {
                                    a = false;
                                    Console.WriteLine("Desea modificar un cliente de que servicio? ");
                                    Console.WriteLine("Estan los servicios (1,2,3): ");
                                    string d = Console.ReadLine();
                                    if (!int.TryParse(d, out ser) || ser < 0 || ser > 3)
                                    {
                                        Console.WriteLine("Por favor, ingrese un número válido no se pudo ejecutar su eleccion.");
                                        Console.ReadKey();
                                        a = true;
                                        continue;
                                    }
                                    if (ser == 1)
                                    {
                                        Console.WriteLine("Toda la cola de solo aspirado: \n");
                                        colaS1.imprimir();
                                        // Preguntar al usuario si desea eliminar a alguien de una posición específica
                                        Console.WriteLine("Ingrese el numero de menbresia del que quiere modificar: ");
                                        int eli = int.Parse(Console.ReadLine());
                                        #region modificar
                                        while (!colaS1.EsVacia())
                                        {
                                            Cliente persona = colaS1.atendida();
                                            if (persona.ObtenerMen() == eli)
                                            {
                                                Console.WriteLine("Se van a modificar los datos de la persona " + persona + " ingrese nuevos datos: \n");
                                                Console.Write("Nombre: ");
                                                string nombre = Console.ReadLine();

                                                Console.Write("Apellido: ");
                                                string apellido = Console.ReadLine();

                                                Console.Write("Cédula de Identidad (CI): ");
                                                string ci = Console.ReadLine();

                                                Console.Write("Tipo de Vehículo (camioneta/auto): ");
                                                string tipoVehiculo = Console.ReadLine();

                                                Console.Write("Modelo de Vehículo: ");
                                                string modeloVehiculo = Console.ReadLine();

                                                Console.Write("Placa del Vehículo: ");
                                                string placa = Console.ReadLine();

                                                // Crear una nueva persona y agregarla a la cola
                                                Cliente persona1 = new Cliente(nombre, apellido, ci, tipoVehiculo, modeloVehiculo, placa, membresia);
                                                Caux.encola(persona1);
                                                Console.WriteLine("Cliente modificado exitosamente.");
                                                Console.ReadKey();
                                                control = true;
                                            }
                                            else
                                            {
                                                Caux.encola(persona);
                                            }

                                        }
                                        int R = Caux.Count();
                                        for (int i = 0; i < R; i++)
                                        {
                                            colaS1.encola(Caux.atendida());
                                        }
                                        if (control == false)
                                        {
                                            Console.WriteLine("No esta la membresia indicada aqui");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Aqui esta la cola reajustada: \n");
                                            colaS1.imprimir();
                                        }
                                        #endregion

                                    }
                                    if (ser == 2)
                                    {
                                        Console.WriteLine("Toda la cola en cambio de aceite: ");
                                        colaS2.imprimir();
                                        // Preguntar al usuario si desea eliminar a alguien de una posición específica
                                        Console.WriteLine("Ingrese el numero de menbresia del que quiere modificar: ");
                                        int eli = int.Parse(Console.ReadLine());
                                        #region modificar
                                        while (!colaS2.EsVacia())
                                        {
                                            Cliente persona = colaS2.atendida();
                                            if (persona.ObtenerMen() == eli)
                                            {
                                                Console.WriteLine("Se van a modificar los datos de la persona " + persona + " ingrese nuevos datos: \n");
                                                Console.Write("Nombre: ");
                                                string nombre = Console.ReadLine();

                                                Console.Write("Apellido: ");
                                                string apellido = Console.ReadLine();

                                                Console.Write("Cédula de Identidad (CI): ");
                                                string ci = Console.ReadLine();

                                                Console.Write("Tipo de Vehículo (camioneta/auto): ");
                                                string tipoVehiculo = Console.ReadLine();

                                                Console.Write("Modelo de Vehículo: ");
                                                string modeloVehiculo = Console.ReadLine();

                                                Console.Write("Placa del Vehículo: ");
                                                string placa = Console.ReadLine();

                                                // Crear una nueva persona y agregarla a la cola
                                                Cliente persona1 = new Cliente(nombre, apellido, ci, tipoVehiculo, modeloVehiculo, placa, membresia);
                                                Caux.encola(persona1);
                                                Console.WriteLine("Cliente modificado exitosamente.");
                                                Console.ReadKey();
                                                control = true;
                                            }
                                            else
                                            {
                                                Caux.encola(persona);
                                            }

                                        }
                                        int R = Caux.Count();
                                        for (int i = 0; i < R; i++)
                                        {
                                            colaS2.encola(Caux.atendida());
                                        }
                                        if (control == false)
                                        {
                                            Console.WriteLine("No esta la membresia indicada aqui");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Aqui esta la cola reajustada: \n");
                                            colaS2.imprimir();
                                        }
                                        #endregion

                                    }
                                    if (ser == 3)
                                    {
                                        Console.WriteLine("Toda la cola en cambio de balanceo: ");
                                        colaS3.imprimir();
                                        // Preguntar al usuario si desea eliminar a alguien de una posición específica
                                        Console.WriteLine("Ingrese el numero de menbresia del que quiere modificar: ");
                                        int eli = int.Parse(Console.ReadLine());
                                        #region modificar

                                        while (!colaS3.EsVacia())
                                        {
                                            Cliente persona = colaS3.atendida();
                                            if (persona.ObtenerMen() == eli)
                                            {
                                                Console.WriteLine("Se van a modificar los datos de la persona " + persona + " ingrese nuevos datos: \n");
                                                Console.Write("Nombre: ");
                                                string nombre = Console.ReadLine();

                                                Console.Write("Apellido: ");
                                                string apellido = Console.ReadLine();

                                                Console.Write("Cédula de Identidad (CI): ");
                                                string ci = Console.ReadLine();

                                                Console.Write("Tipo de Vehículo (camioneta/auto): ");
                                                string tipoVehiculo = Console.ReadLine();

                                                Console.Write("Modelo de Vehículo: ");
                                                string modeloVehiculo = Console.ReadLine();

                                                Console.Write("Placa del Vehículo: ");
                                                string placa = Console.ReadLine();

                                                // Crear una nueva persona y agregarla a la cola
                                                Cliente persona1 = new Cliente(nombre, apellido, ci, tipoVehiculo, modeloVehiculo, placa, membresia);
                                                Caux.encola(persona1);
                                                Console.WriteLine("Cliente modificado exitosamente.");
                                                Console.ReadKey();
                                                control = true;
                                            }
                                            else
                                            {
                                                Caux.encola(persona);
                                            }

                                        }
                                        int R = Caux.Count();
                                        for (int i = 0; i < R; i++)
                                        {
                                            colaS3.encola(Caux.atendida());
                                        }
                                        if (control == false)
                                        {
                                            Console.WriteLine("No esta la membresia indicada aqui");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Aqui esta la cola reajustada: \n");
                                            colaS3.imprimir();
                                        }

                                        #endregion
                                    }



                                } while (a == true);

                            }
                            else// espera
                            {
                                Console.WriteLine("Toda la cola de espera: ");
                                colaespera.imprimir();
                                // Preguntar al usuario si desea modificar a alguien de una posición específica
                                Console.WriteLine("Ingrese el numero de membresia del que quiere modificar: ");
                                int eli = int.Parse(Console.ReadLine());
                                #region modificar

                                while (!colaespera.EsVacia())
                                {
                                    Cliente persona = colaespera.atendida();
                                    if (persona.ObtenerMen() == eli)
                                    {
                                        Console.WriteLine("Se van a modificar los datos de la persona " + persona + " ingrese nuevos datos: \n");
                                        Console.Write("Nombre: ");
                                        string nombre = Console.ReadLine();

                                        Console.Write("Apellido: ");
                                        string apellido = Console.ReadLine();

                                        Console.Write("Cédula de Identidad (CI): ");
                                        string ci = Console.ReadLine();

                                        Console.Write("Tipo de Vehículo (camioneta/auto): ");
                                        string tipoVehiculo = Console.ReadLine();

                                        Console.Write("Modelo de Vehículo: ");
                                        string modeloVehiculo = Console.ReadLine();

                                        Console.Write("Placa del Vehículo: ");
                                        string placa = Console.ReadLine();

                                        // Crear una nueva persona y agregarla a la cola
                                        Cliente persona1 = new Cliente(nombre, apellido, ci, tipoVehiculo, modeloVehiculo, placa, membresia);
                                        Caux.encola(persona1);
                                        Console.WriteLine("Cliente modificado exitosamente.");
                                        Console.ReadKey();
                                        control = true;
                                    }
                                    else
                                    {
                                        Caux.encola(persona);
                                    }

                                }
                                int R = Caux.Count();
                                for (int i = 0; i < R; i++)
                                {
                                    colaespera.encola(Caux.atendida());
                                }
                                if (control == false)
                                {
                                    Console.WriteLine("No esta la membresia indicada aqui");
                                }
                                else
                                {
                                    Console.WriteLine("Aqui esta la cola reajustada: \n");
                                    colaespera.imprimir();
                                }

                                #endregion
                            }
                        }
                        #endregion
                        break;
                    case 5:
                        #region atender
                        Console.WriteLine("Que servicio desea atender? (1,2,3): ");
                        string f = Console.ReadLine();
                        if (!int.TryParse(f, out ser))
                        {
                            Console.WriteLine("Por favor, ingrese un número válido no se pudo ejecutar su eleccion.");
                            Console.ReadKey();
                            continue; // Regresar al inicio del bucle util para no pasar por switchh
                        }
                        if (ser < 1 || ser > 3)
                        {
                            Console.WriteLine("Por favor, ingrese un número válido no se pudo ejecutar su eleccion.");
                            Console.ReadKey();
                            continue;

                        }
                        else
                        { control = false;
                            do
                            {
                                if (ser == 1)
                                {
                                    if (colaS1.EsVacia())
                                    {
                                        Console.WriteLine("No hay nadie que atender ");
                                        Console.ReadKey();
                                        control = true;
                                    }
                                    else
                                    {
                                        Cliente personaf = colaS1.Peek();
                                        lav.encola(colaS1.atendida());
                                        Console.WriteLine("Se atendio la persona " + personaf + " exitosamente y fue al lavado \n");
                                        Console.ReadKey();
                                    }
                                }
                                if(ser == 2)
                                {
                                    if (colaS2.EsVacia())
                                    {
                                        Console.WriteLine("No hay nadie que atender ");
                                        Console.ReadKey();
                                        control = true;
                                    }
                                    else
                                    {
                                        Cliente personaf = colaS2.atendida();
                                        Console.WriteLine("Se atendio la persona "+personaf+" exitosamente del cambio de aceite \n");
                                        bool soniguales = (personaf.ObtenerVeh() == "auto");
                                        if (soniguales)
                                        {
                                            Console.WriteLine("\n                  ------------------------------FACTURA-----------------------------");
                                            Console.WriteLine("                 -- 1. CAM ACEITE                                         15     --");
                                            Console.WriteLine("                 --                                                              --");
                                            Console.WriteLine("                 --                                                              --");
                                            Console.WriteLine("                 -- TOTAL: 15$                                                   --");
                                            Console.WriteLine("                 ------------------------------------------------------------------");
                                            SC2++;
                                           Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.WriteLine("\n                  ------------------------------FACTURA-----------------------------");
                                            Console.WriteLine("                 -- 1. CAM ACEITE                                         20     --");
                                            Console.WriteLine("                 --                                                              --");
                                            Console.WriteLine("                 --                                                              --");
                                            Console.WriteLine("                 -- TOTAL: 20$                                                   --");
                                            Console.WriteLine("                 ------------------------------------------------------------------");
                                            SC2++;
                                            Console.ReadKey();
                                        }
                                    }

                                }
                                if (ser == 3)
                                {
                                    if (colaS3.EsVacia())
                                    {
                                        Console.WriteLine("No hay nadie que atender ");
                                        Console.ReadKey();
                                        control = true;
                                    }
                                    else
                                    {
                                        pilas.encola(colaS3.atendida());
                                       
                                        pila.push("C4");
                                        pila.push("C3");
                                        pila.push("C2");
                                        pila.push("C1");
                                       
                                        Console.WriteLine("Se atendio la persona " + pilas + " y se retiro al balanceo \n");
                                        SC3++;
                                        Console.ReadKey();
                                        
                                    }

                                }
                            } while (control == true);

                        }
                        #endregion
                        break;
                    case 6:
                        #region atender
                        Console.WriteLine("Que servicio desea atender? (1,2,3): ");
                        string x = Console.ReadLine();
                        if (!int.TryParse(x, out ser))
                        {
                            Console.WriteLine("Por favor, ingrese un número válido no se pudo ejecutar su eleccion.");
                            Console.ReadKey();
                            continue; // Regresar al inicio del bucle util para no pasar por switchh
                        }
                        if (ser < 1 || ser > 3)
                        {
                            Console.WriteLine("Por favor, ingrese un número válido no se pudo ejecutar su eleccion.");
                            Console.ReadKey();
                            continue;

                        }
                        else
                        {
                            if (ser == 1)
                            {
                                if (real==-1)
                                {
                                    Console.WriteLine("No hay nadie que atender ");
                                    Console.ReadKey();
                                   
                                }
                                else
                                {
                                    for(int i = 0; i < 4; i++)
                                    {
                                        Console.WriteLine("Se balanceo " + pila.pop() + " \n");
                                    }
                                    Cliente hola=pilas.atendida();
                                    bool iguales = (hola.ObtenerVeh()== "auto");
                                    if (iguales)
                                    {
                                        Console.WriteLine("\n                  ------------------------------FACTURA-----------------------------");
                                        Console.WriteLine("                 -- 1. BALANCEO                                           25     --");
                                        Console.WriteLine("                 --                                                              --");
                                        Console.WriteLine("                 --                                                              --");
                                        Console.WriteLine("                 -- TOTAL: 25$                                                   --");
                                        Console.WriteLine("                 ------------------------------------------------------------------");
                                        
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.WriteLine("\n                 ------------------------------FACTURA-----------------------------");
                                        Console.WriteLine("                 -- 1. BALANCEO                                           35     --");
                                        Console.WriteLine("                 --                                                              --");
                                        Console.WriteLine("                 --                                                              --");
                                        Console.WriteLine("                 -- TOTAL: 35$                                                   --");
                                        Console.WriteLine("                 ------------------------------------------------------------------");
                                        
                                        Console.ReadKey();
                                    }
                                    Console.ReadKey();
                                }
                            }
                            if(ser == 2)
                            {
                                if (lav.EsVacia())
                                {
                                    Console.WriteLine("no hay nadie en lavado ");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    sec.encola(lav.atendida());
                                    Cliente persona = sec.Peek();
                                    Console.WriteLine("La persona " + persona + " es la primera en pasar al secado");
                                    Console.ReadKey();

                                }
                                

                            }
                            if (ser == 3)
                            {
                                if (sec.EsVacia())
                                {
                                    Console.WriteLine("no hay nadie en secado");
                                    Console.ReadKey();

                                }
                                else
                                {
                                    Cliente persona = sec.atendida();
                                    bool iguales = (persona.ObtenerVeh() == "auto");
                                    if (iguales)
                                    {
                                        Console.WriteLine("\n                  ------------------------------FACTURA-----------------------------");
                                        Console.WriteLine("                 -- 1. ASPIRADO                                           4      --");
                                        Console.WriteLine("                 -- 2. LAVADO                                             6      --");
                                        Console.WriteLine("                 -- 3. SECADO                                             4      --");
                                        Console.WriteLine("                 -- TOTAL: 14$                                                   --");
                                        Console.WriteLine("                 ------------------------------------------------------------------");

                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.WriteLine("\n                 ------------------------------FACTURA-----------------------------");
                                        Console.WriteLine("                 -- 1. ASPIRADO                                           6      --");
                                        Console.WriteLine("                 -- 2. LAVADO                                             10     --");
                                        Console.WriteLine("                 -- 3. SECADO                                             5      --");
                                        Console.WriteLine("                 -- TOTAL: 21$                                                   --");
                                        Console.WriteLine("                 ------------------------------------------------------------------");

                                        Console.ReadKey();
                                    }
                                    Console.ReadKey();

                                }

                            }
                        }
                        #endregion
                        break;
                    case 7:
                        colaespera.imprimir();
                        Console.ReadKey();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
               Console.ReadKey();
            } while (opcion != 0);


            #endregion


           


            
        

            
        }
    }
}
