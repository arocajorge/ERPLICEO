CREATE TABLE [dbo].[aca_AnioLectivoEquivalenciaPromedio] (
    [IdEmpresa]              INT             NOT NULL,
    [IdEquivalenciaPromedio] INT             NOT NULL,
    [IdAnio]                 INT             NOT NULL,
    [Descripcion]            VARCHAR (MAX)   NOT NULL,
    [Codigo]                 VARCHAR (50)    NULL,
    [ValorMinimo]            NUMERIC (18, 2) NOT NULL,
    [ValorMaximo]            NUMERIC (18, 2) NOT NULL,
    [Estado]                 BIT             NOT NULL,
    [IdUsuarioCreacion]      VARCHAR (200)   NULL,
    [FechaCreacion]          DATETIME        NULL,
    [IdUsuarioModificacion]  VARCHAR (200)   NULL,
    [FechaModificacion]      DATETIME        NULL,
    [IdUsuarioAnulacion]     VARCHAR (200)   NULL,
    [FechaAnulacion]         DATETIME        NULL,
    [MotivoAnulacion]        VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_aca_AnioLectivoEquivalenciaPromedio_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdEquivalenciaPromedio] ASC, [IdAnio] ASC),
    CONSTRAINT [FK_aca_AnioLectivoEquivalenciaPromedio_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio])
);





