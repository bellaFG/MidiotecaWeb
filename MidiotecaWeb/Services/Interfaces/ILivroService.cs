using MidiotecaWeb.Dto;
using MidiotecaWeb.Dto.MidiotecaWeb.Dto.Livro;

namespace MidiotecaWeb.Services
{
    public interface ILivroService
    {
       
        Task<Guid> CriarLivroAsync(LivroCriacaoDto dto);
        Task EditarLivroAsync(Guid id, LivroEdicaoDto dto);
        Task DeletarLivroAsync(Guid id);
        Task<IEnumerable<LivroListagemDto>> ListarLivrosAsync();
        Task<LivroExibicaoDto> ObterLivroPorIdAsync(Guid id);
    }
}
