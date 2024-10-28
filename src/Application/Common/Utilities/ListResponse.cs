namespace LigChat.Backend.Application.Utilities
{
    /// <summary>
    /// Representa a resposta para uma solicitação que retorna uma lista de dados.
    /// </summary>
    public class ListResponse<T>
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
        /// Lista de dados encapsulados.
        /// </summary>
        public IEnumerable<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="message">Mensagem de resposta.</param>
        /// <param name="code">Código de resposta.</param>
        /// <param name="data">Lista de dados.</param>
        public ListResponse(string message, string code, IEnumerable<T> data)
        {
            Message = message;
            Code = code;
            Data = data ?? new List<T>(); // Evita valores nulos
        }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ListResponse()
        {
            Data = new List<T>();
        }
    }
}
