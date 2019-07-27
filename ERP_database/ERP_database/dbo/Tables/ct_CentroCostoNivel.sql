CREATE TABLE [dbo].[ct_CentroCostoNivel] (
    [IdEmpresa]             INT           NOT NULL,
    [IdNivel]               INT           NOT NULL,
    [nv_NumDigitos]         INT           NOT NULL,
    [nv_Descripcion]        VARCHAR (200) NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ct_CentroCostoNivel] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdNivel] ASC)
);

