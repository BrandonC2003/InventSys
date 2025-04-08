CREATE TABLE [dbo].[DetalleVenta]
(
	[IdDetalleVenta] INT NOT NULL IDENTITY,
	[IdVenta] INT NOT NULL,
	[IdProducto] INT NOT NULL,
	[Cantidad] INT NOT NULL,
	[PrecioUnitario] DECIMAL(18, 2) NOT NULL,	
	CONSTRAINT PK_DetalleVenta PRIMARY KEY CLUSTERED ([IdDetalleVenta] ASC),
	CONSTRAINT FK_DetalleVenta_Ventas FOREIGN KEY ([IdVenta]) REFERENCES [dbo].[Ventas]([IdVenta]),
	CONSTRAINT FK_DetalleVenta_Productos FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Productos]([IdProducto])
)
