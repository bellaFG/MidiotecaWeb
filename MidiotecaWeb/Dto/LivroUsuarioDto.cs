using MidiotecaWeb.Models;

namespace MidiotecaWeb.Dto
{
    public class LivroUsuarioDto
    {
        public Guid LivroId { get; set; }
        public string TituloLivro { get; set; }
        public StatusLeitura Status { get; set; }
        public DateTime? DataLeitura { get; set; }
        public int? Nota { get; set; }
        public string Autor { get; internal set; }
        public string StatusLeitura { get; internal set; }
    }
}
