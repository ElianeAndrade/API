namespace WebCredentialsApi.Service;

public class PermissionService
{
    private readonly string _senha;

    public PermissionService(string senha)
    {
        _senha = senha;
    }

    public bool ValidarSenha(string senhaUsuario)
    {
        return senhaUsuario == _senha;
    }
}


