public class Cliente
{
    public string Cedula { get; set; }
    public string NombreCompleto { get; set; }
    public string Celular { get; set; }
    public string Email { get; set; }

    public Cliente(string cedula, string nombreCompleto, string celular, string email)
    {
        if (string.IsNullOrWhiteSpace(cedula))
            throw new ArgumentException("La cédula no puede estar vacía.");
        if (string.IsNullOrWhiteSpace(nombreCompleto))
            throw new ArgumentException("El nombre no puede estar vacío.");
        if (celular.Length != 10)
            throw new ArgumentException("El celular debe tener 10 dígitos.");
        if (!email.Contains("@"))
            throw new ArgumentException("El email no tiene un formato válido.");

        Cedula = cedula;
        NombreCompleto = nombreCompleto;
        Celular = celular;
        Email = email;
    }

    public override string ToString()
    {
        return $"{NombreCompleto} - Cédula: {Cedula} - Cel: {Celular} - Email: {Email}";
    }
}
                                                  