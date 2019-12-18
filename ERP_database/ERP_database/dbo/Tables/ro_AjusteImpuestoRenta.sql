CREATE TABLE [dbo].[ro_AjusteImpuestoRenta] (
    [IdEmpresa]             INT           NOT NULL,
    [IdAjuste]              NUMERIC (18)  NOT NULL,
    [IdAnio]                INT           NOT NULL,
    [Fecha]                 DATE          NOT NULL,
    [FechaCorte]            DATE          NOT NULL,
    [IdSucursal]            INT           NULL,
    [Observacion]           VARCHAR (MAX) NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ro_AjusteImpuestoRenta] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAjuste] ASC)
);

