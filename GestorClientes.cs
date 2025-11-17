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
        // Verificar si ya existe la cédula
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

 
    public void EliminarCliente(string cedula)
    {
        Nodo<Cliente> actual = clientes.Cabeza;
        int posicion = 0;

        while (actual != null)
        {
            if (actual.Valor.Cedula == cedula)
            {
                clientes.EliminarPosicion(posicion);
                return;
            }

            actual = actual.Siguiente;
            posicion++;
        }

        Console.WriteLine("Cliente no encontrado.");
    }
}

