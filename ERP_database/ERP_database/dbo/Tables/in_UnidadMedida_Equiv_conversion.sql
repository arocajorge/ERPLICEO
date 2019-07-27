CREATE TABLE [dbo].[in_UnidadMedida_Equiv_conversion] (
    [IdUnidadMedida]        VARCHAR (25)  NOT NULL,
    [IdUnidadMedida_equiva] VARCHAR (25)  NOT NULL,
    [valor_equiv]           FLOAT (53)    NOT NULL,
    [interpretacion]        VARCHAR (500) NULL,
    CONSTRAINT [PK_in_Unidad_Medida_Equiv_conversion] PRIMARY KEY CLUSTERED ([IdUnidadMedida] ASC, [IdUnidadMedida_equiva] ASC),
    CONSTRAINT [FK_in_UnidadMedida_Equiv_conversion_in_UnidadMedida] FOREIGN KEY ([IdUnidadMedida]) REFERENCES [dbo].[in_UnidadMedida] ([IdUnidadMedida]),
    CONSTRAINT [FK_in_UnidadMedida_Equiv_conversion_in_UnidadMedida1] FOREIGN KEY ([IdUnidadMedida_equiva]) REFERENCES [dbo].[in_UnidadMedida] ([IdUnidadMedida])
);

