using LigChat.Backend.Application.Interface.UserInterface;
using LigChat.Backend.Web.Extensions.Cors;
using LigChat.Backend.Web.Extensions.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LigChat.Backend.Web.Services;
using LigChat.Data.Interfaces.IRepositories;
using LigChat.Data.Repositories;
using LigChat.Backend.Data.Interfaces.IRepositories;
using LigChat.Data.Interfaces.IServices;
using LigChat.Api.Services.TagService;
using LigChat.Api.Services.FolderService;
using LigChat.Api.Services.SectorService;
using LigChat.Api.Services.TeamService;
using LigChat.Api.Services.FlowService;
using LigChat.Backend.Application.Interface.ContactInterface;
using LigChat.Backend.Application.Interface.WebhookInterface;
using LigChat.Backend.Application.Interface.WebhookEventInterface;
using LigChat.Com.Api.Mvc.WebhookMvc.Service;
using LigChat.Com.Api.Mvc.WebhookEventMvc.Service;
using tests_.src.Application.Services;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LigChat.Com.Api.Mvc.UserMvc.Service;
using tests_.src.Web.Controller;
using tests_.src.Application.Repositories;
using tests_.src.Application.Interface.CardInterface;
using tests_.src.Application.Interface.ColunaInterface;
using tests_.src.Application.Interface.ContatoInterface;
using tests_.src.Application.Common.Utilities;
using tests_.src.Application.Interface.Message.Ligchat.Application.Interfaces;
using Ligchat.Application.Services;
using tests_.src.Domain.Services;
using tests_.src.Application.Services.tests_.src.Application.Services;
using LigChat.Data.Interfaces.IRepositories;
using LigChat.Data.Repositories;
using LigChat.Api.Services;
using LigChat.Backend.Application.Services;
using LigChat.Backend.Application.Services.Storage;
using LigChat.Backend.Application.Repositories;
using LigChat.Backend.Application.Interface.MessageSchedulingInterface;
using LigChat.Backend.Application.Interface.S3StorageInterface;
using LigChat.Backend.Application.Common.Mappings;
using Amazon.Runtime;
using Amazon.Extensions.NETCore.Setup;
using Amazon;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar suporte para controllers
builder.Services.AddControllers();

// Configuração de autenticação
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true, // Habilite a validação da chave de assinatura
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

// Adicionar suporte para autorização
builder.Services.AddAuthorization();

// Adicione o registro do serviço JwtTokenService
builder.Services.AddScoped<JwtTokenService>();

// Configurar o DbContext para usar PostgreSQL
builder.Services.AddDbContext<DatabaseConfiguration>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 23))));

// Configuração do AWS S3
builder.Services.AddScoped<IS3StorageService, S3StorageService>();

var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDbConnection");
var mongoDatabaseName = builder.Configuration["MongoDbSettings:DatabaseName"];

// Registrar o MongoDbContext na injeção de dependência
builder.Services.AddSingleton(new MongoDbContext(mongoConnectionString, mongoDatabaseName));

// Injeção de dependências de repositórios
builder.Services.AddTransient<IContactRepositoryInterface, ContactRepository>();
builder.Services.AddTransient<IUserRepositoryInterface, UserRepository>();
builder.Services.AddTransient<IAgentRepository, AgentRepository>();
builder.Services.AddTransient<IFolderRepositoryInterface, FolderRepository>();
builder.Services.AddTransient<IFlowRepositoryInterface, FlowRepository>();
builder.Services.AddTransient<ITagRepositoryInterface, TagRepository>();
builder.Services.AddTransient<IMessageSchedulingRepositoryInterface, MessageSchedulingRepository>();
builder.Services.AddTransient<ISectorRepositoryInterface, SectorRepository>();
builder.Services.AddTransient<ITeamRepositoryInterface, TeamRepository>();
builder.Services.AddTransient<IWebhookRepositoryInterface, WebhookRepository>();
builder.Services.AddTransient<IWebhookEventRepositoryInterface, WebhookEventRepository>();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddTransient<IUserSectorRepositoryInterface, UserSectorRepository>();


// Adicione o registro dos serviços
builder.Services.AddScoped<IContactServiceInterface>(provider => 
    new ContactService(
        provider.GetRequiredService<IContactRepositoryInterface>(),
        provider.GetRequiredService<DatabaseConfiguration>()
    )
);
builder.Services.AddTransient<IUserServiceInterface, UserService>();
builder.Services.AddScoped<AgentService>();
builder.Services.AddTransient<ITagServiceInterface, TagService>();
builder.Services.AddTransient<IFolderServiceInterface, FolderService>();
builder.Services.AddTransient<IMessageSchedulingServiceInterface, MessageSchedulingService>();
builder.Services.AddTransient<ISectorServiceInterface, SectorService>();
builder.Services.AddTransient<ITeamServiceInterface, TeamService>();
builder.Services.AddTransient<IFlowServiceInterface, FlowService>();
builder.Services.AddTransient<IWebhookServiceInterface, WebhookService>();
builder.Services.AddTransient<IWebhookEventServiceInterface, WebhookEventService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IUserPermissionService, UserPermissionService>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
builder.Services.AddScoped<IColunaService, ColunaService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<BusinessDayService>();
builder.Services.AddScoped<FlowWhatsappService>();
builder.Services.AddScoped<VariablesService>();
builder.Services.AddScoped<IS3StorageService, S3StorageService>();

builder.Services.AddHttpClient();

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(MessageSchedulingProfile));

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
