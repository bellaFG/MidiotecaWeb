using MidiotecaWeb.Dto;
using MidiotecaWeb.Models;
using MidiotecaWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace MidiotecaWeb.Services
{
    public class LivroUsuarioService : ILivroUsuarioService
    {
        private readonly MidiotecaDbContext _context;

        public LivroUsuarioService(MidiotecaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LivroUsuarioDto>> ListarPorUsuarioAsync(Guid usuarioId)
        {
            return await _context.LivroUsuario
                .Include(lu => lu.Livro)
                .Where(lu => lu.UsuarioId == usuarioId)
                .Select(lu => new LivroUsuarioDto
                {
                    LivroId = lu.LivroId,
                    TituloLivro = lu.Livro.Titulo,
                    Autor = lu.Livro.Autor,
                    Status = lu.Status,
                    StatusLeitura = lu.Status.ToString(),
                    DataLeitura = lu.DataLeitura,
                    Nota = lu.Nota
                })
                .ToListAsync();
        }

        public async Task<LivroUsuarioDto> ObterPorIdAsync(Guid usuarioId, Guid livroId)
        {
            var lu = await _context.LivroUsuario
                .Include(lu => lu.Livro)
                .FirstOrDefaultAsync(l => l.UsuarioId == usuarioId && l.LivroId == livroId);

            if (lu == null) return null;

            return new LivroUsuarioDto
            {
                LivroId = lu.LivroId,
                TituloLivro = lu.Livro.Titulo,
                Autor = lu.Livro.Autor,
                Status = lu.Status,
                StatusLeitura = lu.Status.ToString(),
                DataLeitura = lu.DataLeitura,
                Nota = lu.Nota
            };
        }

        public async Task<bool> AdicionarAsync(Guid usuarioId, LivroUsuarioCriacaoDto dto)
        {
            var existe = await _context.LivroUsuario
                .AnyAsync(lu => lu.UsuarioId == usuarioId && lu.LivroId == dto.LivroId);

            if (existe) return false;

            var livroUsuario = new LivroUsuario
            {
                UsuarioId = usuarioId,
                LivroId = dto.LivroId,
                Status = dto.Status,
                DataLeitura = dto.DataLeitura,
                Nota = dto.Nota
            };

            _context.LivroUsuario.Add(livroUsuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AtualizarAsync(Guid usuarioId, Guid livroId, LivroUsuarioEdicaoDto dto)
        {
            var lu = await _context.LivroUsuario
                .FirstOrDefaultAsync(l => l.UsuarioId == usuarioId && l.LivroId == livroId);

            if (lu == null) return false;

            lu.Status = dto.Status;
            lu.DataLeitura = dto.DataLeitura;
            lu.Nota = dto.Nota;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(Guid usuarioId, Guid livroId)
        {
            var lu = await _context.LivroUsuario
                .FirstOrDefaultAsync(l => l.UsuarioId == usuarioId && l.LivroId == livroId);

            if (lu == null) return false;

            _context.LivroUsuario.Remove(lu);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
