namespace tests_.src.Domain.DTOs.Whatsapp
{
    public class Whatsapp
    {
        // DTO para criar configurações de integração do WhatsApp
        public class CreateWhatsAppIntegrationSettingsDTO
        {
            public string ApiKey { get; set; }
            public string ApiSecret { get; set; }
            public string WebhookToken { get; set; }
            public string ValidatedPhone { get; set; }
            public int SectorId { get; set; }
        }

        // DTO para atualizar configurações de integração do WhatsApp
        public class UpdateWhatsAppIntegrationSettingsDTO
        {
            public string ApiKey { get; set; }
            public string ApiSecret { get; set; }
            public string WebhookToken { get; set; }
            public string ValidatedPhone { get; set; }
            public int SectorId { get; set; }
        }

        // DTO genérico para envio de mensagens no WhatsApp
        public class SendMessageDTO
        {
            public string To { get; set; }
            public string Body { get; set; }
            public string MessageType { get; set; } // Tipo da mensagem, pode ser "text", "image", "audio", "video", etc.
            public string MediaId { get; set; } // ID da mídia (caso a mensagem contenha mídia)
            public string MimeType { get; set; } // Tipo MIME da mídia (imagem, áudio, vídeo, documento)
            public string RecipientPhone { get; set; } // Número de telefone do destinatário
            public int SettingsId { get; set; } // ID das configurações do WhatsApp
        }

        // DTO para envio de mensagens de texto no WhatsApp
        public class WhatsAppTextMessageDTO
        {
            public string To { get; set; } // Número do destinatário no formato internacional
            public string Text { get; set; } // Conteúdo da mensagem de texto
            public string RecipientPhone { get; set; } // Número de telefone do destinatário
            public int SettingsId { get; set; } // ID das configurações do WhatsApp
        }

        // DTO para envio de mensagens de mídia no WhatsApp
        public class WhatsAppMediaMessageDTO
        {
            public string To { get; set; } // Número do destinatário no formato internacional
            public string MediaUrl { get; set; } // URL da mídia (imagem, vídeo, áudio ou documento)
            public string Caption { get; set; } // Texto opcional para legendas
            public string FileName { get; set; } // Nome do arquivo (somente para documentos)
            public string RecipientPhone { get; set; } // Número de telefone do destinatário
            public int SettingsId { get; set; } // ID das configurações do WhatsApp
        }
    }
}
