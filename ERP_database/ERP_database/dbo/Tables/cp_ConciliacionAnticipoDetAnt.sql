CREATE TABLE [dbo].[cp_ConciliacionAnticipoDetAnt] (
    [IdEmpresa]      INT          NOT NULL,
    [IdConciliacion] NUMERIC (18) NOT NULL,
    [Secuencia]      INT          NOT NULL,
    [IdOrdenPago]    NUMERIC (18) NOT NULL,
    [MontoAplicado]  FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_cp_ConciliacionAnticipoDetAnt] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConciliacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cp_ConciliacionAnticipoDetAnt_cp_ConciliacionAnticipo] FOREIGN KEY ([IdEmpresa], [IdConciliacion]) REFERENCES [dbo].[cp_ConciliacionAnticipo] ([IdEmpresa], [IdConciliacion]),
    CONSTRAINT [FK_cp_ConciliacionAnticipoDetAnt_cp_orden_pago] FOREIGN KEY ([IdEmpresa], [IdOrdenPago]) REFERENCES [dbo].[cp_orden_pago] ([IdEmpresa], [IdOrdenPago])
);

