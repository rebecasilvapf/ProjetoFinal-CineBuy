CREATE DATABASE DbSistemaCineBuy
GO

USE DbSistemaCineBuy
GO

--Tabela de Filmes
CREATE TABLE Filmes(
	FilmeId INT PRIMARY KEY IDENTITY(1,1),
	Titulo NVARCHAR(100) NOT NULL,
	Genero NVARCHAR(100) NOT NULL,
	ClassiEtaria NVARCHAR(15),
	Duracao INT,
	Sinopse NVARCHAR(MAX),
	ImageUrl VARCHAR(255) NULL
);

--Tabela de Hor�rios
CREATE TABLE Horarios(
	HorarioId INT PRIMARY KEY IDENTITY(1,1),
	FilmeId INT FOREIGN KEY REFERENCES Filmes(FilmeId) NOT NULL,
	DataHora DATETIME NOT NULL,
	Sala NVARCHAR(50) NOT NULL,
	Preco DECIMAL(10,2) NOT NULL
);

-- Tabela de Assentos
CREATE TABLE Assentos (
	AssentoId INT PRIMARY KEY IDENTITY(1,1),
	HorarioId INT FOREIGN KEY REFERENCES Horarios(HorarioId) NOT NULL,
	Fileira NVARCHAR(10) NOT NULL,
	Numero INT NOT NULL,
	Disponivel BIT Default 1 NOT NULL
);

--Tabela de Reservas
CREATE TABLE Reservas (
    ReservaId INT PRIMARY KEY IDENTITY(1,1),
    HorarioId INT FOREIGN KEY REFERENCES Horarios(HorarioId) NOT NULL,
    Cliente NVARCHAR(100) NOT NULL,
    DataReserva DATETIME DEFAULT GETDATE() NOT NULL,
    Confirmado BIT DEFAULT 0 NOT NULL
);

--Tabela de Detalhes de Reserva
CREATE TABLE DetalhesReserva (
	DetalheReservaId INT PRIMARY KEY IDENTITY(1,1),
    ReservaId INT FOREIGN KEY REFERENCES Reservas(ReservaId),
    AssentoId INT FOREIGN KEY REFERENCES Assentos(AssentoId),
	CONSTRAINT UC_ReservaAssento UNIQUE (ReservaId, AssentoId) --Garantir que o mesmo assento n�o seja reservado duas vezes
);

