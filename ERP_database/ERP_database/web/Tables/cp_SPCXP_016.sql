CREATE TABLE [web].[cp_SPCXP_016] (
    [IdEmpresa]         INT            NOT NULL,
    [IdSucursal]        INT            NOT NULL,
    [IdProveedor]       NUMERIC (18)   NOT NULL,
    [IdUsuario]         VARCHAR (50)   NOT NULL,
    [Su_Descripcion]    VARCHAR (500)  NOT NULL,
    [pe_CedulaRuc]      VARCHAR (20)   NOT NULL,
    [pe_nombreCompleto] VARCHAR (5000) NOT NULL,
    [SaldoInicial]      FLOAT (53)     NOT NULL,
    [Compra]            FLOAT (53)     NOT NULL,
    [Retenciones]       FLOAT (53)     NOT NULL,
    [Pagos]             FLOAT (53)     NOT NULL,
    [Saldo]             FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_SPCXP_016] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdProveedor] ASC, [IdUsuario] ASC)
);

