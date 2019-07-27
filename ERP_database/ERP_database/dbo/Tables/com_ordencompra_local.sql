CREATE TABLE [dbo].[com_ordencompra_local] (
    [IdEmpresa]              INT            NOT NULL,
    [IdSucursal]             INT            NOT NULL,
    [IdOrdenCompra]          NUMERIC (18)   NOT NULL,
    [Tipo]                   VARCHAR (2)    NOT NULL,
    [SecuenciaTipo]          INT            NOT NULL,
    [IdProveedor]            NUMERIC (18)   NOT NULL,
    [IdTerminoPago]          INT            NOT NULL,
    [oc_plazo]               INT            NOT NULL,
    [oc_fecha]               DATETIME       NOT NULL,
    [oc_observacion]         VARCHAR (MAX)  NULL,
    [Estado]                 VARCHAR (1)    NOT NULL,
    [IdEstadoAprobacion_cat] VARCHAR (25)   NOT NULL,
    [IdDepartamento]         NUMERIC (18)   NOT NULL,
    [oc_fechaVencimiento]    DATETIME       NOT NULL,
    [IdEstado_cierre]        VARCHAR (25)   NULL,
    [IdComprador]            NUMERIC (18)   NOT NULL,
    [IdUsuarioAprobacion]    VARCHAR (20)   NULL,
    [MotivoAprobacion]       VARCHAR (1000) NULL,
    [FechaAprobacion]        DATETIME       NULL,
    [IdUsuario]              VARCHAR (25)   NULL,
    [Fecha_Transac]          DATETIME       NULL,
    [IdUsuarioUltMod]        VARCHAR (50)   NULL,
    [Fecha_UltMod]           DATETIME       NULL,
    [IdUsuarioUltAnu]        VARCHAR (50)   NULL,
    [Fecha_UltAnu]           DATETIME       NULL,
    [MotivoAnulacion]        VARCHAR (1000) NULL,
    CONSTRAINT [PK_in_ordencompra_local] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdOrdenCompra] ASC),
    CONSTRAINT [FK_com_ordencompra_local_com_catalogo] FOREIGN KEY ([IdEstadoAprobacion_cat]) REFERENCES [dbo].[com_catalogo] ([IdCatalogocompra]),
    CONSTRAINT [FK_com_ordencompra_local_com_comprador] FOREIGN KEY ([IdEmpresa], [IdComprador]) REFERENCES [dbo].[com_comprador] ([IdEmpresa], [IdComprador]),
    CONSTRAINT [FK_com_ordencompra_local_com_departamento] FOREIGN KEY ([IdEmpresa], [IdDepartamento]) REFERENCES [dbo].[com_departamento] ([IdEmpresa], [IdDepartamento]),
    CONSTRAINT [FK_com_ordencompra_local_com_estado_cierre] FOREIGN KEY ([IdEstado_cierre]) REFERENCES [dbo].[com_estado_cierre] ([IdEstado_cierre]),
    CONSTRAINT [FK_com_ordencompra_local_com_TerminoPago1] FOREIGN KEY ([IdEmpresa], [IdTerminoPago]) REFERENCES [dbo].[com_TerminoPago] ([IdEmpresa], [IdTerminoPago]),
    CONSTRAINT [FK_com_ordencompra_local_cp_proveedor] FOREIGN KEY ([IdEmpresa], [IdProveedor]) REFERENCES [dbo].[cp_proveedor] ([IdEmpresa], [IdProveedor]),
    CONSTRAINT [FK_com_ordencompra_local_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);







