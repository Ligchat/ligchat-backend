using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Domain.Entities;
using LigChat.Backend.Domain.Entities;


using Microsoft.EntityFrameworkCore;
using LigChat.Backend.Domain.Entities;
using tests_.src.Domain.Entities;

namespace LigChat.Backend.Web.Extensions.Database
{
    // Contexto principal para acesso ao banco de dados.
    public class DatabaseConfiguration : DbContext
    {
        // Construtor que recebe as opções de configuração do DbContext e as passa para a classe base.
        public DatabaseConfiguration(DbContextOptions<DatabaseConfiguration> options)
            : base(options)
        {
        }

        // DbSet para a entidade User. Permite operações CRUD na tabela "usuarios".
        public DbSet<User> Users { get; set; }

        // DbSet para entidade Times (Teams). Permite operações CRUD na tabela "times".
        public DbSet<Team> Teams { get; set; }

        public DbSet<Variables> Variables { get; set; }

        public DbSet<Messeageging> Messeageging { get; set; }

        public DbSet<BusinessHours> BusinessHours { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Coluna> Colunas { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Card> Cards { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }

        public DbSet<Flow> Flows { get; set; }

        public DbSet<WhatsAppAccount> WhatsAppAccount { get; set; }

        public DbSet<WhatsAppIntegrationSettings> WhatsAppIntegrationSettings { get; set; }

        public DbSet<WhatsAppMessage> WhatsAppMessages { get; set; }

        public DbSet<Folder> Folders { get; set; }

        public DbSet<MessageScheduling> MessageSchedulings { get; set; }

        public DbSet<Sector> Sectors { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Webhook> Webhooks { get; set; }

        public DbSet<WebhookEvent> WebhookEvents { get; set; }

    }
}

