CREATE TABLE [dbo].[ba_Conciliacion_det_IngEgr] (
    [IdEmpresa]         INT           NOT NULL,
    [IdConciliacion]    NUMERIC (18)  NOT NULL,
    [Secuencia]         INT           NOT NULL,
    [tipo_IngEgr]       CHAR (1)      NOT NULL,
    [IdCbteCble]        NUMERIC (18)  NOT NULL,
    [IdTipocbte]        INT           NOT NULL,
    [SecuenciaCbteCble] INT           NOT NULL,
    [checked]           BIT           NOT NULL,
    [Estado]            CHAR (1)      NULL,
    [IdUsuario]         VARCHAR (50)  NULL,
    [IdUsuario_Anu]     VARCHAR (50)  NULL,
    [IdUsuarioUltMod]   VARCHAR (50)  NULL,
    [Fecha_Transac]     DATETIME      NULL,
    [Fecha_UltMod]      DATETIME      NULL,
    [FechaAnulacion]    DATETIME      NULL,
    [ip]                VARCHAR (25)  NULL,
    [nom_pc]            VARCHAR (50)  NULL,
    [MotivoAnulacion]   VARCHAR (250) NULL,
    CONSTRAINT [PK_ba_ConciliacionBancaria_det_IngEgr] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConciliacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ba_Conciliacion_det_IngEgr_ba_Conciliacion] FOREIGN KEY ([IdEmpresa], [IdConciliacion]) REFERENCES [dbo].[ba_Conciliacion] ([IdEmpresa], [IdConciliacion])
);

