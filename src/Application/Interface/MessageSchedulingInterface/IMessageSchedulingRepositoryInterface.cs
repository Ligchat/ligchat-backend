using LigChat.Backend.Domain.Entities;
using System;
using System.Collections.Generic;

namespace LigChat.Data.Interfaces.IRepositories
{
    public interface IMessageSchedulingRepositoryInterface
    {
        // Obtém todos os agendamentos de mensagens. Retorna uma coleção de agendamentos de mensagens.
        IEnumerable<MessageScheduling> GetAll(int sectorId);

        // Obtém um agendamento de mensagem pelo ID. Retorna um agendamento de mensagem ou null se não encontrado.
        MessageScheduling? GetById(int id);

        // Obtém todos os agendamentos de mensagens para uma data específica. Retorna uma coleção de agendamentos de mensagens.
        IEnumerable<MessageScheduling> GetBySendDate(string sendDate);

        // Salva um novo agendamento de mensagem no repositório. Retorna o agendamento de mensagem salvo.
        MessageScheduling Save(MessageScheduling messageScheduling);

        // Atualiza um agendamento de mensagem existente pelo ID. Retorna o agendamento de mensagem atualizado.
        MessageScheduling Update(int id, MessageScheduling messageScheduling);

        // Deleta um agendamento de mensagem pelo ID. Retorna o agendamento de mensagem deletado.
        MessageScheduling Delete(int id);
    }
}
