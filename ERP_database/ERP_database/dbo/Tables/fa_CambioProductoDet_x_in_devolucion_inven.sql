CREATE TABLE [dbo].[fa_CambioProductoDet_x_in_devolucion_inven] (
    [IdEmpresa_ca]  INT          NOT NULL,
    [IdSucursal_ca] INT          NOT NULL,
    [IdBodega_ca]   INT          NOT NULL,
    [IdCambio]      NUMERIC (18) NOT NULL,
    [Secuencia_ca]  INT          NOT NULL,
    [IdEmpresa_de]  INT          NOT NULL,
    [IdDev_Inven]   NUMERIC (18) NOT NULL,
    [Observacion]   VARCHAR (2)  NULL,
    CONSTRAINT [PK_fa_CambioProductoDet_x_in_devolucion_inven] PRIMARY KEY CLUSTERED ([IdEmpresa_ca] ASC, [IdSucursal_ca] ASC, [IdBodega_ca] ASC, [IdCambio] ASC, [Secuencia_ca] ASC, [IdEmpresa_de] ASC, [IdDev_Inven] ASC),
    CONSTRAINT [FK_fa_CambioProductoDet_x_in_devolucion_inven_fa_CambioProductoDet] FOREIGN KEY ([IdEmpresa_ca], [IdSucursal_ca], [IdBodega_ca], [IdCambio], [Secuencia_ca]) REFERENCES [dbo].[fa_CambioProductoDet] ([IdEmpresa], [IdSucursal], [IdBodega], [IdCambio], [Secuencia]),
    CONSTRAINT [FK_fa_CambioProductoDet_x_in_devolucion_inven_in_devolucion_inven] FOREIGN KEY ([IdEmpresa_de], [IdDev_Inven]) REFERENCES [dbo].[in_devolucion_inven] ([IdEmpresa], [IdDev_Inven])
);

