using LigChat.Backend.Application.Common.Mappings.TagActionResults;
using LigChat.Backend.Domain.DTOs.TagDto;
using LigChat.Backend.Domain.Entities;
using LigChat.Data.Interfaces.IRepositories;
using LigChat.Data.Interfaces.IServices;

namespace LigChat.Api.Services.TagService
{
    public class TagService : ITagServiceInterface
    {
        private readonly ITagRepositoryInterface _tagRepository;

        public TagService(ITagRepositoryInterface tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public TagListResponse GetAll(int sectorId)
        {
            var tags = _tagRepository.GetAll(sectorId);
            var tagDtos = tags.Select(tag => new TagViewModel(tag.Id, tag.Name, tag.Description, tag.SectorId, tag.Status));
            return new TagListResponse("Success", "200", tagDtos);
        }

        public SingleTagResponse? GetById(int id)
        {
            var tag = _tagRepository.GetById(id);
            if (tag == null)
            {
                return new SingleTagResponse("Tag not found", "404", null);
            }
            var tagDto = new TagViewModel(tag.Id, tag.Name, tag.Description, tag.SectorId, tag.Status);
            return new SingleTagResponse("Success", "200", tagDto);
        }

        public SingleTagResponse? GetByName(string name)
        {
            var tag = _tagRepository.GetByName(name);
            if (tag == null)
            {
                return new SingleTagResponse("Tag not found", "404", null);
            }
            var tagDto = new TagViewModel(tag.Id, tag.Name, tag.Description, tag.SectorId, tag.Status);
            return new SingleTagResponse("Success", "200", tagDto);
        }

        public SingleTagResponse Save(CreateTagRequestDTO tagDto)
        {
            // Valida��o manual (implementar a l�gica de valida��o aqui)
            if (string.IsNullOrWhiteSpace(tagDto.Name))
            {
                return new SingleTagResponse("Invalid request", "400", null);
            }

            // Cria��o do objeto Tag
            var tag = new Tag
            {
                Name = tagDto.Name,
                Description = tagDto.Description,
                SectorId = tagDto.SectorId,
                Status = tagDto.Status
            };

            var savedTag = _tagRepository.Save(tag);
            var responseDto = new TagViewModel(savedTag.Id, savedTag.Name, savedTag.Description, savedTag.SectorId, savedTag.Status);
            return new SingleTagResponse("Tag created successfully", "201", responseDto);
        }

        public SingleTagResponse Update(int id, UpdateTagRequestDTO tagDto)
        {
            // Valida��o manual (implementar a l�gica de valida��o aqui)
            if (string.IsNullOrWhiteSpace(tagDto.Name))
            {
                return new SingleTagResponse("Invalid request", "400", null);
            }

            var existingTag = _tagRepository.GetById(id);
            if (existingTag == null)
            {
                return new SingleTagResponse("Tag not found", "404", null);
            }

            // Atualizando o tag
            existingTag.Name = tagDto.Name;
            existingTag.Description = tagDto.Description;
            existingTag.SectorId = tagDto.SectorId;
            existingTag.Status = tagDto.Status;

            var savedTag = _tagRepository.Update(id, existingTag);
            var responseDto = new TagViewModel(savedTag.Id, savedTag.Name, savedTag.Description, savedTag.SectorId, savedTag.Status);
            return new SingleTagResponse("Tag updated successfully", "200", responseDto);
        }

        public SingleTagResponse Delete(int id)
        {
            var deletedTag = _tagRepository.Delete(id);
            if (deletedTag == null)
            {
                return new SingleTagResponse("Tag not found", "404", null);
            }
            var responseDto = new TagViewModel(deletedTag.Id, deletedTag.Name, deletedTag.Description, deletedTag.SectorId, deletedTag.Status);
            return new SingleTagResponse("Tag deleted successfully", "200", responseDto);
        }
    }
}
