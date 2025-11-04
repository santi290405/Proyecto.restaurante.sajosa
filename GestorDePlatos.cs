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
            throw new InvalidOperationException($"Ya existe un plato con el código {codigo}.");

        Plato nuevo = new Plato(codigo, nombre, descripcion, precio);
        platos.Agregar(nuevo);
        Console.WriteLine($"✅ Plato '{nombre}' agregado correctamente.");
    }

    public Plato BuscarPlato(string codigo)
    {
        return platos.Buscar(p => p.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
    }

    public bool EliminarPlato(string codigo)
    {
        bool eliminado = platos.Eliminar(p => p.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
        if (eliminado)
            Console.WriteLine($"🗑️ Plato con código {codigo} eliminado correctamente.");
        else
            Console.WriteLine($"⚠️ No se encontró ningún plato con código {codigo}.");
        return eliminado;
    }

    public void ListarPlatos()
    {
        Console.WriteLine("🍽️ Lista de platos del menú:\n");
        platos.Mostrar();
    }

    public bool EditarPlato(string codigo, string nuevoNombre, string nuevaDescripcion, decimal nuevoPrecio)
    {
        Plato plato = BuscarPlato(codigo);
        if (plato == null)
        {
            Console.WriteLine($"⚠️ No se encontró el plato con código {codigo}.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(nuevoNombre))
            plato.Nombre = nuevoNombre;

        if (!string.IsNullOrWhiteSpace(nuevaDescripcion))
            plato.Descripcion = nuevaDescripcion;

        if (nuevoPrecio > 0)
            plato.Precio = nuevoPrecio;

        Console.WriteLine($"✏️ Plato con código {codigo} actualizado correctamente.");
        return true;
    }

    public Plato ObtenerPlatoPorCodigo(string codigo)
    {
        return BuscarPlato(codigo);
    }
}
