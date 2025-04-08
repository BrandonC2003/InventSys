CREATE TABLE [dbo].[Categorias]
(
	[IdCategoria] INT NOT NULL IDENTITY, 
    [NombreCategoria] NVARCHAR(50) NOT NULL, 
    [Descripcion] NVARCHAR(MAX) NULL,
    CONSTRAINT PK_Categorias PRIMARY KEY CLUSTERED ([IdCategoria] ASC),

)
