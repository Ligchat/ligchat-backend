namespace tests_.src.Domain.DTOs.Permission
{
    public class CreatePermissionDTO
    {
        /// <summary>
        /// The name of the permission.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// A description of the permission.
        /// </summary>
        public string? Description { get; set; }
    }
}
