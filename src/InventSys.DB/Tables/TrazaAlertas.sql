CREATE TABLE [dbo].[TrazaAlertas]
(
	[IdTrazaAlerta] INT NOT NULL IDENTITY,
	[IdUsuario] INT NOT NULL,
	[IdProducto] INT NOT NULL,
	EstadoAlerta TINYINT NOT NULL,
	[Fecha] DATETIME NOT NULL, 
    [Contenido] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [PK_TrazaAlertas] PRIMARY KEY CLUSTERED ([IdTrazaAlerta] ASC),
	CONSTRAINT [FK_TrazaAlertas_Productos] FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Productos] ([IdProducto]),
	CONSTRAINT [FK_TrazaAlertas_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario])
)
