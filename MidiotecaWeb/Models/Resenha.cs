namespace MidiotecaWeb.Models
{
    public class Resenha : Entity
    {
        public string Titulo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public Guid LivroId { get; set; }
        public Livro Livro { get; set; }
    }
}
