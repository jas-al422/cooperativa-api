CREATE DATABASE Cooperativa;
GO
USE Cooperativa;
GO

CREATE TABLE Socios (
  SocioId INT IDENTITY PRIMARY KEY,
  Nombre NVARCHAR(100) NOT NULL,
  Municipio NVARCHAR(50),
  FechaIngreso DATE DEFAULT GETDATE()
);

CREATE TABLE Cuentas (
  CuentaId INT IDENTITY PRIMARY KEY,
  SocioId INT FOREIGN KEY REFERENCES Socios(SocioId),
  Tipo NVARCHAR(20),          -- 'Ahorro' o 'Aportacion'
  Saldo DECIMAL(12,2) DEFAULT 0
);

CREATE TABLE Prestamos (
  PrestamoId INT IDENTITY PRIMARY KEY,
  SocioId INT FOREIGN KEY REFERENCES Socios(SocioId),
  Monto DECIMAL(12,2),
  Tasa DECIMAL(5,2),
  Estado NVARCHAR(20)         -- 'Activo', 'Pagado', 'Moroso'
);
GO

CREATE PROCEDURE sp_PrestamosPorSocio
    @SocioId INT
AS
BEGIN
    SELECT PrestamoId, Monto, Tasa, Estado
    FROM Prestamos
    WHERE SocioId = @SocioId;
END;
GO