CREATE TABLE [dbo].[aca_AnioLectivoCalificacionCualitativa] (
    [IdEmpresa]                 INT             NOT NULL,
    [IdCalificacionCualitativa] INT             NOT NULL,
    [IdAnio]                    INT             NOT NULL,
    [Codigo]                    VARCHAR (50)    NULL,
    [DescripcionCorta]          VARCHAR (50)    NULL,
    [DescripcionLarga]          VARCHAR (MAX)   NULL,
    [Calificacion]              NUMERIC (18, 2) NULL,
    [Estado]                    BIT             NOT NULL,
    [IdUsuarioCreacion]         VARCHAR (200)   NULL,
    [FechaCreacion]             DATETIME        NULL,
    [IdUsuarioModificacion]     VARCHAR (200)   NULL,
    [FechaModificacion]         DATETIME        NULL,
    [IdUsuarioAnulacion]        VARCHAR (200)   NULL,
    [FechaAnulacion]            DATETIME        NULL,
    [MotivoAnulacion]           VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_aca_AnioLectivoCalificacionCualitativa] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCalificacionCualitativa] ASC),
    CONSTRAINT [FK_aca_AnioLectivoCalificacionCualitativa_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio])
);



