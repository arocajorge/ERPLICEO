CREATE TABLE [dbo].[fa_cliente_tipo] (
    [IdEmpresa]               INT           NOT NULL,
    [Idtipo_cliente]          INT           NOT NULL,
    [Cod_cliente_tipo]        VARCHAR (10)  NULL,
    [Descripcion_tip_cliente] VARCHAR (500) NOT NULL,
    [IdCtaCble_CXC_Cred]      VARCHAR (20)  NULL,
    [IdUsuario]               VARCHAR (20)  NULL,
    [Fecha_Transac]           DATETIME      NULL,
    [IdUsuarioUltMod]         VARCHAR (20)  NULL,
    [Fecha_UltMod]            DATETIME      NULL,
    [IdUsuarioUltAnu]         VARCHAR (20)  NULL,
    [Fecha_UltAnu]            DATETIME      NULL,
    [MotivoAnula]             VARCHAR (100) NULL,
    [Estado]                  VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_fa_cliente_tipo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [Idtipo_cliente] ASC),
    CONSTRAINT [FK_fa_cliente_tipo_ct_plancta1] FOREIGN KEY ([IdEmpresa], [IdCtaCble_CXC_Cred]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble])
);





