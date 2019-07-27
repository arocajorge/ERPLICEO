CREATE TABLE [dbo].[Af_Depreciacion_Det] (
    [IdEmpresa]          INT            NOT NULL,
    [IdDepreciacion]     DECIMAL (18)   NOT NULL,
    [Secuencia]          INT            NOT NULL,
    [IdActivoFijo]       INT            NOT NULL,
    [Concepto]           VARCHAR (1000) NULL,
    [Valor_Compra]       FLOAT (53)     NOT NULL,
    [Valor_Salvamento]   FLOAT (53)     NOT NULL,
    [Vida_Util]          INT            NOT NULL,
    [Porc_Depreciacion]  FLOAT (53)     NOT NULL,
    [Valor_Depreciacion] FLOAT (53)     NOT NULL,
    [Valor_Depre_Acum]   FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_Af_Depreciacion_Det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdDepreciacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_Af_Depreciacion_Det_Af_Activo_fijo] FOREIGN KEY ([IdEmpresa], [IdActivoFijo]) REFERENCES [dbo].[Af_Activo_fijo] ([IdEmpresa], [IdActivoFijo])
);

