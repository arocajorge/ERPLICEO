CREATE TABLE [dbo].[tb_CatalogoTipo] (
    [IdTipoCatalogo]    INT           NOT NULL,
    [Codigo]            VARCHAR (10)  NOT NULL,
    [tc_Descripcion]    NVARCHAR (50) NULL,
    [IdUsuario]         VARCHAR (20)  NULL,
    [Fecha_Transaccion] DATETIME      NULL,
    [IdUsuarioUltModi]  VARCHAR (20)  NULL,
    [Fecha_UltMod]      DATETIME      NULL,
    [IdUsuarioUltAnu]   VARCHAR (20)  NULL,
    [Fecha_UltAnu]      DATETIME      NULL,
    [MotivoAnulacion]   VARCHAR (100) NULL,
    CONSTRAINT [PK_tb_CatalogoTipo] PRIMARY KEY CLUSTERED ([IdTipoCatalogo] ASC)
);

