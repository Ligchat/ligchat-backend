namespace tests_.src.Domain.DTOs.UserPermissionDTO
{
    public class CreateUserPermissionDTO
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The ID of the permission to assign.
        /// </summary>
        public int PermissionId { get; set; }
    }
}
