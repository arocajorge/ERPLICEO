CREATE TABLE [web].[ba_spBAN_015] (
    [IdUsuario]   VARCHAR (50)  NOT NULL,
    [Reporte]     VARCHAR (50)  NOT NULL,
    [Secuencia]   NUMERIC (18)  NOT NULL,
    [Tipo]        VARCHAR (50)  NOT NULL,
    [Codigo]      VARCHAR (50)  NOT NULL,
    [Descripcion] VARCHAR (500) NOT NULL,
    [SaldoE1]     FLOAT (53)    NOT NULL,
    [SaldoE2]     FLOAT (53)    NOT NULL,
    [SaldoE3]     FLOAT (53)    NOT NULL,
    [SaldoE4]     FLOAT (53)    NOT NULL,
    [Total]       FLOAT (53)    NOT NULL,
    CONSTRAINT [PK_ba_spBAN_015] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [Reporte] ASC, [Secuencia] ASC)
);

