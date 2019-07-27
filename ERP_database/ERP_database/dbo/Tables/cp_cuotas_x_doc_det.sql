CREATE TABLE [dbo].[cp_cuotas_x_doc_det] (
    [IdEmpresa]        INT           NOT NULL,
    [IdCuota]          NUMERIC (18)  NOT NULL,
    [Secuencia]        INT           NOT NULL,
    [Num_cuota]        INT           NOT NULL,
    [Fecha_vcto_cuota] DATETIME      NOT NULL,
    [Valor_cuota]      FLOAT (53)    NOT NULL,
    [Observacion]      VARCHAR (500) NULL,
    [Estado]           BIT           NOT NULL,
    [IdEmpresa_op]     INT           NULL,
    [IdOrdenPago]      NUMERIC (18)  NULL,
    [Secuencia_op]     INT           NULL,
    CONSTRAINT [PK_cp_cuotas_x_doc_det_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCuota] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cp_cuotas_x_doc_det_cp_cuotas_x_doc1] FOREIGN KEY ([IdEmpresa], [IdCuota]) REFERENCES [dbo].[cp_cuotas_x_doc] ([IdEmpresa], [IdCuota]),
    CONSTRAINT [FK_cp_cuotas_x_doc_det_cp_orden_pago_det1] FOREIGN KEY ([IdEmpresa_op], [IdOrdenPago], [Secuencia_op]) REFERENCES [dbo].[cp_orden_pago_det] ([IdEmpresa], [IdOrdenPago], [Secuencia])
);

