using MidiotecaWeb.Dto;

public class UsuarioDetalhadoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public bool Ativo { get; set; }

    public List<LivroUsuarioDto>? LivrosRelacionados { get; set; }
}