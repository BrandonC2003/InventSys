CREATE TABLE [dbo].[DetalleCompra]
(
	[IdDetalleCompra] INT NOT NULL IDENTITY,
	[IdCompra] INT NOT NULL,
	[IdProducto] INT NOT NULL,
	[Cantidad] INT NOT NULL,
	[PrecioUnitario] DECIMAL(18, 2) NOT NULL,	
	CONSTRAINT PK_DetalleCompra PRIMARY KEY CLUSTERED ([IdDetalleCompra] ASC),
	CONSTRAINT FK_DetalleCompra_Compras FOREIGN KEY ([IdCompra]) REFERENCES [dbo].[Compras]([IdCompra]),
	CONSTRAINT FK_DetalleCompra_Productos FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Productos]([IdProducto])
)
