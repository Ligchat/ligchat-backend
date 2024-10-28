namespace tests_.src.Application.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using tests_.src.Domain.Entities;
    using LigChat.Backend.Web.Extensions.Database;

    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly DatabaseConfiguration _context;

        public UserPermissionRepository(DatabaseConfiguration context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all permissions associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of permissions.</returns>
        public async Task<IEnumerable<Permission>> GetPermissionsByUserIdAsync(int userId)
        {
            return await _context.UserPermissions
                .Where(up => up.UserId == userId)
                .Select(up => up.Permission)
                .ToListAsync();
        }

        /// <summary>
        /// Assigns a permission to a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="permissionId">The ID of the permission.</param>
        public async Task AddPermissionToUserAsync(int userId, int permissionId)
        {
            var userPermission = new UserPermission
            {
                UserId = userId,
                PermissionId = permissionId
            };

            _context.UserPermissions.Add(userPermission);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a permission from a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="permissionId">The ID of the permission to remove.</param>
        public async Task RemovePermissionFromUserAsync(int userId, int permissionId)
        {
            var userPermission = await _context.UserPermissions
                .FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permissionId);

            if (userPermission != null)
            {
                _context.UserPermissions.Remove(userPermission);
                await _context.SaveChangesAsync();
            }
        }
    }
}
