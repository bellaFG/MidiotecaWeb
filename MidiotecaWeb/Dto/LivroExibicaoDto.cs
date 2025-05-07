using System.ComponentModel.DataAnnotations.Schema;

namespace MidiotecaWeb.Dto
{
    namespace MidiotecaWeb.Dto.Livro
    {
        public class LivroExibicaoDto
        {
            public Guid Id { get; set; }
            public string Titulo { get; set; }
            public string Autor { get; set; }
            public string? Editora { get; set; }
            public int? AnoPublicacao { get; set; }
            public string? Sinopse { get; set; }
            public IFormFile? CapaUrl { get; set; }

            public Guid GeneroId { get; set; }

            public int QuantidadeResenhas { get; set; }
            public int QuantidadeUsuariosRelacionados { get; set; }
        }
    }

}
