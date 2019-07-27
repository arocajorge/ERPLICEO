CREATE TABLE [dbo].[cp_pagos_sri] (
    [formas_pago_sri] VARCHAR (255) NOT NULL,
    [codigo_pago_sri] VARCHAR (25)  NOT NULL,
    CONSTRAINT [PK_cp_pagos_sri] PRIMARY KEY CLUSTERED ([codigo_pago_sri] ASC)
);

