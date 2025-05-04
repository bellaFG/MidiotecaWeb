namespace MidiotecaWeb.Models
{
    public class Genero : Entity
    {
        public string Nome { get; set; }
        public virtual ICollection<Livro> Livros { get; set; } = new List<Livro>();
    }   
}
