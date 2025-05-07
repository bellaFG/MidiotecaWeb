using MidiotecaWeb.Dto;
using MidiotecaWeb.Dto.MidiotecaWeb.Dto.Livro;

namespace MidiotecaWeb.Services
{
    public interface ILivroService
    {
       
        Task<Guid> CriarLivroAsync(LivroCriacaoDto dto);
        Task<LivroExibicaoDto> ObterLivroPorIdAsync(Guid id);
        Task EditarLivroAsync(Guid id, LivroEdicaoDto dto);
        Task DeletarLivroAsync(Guid id);
    }
}
