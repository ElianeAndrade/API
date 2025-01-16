using Microsoft.OpenApi.Models;
using WebCredentialsApi.Service;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// Obter a senha da configuração
var password = builder.Configuration["AuthSettings:Password"];

// Registre o Permissoes no container de dependências
void ConfigureServices(IServiceCollection services)
{
    builder.Services.AddSingleton(new PermissionService(password));

    // Registre o MenusService
    builder.Services.AddSingleton<MenusService>();

    // Adicionar suporte ao Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { 
            Title = "BOTS - Credentials API", 
            Version = "v1",
            Description = "API para gerenciar credenciais de bots e ambientes.",
            Contact = new OpenApiContact
            {
                Name = "QA - Eliane Andrade",
                Email = "eliane.andrade.unica@bravium.com.br"
            }
        });


        // Adiciona uma definição de segurança (cabeçalho para senha)
        c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Description = "INFORME A SENHA: ",
        });

        // Aplica a segurança em todos os endpoints
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
        });
    });

}

// Chamando o método ConfigureServices
ConfigureServices(builder.Services);

// Adicionar outros serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure a pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
