public class PlatoPedido
{
    public string CodigoPlato { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public PlatoPedido(string codigoPlato, int cantidad, decimal precioUnitario)
    {
        if (string.IsNullOrWhiteSpace(codigoPlato))
            throw new ArgumentException("El código del plato no puede estar vacío.");
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");
        if (precioUnitario <= 0)
            throw new ArgumentException("El precio unitario debe ser mayor que cero.");

        CodigoPlato = codigoPlato;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
    }

    public decimal CalcularSubtotal()
    {
        return Cantidad * PrecioUnitario;
    }

    public override string ToString()
    {
        return $"Plato: {CodigoPlato} - Cantidad: {Cantidad} - Precio Unitario: ${PrecioUnitario:F2} - Subtotal: ${CalcularSubtotal():F2}";
    }
}
