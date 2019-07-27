CREATE TABLE [dbo].[cp_Aprobacion_Orden_pago] (
    [IdEmpresa]         INT            NOT NULL,
    [IdAprobacion]      NUMERIC (18)   NOT NULL,
    [Observacion]       VARCHAR (1500) NULL,
    [Fecha_Aprobacion]  DATETIME       NULL,
    [UsuarioAprobacion] VARCHAR (20)   NULL,
    [Fecha_Transaccion] DATETIME       NULL,
    CONSTRAINT [PK_cp_Aprobacion_Orden_pago] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAprobacion] ASC)
);

