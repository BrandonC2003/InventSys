CREATE TABLE [dbo].[Productos]
(
	[IdProducto] INT NOT NULL IDENTITY, 
    [IdCategoria] INT NOT NULL, 
    [IdProveedor] INT NOT NULL, 
    [NombreProducto] NVARCHAR(50) NOT NULL, 
    [Descripcion] NVARCHAR(MAX) NULL, 
    [PrecioCompra] DECIMAL(18, 2) NOT NULL, 
    [PrecioVenta] DECIMAL(18, 2) NOT NULL, 
    [Stok] INT NOT NULL DEFAULT 0, 
    [StokBajo] INT NOT NULL, 
    [MensajeAlerta] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT PK_Productos PRIMARY KEY CLUSTERED ([IdProducto] ASC),
    CONSTRAINT FK_Productos_Categorias FOREIGN KEY ([IdCategoria]) REFERENCES [dbo].[Categorias]([IdCategoria]),
    CONSTRAINT FK_Productos_Proveedores FOREIGN KEY ([IdProveedor]) REFERENCES [dbo].[Proveedores]([IdProveedor])

)
