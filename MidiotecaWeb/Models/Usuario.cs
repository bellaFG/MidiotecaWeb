namespace MidiotecaWeb.Models
{
    public class Usuario : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;

        public virtual ICollection<LivroUsuario> LivrosRelacionados { get; set; } = new List<LivroUsuario>();
        public virtual ICollection<Resenha> Resenhas { get; set; } = new List<Resenha>();

    }
}
