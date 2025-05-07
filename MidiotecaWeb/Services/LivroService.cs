using Microsoft.EntityFrameworkCore;
using MidiotecaWeb.Data;
using MidiotecaWeb.Dto;
using MidiotecaWeb.Dto.MidiotecaWeb.Dto.Livro;
using MidiotecaWeb.Models;

namespace MidiotecaWeb.Services
{
    public class LivroService : ILivroService
    {
        private readonly MidiotecaDbContext _context;

        public LivroService(MidiotecaDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CriarLivroAsync(LivroCriacaoDto dto)
        {
            var livro = new Livro
            {
                Id = Guid.NewGuid(),
                Titulo = dto.Titulo,
                Autor = dto.Autor,
                Editora = dto.Editora ?? string.Empty,
                GeneroId = dto.GeneroId,
                AnoPublicacao = dto.AnoPublicacao ?? 0,
                Sinopse = dto.Sinopse ?? string.Empty
           
            };

            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            return livro.Id;
        }

        public async Task<LivroExibicaoDto> ObterLivroPorIdAsync(Guid id)
        {
            var livro = await _context.Livros
                .Include(l => l.Genero)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (livro == null) throw new Exception("Livro não encontrado.");

            return new LivroExibicaoDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                Editora = livro.Editora,
                AnoPublicacao = livro.AnoPublicacao,
                Sinopse = livro.Sinopse,
                GeneroId = livro.GeneroId,
                CapaUrl = livro.CapaUrl
            };
        }

        public async Task EditarLivroAsync(Guid id, LivroEdicaoDto dto)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null) throw new Exception("Livro não encontrado.");

            if (!string.IsNullOrWhiteSpace(dto.Titulo)) livro.Titulo = dto.Titulo;
            if (!string.IsNullOrWhiteSpace(dto.Autor)) livro.Autor = dto.Autor;
            if (!string.IsNullOrWhiteSpace(dto.Editora)) livro.Editora = dto.Editora;
            if (dto.AnoPublicacao.HasValue) livro.AnoPublicacao = dto.AnoPublicacao.Value;
            if (!string.IsNullOrWhiteSpace(dto.Sinopse)) livro.Sinopse = dto.Sinopse;
            if (dto.GeneroId.HasValue) livro.GeneroId = dto.GeneroId.Value;

            await _context.SaveChangesAsync();
        }

        public async Task DeletarLivroAsync(Guid id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null) throw new Exception("Livro não encontrado.");

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
        }

    }
}