--Inserindo dados na Tabela Filmes
INSERT INTO Filmes (Titulo, Genero, ClassiEtaria, Duracao, Sinopse, ImageUrl)
VALUES
('Os Fantasmas Ainda Se Divertem: Beetlejuice Beetlejuice', 'Fantasia', '14', 98, 'Os Fantasmas Ainda se Divertem: Beetlejuice � a sequ�ncia direta do filme cl�ssico de Tim Burton, lan�ado em 1988. Na nova hist�ria, a trama retorna � casa da fam�lia Deetz em Winter River, onde tr�s gera��es da fam�lia se re�nem ap�s uma trag�dia. Lydia Deetz (Winona Ryder), agora adulta e m�e da adolescente Astrid (Jenna Ortega), encontra uma maquete misteriosa no s�t�o, que, ao ser aberta, reabre o portal para o al�m. Isso traz de volta o exc�ntrico fantasma Beetlejuice (Michael Keaton), causando novas confus�es e desafios para a fam�lia Deetz.','~/Images/OsFantasmasSeDivertem.jpg'),
('� Assim Que Acaba', 'Drama', '14', 130, '� Assim Que Acaba � a adapta��o do romance de Colleen Hoover, que segue Lily Bloom (Blake Lively), uma mulher que come�a uma nova vida em Boston ap�s superar um passado traum�tico. Ela se apaixona pelo neurocirurgi�o Ryle Kincaid (Justin Baldoni), mas seu relacionamento � complicado pelo retorno de Atlas Corrigan (Brandon Sklenar), seu primeiro amor. Lily precisa decidir seu futuro e confiar em sua pr�pria for�a.', '~/Images/EAssimQueAcaba.jpg'),
('Pets em A��o!', 'Anima��o', 'Livre', 80, 'Pets em A��o! � uma hist�ria sobre uma cachorrinha de ra�a, Gracie, e um gato adotado, Pedro, que n�o poderiam ser mais diferentes. Enquanto Gracie se considera a melhor, Pedro vive aventuras pelas ruas e no lixo. Apesar de serem da mesma fam�lia, eles vivem brigando. Durante uma viagem em fam�lia, eles se perdem no aeroporto e precisam superar suas diferen�as para encontrar o caminho de volta para casa e para a fam�lia que amam.', '~/Images/PetsEmAcao.jpg'),
('A Ca�a', 'Suspense', '18', 94, 'Em A Ca�a, uma jovem atravessa o Mar Mediterr�neo com um grupo de refugiados para escapar do seu pa�s que est� em guerra civil. Durante a travessia, o motor do barco falha, e os refugiados acabam sendo resgatados por um iate que os oferece abrigo em uma ilha. O que inicialmente parecia um milagre, logo se torna um pesadelo quando os anfitri�es mostram suas inten��es verdadeiras: ca�ar seus visitantes por esporte!','~/Images/ACaca.jpg'),
('Est�mago 2 - O Poderoso Chef', 'Com�dia Dram�tica', '18', 130, 'Quinze anos ap�s o primeiro filme, Raimundo Nonato (Jo�o Miguel) � o chef dos chefs na pris�o, encantando com seu talento culin�rio e sua saborosa l�bia tanto o diretor do pres�dio quanto o veterano l�der dos detentos (Paulo Miklos). At� que um terceiro chef�o, o mafioso italiano Dom Caroglio (Nicola Siri), chega para disputar o controle da penitenci�ria e o privil�gio de ser servido pelo carism�tico cozinheiro.', '~/Images/Estomago2.jpg'),
('Zuzubal�ndia O Filme', 'Anima��o', 'Livre', 60, 'Zuzubal�ndia � um reino encantado onde tudo � feito de comida. Ao lado desse lugar m�gico vive uma Bruxa que n�o gosta de se alimentar. Disfar�ada de Web Influencer, ela convence as abelhas a pararem de polinizar e seguirem profiss�es como youtubers, designers de sobrancelha, professoras de ioga e outras. Em pouco tempo a comida do reino come�a a acabar e o �nico jeito de salv�-lo � passar pelo ex�rcito de zumbis da Bruxa e polinizar a �ltima flor m�gica da Floresta Mam�nica. Ser� que nossa hero�na Zuzu vai dar conta do recado?', '~/Images/Zuzubalandia.jpg'),
('Bernadette', 'Com�dia Dram�tica', '14', 95, 'No pal�cio Eliseu, Bernadette espera finalmente ser reconhecida ap�s trabalhar durante anos � sombra do seu marido, para que ele se tornasse presidente. Esquecida e considerada antiquada, Bernadette decide se vingar, tornando-se uma figura p�blica famosa.', '~/Images/Bernadette.jpg'),
('Deadpool & Wolverine', 'A��o, Com�dia', '18', 127, 'A Marvel Studios apresenta seu erro mais significativo at� agora � Deadpool & Wolverine. Um ap�tico Wade Wilson trabalha duro na vida civil. Seus dias como mercen�rio moralmente flex�vel, Deadpool, ficaram para tr�s. Quando seu planeta enfrenta uma amea�a, Wade deve relutantemente vestir-se novamente com um ainda mais relutante... relutante? Mais relutante? Ele deve convencer um Wolverine relutante em... Aff. As sinopses s�o t�o est�pidas.', '~/Images/Deadpool&Wolverine.jpg'),
('Meu Malvado Favorito 4', 'Anima��o', 'Livre', 94, 'Gru enfrenta novos inimigos. Maxime Le Mal e sua namorada mulher-fatal Valentina (Sofia Vergara, indicada ao Emmy) s�o t�o mal�volos que n�o deixam alternativa � fam�lia Gru sen�o fugir.', '~/Images/meuMalvadoFavorito4.jpg'),
('Princesa Adormecida', 'Com�dia', '10', 80, 'Princesa Adormecida � a adapta��o cinematogr�fica da cole��o �Princesas Modernas� de Paula Pimenta. Rosa, uma adolescente de 15 anos, sonha com liberdade, mas � superprotegida por seus tr�s tios. Ela descobre que sua vida tranquila era apenas um sonho e que um mist�rio do passado e uma vil� vingativa a colocam em perigo. A hist�ria segue Rosa enquanto ela enfrenta esses desafios e busca a verdade sobre sua identidade.', '~/Images/PrincesaAdormecida.jpg'),
('Baile Das Loucas', 'Drama', '14', 128, 'Paris, 1894. Todos os anos, durante o Carnaval, um grande e popular baile � realizado no hosp�cio feminino La Piti� Salp�tri�re. L�, 150 mulheres s�o selecionadas entre as 4500 pacientes para participarem do �Baile das Loucas�, onde s�o aguardadas com grande apreens�o pelos convidados. Fanni, de 35 anos, que ao contr�rio das outras mulheres que s�o internadas injustamente a for�a, se internou voluntariamente. Seu �nico objetivo � encontrar sua m�e para fugirem juntas.', '~/Images/BaileDasLoucas.jpg'),
('Divertida Mente 2', 'Anima��o', 'Livre', 85, 'Sequ�ncia de Divertidamente em que o tempo passou e Riley cresceu, agora j� tem corpo e mente de adolescente. Alegria, Raiva, Medo, Nojo e Tristeza s�o mais uma vez os protagonistas da hist�ria.', '~/Images/Divertidamente2.jpg');

