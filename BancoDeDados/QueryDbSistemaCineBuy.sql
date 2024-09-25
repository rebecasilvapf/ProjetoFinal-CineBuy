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

--Tabela de Horários
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
	CONSTRAINT UC_ReservaAssento UNIQUE (ReservaId, AssentoId) --Garantir que o mesmo assento não seja reservado duas vezes
);

--Inserindo dados na Tabela Filmes
INSERT INTO Filmes (Titulo, Genero, ClassiEtaria, Duracao, Sinopse, ImageUrl)
VALUES
('Os Fantasmas Ainda Se Divertem: Beetlejuice Beetlejuice', 'Fantasia', '14', 98, 'Os Fantasmas Ainda se Divertem: Beetlejuice é a sequência direta do filme clássico de Tim Burton, lançado em 1988. Na nova história, a trama retorna à casa da família Deetz em Winter River, onde três gerações da família se reúnem após uma tragédia. Lydia Deetz (Winona Ryder), agora adulta e mãe da adolescente Astrid (Jenna Ortega), encontra uma maquete misteriosa no sótão, que, ao ser aberta, reabre o portal para o além. Isso traz de volta o excêntrico fantasma Beetlejuice (Michael Keaton), causando novas confusões e desafios para a família Deetz.','~/Images/OsFantasmasSeDivertem.jpg'),
('É Assim Que Acaba', 'Drama', '14', 130, 'É Assim Que Acaba é a adaptação do romance de Colleen Hoover, que segue Lily Bloom (Blake Lively), uma mulher que começa uma nova vida em Boston após superar um passado traumático. Ela se apaixona pelo neurocirurgião Ryle Kincaid (Justin Baldoni), mas seu relacionamento é complicado pelo retorno de Atlas Corrigan (Brandon Sklenar), seu primeiro amor. Lily precisa decidir seu futuro e confiar em sua própria força.', '~/Images/EAssimQueAcaba.jpg'),
('Pets em Ação!', 'Animação', 'Livre', 80, 'Pets em Ação! é uma história sobre uma cachorrinha de raça, Gracie, e um gato adotado, Pedro, que não poderiam ser mais diferentes. Enquanto Gracie se considera a melhor, Pedro vive aventuras pelas ruas e no lixo. Apesar de serem da mesma família, eles vivem brigando. Durante uma viagem em família, eles se perdem no aeroporto e precisam superar suas diferenças para encontrar o caminho de volta para casa e para a família que amam.', '~/Images/PetsEmAcao.jpg'),
('A Caça', 'Suspense', '18', 94, 'Em A Caça, uma jovem atravessa o Mar Mediterrâneo com um grupo de refugiados para escapar do seu país que está em guerra civil. Durante a travessia, o motor do barco falha, e os refugiados acabam sendo resgatados por um iate que os oferece abrigo em uma ilha. O que inicialmente parecia um milagre, logo se torna um pesadelo quando os anfitriões mostram suas intenções verdadeiras: caçar seus visitantes por esporte!','~/Images/ACaca.jpg'),
('Estômago 2 - O Poderoso Chef', 'Comédia Dramática', '18', 130, 'Quinze anos após o primeiro filme, Raimundo Nonato (João Miguel) é o chef dos chefs na prisão, encantando com seu talento culinário e sua saborosa lábia tanto o diretor do presídio quanto o veterano líder dos detentos (Paulo Miklos). Até que um terceiro chefão, o mafioso italiano Dom Caroglio (Nicola Siri), chega para disputar o controle da penitenciária e o privilégio de ser servido pelo carismático cozinheiro.', '~/Images/Estomago2.jpg'),
('Zuzubalândia O Filme', 'Animação', 'Livre', 60, 'Zuzubalândia é um reino encantado onde tudo é feito de comida. Ao lado desse lugar mágico vive uma Bruxa que não gosta de se alimentar. Disfarçada de Web Influencer, ela convence as abelhas a pararem de polinizar e seguirem profissões como youtubers, designers de sobrancelha, professoras de ioga e outras. Em pouco tempo a comida do reino começa a acabar e o único jeito de salvá-lo é passar pelo exército de zumbis da Bruxa e polinizar a última flor mágica da Floresta Mamônica. Será que nossa heroína Zuzu vai dar conta do recado?', '~/Images/Zuzubalandia.jpg'),
('Bernadette', 'Comédia Dramática', '14', 95, 'No palácio Eliseu, Bernadette espera finalmente ser reconhecida após trabalhar durante anos à sombra do seu marido, para que ele se tornasse presidente. Esquecida e considerada antiquada, Bernadette decide se vingar, tornando-se uma figura pública famosa.', '~/Images/Bernadette.jpg'),
('Deadpool & Wolverine', 'Ação, Comédia', '18', 127, 'A Marvel Studios apresenta seu erro mais significativo até agora – Deadpool & Wolverine. Um apático Wade Wilson trabalha duro na vida civil. Seus dias como mercenário moralmente flexível, Deadpool, ficaram para trás. Quando seu planeta enfrenta uma ameaça, Wade deve relutantemente vestir-se novamente com um ainda mais relutante... relutante? Mais relutante? Ele deve convencer um Wolverine relutante em... Aff. As sinopses são tão estúpidas.', '~/Images/Deadpool&Wolverine.jpg'),
('Meu Malvado Favorito 4', 'Animação', 'Livre', 94, 'Gru enfrenta novos inimigos. Maxime Le Mal e sua namorada mulher-fatal Valentina (Sofia Vergara, indicada ao Emmy) são tão malévolos que não deixam alternativa à família Gru senão fugir.', '~/Images/meuMalvadoFavorito4.jpg'),
('Princesa Adormecida', 'Comédia', '10', 80, 'Princesa Adormecida é a adaptação cinematográfica da coleção “Princesas Modernas” de Paula Pimenta. Rosa, uma adolescente de 15 anos, sonha com liberdade, mas é superprotegida por seus três tios. Ela descobre que sua vida tranquila era apenas um sonho e que um mistério do passado e uma vilã vingativa a colocam em perigo. A história segue Rosa enquanto ela enfrenta esses desafios e busca a verdade sobre sua identidade.', '~/Images/PrincesaAdormecida.jpg'),
('Baile Das Loucas', 'Drama', '14', 128, 'Paris, 1894. Todos os anos, durante o Carnaval, um grande e popular baile é realizado no hospício feminino La Pitié Salpétrière. Lá, 150 mulheres são selecionadas entre as 4500 pacientes para participarem do “Baile das Loucas”, onde são aguardadas com grande apreensão pelos convidados. Fanni, de 35 anos, que ao contrário das outras mulheres que são internadas injustamente a força, se internou voluntariamente. Seu único objetivo é encontrar sua mãe para fugirem juntas.', '~/Images/BaileDasLoucas.jpg'),
('Divertida Mente 2', 'Animação', 'Livre', 85, 'Sequência de Divertidamente em que o tempo passou e Riley cresceu, agora já tem corpo e mente de adolescente. Alegria, Raiva, Medo, Nojo e Tristeza são mais uma vez os protagonistas da história.', '~/Images/Divertidamente2.jpg');

SELECT * FROM Filmes

-- Definir o formato de data que o servidor deve usar para interpretar literais de data.
SET DATEFORMAT ymd

--Inserindo dados na Tabela Horários
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
-- Tabelas temporárias para gerar assentos para todas as salas e horários sem repetir números de assento nas fileiras
DECLARE @Fileiras TABLE (Fileira NVARCHAR(1));
DECLARE @Numeros TABLE (Numero INT);

-- Inserir as fileiras e números nas tabelas temporárias
INSERT INTO @Fileiras (Fileira) VALUES ('A'), ('B'), ('C');
INSERT INTO @Numeros (Numero) VALUES (1), (2), (3), (4), (5), (6);

-- Gerar assentos para cada horário
INSERT INTO Assentos (HorarioId, Fileira, Numero, Disponivel)
SELECT h.HorarioId, f.Fileira, n.Numero, 1
FROM Horarios h
CROSS JOIN @Fileiras f --Combina cada horário com todas as fileiras.
CROSS JOIN @Numeros n; -- Combina cada horário e fileira com todos os números de assento.

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

	
