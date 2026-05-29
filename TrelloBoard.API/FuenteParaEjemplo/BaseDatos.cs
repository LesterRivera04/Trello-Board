namespace TrelloBoard.API.FuenteParaEjemplo
{
    public class BaseDatos
    {
        /*
         
-- Trello.API

CREATE DATABASE UserStoriesDB;
GO

USE UserStoriesDB;
GO

CREATE TABLE Usuario
(
	Id INT PRIMARY KEY IDENTITY (1,1),
	Nombre VARCHAR(25) NOT NULL,
	Apellidos VARCHAR(50) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	IdentificadorPokemon INT DEFAULT 1
);
GO

CREATE TABLE UserStory
(
    Id INT IDENTITY PRIMARY KEY,
    Titulo VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(50) NOT NULL,
    AsignadoA INT NOT NULL,
    Estado INT NOT NULL,
    Estimacion INT NOT NULL,
	CONSTRAINT FK_UserStory_Usuario FOREIGN KEY (AsignadoA) REFERENCES Usuario(Id)
);

INSERT INTO Usuario (Nombre, Apellidos, Email)
VALUES
(
	'Lester',
	'Rivera U',
	'les@email.com'
);
GO

INSERT INTO UserStory (Titulo, Descripcion, AsignadoA, Estado, Estimacion)
VALUES 
(
    'Registrar pedido en línea',
    'Como cliente quiero hacer pedidos en línea para ahorrar tiempo',
    1,
    1,
    8
 ),
(
    'Actualizar estado del pedido',
    'Como cocinero quiero cambiar el estado del pedido para notificar avance',
    2,
    2,
    5
);
GO


         */
    }
}
