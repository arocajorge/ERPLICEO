CREATE TABLE [dbo].[aca_Profesor] (
    [IdEmpresa]             INT            NOT NULL,
    [IdProfesor]            NUMERIC (18)   NOT NULL,
    [IdPersona]             NUMERIC (18)   NOT NULL,
    [Codigo]                VARCHAR (50)   NULL,
    [Estado]                BIT            NOT NULL,
    [Correo]                VARCHAR (500)  NULL,
    [Direccion]             VARCHAR (1000) NULL,
    [Telefonos]             VARCHAR (500)  NULL,
    [EsProfesor]            BIT            NOT NULL,
    [EsInspector]           BIT            NOT NULL,
    [IdUsuario]             VARCHAR (50)   NULL,
    [IdUsuarioCreacion]     VARCHAR (200)  NULL,
    [FechaCreacion]         DATETIME       NULL,
    [IdUsuarioModificacion] VARCHAR (200)  NULL,
    [FechaModificacion]     DATETIME       NULL,
    [IdUsuarioAnulacion]    VARCHAR (200)  NULL,
    [FechaAnulacion]        DATETIME       NULL,
    [MotivoAnulacion]       VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_aca_Profesor] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdProfesor] ASC),
    CONSTRAINT [FK_aca_Profesor_seg_usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[seg_usuario] ([IdUsuario]),
    CONSTRAINT [FK_aca_Profesor_tb_persona] FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[tb_persona] ([IdPersona])
);







