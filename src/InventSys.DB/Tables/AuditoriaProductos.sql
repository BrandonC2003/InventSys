CREATE TABLE [dbo].[AuditoriaProductos]
(
	[IdAuditoriaProducto] INT NOT NULL IDENTITY,
	[IdProducto] INT NOT NULL,
	[IdUsuario] INT NOT NULL, 
    [FechaAccion] DATETIME NOT NULL DEFAULT GETDATE(),
	[Accion] TINYINT NOT NULL, 
    [DatosAnteriores] XML NOT NULL, 
    [DatosNuevos] XML NOT NULL, 
    [Descripcion] NVARCHAR(MAX) NULL,
	CONSTRAINT [PK_AuditoriaProductos] PRIMARY KEY CLUSTERED ([IdAuditoriaProducto] ASC),
	CONSTRAINT [FK_AuditoriaProductos_Productos] FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Productos] ([IdProducto]),
	CONSTRAINT [FK_AuditoriaProductos_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario])

)
