CREATE TABLE [dbo].[fa_formaPago] (
    [IdFormaPago]     VARCHAR (2)   NOT NULL,
    [nom_FormaPago]   VARCHAR (500) NOT NULL,
    [Estado]          BIT           NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    CONSTRAINT [PK_fa_factura_FormaPago] PRIMARY KEY CLUSTERED ([IdFormaPago] ASC)
);



