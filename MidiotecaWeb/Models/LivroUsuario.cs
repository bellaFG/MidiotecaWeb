using System.Data;

namespace MidiotecaWeb.Models
{
    public class LivroUsuario : Entity
    {
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public Guid LivroId { get; set; }
        public Livro Livro { get; set; }

        public StatusLeitura Status { get; set; } = StatusLeitura.Nenhum;

        public DateTime? DataLeitura { get; set; }
        public int? Nota { get; set; } 
  
    }
        public enum StatusLeitura
        {
            Nenhum = 0,
            Desejado = 1,
            Lendo = 2,
            Lido = 3,
            Abandonado = 4
        }
    }


