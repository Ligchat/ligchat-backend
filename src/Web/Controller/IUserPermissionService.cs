namespace tests_.src.Web.Controller
{
    using global::tests_.src.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserPermissionService
    {
        /// <summary>
        /// Gets all permissions associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of permissions.</returns>
        Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId);

        /// <summary>
        /// Assigns a permission to a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="permissionId">The ID of the permission to assign.</param>
        Task AddPermissionToUserAsync(int userId, int permissionId);

        /// <summary>
        /// Removes a permission from a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="permissionId">The ID of the permission to remove.</param>
        Task RemovePermissionFromUserAsync(int userId, int permissionId);
    }
}
