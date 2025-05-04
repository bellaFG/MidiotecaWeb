public interface IUsuarioService
{
    Task<string> CriarUsuarioAsync(UsuarioCriacaoDto dto);
    Task<string> AutenticarUsuarioAsync(UsuarioLoginDto dto);
    Task EditarUsuarioAsync(Guid id, UsuarioEdicaoDto dto);
    Task AlterarSenhaAsync(Guid id, UsuarioAlterarSenhaDto dto);
    Task<UsuarioDetalhadoDto> ObterUsuarioPorIdAsync(Guid id);
}

