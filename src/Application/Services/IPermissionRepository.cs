namespace tests_.src.Application.Services
{
    using global::tests_.src.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPermissionRepository
    {
        /// <summary>
        /// Retrieves all permissions.
        /// </summary>
        /// <returns>A collection of permissions.</returns>
        Task<IEnumerable<Permission>> GetAllAsync();

        /// <summary>
        /// Retrieves a permission by its ID.
        /// </summary>
        /// <param name="id">The ID of the permission.</param>
        /// <returns>The permission if found, otherwise null.</returns>
        Task<Permission?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new permission.
        /// </summary>
        /// <param name="permission">The permission to add.</param>
        Task AddAsync(Permission permission);

        /// <summary>
        /// Updates an existing permission.
        /// </summary>
        /// <param name="permission">The permission with updated information.</param>
        Task UpdateAsync(Permission permission);

        /// <summary>
        /// Deletes a permission by its ID.
        /// </summary>
        /// <param name="id">The ID of the permission to delete.</param>
        Task DeleteAsync(int id);
    }
}
