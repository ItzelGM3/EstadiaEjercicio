
CREATE DATABASE Ejercicio;

USE Ejercicio;

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
	Apellidos NVARCHAR (100) NOT NULL,
	Contrasena VARCHAR (100),
);

CREATE TABLE App (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    FechaEntrada DATETIME NOT NULL,
    FechaSalida DATETIME NULL
);

INSERT INTO Usuarios (Nombre, Apellidos, Contrasena)
VALUES ('Pepe', 'Perez', '1234');
INSERT INTO Usuarios (Nombre, Apellidos, Contrasena)
VALUES ('Itzel', 'Guarneros', '1234');
INSERT INTO Usuarios (Nombre, Apellidos, Contrasena)
VALUES ('admin', 'Guarneros', 'admin');
