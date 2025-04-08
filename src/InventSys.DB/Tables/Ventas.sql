CREATE TABLE [dbo].[Ventas]
(
	[IdVenta] INT NOT NULL IDENTITY, 
    [IdUsuario] INT NOT NULL, 
    [PrecioTotal] DECIMAL(18, 2) NOT NULL, 
    [FechaVenta] DATETIME NOT NULL,
    CONSTRAINT PK_Ventas PRIMARY KEY CLUSTERED ([IdVenta] ASC),
    CONSTRAINT FK_Ventas_Usuarios FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios]([IdUsuario])
)
