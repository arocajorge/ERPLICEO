CREATE TABLE [dbo].[cp_ConciliacionAnticipoDetCXP] (
    [IdEmpresa]       INT           NOT NULL,
    [IdConciliacion]  NUMERIC (18)  NOT NULL,
    [Secuencia]       INT           NOT NULL,
    [IdOrdenPago]     NUMERIC (18)  NOT NULL,
    [IdEmpresa_cxp]   INT           NOT NULL,
    [IdTipoCbte_cxp]  INT           NOT NULL,
    [IdCbteCble_cxp]  NUMERIC (18)  NOT NULL,
    [MontoAplicado]   FLOAT (53)    NOT NULL,
    [Fecha_cxp]       DATETIME      NOT NULL,
    [Observacion_cxp] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_cp_ConciliacionAnticipoDetCXP] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConciliacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cp_ConciliacionAnticipoDetCXP_cp_ConciliacionAnticipo] FOREIGN KEY ([IdEmpresa], [IdConciliacion]) REFERENCES [dbo].[cp_ConciliacionAnticipo] ([IdEmpresa], [IdConciliacion]),
    CONSTRAINT [FK_cp_ConciliacionAnticipoDetCXP_cp_orden_pago] FOREIGN KEY ([IdEmpresa], [IdOrdenPago]) REFERENCES [dbo].[cp_orden_pago] ([IdEmpresa], [IdOrdenPago])
);



