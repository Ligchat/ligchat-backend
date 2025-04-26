-- Criação da tabela de mensagens agendadas
CREATE TABLE IF NOT EXISTS ligchat.mensagens_agendadas (
    id BIGINT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    mensagem_de_texto TEXT NOT NULL,
    data_envio VARCHAR(20) NOT NULL,
    contato_id INTEGER NOT NULL,
    setor_id INTEGER NOT NULL,
    status BOOLEAN DEFAULT true,
    tag_id VARCHAR(255),
    data_criacao TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    data_atualizacao TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_contato FOREIGN KEY (contato_id) REFERENCES ligchat.contacts(id) ON DELETE CASCADE,
    CONSTRAINT fk_setor FOREIGN KEY (setor_id) REFERENCES ligchat.setores(id) ON DELETE CASCADE
);

-- Criação da tabela de anexos de mensagens
CREATE TABLE IF NOT EXISTS ligchat.mensagens_anexos (
    id BIGINT AUTO_INCREMENT PRIMARY KEY,
    mensagem_id BIGINT NOT NULL,
    tipo VARCHAR(10) NOT NULL CHECK (tipo IN ('image', 'audio', 'file')),
    nome_arquivo VARCHAR(255) NOT NULL,
    url_s3 VARCHAR(500) NOT NULL,
    data_criacao TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    data_atualizacao TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_mensagem FOREIGN KEY (mensagem_id) REFERENCES ligchat.mensagens_agendadas(id) ON DELETE CASCADE
);

-- Índices para melhorar a performance
CREATE INDEX idx_mensagens_agendadas_contato ON ligchat.mensagens_agendadas(contato_id);
CREATE INDEX idx_mensagens_agendadas_setor ON ligchat.mensagens_agendadas(setor_id);
CREATE INDEX idx_mensagens_agendadas_data_envio ON ligchat.mensagens_agendadas(data_envio);
CREATE INDEX idx_mensagens_anexos_mensagem ON ligchat.mensagens_anexos(mensagem_id);

-- Comentários nas tabelas e colunas
COMMENT ON TABLE ligchat.mensagens_agendadas IS 'Tabela que armazena as mensagens agendadas para envio';
COMMENT ON COLUMN ligchat.mensagens_agendadas.id IS 'Identificador único da mensagem agendada';
COMMENT ON COLUMN ligchat.mensagens_agendadas.nome IS 'Título ou nome da mensagem agendada';
COMMENT ON COLUMN ligchat.mensagens_agendadas.mensagem_de_texto IS 'Conteúdo completo da mensagem que será enviada';
COMMENT ON COLUMN ligchat.mensagens_agendadas.data_envio IS 'Data e hora programada para envio da mensagem (formato: YYYY-MM-DD HH:mm:ss)';
COMMENT ON COLUMN ligchat.mensagens_agendadas.contato_id IS 'ID do contato para quem a mensagem será enviada';
COMMENT ON COLUMN ligchat.mensagens_agendadas.setor_id IS 'ID do setor ao qual a mensagem pertence';
COMMENT ON COLUMN ligchat.mensagens_agendadas.status IS 'Status da mensagem (ativo/inativo)';
COMMENT ON COLUMN ligchat.mensagens_agendadas.tag_id IS 'IDs das tags associadas à mensagem, separados por vírgula';
COMMENT ON COLUMN ligchat.mensagens_agendadas.data_criacao IS 'Data e hora de criação do registro';
COMMENT ON COLUMN ligchat.mensagens_agendadas.data_atualizacao IS 'Data e hora da última atualização do registro';

COMMENT ON TABLE ligchat.mensagens_anexos IS 'Tabela que armazena os anexos das mensagens agendadas';
COMMENT ON COLUMN ligchat.mensagens_anexos.id IS 'Identificador único do anexo';
COMMENT ON COLUMN ligchat.mensagens_anexos.mensagem_id IS 'ID da mensagem agendada à qual o anexo pertence';
COMMENT ON COLUMN ligchat.mensagens_anexos.tipo IS 'Tipo do anexo (image, audio ou file)';
COMMENT ON COLUMN ligchat.mensagens_anexos.nome_arquivo IS 'Nome original do arquivo anexado';
COMMENT ON COLUMN ligchat.mensagens_anexos.url_s3 IS 'URL do arquivo no bucket S3';
COMMENT ON COLUMN ligchat.mensagens_anexos.data_criacao IS 'Data e hora de criação do registro';
COMMENT ON COLUMN ligchat.mensagens_anexos.data_atualizacao IS 'Data e hora da última atualização do registro';