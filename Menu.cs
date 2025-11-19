using Listas;
using System;

public class Menu
{
    private string codigoMenu;
    private string nombre;
    private ListaEnlazada<Plato> platos;

   
    public Menu(string codigoMenu, string nombre)
    {
        this.codigoMenu = codigoMenu;
        this.nombre = nombre;
        this.platos = new ListaEnlazada<Plato>();
    }

 
    public string CodigoMenu 
    { 
        get { return codigoMenu; } 
        set { codigoMenu = value; }
    }

    public string Nombre 
    { 
        get { return nombre; } 
        set { nombre = value; }
    }

    public ListaEnlazada<Plato> Platos 
    { 
        get { return platos; } 
    }


    public void AgregarPlato(Plato plato)
    {
        platos.Agregar(plato);
    }


    public bool EliminarPlato(Plato plato)
    {
    
        int posicion = BuscarPosicionPlato(plato);
        if (posicion != -1)
        {
            platos.EliminarPosicion(posicion);
            return true;
        }
        return false;
    }


    public bool EliminarPlatoPorCodigo(string codigoPlato)
    {
        int posicion = BuscarPosicionPorCodigo(codigoPlato);
        if (posicion != -1)
        {
            platos.EliminarPosicion(posicion);
            return true;
        }
        return false;
    }


    public Plato BuscarPlatoPorCodigo(string codigo)
    {
        if (platos.Cabeza == null) return null;

        Nodo<Plato> actual = platos.Cabeza;
        while (actual != null)
        {
            if (actual.Valor.Codigo == codigo)
                return actual.Valor;
            actual = actual.Siguiente;
        }
        return null;
    }


    private int BuscarPosicionPorCodigo(string codigo)
    {
        if (platos.Cabeza == null) return -1;

        Nodo<Plato> actual = platos.Cabeza;
        int posicion = 0;
        
        while (actual != null)
        {
            if (actual.Valor.Codigo == codigo)
                return posicion;
            actual = actual.Siguiente;
            posicion++;
        }
        return -1;
    }


    private int BuscarPosicionPlato(Plato plato)
    {
        if (platos.Cabeza == null) return -1;

        Nodo<Plato> actual = platos.Cabeza;
        int posicion = 0;
        
        while (actual != null)
        {
            if (actual.Valor.Equals(plato))
                return posicion;
            actual = actual.Siguiente;
            posicion++;
        }
        return -1;
    }


    public void ListarPlatos()
    {
        if (platos.Cabeza == null)
        {
            Console.WriteLine("El menú no tiene platos.");
            return;
        }

        Console.WriteLine($"=== PLATOS DEL MENÚ: {this.nombre} ===");
        Nodo<Plato> actual = platos.Cabeza;
        int contador = 1;
        
        while (actual != null)
        {
            Plato plato = actual.Valor;
            Console.WriteLine($"{contador}. {plato.Nombre} - ${plato.Precio} ({plato.Codigo})");
            Console.WriteLine($"   Descripción: {plato.Descripcion}");
            Console.WriteLine();
            actual = actual.Siguiente;
            contador++;
        }
    }


    public bool ContienePlato(string codigoPlato)
    {
        return BuscarPlatoPorCodigo(codigoPlato) != null;
    }


    public override string ToString()
    {
        return $"Menú: {nombre} (Código: {codigoMenu}) - Platos: {platos.Contar()}";
    }
}
