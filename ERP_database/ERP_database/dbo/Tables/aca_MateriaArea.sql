﻿CREATE TABLE [dbo].[aca_MateriaArea] (
    [IdEmpresa]             INT           NOT NULL,
    [IdMateriaArea]         INT           NOT NULL,
    [NomMateriaArea]        VARCHAR (500) NOT NULL,
    [OrdenMateriaArea]      INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_MateriaArea] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMateriaArea] ASC)
);

