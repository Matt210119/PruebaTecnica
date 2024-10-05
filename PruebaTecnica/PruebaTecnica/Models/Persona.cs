namespace PruebaTecnica.Models;

public class Persona
{
    public int PersonaId { get; set; } // Primary Key
    public string Nombre { get; set; }
    public string Genero { get; set; }
    public int Edad { get; set; }
    public string Identificacion { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
}
