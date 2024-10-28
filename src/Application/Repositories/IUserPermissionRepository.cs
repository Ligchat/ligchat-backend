namespace tests_.src.Application.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using tests_.src.Domain.Entities;

    public interface IUserPermissionRepository
    {
        /// <summary>
        /// Gets all permissions associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of permissions.</returns>
        Task<IEnumerable<Permission>> GetPermissionsByUserIdAsync(int userId);

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
