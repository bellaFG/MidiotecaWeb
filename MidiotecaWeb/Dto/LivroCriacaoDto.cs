using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MidiotecaWeb.Dto
{
    public class LivroCriacaoDto
    {
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Autor { get; set; }

        public string? Editora { get; set; }
        public Guid GeneroId { get; set; }
        public int? AnoPublicacao { get; set; }
        public string? Sinopse { get; set; }
        public IFormFile? CapaUrl { get; set; }
    }
}
