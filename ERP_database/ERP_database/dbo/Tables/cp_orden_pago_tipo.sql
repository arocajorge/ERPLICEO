CREATE TABLE [dbo].[cp_orden_pago_tipo] (
    [IdTipo_op]    VARCHAR (20) NOT NULL,
    [Descripcion]  VARCHAR (50) NOT NULL,
    [Estado]       CHAR (1)     NOT NULL,
    [GeneraDiario] CHAR (1)     NOT NULL,
    CONSTRAINT [PK_cp_orden_pago_tipo] PRIMARY KEY CLUSTERED ([IdTipo_op] ASC)
);

