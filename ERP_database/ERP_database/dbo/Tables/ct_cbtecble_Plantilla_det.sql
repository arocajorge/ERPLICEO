CREATE TABLE [dbo].[ct_cbtecble_Plantilla_det] (
    [IdEmpresa]                      INT           NOT NULL,
    [IdPlantilla]                    NUMERIC (18)  NOT NULL,
	[secuencia]                      INT           NOT NULL,
    [IdCtaCble]                      VARCHAR (20)  NOT NULL,
    [dc_Valor]                       FLOAT (53)    NOT NULL,
    [dc_Observacion]                 VARCHAR (MAX) NOT NULL,
	[IdPunto_cargo_grupo]			 INT           NULL,
    [IdPunto_cargo]					 INT           NULL,
    [IdCentroCosto]					 VARCHAR (200) NULL,
    CONSTRAINT [PK_ct_cbtecble_Plantilla_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPlantilla] ASC, [secuencia] ASC),
    CONSTRAINT [FK_ct_cbtecble_Plantilla_det_ct_cbtecble_Plantilla] FOREIGN KEY ([IdEmpresa], [IdPlantilla]) REFERENCES [dbo].[ct_cbtecble_Plantilla] ([IdEmpresa], [IdPlantilla]),
    CONSTRAINT [FK_ct_cbtecble_Plantilla_det_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
	CONSTRAINT [FK_ct_cbtecble_Plantilla_det_ct_CentroCosto] FOREIGN KEY ([IdEmpresa], [IdCentroCosto]) REFERENCES [dbo].[ct_CentroCosto] ([IdEmpresa], [IdCentroCosto]),
	CONSTRAINT [FK_ct_cbtecble_Plantilla_det_ct_punto_cargo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo]) REFERENCES [dbo].[ct_punto_cargo] ([IdEmpresa], [IdPunto_cargo]),
    CONSTRAINT [FK_ct_cbtecble_Plantilla_det_ct_punto_cargo_grupo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo_grupo]) REFERENCES [dbo].[ct_punto_cargo_grupo] ([IdEmpresa], [IdPunto_cargo_grupo])
);

