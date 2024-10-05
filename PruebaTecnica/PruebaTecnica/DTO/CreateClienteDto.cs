namespace PruebaTecnica.DTO;

using System.ComponentModel.DataAnnotations;

public class CreateClienteDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El género es obligatorio")]
    public string Genero { get; set; }

    [Range(18, 100, ErrorMessage = "La edad debe estar entre 18 y 100 años")]
    public int Edad { get; set; }

    [Required(ErrorMessage = "La identificación es obligatoria")]
    public string Identificacion { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria")]
    public string Direccion { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    public string Telefono { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string Contraseña { get; set; }

    public bool Estado { get; set; }
}

