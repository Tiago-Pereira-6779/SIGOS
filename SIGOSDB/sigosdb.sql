-- =====================================================================
-- SIGOS - Sistema Integrado de Gestão de Ordens de Serviço
-- Script do banco SIGOSDB (MariaDB/MySQL - XAMPP)
-- Versão aprimorada a partir do script base do professor
-- Como rodar: phpMyAdmin > aba "Importar" (ou aba SQL) > executar.
-- Requer MariaDB 10.2+ ou MySQL 8.0.16+ (CHECK constraints).
-- =====================================================================

CREATE DATABASE IF NOT EXISTS sigosdb
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE sigosdb;

-- ---------------------------------------------------------------------
-- 1. TABELAS
-- ---------------------------------------------------------------------

CREATE TABLE tecnicos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(120) NOT NULL,
    especialidade VARCHAR(100),
    telefone VARCHAR(20),
    email VARCHAR(120),
    ativo BOOLEAN NOT NULL DEFAULT TRUE
) ENGINE=InnoDB;

CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    login VARCHAR(50) NOT NULL UNIQUE,
    senha_hash VARCHAR(255) NOT NULL,           -- hash (ex.: BCrypt = 60 chars), NUNCA senha pura
    perfil ENUM('ADMIN','ATENDENTE','TECNICO') NOT NULL,
    tecnico_id INT NULL,                        -- opcional: liga usuário do perfil TECNICO ao cadastro de técnico
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_usuarios_tecnico FOREIGN KEY (tecnico_id)
        REFERENCES tecnicos(id) ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE TABLE clientes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(120) NOT NULL,
    documento VARCHAR(14) NOT NULL UNIQUE,      -- CPF (11) ou CNPJ (14), somente números
    telefone VARCHAR(20),
    email VARCHAR(120),
    endereco VARCHAR(200),
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT chk_clientes_documento CHECK (CHAR_LENGTH(documento) IN (11, 14))
) ENGINE=InnoDB;

CREATE TABLE equipamentos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    cliente_id INT NOT NULL,
    tipo VARCHAR(60) NOT NULL,
    marca VARCHAR(60),
    modelo VARCHAR(80),
    numero_serie VARCHAR(100),
    defeito_informado TEXT,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    -- necessário para a FK composta de ordens_servico (garante cliente x equipamento)
    UNIQUE KEY uq_equip_cliente (id, cliente_id),
    INDEX idx_equip_serie (numero_serie),
    CONSTRAINT fk_equip_cliente FOREIGN KEY (cliente_id)
        REFERENCES clientes(id) ON DELETE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE servicos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    descricao VARCHAR(120) NOT NULL UNIQUE,
    preco DECIMAL(10,2) NOT NULL,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT chk_servicos_preco CHECK (preco >= 0)
) ENGINE=InnoDB;

CREATE TABLE ordens_servico (
    id INT AUTO_INCREMENT PRIMARY KEY,           -- número automático da OS
    cliente_id INT NOT NULL,
    equipamento_id INT NOT NULL,
    tecnico_id INT NULL,
    usuario_abertura_id INT NOT NULL,
    data_abertura DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    data_inicio DATETIME NULL,                   -- preenchida ao entrar em EM_EXECUCAO
    data_conclusao DATETIME NULL,
    descricao_problema TEXT NOT NULL,            -- exigida no Módulo 3
    prioridade ENUM('BAIXA','MEDIA','ALTA','URGENTE') NOT NULL DEFAULT 'MEDIA',
    status ENUM('ABERTA','DIAGNOSTICO','AGUARDANDO_APROVACAO',
                'EM_EXECUCAO','CONCLUIDA','CANCELADA') NOT NULL DEFAULT 'ABERTA',
    diagnostico TEXT,
    solucao TEXT,
    justificativa_cancelamento VARCHAR(255) NULL,
    subtotal DECIMAL(10,2) NOT NULL DEFAULT 0,   -- soma dos itens (mantido por trigger)
    desconto DECIMAL(10,2) NOT NULL DEFAULT 0,
    total DECIMAL(10,2) NOT NULL DEFAULT 0,      -- subtotal - desconto (mantido por trigger)
    INDEX idx_os_status (status),
    INDEX idx_os_abertura (data_abertura),
    INDEX idx_os_cliente (cliente_id),
    INDEX idx_os_tecnico (tecnico_id),
    CONSTRAINT fk_os_cliente FOREIGN KEY (cliente_id)
        REFERENCES clientes(id) ON DELETE RESTRICT,
    -- FK composta: o equipamento TEM que pertencer ao cliente da OS
    CONSTRAINT fk_os_equip_cliente FOREIGN KEY (equipamento_id, cliente_id)
        REFERENCES equipamentos(id, cliente_id) ON DELETE RESTRICT,
    CONSTRAINT fk_os_tecnico FOREIGN KEY (tecnico_id)
        REFERENCES tecnicos(id) ON DELETE RESTRICT,
    CONSTRAINT fk_os_usuario FOREIGN KEY (usuario_abertura_id)
        REFERENCES usuarios(id) ON DELETE RESTRICT,
    CONSTRAINT chk_os_desconto CHECK (desconto >= 0),
    CONSTRAINT chk_os_subtotal CHECK (subtotal >= 0),
    CONSTRAINT chk_os_total CHECK (total >= 0)
) ENGINE=InnoDB;

