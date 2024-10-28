namespace tests_.src.Web.Controller
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::tests_.src.Domain.Entities;
    using global::tests_.src.Domain.DTOs.UserPermissionDTO;

    [ApiController]
    [Route("api/user-permissions")]
    public class UserPermissionController : ControllerBase
    {
        private readonly IUserPermissionService _userPermissionService;

        public UserPermissionController(IUserPermissionService userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        /// <summary>
        /// Gets all permissions of a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of permissions associated with the user.</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Permission>>> GetUserPermissions(int userId)
        {
            var permissions = await _userPermissionService.GetUserPermissionsAsync(userId);
            return Ok(permissions);
        }

        /// <summary>
        /// Assigns a permission to a user.
        /// </summary>
        /// <param name="createUserPermissionDto">The DTO containing the user ID and permission ID.</param>
        [HttpPost]
        public async Task<IActionResult> AddPermissionToUser([FromBody] CreateUserPermissionDTO createUserPermissionDto)
        {
            await _userPermissionService.AddPermissionToUserAsync(
                createUserPermissionDto.UserId,
                createUserPermissionDto.PermissionId);

            return Ok("Permission added to user successfully.");
        }

        /// <summary>
        /// Removes a permission from a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="permissionId">The ID of the permission to remove.</param>
        [HttpDelete("{userId}/{permissionId}")]
        public async Task<IActionResult> RemovePermissionFromUser(int userId, int permissionId)
        {
            await _userPermissionService.RemovePermissionFromUserAsync(userId, permissionId);
            return Ok("Permission removed from user successfully.");
        }
    }
}
