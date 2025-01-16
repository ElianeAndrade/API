using Microsoft.AspNetCore.Mvc;
using WebCredentialsApi.Service;


[ApiController]
[Route("api/[controller]")]
public class CredentialsController : ControllerBase
{
    private readonly MenusService _menusService;
    private readonly PermissionService _permissoes;

    // Passando a senha como parâmetro para a classe de permissões
    public CredentialsController(MenusService menusService, PermissionService permissoes)
    {
        _menusService = menusService;
        _permissoes = permissoes;
    }

    // Endpoint para listar as APIs e ambientes disponíveis
    [HttpGet("listar-apis-e-ambientes")]
    public IActionResult ListarApisEAmbientes()
    {

        // Obtém a senha do cabeçalho da requisição
        var senha = Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Trim();

        // Verificando a senha
        if (!ValidarRequisicao(out var mensagemErro))
        {
            return Unauthorized(mensagemErro);
        }

        // Obtem as listas de APIs e ambientes
        var apis = _menusService.ListarApis();
        var ambientes = _menusService.ListarAmbientes();

        // Cria um objeto para retornar ambas as informações
        var result = new
        {
            Apis = apis,
            Ambientes = ambientes
        };

        return Ok(result);
    }

    // Endpoint para verificar uma API e ambiente
    [HttpGet("verificar/{api}/{ambiente}")]
    public IActionResult VerificarAPI(int api, int ambiente)
    {
        // Obtém a senha do cabeçalho da requisição
        var senha = Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Trim();

        // Verificando a senha
        if (!ValidarRequisicao(out var mensagemErro))
        {
            return Unauthorized(mensagemErro);
        }

        var info = _menusService.VerificarAPI(api, ambiente);

        if (info is not null)
        {
            var (login, senhaApi) = info.Value;
            return Ok(new
            {
                Api = _menusService.GetApiName(api),
                Ambiente = _menusService.GetAmbiente(ambiente),
                Login = login,
                Senha = senhaApi 
            });
        }

        return BadRequest("Código inválido para API ou ambiente.");
    }


    private bool ValidarRequisicao(out string mensagemErro)
    {
        var senha = Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Trim();
        if (string.IsNullOrEmpty(senha) || !_permissoes.ValidarSenha(senha))
        {
            mensagemErro = "Senha inválida >>> Unauthorized.";
            return false;
        }
        mensagemErro = null;
        return true;
    }
}
