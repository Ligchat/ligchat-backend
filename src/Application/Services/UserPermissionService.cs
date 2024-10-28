namespace tests_.src.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::tests_.src.Application.Repositories;
    using global::tests_.src.Domain.Entities;
    using global::tests_.src.Web.Controller;

    public class UserPermissionService : IUserPermissionService
    {
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IPermissionRepository _permissionRepository;

        public UserPermissionService(
            IUserPermissionRepository userPermissionRepository,
            IPermissionRepository permissionRepository)
        {
            _userPermissionRepository = userPermissionRepository;
            _permissionRepository = permissionRepository;
        }

        /// <summary>
        /// Gets all permissions associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of permissions.</returns>
        public async Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId)
        {
            return await _userPermissionRepository.GetPermissionsByUserIdAsync(userId);
        }

        /// <summary>
        /// Assigns a permission to a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="permissionId">The ID of the permission to assign.</param>
        public async Task AddPermissionToUserAsync(int userId, int permissionId)
        {
            // Check if the permission exists
            var permission = await _permissionRepository.GetByIdAsync(permissionId);
            if (permission == null)
            {
                throw new KeyNotFoundException("Permission not found.");
            }

            // Add the permission to the user
            await _userPermissionRepository.AddPermissionToUserAsync(userId, permissionId);
        }

        /// <summary>
        /// Removes a permission from a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="permissionId">The ID of the permission to remove.</param>
        public async Task RemovePermissionFromUserAsync(int userId, int permissionId)
        {
            // Remove the permission from the user
            await _userPermissionRepository.RemovePermissionFromUserAsync(userId, permissionId);
        }
    }
}
