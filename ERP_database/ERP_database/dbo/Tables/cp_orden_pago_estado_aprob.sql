CREATE TABLE [dbo].[cp_orden_pago_estado_aprob] (
    [IdEstadoAprobacion] VARCHAR (10)  NOT NULL,
    [Descripcion]        VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_cp_orden_pago_estado_aprob] PRIMARY KEY CLUSTERED ([IdEstadoAprobacion] ASC)
);

