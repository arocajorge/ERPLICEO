﻿CREATE TABLE [dbo].[aca_SocioEconomico] (
    [IdEmpresa]             INT           NOT NULL,
    [IdSocioEconomico]      INT           NOT NULL,
    [IdAlumno]              NUMERIC (18)  NOT NULL,
    [IdCatalogoFichaVi]     INT           NOT NULL,
    [IdCatalogoFichaTVi]    INT           NOT NULL,
    [IdCatalogoFichaAg]     INT           NOT NULL,
    [TieneElectricidad]     BIT           NOT NULL,
    [TieneHermanos]         BIT           NOT NULL,
    [SueldoPadre]           FLOAT (53)    NOT NULL,
    [SueldoMadre]           FLOAT (53)    NOT NULL,
    [OtroIngresoPadre]      FLOAT (53)    NOT NULL,
    [OtroIngresoMadre]      FLOAT (53)    NOT NULL,
    [GastoAlimentacion]     FLOAT (53)    NOT NULL,
    [GastoEducacion]        FLOAT (53)    NOT NULL,
    [GastoServicioBasico]   FLOAT (53)    NOT NULL,
    [GastoSalud]            FLOAT (53)    NOT NULL,
    [GastoArriendo]         FLOAT (53)    NOT NULL,
    [GastoPrestamo]         FLOAT (53)    NOT NULL,
    [OtroGasto]             FLOAT (53)    NOT NULL,
    [IdCatalogoFichaMot]    INT           NOT NULL,
    [IdCatalogoFichaIns]    INT           NOT NULL,
    [IdCatalogoFichaFin]    INT           NOT NULL,
    [IdCatalogoFichaVive]   INT           NOT NULL,
    [OtroMotivoIngreso]     VARCHAR (MAX) NULL,
    [OtroInformacionInst]   VARCHAR (MAX) NULL,
    [OtroFinanciamiento]    VARCHAR (MAX) NULL,
    [CantidadHermanos]      INT           NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    CONSTRAINT [PK_aca_SocioEconomico] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSocioEconomico] ASC),
    CONSTRAINT [FK_aca_SocioEconomico_aca_Alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_aca_SocioEconomico_aca_CatalogoFicha] FOREIGN KEY ([IdCatalogoFichaVi]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_SocioEconomico_aca_CatalogoFicha1] FOREIGN KEY ([IdCatalogoFichaTVi]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_SocioEconomico_aca_CatalogoFicha2] FOREIGN KEY ([IdCatalogoFichaAg]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_SocioEconomico_aca_CatalogoFicha3] FOREIGN KEY ([IdCatalogoFichaMot]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_SocioEconomico_aca_CatalogoFicha4] FOREIGN KEY ([IdCatalogoFichaIns]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_SocioEconomico_aca_CatalogoFicha5] FOREIGN KEY ([IdCatalogoFichaFin]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_SocioEconomico_aca_CatalogoFicha6] FOREIGN KEY ([IdCatalogoFichaVive]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha])
);













