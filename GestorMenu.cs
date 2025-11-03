public class GestorMenu
{
    private ListaEnlazada<Plato> platos;

    public GestorMenu()
    {
        platos = new ListaEnlazada<Plato>();
    }

    public void AgregarPlato(Plato p)
    {
        if (platos.Buscar(x => x.Codigo == p.Codigo) != null)
            throw new Exception("Ya existe un plato con ese código.");
        platos.Insertar(p);
    }

    public void EliminarPlato(string codigo)
    {
        platos.Eliminar(p => p.Codigo == codigo);
    }

    public Plato BuscarPlato(string codigo)
    {
        return platos.Buscar(p => p.Codigo == codigo);
    }

    public void ListarPlatos()
    {
        Console.WriteLine("\n--- MENÚ DEL RESTAURANTE ---");
        platos.Recorrer(p => Console.WriteLine($"{p.Codigo} - {p.Nombre} - ${p.Precio}"));
    }
}
