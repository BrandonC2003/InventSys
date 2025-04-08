CREATE TABLE [dbo].[Usuarios]
(
    [IdUsuario] INT NOT NULL IDENTITY,
    [IdRol] INT NOT NULL, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(MAX) NOT NULL,
    [Nombre] NVARCHAR(50) NOT NULL, 
    [Apellido] NVARCHAR(50) NOT NULL, 
    [CorreoElectronico] NVARCHAR(100) NOT NULL, 
    [Estado] TINYINT NOT NULL DEFAULT 0, 
    [Activo] BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Usuarios PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    CONSTRAINT FK_Usuarios_RolCatalogo FOREIGN KEY ([IdRol]) REFERENCES [dbo].[RolCatalogo]([IdRol])
)
