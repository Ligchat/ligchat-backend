namespace tests_.src.Domain.DTOs.UserPermissionDTO
{
    public class UpdateUserPermissionDTO
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The ID of the permission.
        /// </summary>
        public int PermissionId { get; set; }

        /// <summary>
        /// Indicates if the permission should be active or not.
        /// This field can be used to enable or disable the permission for the user.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
