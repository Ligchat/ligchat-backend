namespace tests_.src.Domain.DTOs.Permission
{
    public class UpdatePermissionDTO
    {

        /// <summary>
        /// The updated name of the permission.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The updated description of the permission.
        /// </summary>
        public string? Description { get; set; }
    }
}
