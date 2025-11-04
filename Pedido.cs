using Listas;
using System;

public class Pedido
{
    private int idPedido;
    private string cedulaCliente;
    private ListaEnlazada<PlatoPedido> platos;
    private decimal total;
    private DateTime fechaHora;
    private string estado;

    public Pedido(int idPedido, string cedulaCliente)
    {
        this.idPedido = idPedido;
        this.cedulaCliente = cedulaCliente;
        this.platos = new ListaEnlazada<PlatoPedido>();
        this.total = 0;
        this.fechaHora = DateTime.Now;
        this.estado = "PENDIENTE";
    }

    public int IdPedido 
    { 
        get { return idPedido; } 
    }

    public string CedulaCliente 
    { 
        get { return cedulaCliente; } 
        set { cedulaCliente = value; }
    }

    public ListaEnlazada<PlatoPedido> Platos 
    { 
        get { return platos; } 
    }

    public decimal Total 
    { 
        get { return total; } 
    }

    public DateTime FechaHora 
    { 
        get { return fechaHora; } 
    }

    public string Estado 
    { 
        get { return estado; } 
        set { estado = value; }
    }

    public void AgregarPlato(PlatoPedido platoPedido)
    {
        platos.Agregar(platoPedido);
        CalcularTotal();
    }

    public bool EliminarPlato(int posicion)
    {
        if (posicion >= 0 && posicion < platos.Contar())
        {
            platos.EliminarPosicion(posicion);
            CalcularTotal();
            return true;
        }
        return false;
    }

    private void CalcularTotal()
{
    total = 0;

    if (platos.Cabeza == null) return;

    Nodo<PlatoPedido> actual = platos.Cabeza;
    while (actual != null)
    {
        PlatoPedido platoPedido = actual.Valor;
        decimal subtotal = platoPedido.Cantidad * platoPedido.PrecioUnitario;
        total += subtotal;
        actual = actual.Siguiente;
    }
}

    public void Despachar()
    {
        this.estado = "DESPACHADO";
    }

    public bool EstaPendiente()
    {
        return estado == "PENDIENTE";
    }

    public bool EstaDespachado()
    {
        return estado == "DESPACHADO";
    }

    public void ListarPlatosPedido()
    {
        if (platos.Cabeza == null)
        {
            Console.WriteLine("El pedido no tiene platos.");
            return;
        }

        Console.WriteLine($"=== DETALLE DEL PEDIDO #{idPedido} ===");
        Nodo<PlatoPedido> actual = platos.Cabeza;
        int contador = 1;
        
        while (actual != null)
        {
            PlatoPedido platoPedido = actual.Valor;
            Console.WriteLine($"{contador}. {platoPedido.CodigoPlato} - Cantidad: {platoPedido.Cantidad} - Subtotal: ${platoPedido.Subtotal}");
            actual = actual.Siguiente;
            contador++;
        }
        
        Console.WriteLine($"TOTAL DEL PEDIDO: ${total}");
        Console.WriteLine($"Estado: {estado}");
    }

    public override string ToString()
    {
        return $"Pedido #{idPedido} - Cliente: {cedulaCliente} - Total: ${total} - Estado: {estado} - Fecha: {fechaHora:dd/MM/yyyy HH:mm}";
    }
}