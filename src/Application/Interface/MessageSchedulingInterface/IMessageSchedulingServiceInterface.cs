using LigChat.Backend.Application.Common.Mappings.MessageSchedulingActionResults;
using LigChat.Backend.Domain.DTOs.MessageSchedulingDto;

namespace LigChat.Data.Interfaces.IServices
{
    public interface IMessageSchedulingServiceInterface
    {
        // Retorna uma coleção de mensagens agendadas encapsulada em um DTO com mensagem e código
        MessageSchedulingListResponse GetAll(int sectorId);

        // Retorna uma mensagem agendada única com base no ID encapsulada em um DTO com mensagem e código, ou nulo se não encontrado
        SingleMessageSchedulingResponse? GetById(int id);

        // Cria uma nova mensagem agendada com base nos dados fornecidos e retorna um DTO com mensagem e código
        SingleMessageSchedulingResponse? Save(CreateMessageSchedulingRequestDTO messageSchedulingDto);

        // Atualiza uma mensagem agendada existente com base no ID e nos dados fornecidos, retornando um DTO com mensagem e código
        SingleMessageSchedulingResponse? Update(int id, UpdateMessageSchedulingRequestDTO messageSchedulingDto);

        // Deleta uma mensagem agendada com base no ID e retorna um DTO com mensagem e código, ou nulo se não encontrado
        SingleMessageSchedulingResponse? Delete(int id);
    }
}
