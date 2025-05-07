using MidiotecaWeb.Dto;

namespace MidiotecaWeb.Services
{
    public interface ILivroUsuarioService
    {
        Task<IEnumerable<LivroUsuarioDto>> ListarPorUsuarioAsync(Guid usuarioId);
        Task<LivroUsuarioDto> ObterPorIdAsync(Guid usuarioId, Guid livroId);
        Task<bool> AdicionarAsync(Guid usuarioId, LivroUsuarioCriacaoDto dto);
        Task<bool> AtualizarAsync(Guid usuarioId, Guid livroId, LivroUsuarioEdicaoDto dto);
        Task<bool> RemoverAsync(Guid usuarioId, Guid livroId);
    }
}