SELECT * FROM Filmes

-- Definir o formato de data que o servidor deve usar para interpretar literais de data.
SET DATEFORMAT ymd

--Inserindo dados na Tabela Hor�rios
INSERT INTO Horarios (FilmeId, DataHora, Sala, Preco)
VALUES
(1, '2024-09-25 17:00:00', 'Sala 1', 20.00),
(1, '2024-09-25 19:00:00', 'Sala 2', 20.00),
(2, '2024-09-25 20:00:00', 'Sala 3', 20.00),
(2, '2024-09-25 22:30:00', 'Sala 4', 20.00),
(3, '2024-09-26 10:00:00', 'Sala 5', 15.00),
(3, '2024-09-26 12:30:00', 'Sala 1', 15.00),
(4, '2024-09-26 15:00:00', 'Sala 2', 25.00),
(4, '2024-09-26 17:30:00', 'Sala 3', 25.00),
(5, '2024-09-26 19:00:00', 'Sala 4', 30.00),
(5, '2024-09-26 21:30:00', 'Sala 5', 30.00),
(6, '2024-09-27 10:00:00', 'Sala 1', 20.00),
(6, '2024-09-27 12:30:00', 'Sala 2', 20.00),
(7, '2024-09-27 15:00:00', 'Sala 3', 25.00),
(7, '2024-09-27 17:30:00', 'Sala 4', 25.00),
(8, '2024-09-27 19:00:00', 'Sala 5', 30.00),
(8, '2024-09-27 21:30:00', 'Sala 1', 30.00),
(9, '2024-09-28 10:00:00', 'Sala 2', 15.00),
(9, '2024-09-28 12:30:00', 'Sala 3', 15.00),
(10, '2024-09-28 15:00:00', 'Sala 4', 20.00),
(10, '2024-09-28 17:30:00', 'Sala 5', 20.00),
(11, '2024-09-28 19:00:00', 'Sala 1', 25.00),
(11, '2024-09-28 21:30:00', 'Sala 2', 25.00),
(12, '2024-09-29 10:00:00', 'Sala 3', 15.00),
(12, '2024-09-29 12:30:00', 'Sala 4', 15.00);

SELECT * FROM Horarios

--Inserindo dados na Tabela Assentos
-- Tabelas tempor�rias para gerar assentos para todas as salas e hor�rios sem repetir n�meros de assento nas fileiras
DECLARE @Fileiras TABLE (Fileira NVARCHAR(1));
DECLARE @Numeros TABLE (Numero INT);

-- Inserir as fileiras e n�meros nas tabelas tempor�rias
INSERT INTO @Fileiras (Fileira) VALUES ('A'), ('B'), ('C');
INSERT INTO @Numeros (Numero) VALUES (1), (2), (3), (4), (5), (6);

-- Gerar assentos para cada hor�rio
INSERT INTO Assentos (HorarioId, Fileira, Numero, Disponivel)
SELECT h.HorarioId, f.Fileira, n.Numero, 1
FROM Horarios h
CROSS JOIN @Fileiras f --Combina cada hor�rio com todas as fileiras.
CROSS JOIN @Numeros n; -- Combina cada hor�rio e fileira com todos os n�meros de assento.

SELECT * FROM Assentos

SELECT * FROM Reservas

SELECT * FROM DetalhesReserva

ALTER TABLE DetalhesReserva
ADD CONSTRAINT UC_ReservaAssentoRestricao UNIQUE (ReservaId, AssentoId);

SELECT
    r.ReservaId,
    r.Cliente,
    r.DataReserva,
    r.Confirmado,
    h.DataHora AS DataHorarioFilme,
    h.Sala,
    h.Preco,
    a.Fileira,
    a.Numero,
    f.Titulo AS TituloFilme
FROM
    Reservas r
INNER JOIN
    Horarios h ON r.HorarioId = h.HorarioId
INNER JOIN
    DetalhesReserva dr ON r.ReservaId = dr.ReservaId
INNER JOIN
    Assentos a ON dr.AssentoId = a.AssentoId
INNER JOIN
    Filmes f ON h.FilmeId = f.FilmeId
ORDER BY
    r.ReservaId;

	
