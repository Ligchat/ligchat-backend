using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LigChat.Backend.Web.Extensions.Database;
using Microsoft.EntityFrameworkCore;
using tests_.src.Domain.Entities;

namespace tests_.src.Application.Services
{
    public class BusinessDayService
    {
        private readonly DatabaseConfiguration _context;

        public BusinessDayService(DatabaseConfiguration context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria ou atualiza um horário comercial.
        /// </summary>
        /// <param name="businessDayDto">DTO contendo os dados do horário comercial.</param>
        /// <returns>Retorna uma mensagem de sucesso ou erro.</returns>
        public async Task<string> CreateOrUpdateBusinessDay(BusinessHoursDto businessDayDto)
        {
            // Verificar se o registro já existe para o UserId e o DayOfWeek
            var existingBusinessDay = await _context.BusinessHours
                .FirstOrDefaultAsync(b => b.SectorId == businessDayDto.SectorId && b.DayOfWeek == businessDayDto.DayOfWeek);

            if (existingBusinessDay != null)
            {

                existingBusinessDay.OpeningTime = businessDayDto.OpeningTime;
                existingBusinessDay.ClosingTime = businessDayDto.ClosingTime;
                existingBusinessDay.IsOpen = businessDayDto.IsOpen;

                _context.BusinessHours.Update(existingBusinessDay);
            }
            else
            {
                // Criar novo registro
                var newBusinessDay = new BusinessHours
                {
                    DayOfWeek = businessDayDto.DayOfWeek,
                    OpeningTime = businessDayDto.OpeningTime,
                    ClosingTime = businessDayDto.ClosingTime,
                    IsOpen = businessDayDto.IsOpen,
                    SectorId = businessDayDto.SectorId
                };

                _context.BusinessHours.Add(newBusinessDay);
            }

            await _context.SaveChangesAsync();

            return "Business day saved successfully.";
        }

        /// <summary>
        /// Busca todos os horários comerciais de um usuário específico.
        /// </summary>
        /// <param name="sectorId">ID do usuário.</param>
        /// <returns>Retorna uma lista de horários comerciais do usuário.</returns>
        public async Task<List<BusinessHoursDto>> GetBusinessDaysByUser(int sectorId)
        {
            var businessDays = await _context.BusinessHours
                .Where(b => b.SectorId == sectorId)
                .ToListAsync();

            return businessDays.Select(b => new BusinessHoursDto
            {
                DayOfWeek = b.DayOfWeek,
                OpeningTime = b.OpeningTime,
                ClosingTime = b.ClosingTime,
                IsOpen = b.IsOpen,
                SectorId = b.SectorId
            }).ToList();
        }
    }

    // DTO para transferência de dados entre a aplicação e o serviço
    public class BusinessHoursDto
    {
        public string DayOfWeek { get; set; }
        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }
        public bool IsOpen { get; set; }
        public int SectorId { get; set; }
    }
}
