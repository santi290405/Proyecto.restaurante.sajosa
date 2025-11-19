using System;
using Listas;

public class GestorClientes
{
    private ListaEnlazada<Cliente> clientes;

    public GestorClientes()
    {
        clientes = new ListaEnlazada<Cliente>();
    }

    public void AgregarCliente(Cliente c)
    {
        if (BuscarCliente(c.Cedula) != null)
            throw new Exception("Ya existe un cliente con esa cédula.");

        clientes.Agregar(c);
    }

    public Cliente BuscarCliente(string cedula)
    {
        Nodo<Cliente> actual = clientes.Cabeza;

        while (actual != null)
        {
            if (actual.Valor.Cedula == cedula)
                return actual.Valor;

            actual = actual.Siguiente;
        }

        return null;
    }

    public void ListarClientes()
    {
        Console.WriteLine("\n--- LISTA DE CLIENTES ---");

        Nodo<Cliente> actual = clientes.Cabeza;
        while (actual != null)
        {
            Console.WriteLine(actual.Valor);
            actual = actual.Siguiente;
        }
    }

    public void EliminarCliente(string cedula, GestorDePedidos gestorPedidos)
    {
        Nodo<Cliente> actual = clientes.Cabeza;
        int posicion = 0;

        while (actual != null)
        {
            if (actual.Valor.Cedula == cedula)
            {
                if (gestorPedidos.TienePedidosPendientesDe(cedula))
                    throw new Exception("No se puede eliminar el cliente porque tiene pedidos pendientes.");

                clientes.EliminarPosicion(posicion);
                return;
            }

            actual = actual.Siguiente;
            posicion++;
        }

        Console.WriteLine("Cliente no encontrado.");
    }
    public bool EditarCliente(
        string cedula,
        string? nuevoNombre = null,
        string? nuevoCelular = null,
        string? nuevoEmail = null)
    {
        Cliente cli = BuscarCliente(cedula);
        if (cli == null)
            return false;

        if (!string.IsNullOrWhiteSpace(nuevoNombre))
            cli.NombreCompleto = nuevoNombre;

        if (!string.IsNullOrWhiteSpace(nuevoCelular))
        {
            if (nuevoCelular.Length != 10)
                throw new Exception("El celular debe tener 10 dígitos.");
            cli.Celular = nuevoCelular;
        }

        if (!string.IsNullOrWhiteSpace(nuevoEmail))
        {
            if (!nuevoEmail.Contains("@"))
                throw new Exception("El email no es válido.");
            cli.Email = nuevoEmail;
        }

        return true;
    }
}


