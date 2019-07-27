CREATE TABLE [dbo].[cp_Conciliacion_Ing_Bodega_x_Orden_Giro] (
    [IdEmpresa]             INT           NOT NULL,
    [IdConciliacion]        NUMERIC (18)  NOT NULL,
    [Fecha_Conciliacion]    DATETIME      NOT NULL,
    [IdProveedor]           NUMERIC (18)  NOT NULL,
    [Observacion]           VARCHAR (200) NOT NULL,
    [IdEmpresa_Apro_Ing]    INT           NOT NULL,
    [IdAprobacion_Apro_Ing] NUMERIC (18)  NOT NULL,
    [IdUsuario]             VARCHAR (20)  NULL,
    [MotiAnula]             VARCHAR (100) NULL,
    [IdUsuarioUltAnu]       VARCHAR (20)  NULL,
    [Fecha_UltAnu]          DATETIME      NULL,
    [nom_pc]                VARCHAR (50)  NULL,
    [ip]                    VARCHAR (25)  NULL,
    [Estado]                VARCHAR (1)   NOT NULL,
    CONSTRAINT [PK_cp_Conciliacion_Ing_Bodega_x_Orden_Giro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConciliacion] ASC),
    CONSTRAINT [FK_cp_Conciliacion_Ing_Bodega_x_Orden_Giro_cp_Aprobacion_Ing_Bod_x_OC] FOREIGN KEY ([IdEmpresa_Apro_Ing], [IdAprobacion_Apro_Ing]) REFERENCES [dbo].[cp_Aprobacion_Ing_Bod_x_OC] ([IdEmpresa], [IdAprobacion])
);

