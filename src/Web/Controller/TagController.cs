using LigChat.Backend.Application.Common.Mappings.TagActionResults;
using LigChat.Backend.Domain.DTOs.TagDto;
using LigChat.Data.Interfaces.IControllers;
using LigChat.Data.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LigChat.Api.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagController : ControllerBase, ITagControllerInterface
    {
        private readonly ITagServiceInterface _tagService;

        public TagController(ITagServiceInterface tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public IActionResult GetAll(int sectorId)
        {
            var tagListResponse = _tagService.GetAll(sectorId); // Passando o sectorId para o servi�o
            if (tagListResponse == null || !tagListResponse.Data.Any())
            {
                return NotFound(new TagListResponse
                {
                    Message = "No tags found.",
                    Code = "404",
                    Data = new List<TagViewModel>()
                });
            }

            return Ok(tagListResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tagResponse = _tagService.GetById(id);

            if (tagResponse == null)
            {
                return NotFound(new SingleTagResponse
                {
                    Message = "Tag not found.",
                    Code = "404",
                    Data = null
                });
            }

            return Ok(tagResponse);
        }

        [HttpPost]
        public IActionResult Save([FromBody] CreateTagRequestDTO tagDto)
        {
            if (tagDto == null)
            {
                return BadRequest("Tag data is required.");
            }

            var createdTagResponse = _tagService.Save(tagDto);

            if (createdTagResponse == null)
            {
                return BadRequest(new SingleTagResponse
                {
                    Message = "Tag could not be created.",
                    Code = "400",
                    Data = null
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdTagResponse.Data?.Id },
                createdTagResponse
            );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateTagRequestDTO tagDto)
        {
            if (tagDto == null)
            {
                return BadRequest("Tag data is required.");
            }

            var existingTagResponse = _tagService.GetById(id);

            if (existingTagResponse == null)
            {
                return NotFound(new SingleTagResponse
                {
                    Message = "Tag not found.",
                    Code = "404",
                    Data = null
                });
            }

            var updatedTagResponse = _tagService.Update(id, tagDto);
            if (updatedTagResponse == null)
            {
                return BadRequest(new SingleTagResponse
                {
                    Message = "Tag could not be updated.",
                    Code = "400",
                    Data = null
                });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tagResponse = _tagService.GetById(id);

            if (tagResponse == null)
            {
                return NotFound(new SingleTagResponse
                {
                    Message = "Tag not found.",
                    Code = "404",
                    Data = null
                });
            }

            _tagService.Delete(id);
            return NoContent();
        }
    }
}
