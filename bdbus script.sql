


USE [master];
GO


DROP DATABASE IF EXISTS [SistemaTransporteDB];
GO

CREATE DATABASE [SistemaTransporteDB];
GO

USE [SistemaTransporteDB];
GO



CREATE TABLE [dbo].[Usuarios] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Username] VARCHAR(50) NOT NULL,
    [Password] VARCHAR(100) NOT NULL,
    [NombreCompleto] VARCHAR(100) NOT NULL,
    [Rol] VARCHAR(20) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);
GO

CREATE TABLE [dbo].[Rutas] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Origen] VARCHAR(50) NOT NULL,
    [Destino] VARCHAR(50) NOT NULL,
    [Distancia] VARCHAR(20) NOT NULL,
    [Duracion] VARCHAR(20) NOT NULL,
    [Estado] VARCHAR(20) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Buses] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Placa] VARCHAR(20) NOT NULL,
    [Modelo] VARCHAR(50) NOT NULL,
    [Capacidad] INT NOT NULL,
    [Estado] VARCHAR(20) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Placa] ASC)
);
GO

CREATE TABLE [dbo].[Conductores] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Nombre] VARCHAR(100) NOT NULL,
    [Cedula] VARCHAR(20) NOT NULL,
    [Licencia] VARCHAR(20) NOT NULL,
    [Telefono] VARCHAR(20) NOT NULL,
    [Estado] VARCHAR(20) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Cedula] ASC)
);
GO

CREATE TABLE [dbo].[Viajes] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [RutaId] INT NULL,
    [BusId] INT NULL,
    [ConductorId] INT NULL,
    [Fecha] DATE NOT NULL,
    [HoraSalida] TIME(7) NOT NULL,
    [HoraLlegada] TIME(7) NOT NULL,
    [Precio] DECIMAL(18, 2) NOT NULL,
    [AsientosDisponibles] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Boletos] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [ViajeId] INT NULL,
    [NombrePasajero] VARCHAR(100) NOT NULL,
    [CedulaPasajero] VARCHAR(20) NOT NULL,
    [TelefonoPasajero] VARCHAR(100) NOT NULL,
    [EmailPasajero] VARCHAR(100) NOT NULL,
    [AsientoSeleccionado] INT NOT NULL,
    [MetodoPago] VARCHAR(30) NOT NULL,
    [TotalPagar] DECIMAL(18, 2) NOT NULL,
    [FechaVenta] DATETIME DEFAULT (GETDATE()),
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ViajeId]) REFERENCES [dbo].[Viajes] ([Id])
);
GO




ALTER TABLE [dbo].[Boletos] ADD DEFAULT (getdate()) FOR [FechaVenta];
GO


ALTER TABLE [dbo].[Usuarios] ADD DEFAULT ('Admin') FOR [Rol];
GO


ALTER TABLE [dbo].[Viajes] WITH CHECK ADD FOREIGN KEY([BusId]) REFERENCES [dbo].[Buses] ([Id]);
GO
ALTER TABLE [dbo].[Viajes] WITH CHECK ADD FOREIGN KEY([ConductorId]) REFERENCES [dbo].[Conductores] ([Id]);
GO
ALTER TABLE [dbo].[Viajes] WITH CHECK ADD FOREIGN KEY([RutaId]) REFERENCES [dbo].[Rutas] ([Id]);
GO




INSERT INTO [dbo].[Usuarios] ([Username], [Password], [NombreCompleto], [Rol]) 
VALUES ('admin', '12345', 'Administrador Principal', 'Admin');
GO


INSERT INTO [dbo].[Rutas] ([Origen], [Destino], [Distancia], [Duracion], [Estado]) 
VALUES ('tipitapa huembes', 'mercado huembes', '600km', '7', 'Activo');
GO






SELECT [Id], [Username], [NombreCompleto], [Rol] 
FROM [dbo].[Usuarios] 
WHERE [Username] = 'admin';
GO


SELECT TOP 1 [Id], [Origen], [Destino], [Distancia], [Duracion], [Estado] 
FROM [dbo].[Rutas] 
ORDER BY [Id] DESC;
GO

USE [master];
GO
ALTER DATABASE [SistemaTransporteDB] SET READ_WRITE;
GO
