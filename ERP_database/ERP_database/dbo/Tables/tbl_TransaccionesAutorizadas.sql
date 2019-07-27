CREATE TABLE [web].[tbl_TransaccionesAutorizadas] (
    [IdEmpresa]        INT           NOT NULL,
    [IdTransaccion]    DECIMAL (18)  NOT NULL,
    [IdUsuarioLog]     VARCHAR (50)  NOT NULL,
    [IdUsuarioAut]     VARCHAR (50)  NOT NULL,
    [Observacion]      VARCHAR (MAX) NULL,
    [FechaTransaccion] DATETIME      NOT NULL,
    CONSTRAINT [PK_tbl_TransaccionesAutorizadas] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTransaccion] ASC)
);

