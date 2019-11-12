CREATE TABLE [dbo].[aca_AnioLectivo_Rubro_Periodo] (
    [IdEmpresa]        INT  NOT NULL,
    [IdAnio]           INT  NOT NULL,
    [IdRubro]          INT  NOT NULL,
    [IdPeriodo]        INT  NOT NULL,
    [FechaFacturacion] DATE NULL,
    [FechaPago]        DATE NULL,
    CONSTRAINT [PK_aca_AnioLectivo_Rubro_Periodo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdRubro] ASC, [IdPeriodo] ASC),
    CONSTRAINT [FK_aca_AnioLectivo_Rubro_Periodo_aca_AnioLectivo_Periodo] FOREIGN KEY ([IdEmpresa], [IdPeriodo]) REFERENCES [dbo].[aca_AnioLectivo_Periodo] ([IdEmpresa], [IdPeriodo]),
    CONSTRAINT [FK_aca_AnioLectivo_Rubro_Periodo_aca_AnioLectivo_Rubro] FOREIGN KEY ([IdEmpresa], [IdAnio], [IdRubro]) REFERENCES [dbo].[aca_AnioLectivo_Rubro] ([IdEmpresa], [IdAnio], [IdRubro])
);

