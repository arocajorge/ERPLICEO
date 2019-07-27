CREATE TABLE [dbo].[fa_TerminoPago] (
    [IdTerminoPago]   VARCHAR (20) NOT NULL,
    [nom_TerminoPago] VARCHAR (50) NOT NULL,
    [Num_Coutas]      INT          NOT NULL,
    [Dias_Vct]        INT          NOT NULL,
    [estado]          BIT          NOT NULL,
    CONSTRAINT [PK_fa_factura_tipo_formaPago] PRIMARY KEY CLUSTERED ([IdTerminoPago] ASC)
);

