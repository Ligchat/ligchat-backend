using LigChat.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using tests_.src.Domain.Entities;
using tests_.src.Domain.Entities.LigChat.Backend.Domain.Entities;

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

        public DbSet<Agent> Agents { get; set; }

        public DbSet<Messeageging> Messeageging { get; set; }

        public DbSet<BusinessHours> BusinessHours { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Coluna> Colunas { get; set; }
        public DbSet<Card> Cards { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }

        public DbSet<Flow> Flows { get; set; }

        public DbSet<WhatsAppAccount> WhatsAppAccount { get; set; }

        public DbSet<WhatsAppIntegrationSettings> WhatsAppIntegrationSettings { get; set; }

        public DbSet<WhatsAppMessage> WhatsAppMessages { get; set; }

        public DbSet<Folder> Folders { get; set; }

        public DbSet<UserSector> UserSectors { get; set; }

        public DbSet<MessageScheduling> MessageSchedulings { get; set; }

        public DbSet<MessageAttachment> MessageAttachments { get; set; }

        public DbSet<Sector> Sectors { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Webhook> Webhooks { get; set; }

        public DbSet<WebhookEvent> WebhookEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Agent>()
                .HasIndex(a => new { a.SectorId, a.Status })
                .IsUnique()
                .HasFilter("status = true");

            // Configurando relacionamento Contact-Card
            modelBuilder.Entity<Contact>()
                .HasMany<Card>()
                .WithOne(c => c.Contact)
                .HasForeignKey(c => c.ContactId)
                .IsRequired(false);  // Tornando a relação opcional

            // Configurando relacionamento Card-Coluna
            modelBuilder.Entity<Card>()
                .HasOne(c => c.Column)
                .WithMany(col => col.Cards)
                .HasForeignKey(c => c.ColumnId)
                .IsRequired(false);  // Tornando a relação opcional

            // Configurando relacionamento MessageScheduling-MessageAttachment
            modelBuilder.Entity<MessageScheduling>()
                .HasMany(m => m.Attachments)
                .WithOne(a => a.Message)
                .HasForeignKey(a => a.MessageId)
                .OnDelete(DeleteBehavior.Cascade);

            // Desabilitando carregamento automático de relacionamentos
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }
    }
}

