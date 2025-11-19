using System;
using Listas; 

public class GestorDePlatos
{
    private ListaEnlazada<Plato> platos;

    public GestorDePlatos()
    {
        platos = new ListaEnlazada<Plato>();
    }

    public void AgregarPlato(string codigo, string nombre, string descripcion, decimal precio)
    {
        if (BuscarPlato(codigo) != null)
        {
            Console.WriteLine($"Ya existe un plato con el código {codigo}.");
            return;
        }

        Plato nuevo = new Plato(codigo, nombre, descripcion, precio);
        platos.Agregar(nuevo);  
        Console.WriteLine($"Plato '{nombre}' agregado correctamente.");
    }

    public Plato BuscarPlato(string codigo)
    {
        Nodo<Plato> actual = platos.Cabeza;

        while (actual != null)
        {
            if (actual.Valor.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                return actual.Valor;

            actual = actual.Siguiente;
        }

        return null;
    }
        public bool EliminarPlato(string codigo, GestorDePedidos gestorPedidos)
    {
        Nodo<Plato>? actual = platos.Cabeza;
        int index = 0;

        while (actual != null)
        {
            if (actual.Valor.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
            {
                if (gestorPedidos.PlatoEnPedidoPendiente(codigo))
                    throw new Exception("No se puede eliminar el plato, está en un pedido pendiente.");

                platos.EliminarPosicion(index);
                Console.WriteLine($"Plato con código {codigo} eliminado.");
                return true;
            }

            actual = actual.Siguiente;
            index++;
        }

        Console.WriteLine($"No se encontró el plato con código {codigo}.");
        return false;
    }

    public void ListarPlatos()
    {
        if (platos.Cabeza == null)
        {
            Console.WriteLine("No hay platos registrados.");
            return;
        }

        Console.WriteLine("Lista de platos del menú:");
        platos.Imprimir();
        Console.WriteLine();
    }
    public bool EditarPlato(string codigo, string nuevoNombre, string nuevaDescripcion, decimal nuevoPrecio)
    {
        Plato plato = BuscarPlato(codigo);

        if (plato == null)
        {
            Console.WriteLine($"No se encontró el plato con código {codigo}.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(nuevoNombre))
            plato.Nombre = nuevoNombre;

        if (!string.IsNullOrWhiteSpace(nuevaDescripcion))
            plato.Descripcion = nuevaDescripcion;

        if (nuevoPrecio > 0)
            plato.Precio = nuevoPrecio;

        Console.WriteLine($"Plato con código {codigo} actualizado.");
        return true;
    }

    public Plato ObtenerPlatoPorCodigo(string codigo)
    {
        return BuscarPlato(codigo);
    }
}
