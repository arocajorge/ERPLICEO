CREATE TABLE [dbo].[tb_Catalogo] (
    [CodCatalogo]       VARCHAR (25)  NOT NULL,
    [IdTipoCatalogo]    INT           NOT NULL,
    [IdCatalogo]        INT           NOT NULL,
    [ca_descripcion]    VARCHAR (250) NOT NULL,
    [ca_estado]         VARCHAR (1)   NOT NULL,
    [ca_orden]          INT           NOT NULL,
    [IdUsuario]         VARCHAR (20)  NULL,
    [Fecha_Transaccion] DATETIME      NULL,
    [IdUsuarioUltModi]  VARCHAR (20)  NULL,
    [Fecha_UltMod]      DATETIME      NULL,
    [IdUsuarioUltAnu]   VARCHAR (20)  NULL,
    [Fecha_UltAnu]      DATETIME      NULL,
    [MotivoAnulacion]   VARCHAR (100) NULL,
    CONSTRAINT [PK_tb_Catalogo] PRIMARY KEY CLUSTERED ([CodCatalogo] ASC),
    CONSTRAINT [FK_tb_Catalogo_tb_CatalogoTipo] FOREIGN KEY ([IdTipoCatalogo]) REFERENCES [dbo].[tb_CatalogoTipo] ([IdTipoCatalogo])
);

