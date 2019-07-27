CREATE TABLE [dbo].[cxc_MotivoLiquidacionTarjeta] (
    [IdEmpresa]             INT           NOT NULL,
    [IdMotivo]              NUMERIC (18)  NOT NULL,
    [Descripcion]           VARCHAR (MAX) NOT NULL,
    [ESRetenIVA]            BIT           NOT NULL,
    [ESRetenFTE]            BIT           NOT NULL,
    [Porcentaje]            FLOAT (53)    NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_cxc_MotivoLiquidacionTarjeta] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMotivo] ASC)
);

