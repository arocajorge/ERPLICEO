CREATE TABLE [dbo].[ba_Conciliacion_det] (
    [IdEmpresa]      INT             NOT NULL,
    [IdConciliacion] NUMERIC (18)    NOT NULL,
    [Secuencia]      INT             NOT NULL,
    [IdTipocbte]     INT             NOT NULL,
    [tipo_IngEgr]    CHAR (1)        NOT NULL,
    [Referencia]     VARCHAR (50)    NULL,
    [Seleccionado]   BIT             NOT NULL,
    [Observacion]    VARCHAR (MAX)   NULL,
    [Fecha]          DATE            NOT NULL,
    [Valor]          NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_ba_Conciliacion_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConciliacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ba_Conciliacion_det_ba_Conciliacion] FOREIGN KEY ([IdEmpresa], [IdConciliacion]) REFERENCES [dbo].[ba_Conciliacion] ([IdEmpresa], [IdConciliacion])
);

