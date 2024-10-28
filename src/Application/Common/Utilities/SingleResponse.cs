namespace LigChat.Backend.Application.Utilities
{
    /// <summary>
    /// Representa a resposta para uma solicitação de um único item de dados.
    /// </summary>
    public class SingleResponse<T>
    {
        /// <summary>
        /// Mensagem de resposta (por exemplo, "Sucesso" ou "Erro").
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Código de resposta (por exemplo, um código de status ou erro).
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Dados encapsulados.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="message">Mensagem de resposta.</param>
        /// <param name="code">Código de resposta.</param>
        /// <param name="data">Dados encapsulados.</param>
        public SingleResponse(string message, string code, T data)
        {
            Message = message;
            Code = code;
            Data = data;
        }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public SingleResponse()
        {
        }
    }
}
