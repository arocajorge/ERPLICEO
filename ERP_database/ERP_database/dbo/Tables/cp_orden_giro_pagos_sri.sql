CREATE TABLE [dbo].[cp_orden_giro_pagos_sri] (
    [IdEmpresa]        INT           NOT NULL,
    [IdCbteCble_Ogiro] NUMERIC (18)  NOT NULL,
    [IdTipoCbte_Ogiro] INT           NOT NULL,
    [codigo_pago_sri]  VARCHAR (25)  NOT NULL,
    [formas_pago_sri]  VARCHAR (255) NULL,
    CONSTRAINT [PK_cp_orden_giro_pagos_sri] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCbteCble_Ogiro] ASC, [IdTipoCbte_Ogiro] ASC, [codigo_pago_sri] ASC),
    CONSTRAINT [FK_cp_orden_giro_pagos_sri_cp_orden_giro] FOREIGN KEY ([IdEmpresa], [IdCbteCble_Ogiro], [IdTipoCbte_Ogiro]) REFERENCES [dbo].[cp_orden_giro] ([IdEmpresa], [IdCbteCble_Ogiro], [IdTipoCbte_Ogiro]),
    CONSTRAINT [FK_cp_orden_giro_pagos_sri_cp_pagos_sri] FOREIGN KEY ([codigo_pago_sri]) REFERENCES [dbo].[cp_pagos_sri] ([codigo_pago_sri])
);

