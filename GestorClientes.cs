public class GestorClientes
{
    private ListaEnlazada<Cliente> clientes;

    public GestorClientes()
    {
        clientes = new ListaEnlazada<Cliente>();
    }

    public void AgregarCliente(Cliente c)
    {
        if (clientes.Buscar(x => x.Cedula == c.Cedula) != null)
            throw new Exception("Ya existe un cliente con esa cédula.");
        clientes.Insertar(c);
    }

    public Cliente BuscarCliente(string cedula)
    {
        return clientes.Buscar(c => c.Cedula == cedula);
    }

    public void ListarClientes()
    {
        Console.WriteLine("\n--- LISTA DE CLIENTES ---");
        clientes.Recorrer(c => Console.WriteLine(c));
    }

    public void EliminarCliente(string cedula)
    {
        clientes.Eliminar(c => c.Cedula == cedula);
    }
}
