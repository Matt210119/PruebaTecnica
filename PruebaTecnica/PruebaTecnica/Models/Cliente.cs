namespace PruebaTecnica.Models;

public class Cliente : Persona
{
    public int ClienteId { get; set; } // Primary Key (Unique)
    public string Contrasena { get; set; }
    public bool Estado { get; set; }
}
