CREATE TABLE [dbo].[tb_banco_procesos_bancarios_x_empresa] (
    [IdEmpresa]               INT           NOT NULL,
    [IdProceso]               INT           NOT NULL,
    [IdProceso_bancario_tipo] VARCHAR (25)  NOT NULL,
    [IdBanco]                 INT           NOT NULL,
    [Codigo_Empresa]          VARCHAR (50)  NULL,
    [IdTipoNota]              INT           NULL,
    [Se_contabiliza]          BIT           NULL,
    [estado]                  VARCHAR (1)   NULL,
    [Fecha_Transaccion]       DATETIME      NULL,
    [IdUsuarioUltModi]        VARCHAR (20)  NULL,
    [Fecha_UltMod]            DATETIME      NULL,
    [IdUsuarioUltAnu]         VARCHAR (20)  NULL,
    [Fecha_UltAnu]            DATETIME      NULL,
    [MotivoAnulacion]         VARCHAR (100) NULL,
    [NombreProceso]           VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_tb_banco_procesos_bancarios_x_empresa] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdProceso] ASC),
    CONSTRAINT [FK_tb_banco_procesos_bancarios_x_empresa_ba_tipo_nota] FOREIGN KEY ([IdEmpresa], [IdTipoNota]) REFERENCES [dbo].[ba_tipo_nota] ([IdEmpresa], [IdTipoNota]),
    CONSTRAINT [FK_tb_banco_procesos_bancarios_x_empresa_tb_banco] FOREIGN KEY ([IdBanco]) REFERENCES [dbo].[tb_banco] ([IdBanco])
);





