using System;
using Listas;

public class HistorialServicio
{
    private Pila<Pedido> pedidosServidos;

    public HistorialServicio()
    {
        this.pedidosServidos = new Pila<Pedido>();
    }

    public Pila<Pedido> PedidosServidos
    {
        get { return pedidosServidos; }
    }

    public void AgregarPedidoServido(Pedido pedido)
    {
        if (pedido == null)
        {
            throw new ArgumentNullException("El pedido no puede ser nulo");
        }

        if (!pedido.EstaDespachado())
        {
            throw new InvalidOperationException("Solo se pueden agregar pedidos despachados al historial");
        }

        pedidosServidos.Apilar(pedido);
        Console.WriteLine($" Pedido #{pedido.IdPedido} agregado al historial de servicio");
    }

    public Pedido? VerUltimoPedidoServido()
{
    if (pedidosServidos.EstaVacia())
    {
        Console.WriteLine("No hay pedidos en el historial");
        return null;
    }

    try
    {
        return pedidosServidos.Cima();
    }
    catch (InvalidOperationException)
    {
        Console.WriteLine("Error al obtener el último pedido");
        return null;
    }
}

public Pedido? ObtenerYRemoverUltimoPedido()
{
    if (pedidosServidos.EstaVacia())
    {
        Console.WriteLine("No hay pedidos en el historial");
        return null;
    }

    try
    {
        Pedido pedido = pedidosServidos.Desapilar();
        Console.WriteLine($"📤 Pedido #{pedido.IdPedido} removido del historial");
        return pedido;
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        return null;
    }
}

    public void MostrarHistorial()
    {
        if (pedidosServidos.EstaVacia())
        {
            Console.WriteLine(" El historial de servicio está vacío");
            return;
        }

        Console.WriteLine(" === HISTORIAL DE PEDIDOS SERVIDOS ===");
        Console.WriteLine("(Más recientes primero)");
        Console.WriteLine("===========================================");

        pedidosServidos.MostrarPila(pedido =>
        {
            Console.WriteLine($" PEDIDO #{pedido.IdPedido}");
            Console.WriteLine($" Cliente: {pedido.CedulaCliente}");
            Console.WriteLine($" Total: ${pedido.Total}");
            Console.WriteLine($" Fecha: {pedido.FechaHora:dd/MM/yyyy HH:mm}");
            Console.WriteLine("  Platos:");
            
            if (pedido.Platos.Cabeza != null)
            {
                Nodo<PlatoPedido> actual = pedido.Platos.Cabeza;
                while (actual != null)
                {
                    PlatoPedido platoPedido = actual.Valor;
                    Console.WriteLine($"      • {platoPedido.CodigoPlato} x{platoPedido.Cantidad} - ${platoPedido.Subtotal}");
                    actual = actual.Siguiente;
                }
            }
            Console.WriteLine("-------------------------------------------");
        });
    }

    public int ObtenerCantidadPedidosServidos()
    {

        Pila<Pedido> tempPila = new Pila<Pedido>();
        int contador = 0;
        

        while (!pedidosServidos.EstaVacia())
        {
            Pedido pedido = pedidosServidos.Desapilar();
            tempPila.Apilar(pedido);
            contador++;
        }
        

        while (!tempPila.EstaVacia())
        {
            pedidosServidos.Apilar(tempPila.Desapilar());
        }
        
        return contador;
    }

    public bool EstaVacio()
    {
        return pedidosServidos.EstaVacia();
    }

    public void LimpiarHistorial()
    {
        while (!pedidosServidos.EstaVacia())
        {
            pedidosServidos.Desapilar();
        }
        Console.WriteLine(" Historial de servicio limpiado completamente");
    }

    public void BuscarPedidosPorCliente(string cedulaCliente)
    {
        if (pedidosServidos.EstaVacia())
        {
            Console.WriteLine("No hay pedidos en el historial para buscar");
            return;
        }

        Console.WriteLine($" Buscando pedidos del cliente: {cedulaCliente}");
        bool encontrado = false;

        pedidosServidos.MostrarPila(pedido =>
        {
            if (pedido.CedulaCliente == cedulaCliente)
            {
                Console.WriteLine($"• Pedido #{pedido.IdPedido} - Total: ${pedido.Total} - Fecha: {pedido.FechaHora:dd/MM/yyyy}");
                encontrado = true;
            }
        });

        if (!encontrado)
        {
            Console.WriteLine("No se encontraron pedidos para ese cliente");
        }
    }

    public void MostrarResumen()
{
    int cantidad = ObtenerCantidadPedidosServidos();
    
    if (cantidad == 0)
    {
        Console.WriteLine(" Resumen: Historial vacío");
        return;
    }

    Pedido? ultimoPedido = VerUltimoPedidoServido();
    
    Console.WriteLine($" Resumen del historial:");
    Console.WriteLine($" • Total de pedidos servidos: {cantidad}");
    
    if (ultimoPedido != null)
    {
        Console.WriteLine($"   • Último pedido: #{ultimoPedido.IdPedido} - ${ultimoPedido.Total}");
    }
    else
    {
        Console.WriteLine($"   • Último pedido: No disponible");
    }
}

    public decimal CalcularGananciasTotales()
    {
        Pila<Pedido> tempPila = new Pila<Pedido>();
        decimal ganancias = 0;
        

        while (!pedidosServidos.EstaVacia())
        {
            Pedido pedido = pedidosServidos.Desapilar();
            ganancias += pedido.Total;
            tempPila.Apilar(pedido);
        }
        

        while (!tempPila.EstaVacia())
        {
            pedidosServidos.Apilar(tempPila.Desapilar());
        }
        
        return ganancias;
    }
}