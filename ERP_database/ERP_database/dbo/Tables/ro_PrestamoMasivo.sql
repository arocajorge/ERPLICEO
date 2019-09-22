CREATE TABLE [dbo].[ro_PrestamoMasivo] (
    [IdEmpresa]          INT           NOT NULL,
    [IdSucursal]         INT           NOT NULL,
    [IdCarga]            NUMERIC (18)  NOT NULL,
    [Fecha_PriPago]      DATE          NOT NULL,
    [NumCuotas]          INT           NOT NULL,
    [descuento_mensual]  BIT           NOT NULL,
    [descuento_quincena] BIT           NOT NULL,
    [descuento_men_quin] BIT           NOT NULL,
    [Observacion]        VARCHAR (MAX) NULL,
    [Estado]             BIT           NOT NULL,
    [IdUsuario]          VARCHAR (20)  NULL,
    [Fecha_Transac]      DATETIME      NOT NULL,
    [IdUsuarioUltAnu]    VARCHAR (20)  NULL,
    [Fecha_UltAnu]       DATETIME      NULL,
    [MotiAnula]          VARCHAR (200) NULL,
    CONSTRAINT [PK_ro_PrestamoMasivo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdCarga] ASC)
);

