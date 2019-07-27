CREATE TABLE [dbo].[ba_TipoFlujo_PlantillaDet] (
    [IdEmpresa]   INT          NOT NULL,
    [IdPlantilla] NUMERIC (18) NOT NULL,
    [Secuencia]   INT          NOT NULL,
    [IdTipoFlujo] NUMERIC (18) NOT NULL,
    [Porcentaje]  FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_ba_TipoFlujo_PlantillaDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPlantilla] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ba_TipoFlujo_PlantillaDet_ba_TipoFlujo] FOREIGN KEY ([IdEmpresa], [IdTipoFlujo]) REFERENCES [dbo].[ba_TipoFlujo] ([IdEmpresa], [IdTipoFlujo]),
    CONSTRAINT [FK_ba_TipoFlujo_PlantillaDet_ba_TipoFlujo_Plantilla] FOREIGN KEY ([IdEmpresa], [IdPlantilla]) REFERENCES [dbo].[ba_TipoFlujo_Plantilla] ([IdEmpresa], [IdPlantilla])
);

