public class Plato
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }

    public Plato(string codigo, string nombre, string descripcion, decimal precio)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("El código no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new ArgumentException("La descripción no puede estar vacía.");
        if (precio <= 0)
            throw new ArgumentException("El precio debe ser mayor que cero.");

        Codigo = codigo;
        Nombre = nombre;
        Descripcion = descripcion;
        Precio = precio;
    }

    public override string ToString()
    {
        return $"{Nombre} (Código: {Codigo}) - {Descripcion} - Precio: ${Precio:F2}";
    }
}
