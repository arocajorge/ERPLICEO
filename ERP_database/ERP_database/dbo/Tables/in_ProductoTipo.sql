CREATE TABLE [dbo].[in_ProductoTipo] (
    [IdEmpresa]               INT           NOT NULL,
    [IdProductoTipo]          INT           NOT NULL,
    [tp_descripcion]          VARCHAR (50)  NOT NULL,
    [tp_EsCombo]              CHAR (1)      NULL,
    [tp_ManejaInven]          CHAR (1)      NULL,
    [Estado]                  CHAR (1)      NOT NULL,
    [IdUsuario]               VARCHAR (20)  NULL,
    [Fecha_Transac]           DATETIME      NULL,
    [IdUsuarioUltMod]         VARCHAR (20)  NULL,
    [Fecha_UltMod]            DATETIME      NULL,
    [IdUsuarioUltAnu]         VARCHAR (20)  NULL,
    [Fecha_UltAnu]            DATETIME      NULL,
    [MotivoAnulacion]         VARCHAR (200) NULL,
    [Aparece_modu_Ventas]     BIT           NOT NULL,
    [Aparece_modu_Compras]    BIT           NOT NULL,
    [Aparece_modu_Inventario] BIT           NOT NULL,
    [Aparece_fabricacion]     BIT           NOT NULL,
    CONSTRAINT [PK_in_productoTipo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdProductoTipo] ASC)
);





