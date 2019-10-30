﻿CREATE TABLE [dbo].[aca_rubro] (
    [IdEmpresa]             INT            NOT NULL,
    [IdRubro]               INT            NOT NULL,
    [NomRubro]              VARCHAR (1000) NOT NULL,
    [Estado]                BIT            NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200)  NULL,
    [FechaCreacion]         DATETIME       NULL,
    [IdUsuarioModificacion] VARCHAR (200)  NULL,
    [FechaModificacion]     DATETIME       NULL,
    [IdUsuarioAnulacion]    VARCHAR (200)  NULL,
    [FechaAnulacion]        DATETIME       NULL,
    [MotivoAnulacion]       VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_aca_rubro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRubro] ASC)
);

