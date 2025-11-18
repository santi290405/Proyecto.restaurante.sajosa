using System;
using System.Reflection;
using Listas;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Gestión de Restaurante - by Santi";

        var gestorRestaurantes = new GestorRestaurantes();
        var gestorClientes = new GestorClientes();
        var gestorPlatos = new GestorDePlatos();
        var gestorPedidos = new GestorDePedidos();

        int opcion;

        do
        {
            Console.Clear();
            Titulo("SISTEMA DE GESTIÓN DE RESTAURANTE");

            Console.WriteLine("1. Gestión de Restaurantes");
            Console.WriteLine("2. Gestión de Clientes");
            Console.WriteLine("3. Gestión de Platos");
            Console.WriteLine("4. Gestión de Pedidos");
            Console.WriteLine("0. Salir");

            opcion = LeerEntero("\nSeleccione una opción: ");
            Console.Clear();

            switch (opcion)
            {
                case 1:
                    MenuRestaurantes(gestorRestaurantes);
                    break;
                case 2:
                    MenuClientes(gestorClientes);
                    break;
                case 3:
                    MenuPlatos(gestorPlatos);
                    break;
                case 4:
                    MenuPedidos(gestorPedidos, gestorClientes, gestorPlatos);
                    break;

                case 0:
                    Mensaje("Saliendo del sistema...", ConsoleColor.Cyan);
                    break;
                default:
                    Mensaje("Opción inválida.", ConsoleColor.Red);
                    break;
            }

        } while (opcion != 0);
    }


    static void MenuRestaurantes(GestorRestaurantes gestor)
    {
        int op;
        do
        {
            Console.Clear();
            Titulo("RESTAURANTES REGISTRADOS");
            gestor.ListarRestaurantes();

            Titulo("GESTIÓN DE RESTAURANTES");
            Console.WriteLine("1. Agregar Restaurante");
            Console.WriteLine("2. Buscar Restaurante");
            Console.WriteLine("3. Editar Restaurante");
            Console.WriteLine("4. Eliminar Restaurante");
            Console.WriteLine("0. Volver");

            op = LeerEntero("\nSeleccione: ");
            Console.Clear();

            switch (op)
            {
                case 1:
                    string nit = LeerTexto("NIT: ");
                    string nombre = LeerTexto("Nombre: ");
                    string dueno = LeerTexto("Dueño: ");
                    string cel = LeerTexto("Celular (10 dígitos): ");
                    string dir = LeerTexto("Dirección: ");

                    try
                    {
                        gestor.AgregarRestaurante(new Restaurante(nit, nombre, dueno, cel, dir));
                        Mensaje("Restaurante agregado correctamente.", ConsoleColor.Green);
                    }
                    catch (Exception ex)
                    {
                        Mensaje(ex.Message, ConsoleColor.Red);
                    }
                    break;

                case 2:
                    string nitBus = LeerTexto("NIT a buscar: ");
                    var res = gestor.BuscarRestaurante(nitBus);
                    if (res != null)
                        Mensaje(res.ToString(), ConsoleColor.Green);
                    else
                        Mensaje("No encontrado.", ConsoleColor.Yellow);
                    PromptContinuar();
                    break;

                case 3:
                    Console.WriteLine();
                    Mensaje("Lista de restaurantes (NIT - Nombre):", ConsoleColor.Cyan);
                    PrintRestaurantesShort(gestor);
                    Console.WriteLine();

                    string nitEdit = LeerTexto("Ingrese el NIT del restaurante a editar: ");
                    var r = gestor.BuscarRestaurante(nitEdit);
                    if (r == null)
                    {
                        Mensaje("No existe ese restaurante.", ConsoleColor.Red);
                        PromptContinuar();
                        break;
                    }

                    Mensaje("Deje en blanco para no cambiar el dato.", ConsoleColor.Yellow);
                    string nNom = LeerOpcional("Nuevo nombre: ");
                    string nDue = LeerOpcional("Nuevo dueño: ");
                    string nCel = LeerOpcional("Nuevo celular: ");
                    string nDir = LeerOpcional("Nueva dirección: ");

                    try
                    {
                        gestor.EditarRestaurante(nitEdit, nNom, nDue, nCel, nDir);
                        Mensaje("Restaurante actualizado.", ConsoleColor.Green);
                    }
                    catch (Exception ex)
                    {
                        Mensaje(ex.Message, ConsoleColor.Red);
                    }
                    PromptContinuar();
                    break;

                case 4:
                    Console.WriteLine();
                    Mensaje("Lista de restaurantes (NIT - Nombre):", ConsoleColor.Cyan);
                    PrintRestaurantesShort(gestor);
                    Console.WriteLine();

                    string nitDel = LeerTexto("NIT a eliminar: ");
                    try
                    {
                        gestor.EliminarRestaurante(nitDel);
                        Mensaje("Restaurante eliminado.", ConsoleColor.Green);
                    }
                    catch (Exception ex)
                    {
                        Mensaje(ex.Message, ConsoleColor.Red);
                    }
                    PromptContinuar();
                    break;

                case 0:
                    break;

                default:
                    Mensaje("Opción inválida.", ConsoleColor.Red);
                    break;
            }

        } while (op != 0);
    }

    static void MenuClientes(GestorClientes gestor)
    {
        int op;
        do
        {
            Console.Clear();
            Titulo("CLIENTES REGISTRADOS");
            gestor.ListarClientes();

            Titulo("GESTIÓN DE CLIENTES");
            Console.WriteLine("1. Agregar Cliente");
            Console.WriteLine("2. Buscar Cliente");
            Console.WriteLine("3. Editar Cliente");
            Console.WriteLine("4. Eliminar Cliente");
            Console.WriteLine("0. Volver");

            op = LeerEntero("\nSeleccione: ");
            Console.Clear();

            switch (op)
            {
                case 1:
                    string ced = LeerTexto("Cédula: ");
                    string nom = LeerTexto("Nombre: ");
                    string cel = LeerTexto("Celular: ");
                    string email = LeerTexto("Email: ");

                    try
                    {
                        gestor.AgregarCliente(new Cliente(ced, nom, cel, email));
                        Mensaje("Cliente agregado correctamente.", ConsoleColor.Green);
                    }
                    catch (Exception ex)
                    {
                        Mensaje(ex.Message, ConsoleColor.Red);
                    }
                    PromptContinuar();
                    break;

                case 2:
                    var c = gestor.BuscarCliente(LeerTexto("Cédula a buscar: "));
                    if (c != null) Mensaje(c.ToString(), ConsoleColor.Green);
                    else Mensaje("Cliente no encontrado.", ConsoleColor.Yellow);
                    PromptContinuar();
                    break;

                case 3: 
                    Console.WriteLine();
                    Mensaje("Clientes (Cédula - Nombre):", ConsoleColor.Cyan);
                    PrintClientesShort(gestor);
                    Console.WriteLine();

                    string cedEdit = LeerTexto("Cédula del cliente a editar: ");
                    var cli = gestor.BuscarCliente(cedEdit);
                    if (cli == null)
                    {
                        Mensaje("Cliente no encontrado.", ConsoleColor.Red);
                        PromptContinuar();
                        break;
                    }

                    Mensaje("Deje en blanco para no cambiar el dato.", ConsoleColor.Yellow);
                    string nNom = LeerOpcional("Nuevo nombre: ");
                    string nCel = LeerOpcional("Nuevo celular: ");
                    string nEmail = LeerOpcional("Nuevo email: ");

                    try
                    {
                        gestor.EditarCliente(cedEdit, nNom, nCel, nEmail);
                        Mensaje("Cliente actualizado.", ConsoleColor.Green);
                    }
                    catch (Exception ex)
                    {
                        Mensaje(ex.Message, ConsoleColor.Red);
                    }
                    PromptContinuar();
                    break;

                case 4:
                    Console.WriteLine();
                    Mensaje("Clientes (Cédula - Nombre):", ConsoleColor.Cyan);
                    PrintClientesShort(gestor);
                    Console.WriteLine();

                    gestor.EliminarCliente(LeerTexto("Cédula a eliminar: "));
                    Mensaje("Cliente eliminado (si existía).", ConsoleColor.Green);
                    PromptContinuar();
                    break;

                case 0:
                    break;

                default:
                    Mensaje("Opción inválida.", ConsoleColor.Red);
                    break;
            }

        } while (op != 0);
    }
    static void MenuPlatos(GestorDePlatos gestor)
    {
        int op;
        do
        {
            Console.Clear();
            Titulo("PLATOS REGISTRADOS");
            gestor.ListarPlatos();

            Titulo("GESTIÓN DE PLATOS");
            Console.WriteLine("1. Agregar Plato");
            Console.WriteLine("2. Buscar Plato");
            Console.WriteLine("3. Editar Plato");
            Console.WriteLine("4. Eliminar Plato");
            Console.WriteLine("0. Volver");

            op = LeerEntero("\nSeleccione: ");
            Console.Clear();

            switch (op)
            {
                case 1:
                    string cod = LeerTexto("Código: ");
                    string nom = LeerTexto("Nombre: ");
                    string desc = LeerTexto("Descripción: ");
                    decimal precio = LeerDecimal("Precio: ");
                    gestor.AgregarPlato(cod, nom, desc, precio);
                    Mensaje("Plato agregado.", ConsoleColor.Green);
                    PromptContinuar();
                    break;

                case 2:
                    var p = gestor.BuscarPlato(LeerTexto("Código a buscar: "));
                    if (p != null) Mensaje(p.ToString(), ConsoleColor.Green);
                    else Mensaje("No encontrado.", ConsoleColor.Yellow);
                    PromptContinuar();
                    break;

                case 3:
                    Console.WriteLine();
                    Mensaje("Platos (Código - Nombre):", ConsoleColor.Cyan);
                    PrintPlatosShort(gestor);
                    Console.WriteLine();

                    string codEdit = LeerTexto("Código del plato a editar: ");
                    var pl = gestor.BuscarPlato(codEdit);
                    if (pl == null)
                    {
                        Mensaje("Plato no encontrado.", ConsoleColor.Red);
                        PromptContinuar();
                        break;
                    }

                    Mensaje("Deje en blanco para no cambiar el dato.", ConsoleColor.Yellow);
                    string nNom = LeerOpcional("Nuevo nombre: ");
                    string nDesc = LeerOpcional("Nueva descripción: ");
                    string nPrecioStr = LeerOpcional("Nuevo precio: ");
                    decimal nPrecio = pl.Precio; 
                    if (decimal.TryParse(nPrecioStr, out decimal precioVal))
                        nPrecio = precioVal;

                    try
                    {
                        gestor.EditarPlato(codEdit, nNom, nDesc, nPrecio);
                        Mensaje("Plato actualizado.", ConsoleColor.Green);
                    }
                    catch (Exception ex)
                    {
                        Mensaje(ex.Message, ConsoleColor.Red);
                    }
                    PromptContinuar();
                    break;

                case 4:
                    Console.WriteLine();
                    Mensaje("Platos (Código - Nombre):", ConsoleColor.Cyan);
                    PrintPlatosShort(gestor);
                    Console.WriteLine();

                    gestor.EliminarPlato(LeerTexto("Código a eliminar: "));
                    Mensaje("Plato eliminado (si existía).", ConsoleColor.Green);
                    PromptContinuar();
                    break;

                case 0:
                    break;

                default:
                    Mensaje("Opción inválida.", ConsoleColor.Red);
                    break;
            }

        } while (op != 0);
    }
    static void MenuPedidos(GestorDePedidos gestor, GestorClientes clientes, GestorDePlatos platos)
    {
        int op;
        do
        {
            Console.Clear();

            Titulo("PEDIDOS PENDIENTES");
            gestor.MostrarPedidosPendientes();

            Titulo("PEDIDOS DESPACHADOS");
            gestor.MostrarPedidosDespachados();

            Titulo("GESTIÓN DE PEDIDOS");
            Console.WriteLine("1. Crear Pedido");
            Console.WriteLine("2. Despachar Pedido");
            Console.WriteLine("3. Ver siguiente pedido");
            Console.WriteLine("4. Ganancias del día");
            Console.WriteLine("0. Volver");

            op = LeerEntero("\nSeleccione: ");
            Console.Clear();

            switch (op)
            {
                case 1:
                    string cedCliente = LeerTexto("Cédula del cliente: ");
                    var cliente = clientes.BuscarCliente(cedCliente);

                    if (cliente == null)
                    {
                        Mensaje("El cliente no existe.", ConsoleColor.Red);
                        PromptContinuar();
                        break;
                    }

                    ListaEnlazada<PlatoPedido> listaPlatos = new ListaEnlazada<PlatoPedido>();

                    Mensaje("Agregando platos al pedido. Escriba '0' como código para terminar.", ConsoleColor.Yellow);

                    while (true)
                    {
                        string codPlato = LeerTexto("Código del plato: ");
                        if (codPlato == "0") break;

                        var plato = platos.BuscarPlato(codPlato);
                        if (plato == null)
                        {
                            Mensaje("Plato no encontrado.", ConsoleColor.Red);
                            continue;
                        }

                        int cant = LeerEntero("Cantidad: ");
                        listaPlatos.Agregar(new PlatoPedido(plato.Codigo, cant, plato.Precio));
                    }

                    gestor.CrearPedido(cedCliente, listaPlatos);
                    PromptContinuar();
                    break;

                case 2:
                    gestor.DespacharPedido();
                    PromptContinuar();
                    break;

                case 3:
                    gestor.VerSiguientePedido();
                    PromptContinuar();
                    break;

                case 4:
                    Console.WriteLine($"Ganancias de hoy: ${gestor.CalcularGananciasDelDia()}");
                    PromptContinuar();
                    break;

                case 0:
                    break;

                default:
                    Mensaje("Opción inválida.", ConsoleColor.Red);
                    break;
            }

        } while (op != 0);
    }
    static int LeerEntero(string msg)
    {
        int valor;
        Console.Write(msg);
        while (!int.TryParse(Console.ReadLine(), out valor))
        {
            Mensaje("Ingrese un número válido.", ConsoleColor.Red);
            Console.Write(msg);
        }
        return valor;
    }

    static decimal LeerDecimal(string msg)
    {
        decimal valor;
        Console.Write(msg);
        while (!decimal.TryParse(Console.ReadLine(), out valor))
        {
            Mensaje("Ingrese un valor decimal válido.", ConsoleColor.Red);
            Console.Write(msg);
        }
        return valor;
    }

    static string LeerTexto(string msg)
    {
        string valor;
        do
        {
            Console.Write(msg);
            valor = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(valor));
        return valor;
    }

    static string LeerOpcional(string msg)
    {
        Console.Write(msg);
        return Console.ReadLine();
    }

    static void Mensaje(string msg, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    static void Titulo(string texto)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n=== " + texto.ToUpper() + " ===\n");
        Console.ResetColor();
    }

    static void PromptContinuar()
    {
        Console.WriteLine();
        Console.Write("Presiona ENTER para continuar...");
        Console.ReadLine();
    }
    static void PrintRestaurantesShort(GestorRestaurantes gestor)
    {
        try
        {
            FieldInfo field = gestor.GetType().GetField("restaurantes", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
            {
                gestor.ListarRestaurantes();
                return;
            }

            object lista = field.GetValue(gestor);
            if (lista == null)
            {
                Console.WriteLine("No hay restaurantes registrados.");
                return;
            }

            PropertyInfo cabezaProp = lista.GetType().GetProperty("Cabeza", BindingFlags.Public | BindingFlags.Instance);
            object nodo = cabezaProp.GetValue(lista);

            if (nodo == null)
            {
                Console.WriteLine("No hay restaurantes registrados.");
                return;
            }

            while (nodo != null)
            {
                object valor = nodo.GetType().GetProperty("Valor").GetValue(nodo);
                string nit = valor.GetType().GetProperty("Nit").GetValue(valor)?.ToString();
                string nombre = valor.GetType().GetProperty("Nombre").GetValue(valor)?.ToString();
                Console.WriteLine($"{nit} - {nombre}");
                nodo = nodo.GetType().GetProperty("Siguiente").GetValue(nodo);
            }
        }
        catch
        {
            gestor.ListarRestaurantes();
        }
    }

    static void PrintClientesShort(GestorClientes gestor)
    {
        try
        {
            FieldInfo field = gestor.GetType().GetField("clientes", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
            {
                gestor.ListarClientes();
                return;
            }

            object lista = field.GetValue(gestor);
            if (lista == null)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            PropertyInfo cabezaProp = lista.GetType().GetProperty("Cabeza", BindingFlags.Public | BindingFlags.Instance);
            object nodo = cabezaProp.GetValue(lista);

            if (nodo == null)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            while (nodo != null)
            {
                object valor = nodo.GetType().GetProperty("Valor").GetValue(nodo);
                string ced = valor.GetType().GetProperty("Cedula").GetValue(valor)?.ToString();
                string nom = valor.GetType().GetProperty("NombreCompleto").GetValue(valor)?.ToString();
                Console.WriteLine($"{ced} - {nom}");
                nodo = nodo.GetType().GetProperty("Siguiente").GetValue(nodo);
            }
        }
        catch
        {
            gestor.ListarClientes();
        }
    }

    static void PrintPlatosShort(GestorDePlatos gestor)
    {
        try
        {
            FieldInfo field = gestor.GetType().GetField("platos", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
            {
                gestor.ListarPlatos();
                return;
            }

            object lista = field.GetValue(gestor);
            if (lista == null)
            {
                Console.WriteLine("No hay platos registrados.");
                return;
            }

            PropertyInfo cabezaProp = lista.GetType().GetProperty("Cabeza", BindingFlags.Public | BindingFlags.Instance);
            object nodo = cabezaProp.GetValue(lista);

            if (nodo == null)
            {
                Console.WriteLine("No hay platos registrados.");
                return;
            }

          while (nodo != null)
{
    object valor = nodo.GetType().GetProperty("Valor").GetValue(nodo);
    string codigo = valor.GetType().GetProperty("Codigo").GetValue(valor)?.ToString();
    string nombre = valor.GetType().GetProperty("Nombre").GetValue(valor)?.ToString();
    Console.WriteLine($"{codigo} - {nombre}");
    nodo = nodo.GetType().GetProperty("Siguiente").GetValue(nodo);
} 
        }
        catch
        {
            gestor.ListarPlatos();
        }
    }
}
