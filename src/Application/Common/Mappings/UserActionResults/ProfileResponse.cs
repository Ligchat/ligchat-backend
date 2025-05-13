using LigChat.Backend.Domain.DTOs.UserDto;

namespace LigChat.Backend.Application.Common.Mappings.UserActionResults
{
    /// <summary>
    /// Resposta para operações relacionadas ao perfil básico do usuário.
    /// </summary>
    public class ProfileResponse
    {
        /// <summary>
        /// Mensagem que indica o resultado da operação.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Código HTTP que representa o resultado da operação.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Dados do perfil básico do usuário.
        /// </summary>
        public ProfileResponseDTO? Data { get; set; }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ProfileResponse() { }

        /// <summary>
        /// Construtor para inicializar a resposta com valores específicos.
        /// </summary>
        public ProfileResponse(string message, string code, ProfileResponseDTO? data)
        {
            Message = message;
            Code = code;
            Data = data;
        }
    }
} 