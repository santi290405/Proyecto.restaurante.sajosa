using System;
using Listas;

public class GestorDePedidos
{
    private Cola<Pedido> pedidosPendientes;
    private ListaEnlazada<Pedido> pedidosDespachados;
    private Pila<PlatoPedido> historialPlatosServidos;
    private int contadorPedidos;

    public GestorDePedidos()
    {
        pedidosPendientes = new Cola<Pedido>();
        pedidosDespachados = new ListaEnlazada<Pedido>();
        historialPlatosServidos = new Pila<PlatoPedido>();
        contadorPedidos = 1;
    }


    public void CrearPedido(string nitRestaurante, string cedulaCliente, ListaEnlazada<PlatoPedido> platos)
    {
        if (string.IsNullOrWhiteSpace(cedulaCliente))
        {
            Console.WriteLine("La cédula del cliente no puede estar vacía.");
            return;
        }

        if (platos == null || platos.Cabeza == null)
        {
            Console.WriteLine("El pedido debe contener al menos un plato.");
            return;
        }

        Pedido nuevoPedido = new Pedido(contadorPedidos++, cedulaCliente, nitRestaurante);

        Nodo<PlatoPedido> actual = platos.Cabeza;
        while (actual != null)
        {
            nuevoPedido.AgregarPlato(actual.Valor);
            actual = actual.Siguiente;
        }

        pedidosPendientes.Encolar(nuevoPedido);
        Console.WriteLine($"Pedido #{nuevoPedido.IdPedido} creado y agregado a la cola de pendientes.");
    }

   
    public void DespacharPedido()
    {
        if (pedidosPendientes.EstaVacia())
        {
            Console.WriteLine("No hay pedidos pendientes para despachar.");
            return;
        }

        Pedido pedido = pedidosPendientes.Desencolar();
        pedido.Despachar();
        pedidosDespachados.Agregar(pedido);

        Console.WriteLine($"Pedido #{pedido.IdPedido} despachado correctamente.");

        Nodo<PlatoPedido> actual = pedido.Platos.Cabeza;

        while (actual != null)
    {
    historialPlatosServidos.Apilar(actual.Valor);
    actual = actual.Siguiente;
    }

    }

    public void VerSiguientePedido()
    {
        if (pedidosPendientes.EstaVacia())
        {
            Console.WriteLine("No hay pedidos en espera.");
            return;
        }

        Pedido siguiente = pedidosPendientes.VerFrente();
        Console.WriteLine("Siguiente pedido en cola:");
        Console.WriteLine(siguiente.ToString());
    }

    public void MostrarPedidosPendientes()
    {
        Console.WriteLine("\n=== PEDIDOS PENDIENTES ===");
        if (pedidosPendientes.EstaVacia())
        {
            Console.WriteLine("No hay pedidos pendientes.");
            return;
        }

        pedidosPendientes.Mostrar();
    }

    public void MostrarPedidosDespachados()
    {
        Console.WriteLine("\n=== PEDIDOS DESPACHADOS ===");

        if (pedidosDespachados.Cabeza == null)
        {
            Console.WriteLine("No hay pedidos despachados aún.");
            return;
        }

        pedidosDespachados.Imprimir();
    }

    public decimal CalcularGananciasDelDia()
    {
        decimal total = 0;
        DateTime hoy = DateTime.Today;

        Nodo<Pedido> actual = pedidosDespachados.Cabeza;
        while (actual != null)
        {
            Pedido pedido = actual.Valor;

            if (pedido.FechaHora.Date == hoy && pedido.Estado == "DESPACHADO")
            {
                total += pedido.Total;
            }

            actual = actual.Siguiente;
        }

        return total;
    }
    public void MostrarPlatosServidos()
{
    Console.WriteLine("=== PLATOS SERVIDOS ===");

    if (historialPlatosServidos.EstaVacia())
    {
        Console.WriteLine("No hay platos servidos aún.");
        return;
    }

    historialPlatosServidos.MostrarPila(plato =>
    {
        Console.WriteLine($"{plato.CodigoPlato} x{plato.Cantidad} - ${plato.PrecioUnitario}");
    });
}

   
    public bool EditarPedido(int idPedido, string nuevaCedulaCliente, ListaEnlazada<PlatoPedido> nuevosPlatos)
    {
       
        Cola<Pedido> colaTemporal = new Cola<Pedido>();
        Pedido pedidoEncontrado = null;

        while (!pedidosPendientes.EstaVacia())
        {
            var pedido = pedidosPendientes.Desencolar();

            if (pedido.IdPedido == idPedido)
                pedidoEncontrado = pedido;

            colaTemporal.Encolar(pedido);
        }

        while (!colaTemporal.EstaVacia())
            pedidosPendientes.Encolar(colaTemporal.Desencolar());

      
        if (pedidoEncontrado == null)
        {
            Console.WriteLine("Pedido no encontrado o ya está despachado.");
            return false;
        }

      
        if (!string.IsNullOrWhiteSpace(nuevaCedulaCliente))
            pedidoEncontrado.CedulaCliente = nuevaCedulaCliente;

        if (nuevosPlatos != null && nuevosPlatos.Cabeza != null)
        {
            
            while (pedidoEncontrado.EliminarPlato(0)) { }

            
            Nodo<PlatoPedido> actual = nuevosPlatos.Cabeza;
            while (actual != null)
            {
                pedidoEncontrado.AgregarPlato(actual.Valor);
                actual = actual.Siguiente;
            }
        }

        Console.WriteLine($"Pedido #{idPedido} editado correctamente.");
        return true;
    }
public bool TienePedidosPendientesDe(string cedulaCliente)
{
    Nodo<Pedido>? actual = pedidosPendientes.Frente;

    while (actual != null)
    {
        if (actual.Valor.CedulaCliente == cedulaCliente)
            return true;

        actual = actual.Siguiente;
    }

    return false;
}

public bool PlatoEnPedidoPendiente(string codigoPlato)
{
    Nodo<Pedido>? actual = pedidosPendientes.Frente;

    while (actual != null)
    {
        Nodo<PlatoPedido>? p = actual.Valor.Platos.Cabeza;

        while (p != null)
        {
            if (p.Valor.CodigoPlato == codigoPlato)
                return true;

            p = p.Siguiente;
        }

        actual = actual.Siguiente;
    }

    return false;
}

public bool RestauranteTienePedidosPendientes(string nitRestaurante)
{
    Nodo<Pedido>? actual = pedidosPendientes.Frente;

    while (actual != null)
    {
        if (actual.Valor.NitRestaurante == nitRestaurante)
            return true;

        actual = actual.Siguiente;
    }

    return false;
}


}
