namespace WebCredentialsApi.Service;
public class MenusService
{
    // Dicionários de informações
    private readonly Dictionary<int, string> _apiAmbientes = new Dictionary<int, string>
        {
            { 1, "UAT" },
            { 2, "RC" },
            { 3, "PRODUÇÃO" }
        };

    private readonly Dictionary<int, string> _apiNames = new Dictionary<int, string>
        {
            { 1, "API de Busca HUB09 | PRW Order Status HUB09 - digitar id Remessa" },
            { 2, "API Communication | PRW COMMUNICATION SERVICE" },
            { 3, "API PRW ComunnicationOrderStatus API SERVERLESS | OrderStatus | BOTS COMMUNICATION DATA API" },
            { 4, "API Tracking de Pedido | PRW BotsTracking API SERVERLESS" },
            { 5, "API PRW BotmakerSurvey API SERVERLESS | CSAT" }
        };

    private readonly Dictionary<(int, int), (string login, string senha)> _apiInfo = new Dictionary<(int, int), (string login, string senha)>
        {
            {(1, 1), ("clientWong", "clientWong") },
            {(2, 1), ("clientWong", "clientWong")},
            {(3, 1), ("clientWong", "clientWong")},
            {(4, 1), ("clientWong", "clientWong")},
            {(5, 1), ("clientWong", "clientWong")},
            {(1, 2), ("prwOrderStatusCommunicationRC", "no89nkgum0uhqketj0v1i9mi0dj1imnuomsf28pckit38b33npm")},
            {(2, 2), ("clientWong", "clientWong")},
            {(3, 2), ("prwOrderStatusRC", "kyGfE4gum0uhqketj0v1i9mi0dj1imnuomsf28pckit38b53pjT")},
            {(4, 2), ("prwBotsTrackingRC", "PUAxFExFhRKIxPC3FBuvrcporMQK7w7iD6ruTGac46x1QYGLY6n")},
            {(5, 2), ("botsSurveyRC", "lubEE4gum0uhqketj0v1i9mi0dj1imnuomsf28pckit38b53pjT")},
            {(1, 3), ("prwOrderStatusCommunicationProd", "no89nkgum0uhqketj0v1i9mi0dj1imnuomsf28pckit38b53kjl")},
            {(2, 3), ("clientWong", "clientWong")},
            {(3, 3), ("prwOrderStatusPROD", "LnGfE4gum0uhqketj0v1i9mi0dj1imnuomsf28pckit38b53grW")},
            {(4, 3), ("prwBotsTrackingPROD", "KYTqFExFhRKIxPC3FBuvrcporMQK7w7iD6ruTGac46x1QYYWN7H")},
            {(5, 3), ("botsSurveyPROD", "thrEE4gum0uhqketj0v1i9mi0dj1imnuomsf28pckit38b53pjT")}
        };

    // Método que retorna as APIs disponíveis
    public Dictionary<int, string> ListarApis()
    {
        return _apiNames;
    }

    public Dictionary<int, string> ListarAmbientes()
    {
        return _apiAmbientes;
    }

    // Método para verificar uma API e ambiente
    public (string login, string senha)? VerificarAPI(int api, int ambiente)
    {
        if (_apiInfo.ContainsKey((api, ambiente)))
        {
            return _apiInfo[(api, ambiente)];
        }

        return null; 
    }

    // Métodos adicionais para retornar os ambientes ou dados mais específicos
    public string GetAmbiente(int ambienteId)
    {
        return _apiAmbientes.ContainsKey(ambienteId)? _apiAmbientes[ambienteId] : "Ambiente desconhecido";
    }

    public string GetApiName(int apiId)
    {
        return _apiNames.ContainsKey(apiId)? _apiNames[apiId] : "API desconhecida";
    }
}
