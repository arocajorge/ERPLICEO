CREATE TABLE [dbo].[imp_partida_arancelaria] (
    [IdArancel]                NUMERIC (18)    NOT NULL,
    [CodigoPartidaArancelaria] VARCHAR (200)   NOT NULL,
    [Descripcion]              VARCHAR (MAX)   NOT NULL,
    [TarifaArancelaria]        DECIMAL (18, 2) NOT NULL,
    [Observacion]              VARCHAR (MAX)   NULL,
    [Estado]                   BIT             NOT NULL,
    CONSTRAINT [PK_imp_partida_arancelaria] PRIMARY KEY CLUSTERED ([IdArancel] ASC)
);

