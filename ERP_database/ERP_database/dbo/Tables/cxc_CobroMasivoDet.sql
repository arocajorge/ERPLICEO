CREATE TABLE [dbo].[cxc_CobroMasivoDet] (
    [IdEmpresa]     INT          NOT NULL,
    [IdCobroMasivo] NUMERIC (18) NOT NULL,
    [Secuencia]     INT          NOT NULL,
    [IdAlumno]      NUMERIC (18) NOT NULL,
    [Valor]         FLOAT (53)   NOT NULL,
    [Fecha]         DATE         NOT NULL,
    [IdSucursal]    INT          NOT NULL,
    [IdCobro]       NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_cxc_CobroMasivoDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCobroMasivo] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cxc_CobroMasivoDet_cxc_cobro] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdCobro]) REFERENCES [dbo].[cxc_cobro] ([IdEmpresa], [IdSucursal], [IdCobro]),
    CONSTRAINT [FK_cxc_CobroMasivoDet_cxc_CobroMasivo] FOREIGN KEY ([IdEmpresa], [IdCobroMasivo]) REFERENCES [dbo].[cxc_CobroMasivo] ([IdEmpresa], [IdCobroMasivo])
);

