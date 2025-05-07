using System.ComponentModel.DataAnnotations.Schema;

namespace MidiotecaWeb.Models
{
    public class Livro : Entity
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int AnoPublicacao { get; set; }
        public string Sinopse { get; set; }

        [NotMapped]
        public IFormFile? CapaUrl { get; set; }

        public Guid GeneroId { get; set; }
        public Genero Genero { get; set; }

        public virtual ICollection<LivroUsuario> UsuariosRelacionados { get; set; } = new List<LivroUsuario>();
        public virtual ICollection<Resenha> Resenhas { get; set; } = new List<Resenha>();
    }
}
