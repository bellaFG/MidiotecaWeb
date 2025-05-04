using MidiotecaWeb.Models;

namespace MidiotecaWeb.Dto
{
    public class LivroUsuarioEdicaoDto
    {
        public StatusLeitura Status { get; set; }
        public DateTime? DataLeitura { get; set; }
        public int? Nota { get; set; }
    }
}
