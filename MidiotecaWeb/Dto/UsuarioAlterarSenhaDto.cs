using System.ComponentModel.DataAnnotations;

public class UsuarioAlterarSenhaDto
{
    [Required]
    public string SenhaAtual { get; set; }

    [Required, MinLength(6)]
    public string NovaSenha { get; set; }

    [Required, MinLength(6)]
    public string ConfirmarNovaSenha { get; set; }
}