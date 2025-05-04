using System.ComponentModel.DataAnnotations;

public class UsuarioEdicaoDto
{
    [Required]
    public string Nome { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

  
}