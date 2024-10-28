// Licensed to the .NET Foundation under one or more agreements.

namespace tests_.src.Domain.Entities
{
        public class WhatsAppIntegrationSettings
        {
            public int Id { get; set; }  // ID da integração
            public string ApiKey { get; set; }  // API Key da Meta
            public string ApiSecret { get; set; }  // API Secret da Meta
            public string WebhookToken { get; set; }  // Token do Webhook
            public string ValidatedPhone { get; set; }  // Telefone validado na Meta
            public int SectorId { get; set; }  // Setor associado
        }

        public class WhatsAppMessage
        {
            public int Id { get; set; }  // ID da mensagem
            public string MessageId { get; set; }  // ID da mensagem na plataforma Meta
            public string From { get; set; }  // Origem da mensagem
            public string To { get; set; }  // Destino da mensagem
            public string Body { get; set; }  // Conteúdo da mensagem
            public long Timestamp { get; set; }  // Timestamp da mensagem
            public string MessageType { get; set; }  // Tipo de mensagem (texto, imagem, áudio, etc.)
            public string MediaId { get; set; }  // ID da mídia
            public string MimeType { get; set; }  // MimeType da mídia
            public string FileName { get; set; }  // Nome do arquivo (se for um documento)
            public string Caption { get; set; }  // Legenda da imagem ou mídia
        }

    }
