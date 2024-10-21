using System.ComponentModel.DataAnnotations;
public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string Password { get; set; }
    
    public ICollection<Reservation> Reservations { get; set; }
}

