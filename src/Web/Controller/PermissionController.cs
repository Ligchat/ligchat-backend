using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tests_.src.Application.Services;
using tests_.src.Domain.Entities;
using tests_.src.Domain.DTOs.Permission;

namespace tests_.src.Web.Controller
{
    [ApiController]
    [Route("api/permissoes")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permission>>> GetAll()
        {
            var permissions = await _permissionService.GetAllAsync();
            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Permission>> GetById(int id)
        {
            var permission = await _permissionService.GetByIdAsync(id);
            if (permission == null)
            {
                return NotFound();
            }
            return Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePermissionDTO createPermissionDto)
        {
            var permission = new Permission
            {
                Name = createPermissionDto.Name,
                Description = createPermissionDto.Description
            };

            await _permissionService.AddAsync(permission);
            return CreatedAtAction(nameof(GetById), new { id = permission.Id }, permission);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePermissionDTO updatePermissionDto)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var existingPermission = await _permissionService.GetByIdAsync(id);
            if (existingPermission == null)
            {
                return NotFound();
            }

            existingPermission.Name = updatePermissionDto.Name;
            existingPermission.Description = updatePermissionDto.Description;

            await _permissionService.UpdateAsync(existingPermission);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var permission = await _permissionService.GetByIdAsync(id);
            if (permission == null)
            {
                return NotFound();
            }

            await _permissionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
