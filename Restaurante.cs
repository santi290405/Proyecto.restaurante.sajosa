public class Restaurante
{
    public string Nit { get; set; }
    public string Nombre { get; set; }
    public string Dueno { get; set; }
    public string Celular { get; set; }
    public string Direccion { get; set; }
    
    public GestorClientes GestorClientes { get; set; }
    public Restaurante(string nit, string nombre, string dueno, string celular, string direccion)
    {
        if (string.IsNullOrWhiteSpace(nit))
            throw new ArgumentException("El NIT no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del restaurante no puede estar vacío.");
        if (celular.Length != 10)
            throw new ArgumentException("El número de celular debe tener 10 dígitos.");

        Nit = nit;
        Nombre = nombre;
        Dueno = dueno;
        Celular = celular;
        Direccion = direccion;
// Inicialización de los gestores igual para cuando se creen las clases respectivas (es para mas comodidad profe xdddd
        GestorClientes = new GestorClientes();
    }

    public override string ToString()
    {
        return $"Restaurante: {Nombre} | NIT: {Nit} | Dueño: {Dueno} | Celular: {Celular} | Dirección: {Direccion}";
    }
}
