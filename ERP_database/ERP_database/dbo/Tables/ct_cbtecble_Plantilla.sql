CREATE TABLE [dbo].[ct_cbtecble_Plantilla] (
    [IdEmpresa]        INT           NOT NULL,
    [IdPlantilla]      NUMERIC (18)  NOT NULL,
	[IdTipoCbte]       INT           NOT NULL,
    [cb_Observacion]   VARCHAR (MAX) NOT NULL,
    [cb_Estado]        CHAR (1)      NOT NULL,
	[IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ct_cbtecble_Plantilla] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPlantilla] ASC),
    CONSTRAINT [FK_ct_cbtecble_Plantilla_ct_cbtecble_tipo] FOREIGN KEY ([IdEmpresa], [IdTipoCbte]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte])
);

