CREATE TABLE [dbo].[in_devolucion_inven_det] (
    [IdEmpresa]             INT          NOT NULL,
    [IdDev_Inven]           NUMERIC (18) NOT NULL,
    [secuencia]             INT          NOT NULL,
    [inv_IdEmpresa]         INT          NOT NULL,
    [inv_IdSucursal]        INT          NOT NULL,
    [inv_IdMovi_inven_tipo] INT          NOT NULL,
    [inv_IdNumMovi]         NUMERIC (18) NOT NULL,
    [inv_Secuencia]         INT          NOT NULL,
    [cant_devuelta]         FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_in_devolucion_inven_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdDev_Inven] ASC, [secuencia] ASC),
    CONSTRAINT [FK_in_devolucion_inven_det_in_devolucion_inven] FOREIGN KEY ([IdEmpresa], [IdDev_Inven]) REFERENCES [dbo].[in_devolucion_inven] ([IdEmpresa], [IdDev_Inven]),
    CONSTRAINT [FK_in_devolucion_inven_det_in_Ing_Egr_Inven_det] FOREIGN KEY ([inv_IdEmpresa], [inv_IdSucursal], [inv_IdMovi_inven_tipo], [inv_IdNumMovi], [inv_Secuencia]) REFERENCES [dbo].[in_Ing_Egr_Inven_det] ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi], [Secuencia])
);

