CREATE TABLE [dbo].[seg_usuario] (
    [IdUsuario]                   VARCHAR (50)   NOT NULL,
    [Contrasena]                  VARCHAR (MAX)  NULL,
    [estado]                      NVARCHAR (1)   NULL,
    [Fecha_Transaccion]           DATETIME       NULL,
    [IdUsuarioUltModi]            VARCHAR (20)   NULL,
    [Fecha_UltMod]                DATETIME       NULL,
    [IdUsuarioUltAnu]             VARCHAR (20)   NULL,
    [Fecha_UltAnu]                DATETIME       NULL,
    [MotivoAnulacion]             VARCHAR (100)  NULL,
    [Nombre]                      VARCHAR (50)   NULL,
    [ExigirDirectivaContrasenia]  BIT            NULL,
    [CambiarContraseniaSgtSesion] BIT            NULL,
    [es_super_admin]              BIT            NOT NULL,
    [contrasena_admin]            VARCHAR (MAX)  NULL,
    [IdMenu]                      INT            NULL,
    [IPUsuario]                   VARCHAR (1000) NULL,
    [IPImpresora]                 VARCHAR (1000) NULL,
    CONSTRAINT [PK_seg_usuario] PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    CONSTRAINT [FK_seg_usuario_seg_Menu] FOREIGN KEY ([IdMenu]) REFERENCES [dbo].[seg_Menu] ([IdMenu])
);







