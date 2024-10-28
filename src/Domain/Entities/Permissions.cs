using LigChat.Backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tests_.src.Domain.Entities
{
    [Table("permissoes")]
    public class Permission
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        [StringLength(255)]
        public string? Description { get; set; }

        // Relationship with UserPermission
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    }

    [Table("usuarios_permissoes")]
    public class UserPermission
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("permission_id")]
        public int PermissionId { get; set; }

        // Relationships with User and Permission
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }
    }
}
