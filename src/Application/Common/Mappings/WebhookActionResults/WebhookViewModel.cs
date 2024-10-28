namespace LigChat.Backend.Application.Common.Mappings.WebhookActionResults
{
    /// <summary>
    /// Representa o modelo de visualização para dados do webhook na API.
    /// </summary>
    public class WebhookViewModel
    {
        // Identificador único do webhook
        public int Id { get; }
        
        // Nome do webhook
        public string Name { get; }
        
        // Chave de token para autenticação
        public string TokenKey { get; }
        
        // URL do webhook
        public string Url { get; }
        
        // Status da transição de fluxo
        public bool Status { get; }
        
        // Identificador do setor associado (opcional)
        public int? SectorId { get; }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public WebhookViewModel()
        {
            Id = 0;
            Name = string.Empty;
            TokenKey = string.Empty;
            Url = string.Empty;
            Status = false;
            SectorId = null;
        }

        /// <summary>
        /// Construtor para inicializar o DTO com valores específicos.
        /// </summary>
        /// <param name="id">Identificador único do webhook.</param>
        /// <param name="name">Nome do webhook.</param>
        /// <param name="tokenKey">Chave de token para autenticação.</param>
        /// <param name="url">URL do webhook.</param>
        /// <param name="status">Status do webhook.</param>
        /// <param name="sectorId">Identificador do setor associado (opcional).</param>
        public WebhookViewModel(int id, string name, string tokenKey, string url, bool status, int? sectorId = null)
        {
            Id = id;
            Name = name;
            TokenKey = tokenKey;
            Url = url;
            Status = status;
            SectorId = sectorId;
        }
    }
}
