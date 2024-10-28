namespace tests_.src.Application.Services
{
    using global::tests_.src.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _permissionRepository.GetAllAsync();
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _permissionRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Permission permission)
        {
            await _permissionRepository.AddAsync(permission);
        }

        public async Task UpdateAsync(Permission permission)
        {
            await _permissionRepository.UpdateAsync(permission);
        }

        public async Task DeleteAsync(int id)
        {
            await _permissionRepository.DeleteAsync(id);
        }
    }
}
