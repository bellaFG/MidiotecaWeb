﻿namespace MidiotecaWeb.Dto
{
    public class LivroListagemDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public IFormFile? CapaUrl { get; set; }
    }
}