CREATE TABLE itens_os (
    id INT AUTO_INCREMENT PRIMARY KEY,
    ordem_servico_id INT NOT NULL,
    servico_id INT NOT NULL,
    quantidade DECIMAL(10,2) NOT NULL DEFAULT 1,
    preco_unitario DECIMAL(10,2) NOT NULL,       -- "foto" do preço no momento da OS
    -- um serviço por OS; para repetir, aumente a quantidade
    UNIQUE KEY uq_item_os_servico (ordem_servico_id, servico_id),
    CONSTRAINT fk_itens_os FOREIGN KEY (ordem_servico_id)
        REFERENCES ordens_servico(id) ON DELETE RESTRICT,
    CONSTRAINT fk_itens_servico FOREIGN KEY (servico_id)
        REFERENCES servicos(id) ON DELETE RESTRICT,
    CONSTRAINT chk_itens_qtd CHECK (quantidade > 0),
    CONSTRAINT chk_itens_preco CHECK (preco_unitario >= 0)
) ENGINE=InnoDB;

CREATE TABLE historico_os (
    id INT AUTO_INCREMENT PRIMARY KEY,
    ordem_servico_id INT NOT NULL,
    usuario_id INT NOT NULL,
    status_anterior VARCHAR(30),
    status_novo VARCHAR(30) NOT NULL,
    justificativa VARCHAR(255),
    data_alteracao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_hist_os (ordem_servico_id),
    CONSTRAINT fk_hist_os FOREIGN KEY (ordem_servico_id)
        REFERENCES ordens_servico(id) ON DELETE RESTRICT,
    CONSTRAINT fk_hist_usuario FOREIGN KEY (usuario_id)
        REFERENCES usuarios(id) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- ---------------------------------------------------------------------
-- 2. TRIGGERS (rede de segurança no banco; o C# também deve validar
--    e mostrar mensagens amigáveis ao usuário)
--    No phpMyAdmin o DELIMITER funciona normalmente na importação.
-- ---------------------------------------------------------------------

DELIMITER $$

-- Nova OS: sempre nasce ABERTA e com valores zerados
CREATE TRIGGER trg_os_bi BEFORE INSERT ON ordens_servico
FOR EACH ROW
BEGIN
    SET NEW.status   = 'ABERTA';
    SET NEW.subtotal = 0;
    SET NEW.total    = 0;
END$$

-- Fluxo de status + regras de conclusão/cancelamento + recálculo do total
CREATE TRIGGER trg_os_bu BEFORE UPDATE ON ordens_servico
FOR EACH ROW
BEGIN
    DECLARE v_qtd_itens INT DEFAULT 0;

    SET NEW.total = GREATEST(NEW.subtotal - NEW.desconto, 0);

    IF NEW.status <> OLD.status THEN

        IF OLD.status IN ('CONCLUIDA','CANCELADA') THEN
            SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'OS finalizada nao pode mudar de status.';

        ELSEIF NEW.status = 'CANCELADA' THEN
            IF NEW.justificativa_cancelamento IS NULL
               OR TRIM(NEW.justificativa_cancelamento) = '' THEN
                SIGNAL SQLSTATE '45000'
                    SET MESSAGE_TEXT = 'Cancelamento exige justificativa.';
            END IF;

        ELSEIF NOT (
               (OLD.status = 'ABERTA'               AND NEW.status = 'DIAGNOSTICO')
            OR (OLD.status = 'DIAGNOSTICO'          AND NEW.status = 'AGUARDANDO_APROVACAO')
            OR (OLD.status = 'AGUARDANDO_APROVACAO' AND NEW.status = 'EM_EXECUCAO')
            OR (OLD.status = 'EM_EXECUCAO'          AND NEW.status = 'CONCLUIDA')
        ) THEN
            SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'Transicao de status invalida.';
        END IF;

        IF NEW.status = 'EM_EXECUCAO' AND NEW.data_inicio IS NULL THEN
            SET NEW.data_inicio = NOW();
        END IF;

        IF NEW.status = 'CONCLUIDA' THEN
            SELECT COUNT(*) INTO v_qtd_itens
              FROM itens_os WHERE ordem_servico_id = NEW.id;

            IF NEW.diagnostico IS NULL OR TRIM(NEW.diagnostico) = '' THEN
                SIGNAL SQLSTATE '45000'
                    SET MESSAGE_TEXT = 'Nao e possivel concluir sem diagnostico.';
            ELSEIF v_qtd_itens = 0 THEN
                SIGNAL SQLSTATE '45000'
                    SET MESSAGE_TEXT = 'Nao e possivel concluir sem servico registrado.';
            ELSEIF NEW.total <= 0 THEN
                SIGNAL SQLSTATE '45000'
                    SET MESSAGE_TEXT = 'Nao e possivel concluir sem valor calculado.';
            END IF;

            IF NEW.data_conclusao IS NULL THEN
                SET NEW.data_conclusao = NOW();
            END IF;
        END IF;
    END IF;
END$$

-- Itens só podem ser alterados em OS ainda não finalizada
CREATE TRIGGER trg_itens_bi BEFORE INSERT ON itens_os
FOR EACH ROW
BEGIN
    DECLARE v_status VARCHAR(30);
    SELECT status INTO v_status FROM ordens_servico WHERE id = NEW.ordem_servico_id;
    IF v_status IN ('CONCLUIDA','CANCELADA') THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'OS finalizada nao aceita alteracao de itens.';
    END IF;
END$$

CREATE TRIGGER trg_itens_bu BEFORE UPDATE ON itens_os
FOR EACH ROW
BEGIN
    DECLARE v_status VARCHAR(30);
    SELECT status INTO v_status FROM ordens_servico WHERE id = OLD.ordem_servico_id;
    IF v_status IN ('CONCLUIDA','CANCELADA') THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'OS finalizada nao aceita alteracao de itens.';
    END IF;
END$$

CREATE TRIGGER trg_itens_bd BEFORE DELETE ON itens_os
FOR EACH ROW
BEGIN
    DECLARE v_status VARCHAR(30);
    SELECT status INTO v_status FROM ordens_servico WHERE id = OLD.ordem_servico_id;
    IF v_status IN ('CONCLUIDA','CANCELADA') THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'OS finalizada nao aceita alteracao de itens.';
    END IF;
END$$

-- Recalcula o subtotal da OS a partir dos itens (o total sai do trg_os_bu)
CREATE TRIGGER trg_itens_ai AFTER INSERT ON itens_os
FOR EACH ROW
BEGIN
    UPDATE ordens_servico
       SET subtotal = (SELECT COALESCE(SUM(quantidade * preco_unitario), 0)
                         FROM itens_os WHERE ordem_servico_id = NEW.ordem_servico_id)
     WHERE id = NEW.ordem_servico_id;
END$$

CREATE TRIGGER trg_itens_au AFTER UPDATE ON itens_os
FOR EACH ROW
BEGIN
    UPDATE ordens_servico
       SET subtotal = (SELECT COALESCE(SUM(quantidade * preco_unitario), 0)
                         FROM itens_os WHERE ordem_servico_id = NEW.ordem_servico_id)
     WHERE id = NEW.ordem_servico_id;
END$$

CREATE TRIGGER trg_itens_ad AFTER DELETE ON itens_os
FOR EACH ROW
BEGIN
    UPDATE ordens_servico
       SET subtotal = (SELECT COALESCE(SUM(quantidade * preco_unitario), 0)
                         FROM itens_os WHERE ordem_servico_id = OLD.ordem_servico_id)
     WHERE id = OLD.ordem_servico_id;
END$$

DELIMITER ;

-- ---------------------------------------------------------------------
-- 3. VIEWS (facilitam Módulo 6 - consultas, relatórios e painel)
-- ---------------------------------------------------------------------

CREATE OR REPLACE VIEW vw_os_detalhada AS
SELECT os.id            AS numero_os,
       os.data_abertura,
       os.data_inicio,
       os.data_conclusao,
       os.status,
       os.prioridade,
       c.id             AS cliente_id,
       c.nome           AS cliente,
       e.id             AS equipamento_id,
       CONCAT(e.tipo, ' ', COALESCE(e.marca, ''), ' ', COALESCE(e.modelo, '')) AS equipamento,
       t.id             AS tecnico_id,
       t.nome           AS tecnico,
       os.descricao_problema,
       os.diagnostico,
       os.solucao,
       os.subtotal,
       os.desconto,
       os.total
  FROM ordens_servico os
  JOIN clientes c      ON c.id = os.cliente_id
  JOIN equipamentos e  ON e.id = os.equipamento_id
  LEFT JOIN tecnicos t ON t.id = os.tecnico_id;

CREATE OR REPLACE VIEW vw_resumo_status AS
SELECT status,
       COUNT(*)   AS quantidade,
       SUM(total) AS valor_total
  FROM ordens_servico
 GROUP BY status;

-- ---------------------------------------------------------------------
-- 4. DADOS DE EXEMPLO (opcional - banco recém-criado, IDs começam em 1)
--    O usuário ADMIN deve ser criado pelo C# com hash real (ex.: BCrypt),
--    pois não dá para gerar o hash aqui no SQL.
-- ---------------------------------------------------------------------

INSERT INTO tecnicos (nome, especialidade, telefone, email) VALUES
('Carlos Souza', 'Notebooks e desktops', '11988880001', 'carlos@tech.com'),
('Marina Lima',  'Redes e impressoras',  '11988880002', 'marina@tech.com');

INSERT INTO clientes (nome, documento, telefone, email, endereco) VALUES
('João da Silva',      '11144477735',    '11977770001', 'joao@email.com',    'Rua A, 100 - SBC'),
('Maria Oliveira',     '52998224725',    '11977770002', 'maria@email.com',   'Rua B, 200 - SBC'),
('Tech Solutions LTDA','11222333000181', '1144440003',  'contato@tech.com',  'Av. C, 300 - SBC');

INSERT INTO equipamentos (cliente_id, tipo, marca, modelo, numero_serie, defeito_informado) VALUES
(1, 'Notebook',  'Dell',    'Inspiron 15', 'SN-DELL-001', 'Não liga'),
(2, 'Desktop',   'Positivo','Master',      'SN-POS-002',  'Lento e trava'),
(3, 'Impressora','HP',      'LaserJet',    'SN-HP-003',   'Papel enroscando');

INSERT INTO servicos (descricao, preco) VALUES
('Formatação e instalação do sistema', 120.00),
('Troca de memória RAM',                80.00),
('Troca de SSD/HD',                    150.00),
('Limpeza interna e troca de pasta térmica', 90.00),
('Diagnóstico técnico',                 50.00);

-- =====================================================================
-- Exemplo de uso (C#, dentro de uma transação):
--   1) INSERT em ordens_servico (status ABERTA)
--   2) INSERT em historico_os (NULL -> ABERTA)
--   3) INSERT em itens_os (subtotal/total atualizam sozinhos)
--   4) UPDATE status + INSERT em historico_os (mesma transação)
-- =====================================================================
