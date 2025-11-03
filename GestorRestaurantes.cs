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

    
        var existente = restaurantes.Buscar(x => x.Nit == r.Nit);
        if (existente != null)
            throw new InvalidOperationException("Ya existe un restaurante con ese NIT.");

        restaurantes.Insertar(r);
    }


    public Restaurante BuscarRestaurante(string nit)
    {
        if (string.IsNullOrWhiteSpace(nit))
            return null;
        return restaurantes.Buscar(r => r.Nit == nit);
    }

 
    public void ListarRestaurantes(Action<Restaurante> accion)
    {
        if (accion == null)
            accion = r => Console.WriteLine(r);
        restaurantes.Recorrer(accion);
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

        public void EliminarRestaurante(string nit, Func<Restaurante, bool> puedeEliminar = null)
    {
        var restaurante = BuscarRestaurante(nit);
        if (restaurante == null)
            throw new InvalidOperationException("Restaurante no encontrado.");

        if (puedeEliminar != null)
        {
            bool permitido = puedeEliminar(restaurante);
            if (!permitido)
                throw new InvalidOperationException("No se puede eliminar el restaurante: la verificación externa lo impide.");
        }

        restaurantes.Eliminar(r => r.Nit == nit);
    }
}
