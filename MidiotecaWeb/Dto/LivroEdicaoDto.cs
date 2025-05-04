using System.ComponentModel.DataAnnotations;

namespace MidiotecaWeb.Dto
{
    public class LivroEdicaoDto
    {
        [Required]
        public Guid Id { get; set; } 
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Editora { get; set; }
        public Guid? GeneroId { get; set; }
        public int? AnoPublicacao { get; set; }
        public string? Sinopse { get; set; }
        public IFormFile? CapaUrl { get; set; }
    }
}
