CREATE TABLE [dbo].[cxc_ConciliacionNotaCreditoDet] (
    [IdEmpresa]       INT          NOT NULL,
    [IdConciliacion]  NUMERIC (18) NOT NULL,
    [Secuencia]       INT          NOT NULL,
    [IdSucursal]      INT          NOT NULL,
    [IdBodega]        INT          NOT NULL,
    [IdCbteVtaNota]   NUMERIC (18) NOT NULL,
    [vt_TipoDoc]      VARCHAR (10) NOT NULL,
    [Valor]           FLOAT (53)   NOT NULL,
    [ValorProntoPago] FLOAT (53)   NOT NULL,
    [secuencia_nt]    INT          NULL,
    CONSTRAINT [PK_cxc_ConiliacionNotaCreditoDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConciliacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cxc_ConciliacionNotaCreditoDet_cxc_ConciliacionNotaCredito] FOREIGN KEY ([IdEmpresa], [IdConciliacion]) REFERENCES [dbo].[cxc_ConciliacionNotaCredito] ([IdEmpresa], [IdConciliacion])
);





