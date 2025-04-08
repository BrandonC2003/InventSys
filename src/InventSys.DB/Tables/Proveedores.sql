CREATE TABLE [dbo].[Proveedores]
(
	[IdProveedor] INT NOT NULL IDENTITY, 
    [NombreProveedor] NVARCHAR(50) NOT NULL, 
    [CorreoElectronico] NVARCHAR(100) NULL, 
    [Telefono] NVARCHAR(20) NULL,
    CONSTRAINT PK_Proveedores PRIMARY KEY CLUSTERED ([IdProveedor] ASC)
)
