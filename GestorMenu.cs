using System;
using Listas;

public class GestorMenu
{
    private ListaEnlazada<Plato> platos;

    public GestorMenu()
    {
        platos = new ListaEnlazada<Plato>();
    }

    public void AgregarPlato(Plato p)
    {
        if (BuscarPlato(p.Codigo) != null)
            throw new Exception("Ya existe un plato con ese código.");

        platos.Agregar(p);
    }

    public bool EliminarPlato(string codigo)
    {
        Nodo<Plato> actual = platos.Cabeza;
        int indice = 0;

        while (actual != null)
        {
            if (actual.Valor.Codigo == codigo)
            {
                platos.EliminarPosicion(indice);
                return true;
            }
            actual = actual.Siguiente;
            indice++;
        }

        return false;
    }

    public Plato BuscarPlato(string codigo)
    {
        Nodo<Plato> actual = platos.Cabeza;

        while (actual != null)
        {
            if (actual.Valor.Codigo == codigo)
                return actual.Valor;

            actual = actual.Siguiente;
        }

        return null;
    }

    public void ListarPlatos()
    {
        Console.WriteLine("\n--- MENÚ DEL RESTAURANTE ---");

        Nodo<Plato> actual = platos.Cabeza;

        while (actual != null)
        {
            Plato p = actual.Valor;
            Console.WriteLine($"{p.Codigo} - {p.Nombre} - ${p.Precio}");
            actual = actual.Siguiente;
        }
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
}
