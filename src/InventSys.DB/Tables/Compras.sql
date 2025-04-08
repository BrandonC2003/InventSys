CREATE TABLE [dbo].[Compras]
(
	[IdCompra] INT NOT NULL IDENTITY, 
    [IdUsuario] INT NOT NULL, 
    [PrecioTotal] DECIMAL(18, 2) NOT NULL, 
    [FechaCompra] DATETIME NOT NULL,
    [Descripcion] NVARCHAR(MAX) NULL, 
    CONSTRAINT PK_Compras PRIMARY KEY CLUSTERED ([IdCompra] ASC),
    CONSTRAINT FK_Compras_Usuarios FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios]([IdUsuario])
)
