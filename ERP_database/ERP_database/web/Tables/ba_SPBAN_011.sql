CREATE TABLE [web].[ba_SPBAN_011] (
    [IdEmpresa]      INT           NOT NULL,
    [IdBanco]        INT           NOT NULL,
    [IdUsuario]      VARCHAR (50)  NOT NULL,
    [IdCtaCble]      VARCHAR (200) NOT NULL,
    [Descripcion]    VARCHAR (MAX) NOT NULL,
    [Su_Descripcion] VARCHAR (MAX) NOT NULL,
    [SaldoAnterior]  FLOAT (53)    NOT NULL,
    [Ingreso]        FLOAT (53)    NOT NULL,
    [Egreso]         FLOAT (53)    NOT NULL,
    [SaldoFinal]     FLOAT (53)    NOT NULL,
    CONSTRAINT [PK_ba_SPBAN_011] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdBanco] ASC, [IdUsuario] ASC)
);

