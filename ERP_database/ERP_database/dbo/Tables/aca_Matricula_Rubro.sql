CREATE TABLE [dbo].[aca_Matricula_Rubro] (
    [IdEmpresa]          INT             NOT NULL,
    [IdMatricula]        NUMERIC (18)    NOT NULL,
    [IdPeriodo]          INT             NOT NULL,
    [IdRubro]            INT             NOT NULL,
    [IdAnio]             INT             NOT NULL,
    [IdPlantilla]        INT             NOT NULL,
    [IdSede]             INT             NULL,
    [IdNivel]            INT             NULL,
    [IdJornada]          INT             NULL,
    [IdCurso]            INT             NULL,
    [IdParalelo]         INT             NULL,
    [IdMecanismo]        NUMERIC (18)    NOT NULL,
    [IdProducto]         NUMERIC (18)    NOT NULL,
    [EnMatricula]        BIT             NOT NULL,
    [Subtotal]           NUMERIC (18, 2) NOT NULL,
    [IdCod_Impuesto_Iva] VARCHAR (50)    NOT NULL,
    [Porcentaje]         NUMERIC (18, 2) NOT NULL,
    [ValorIVA]           NUMERIC (18, 2) NOT NULL,
    [Total]              NUMERIC (18, 2) NOT NULL,
    [IdSucursal]         INT             NULL,
    [IdBodega]           INT             NULL,
    [IdCbteVta]          NUMERIC (18)    NULL,
    [FechaFacturacion]   DATETIME        NULL,
    CONSTRAINT [PK_aca_Matricula_Rubro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC, [IdPeriodo] ASC, [IdRubro] ASC),
    CONSTRAINT [FK_aca_Matricula_Rubro_aca_AnioLectivo_Curso_Paralelo] FOREIGN KEY ([IdEmpresa], [IdAnio], [IdSede], [IdNivel], [IdJornada], [IdCurso], [IdParalelo]) REFERENCES [dbo].[aca_AnioLectivo_Curso_Paralelo] ([IdEmpresa], [IdAnio], [IdSede], [IdNivel], [IdJornada], [IdCurso], [IdParalelo]),
    CONSTRAINT [FK_aca_Matricula_Rubro_aca_Matricula] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula]),
    CONSTRAINT [FK_aca_Matricula_Rubro_aca_MecanismoDePago] FOREIGN KEY ([IdEmpresa], [IdMecanismo]) REFERENCES [dbo].[aca_MecanismoDePago] ([IdEmpresa], [IdMecanismo]),
    CONSTRAINT [FK_aca_Matricula_Rubro_aca_Plantilla] FOREIGN KEY ([IdEmpresa], [IdAnio], [IdPlantilla]) REFERENCES [dbo].[aca_Plantilla] ([IdEmpresa], [IdAnio], [IdPlantilla]),
    CONSTRAINT [FK_aca_Matricula_Rubro_aca_Rubro] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[aca_Rubro] ([IdEmpresa], [IdRubro])
);











