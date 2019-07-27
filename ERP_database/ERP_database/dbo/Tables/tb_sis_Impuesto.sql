CREATE TABLE [dbo].[tb_sis_Impuesto] (
    [IdCod_Impuesto]   VARCHAR (25) NOT NULL,
    [nom_impuesto]     VARCHAR (50) NOT NULL,
    [Usado_en_Ventas]  BIT          NOT NULL,
    [Usado_en_Compras] BIT          NOT NULL,
    [porcentaje]       FLOAT (53)   NOT NULL,
    [IdCodigo_SRI]     INT          NULL,
    [estado]           BIT          NOT NULL,
    [IdTipoImpuesto]   VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tb_sis_Impuesto] PRIMARY KEY CLUSTERED ([IdCod_Impuesto] ASC),
    CONSTRAINT [FK_tb_sis_Impuesto_cp_codigo_SRI] FOREIGN KEY ([IdCodigo_SRI]) REFERENCES [dbo].[cp_codigo_SRI] ([IdCodigo_SRI]),
    CONSTRAINT [FK_tb_sis_Impuesto_tb_sis_Impuesto_Tipo] FOREIGN KEY ([IdTipoImpuesto]) REFERENCES [dbo].[tb_sis_Impuesto_Tipo] ([IdTipoImpuesto])
);

