CREATE TABLE [dbo].[fa_TerminoPago_Distribucion] (
    [IdTerminoPago]    VARCHAR (20) NOT NULL,
    [Secuencia]        INT          NOT NULL,
    [Num_Dias_Vcto]    INT          NOT NULL,
    [Por_distribucion] FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_fa_factura_tipo_formaPago_Distribucion] PRIMARY KEY CLUSTERED ([IdTerminoPago] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_fa_TerminoPago_Distribucion_fa_TerminoPago] FOREIGN KEY ([IdTerminoPago]) REFERENCES [dbo].[fa_TerminoPago] ([IdTerminoPago])
);

