using MidiotecaWeb.Models;

namespace MidiotecaWeb.Dto
{
    public class LivroUsuarioCriacaoDto
    {
        public Guid LivroId { get; set; }
        public StatusLeitura Status { get; set; }
        public DateTime? DataLeitura { get; set; }
        public int? Nota { get; set; }
    }
}
