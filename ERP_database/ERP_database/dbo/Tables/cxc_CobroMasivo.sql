CREATE TABLE [dbo].[cxc_CobroMasivo] (
    [IdEmpresa]             INT           NOT NULL,
    [IdCobroMasivo]         NUMERIC (18)  NOT NULL,
    [Fecha]                 DATETIME      NOT NULL,
    [Observacion]           VARCHAR (MAX) NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_cxc_CobroMasivo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCobroMasivo] ASC)
);

