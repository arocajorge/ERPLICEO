CREATE TABLE [dbo].[pre_Presupuesto] (
    [IdEmpresa]             INT           NOT NULL,
    [IdPresupuesto]         NUMERIC (18)  NOT NULL,
    [IdSucursal]            INT           NOT NULL,
    [IdPeriodo]             NUMERIC (18)  NOT NULL,
    [Observacion]           VARCHAR (MAX) NULL,
    [Estado]                BIT           NOT NULL,
    [MontoSolicitado]       FLOAT (53)    NOT NULL,
    [MontoAprobado]         FLOAT (53)    NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    [IdUsuarioAprobacion]   VARCHAR (50)  NULL,
    [FechaAprobacion]       DATETIME      NULL,
    [MotivoAprobacion]      VARCHAR (MAX) NULL,
    CONSTRAINT [PK_pre_Presupuesto_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPresupuesto] ASC),
    CONSTRAINT [FK_pre_Presupuesto_pre_PresupuestoPeriodo] FOREIGN KEY ([IdEmpresa], [IdPeriodo]) REFERENCES [dbo].[pre_PresupuestoPeriodo] ([IdEmpresa], [IdPeriodo]),
    CONSTRAINT [FK_pre_Presupuesto_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);



