CREATE TABLE [dbo].[cp_SolicitudPagoDet] (
    [IdEmpresa]      INT          NOT NULL,
    [IdSolicitud]    NUMERIC (18) NOT NULL,
    [Secuencia]      INT          NOT NULL,
    [IdEmpresa_cxp]  INT          NOT NULL,
    [IdTipoCbte_cxp] INT          NOT NULL,
    [IdCbteCble_cxp] NUMERIC (18) NOT NULL,
    [TipoDocumento]  VARCHAR (20) NOT NULL,
    [ValorAPagar]    FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_cp_SolicitudPagoDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSolicitud] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cp_SolicitudPagoDet_cp_SolicitudPago] FOREIGN KEY ([IdEmpresa], [IdSolicitud]) REFERENCES [dbo].[cp_SolicitudPago] ([IdEmpresa], [IdSolicitud])
);

