namespace tests_.src.Application.Services
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using LigChat.Backend.Web.Extensions.Database;
    using global::tests_.src.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using static global::tests_.src.Domain.DTOs.Whatsapp.Whatsapp;

    public class WhatsAppIntegrationService
    {
        private readonly DatabaseConfiguration _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public WhatsAppIntegrationService(DatabaseConfiguration context, IConfiguration configuration, HttpClient httpClient)
        {
            _context = context;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<WhatsAppAccount> CreateOrUpdateWhatsAppAccount(WhatsAppAccount account)
        {
            var existingAccount = await _context.WhatsAppAccount.FirstOrDefaultAsync(a => a.SessionId == account.SessionId);
            if (existingAccount != null)
            {
                existingAccount.IsConnected = account.IsConnected;
                existingAccount.QrCode = account.QrCode;
                existingAccount.Number = account.Number;
                _context.WhatsAppAccount.Update(existingAccount);
            }
            else
            {
                await _context.WhatsAppAccount.AddAsync(account);
            }
            await _context.SaveChangesAsync();
            return account;
        }

        // Obter uma conta WhatsApp
        public async Task<WhatsAppAccount> GetWhatsAppAccount(string sessionId)
        {
            return await _context.WhatsAppAccount.FirstOrDefaultAsync(a => a.SessionId == sessionId);
        }

        // Criar uma nova mensagem
        public async Task<Messeageging> CreateMessage(Messeageging message)
        {
            await _context.Messeageging.AddAsync(message);
            await _context.SaveChangesAsync();
            return message;
        }

        // Obter mensagens por conexão (session_id)
        public async Task<List<Messeageging>> GetMessagesByConnectionId(int connectionId)
        {
            return await _context.Messeageging.Where(m => m.ContatoId == connectionId).ToListAsync();
        }

        public async Task<WhatsAppIntegrationSettings> CreateSettingsAsync(CreateWhatsAppIntegrationSettingsDTO dto)
        {
            var settings = new WhatsAppIntegrationSettings
            {
                ApiKey = dto.ApiKey,
                ApiSecret = dto.ApiSecret,
                WebhookToken = dto.WebhookToken,
                ValidatedPhone = dto.ValidatedPhone,
                SectorId = dto.SectorId
            };

            _context.WhatsAppIntegrationSettings.Add(settings);
            await _context.SaveChangesAsync();

            return settings;
        }

        public async Task<WhatsAppIntegrationSettings> UpdateSettingsAsync(int id, UpdateWhatsAppIntegrationSettingsDTO dto)
        {
            var settings = await _context.WhatsAppIntegrationSettings.FindAsync(id);

            if (settings == null)
            {
                throw new KeyNotFoundException("Configurações não encontradas");
            }

            settings.ApiKey = dto.ApiKey;
            settings.ApiSecret = dto.ApiSecret;
            settings.WebhookToken = dto.WebhookToken;
            settings.ValidatedPhone = dto.ValidatedPhone;
            settings.SectorId = dto.SectorId;

            await _context.SaveChangesAsync();

            return settings;
        }

        public async Task<WhatsAppIntegrationSettings> GetSettingsAsync(int id)
        {
            var settings = await _context.WhatsAppIntegrationSettings.FindAsync(id);
            if (settings == null)
            {
                throw new KeyNotFoundException("Configurações não encontradas");
            }

            return settings;
        }

        public async Task DeleteSettingsAsync(int id)
        {
            var settings = await _context.WhatsAppIntegrationSettings.FindAsync(id);
            if (settings == null)
            {
                throw new KeyNotFoundException("Configurações não encontradas");
            }

            _context.WhatsAppIntegrationSettings.Remove(settings);
            await _context.SaveChangesAsync();
        }

        // Método para enviar mensagens de texto
        public async Task SendTextMessageAsync(string recipientPhone, string message, int settingsId)
        {
            var settings = await GetSettingsAsync(settingsId);
            var url = $"https://graph.facebook.com/v12.0/{settings.ValidatedPhone}/messages";
            var payload = new
            {
                messaging_product = "whatsapp",
                to = recipientPhone,
                text = new
                {
                    body = message
                }
            };

            await SendRequestAsync(url, settings.WebhookToken, payload);
        }

        // Método para enviar imagens com descrição
        public async Task SendImageMessageAsync(string recipientPhone, string imageUrl, string caption, int settingsId)
        {
            var settings = await GetSettingsAsync(settingsId);
            var url = $"https://graph.facebook.com/v12.0/{settings.ValidatedPhone}/messages";
            var payload = new
            {
                messaging_product = "whatsapp",
                to = recipientPhone,
                type = "image",
                image = new
                {
                    link = imageUrl,
                    caption = caption
                }
            };

            await SendRequestAsync(url, settings.ApiKey, payload);
        }

        // Método para enviar documentos
        public async Task SendDocumentMessageAsync(string recipientPhone, string documentUrl, string fileName, int settingsId)
        {
            var settings = await GetSettingsAsync(settingsId);
            var url = $"https://graph.facebook.com/v12.0/{settings.ValidatedPhone}/messages";
            var payload = new
            {
                messaging_product = "whatsapp",
                to = recipientPhone,
                type = "document",
                document = new
                {
                    link = documentUrl,
                    filename = fileName
                }
            };

            await SendRequestAsync(url, settings.ApiKey, payload);
        }

        // Método para enviar áudios
        public async Task SendAudioMessageAsync(string recipientPhone, string audioUrl, int settingsId)
        {
            var settings = await GetSettingsAsync(settingsId);
            var url = $"https://graph.facebook.com/v12.0/{settings.ValidatedPhone}/messages";
            var payload = new
            {
                messaging_product = "whatsapp",
                to = recipientPhone,
                type = "audio",
                audio = new
                {
                    link = audioUrl
                }
            };

            await SendRequestAsync(url, settings.ApiKey, payload);
        }

        // Método privado para fazer a requisição HTTP com a API do WhatsApp
        private async Task SendRequestAsync(string url, string accessToken, object payload)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error sending message: {errorMessage}");
            }
        }
    }
}
