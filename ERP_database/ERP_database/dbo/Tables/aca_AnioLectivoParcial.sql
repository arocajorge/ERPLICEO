CREATE TABLE [dbo].[aca_AnioLectivoParcial] (
    [IdEmpresa]   INT          NOT NULL,
    [IdAnio]      INT          NOT NULL,
    [IdSede]      INT          NOT NULL,
    [Parcial]     VARCHAR (10) NOT NULL,
    [FechaInicio] DATETIME     NULL,
    [FechaFin]    DATETIME     NULL,
    CONSTRAINT [PK_aca_AnioLectivoParcial] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdSede] ASC, [Parcial] ASC),
    CONSTRAINT [FK_aca_AnioLectivoParcial_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio]),
    CONSTRAINT [FK_aca_AnioLectivoParcial_aca_Sede] FOREIGN KEY ([IdEmpresa], [IdSede]) REFERENCES [dbo].[aca_Sede] ([IdEmpresa], [IdSede])
);

