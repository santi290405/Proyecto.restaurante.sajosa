using System;
using Listas;

public class GestorRestaurantes
{
    private ListaEnlazada<Restaurante> restaurantes;

    public GestorRestaurantes()
    {
        restaurantes = new ListaEnlazada<Restaurante>();
    }

    public void AgregarRestaurante(Restaurante r)
    {
        if (r == null)
            throw new ArgumentNullException(nameof(r));
        if (string.IsNullOrWhiteSpace(r.Nit))
            throw new ArgumentException("El NIT no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(r.Nombre))
            throw new ArgumentException("El nombre no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(r.Celular) || r.Celular.Length != 10)
            throw new ArgumentException("El celular debe tener 10 dígitos.");

        if (BuscarRestaurante(r.Nit) != null)
            throw new InvalidOperationException("Ya existe un restaurante con ese NIT.");

        restaurantes.Agregar(r);
    }

    public Restaurante BuscarRestaurante(string nit)
    {
        if (string.IsNullOrWhiteSpace(nit))
            return null;

        Nodo<Restaurante> actual = restaurantes.Cabeza;
        while (actual != null)
        {
            if (actual.Valor.Nit == nit)
                return actual.Valor;

            actual = actual.Siguiente;
        }

        return null;
    }

    public void ListarRestaurantes()
    {
        Console.WriteLine("\n--- LISTADO DE RESTAURANTES ---");

        Nodo<Restaurante> actual = restaurantes.Cabeza;
        while (actual != null)
        {
            var r = actual.Valor;
            Console.WriteLine($"{r.Nit} - {r.Nombre} - {r.Celular} - {r.Direccion}");
            actual = actual.Siguiente;
        }
    }

    public void EditarRestaurante(string nit, string nuevoNombre = null, string nuevoDueno = null,
                                  string nuevoCelular = null, string nuevaDireccion = null)
    {
        var restaurante = BuscarRestaurante(nit);
        if (restaurante == null)
            throw new InvalidOperationException("Restaurante no encontrado.");

        if (!string.IsNullOrWhiteSpace(nuevoNombre))
            restaurante.Nombre = nuevoNombre;

        if (!string.IsNullOrWhiteSpace(nuevoDueno))
            restaurante.Dueno = nuevoDueno;

        if (!string.IsNullOrWhiteSpace(nuevoCelular))
        {
            if (nuevoCelular.Length != 10)
                throw new ArgumentException("El celular debe tener 10 dígitos.");
            restaurante.Celular = nuevoCelular;
        }

        if (!string.IsNullOrWhiteSpace(nuevaDireccion))
            restaurante.Direccion = nuevaDireccion;
    }

    public void EliminarRestaurante(string nit)
    {
        Nodo<Restaurante> actual = restaurantes.Cabeza;
        int indice = 0;

        while (actual != null)
        {
            if (actual.Valor.Nit == nit)
            {
                restaurantes.EliminarPosicion(indice);
                return;
            }

            actual = actual.Siguiente;
            indice++;
        }

        throw new InvalidOperationException("Restaurante no encontrado.");
    }
}
