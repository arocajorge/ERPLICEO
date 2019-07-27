CREATE TABLE [web].[in_SPINV_010] (
    [IdEmpresa]      INT            NOT NULL,
    [IdUsuario]      VARCHAR (50)   NOT NULL,
    [IdAnio]         INT            NOT NULL,
    [IdProducto]     NUMERIC (18)   NOT NULL,
    [pr_descripcion] VARCHAR (3000) NOT NULL,
    [IdCategoria]    VARCHAR (25)   NOT NULL,
    [IdLinea]        INT            NOT NULL,
    [IdGrupo]        INT            NOT NULL,
    [IdSubGrupo]     INT            NOT NULL,
    [IdMarca]        INT            NOT NULL,
    [IdPresentacion] VARCHAR (25)   NOT NULL,
    [Enero]          FLOAT (53)     NOT NULL,
    [Febrero]        FLOAT (53)     NOT NULL,
    [Marzo]          FLOAT (53)     NOT NULL,
    [Abril]          FLOAT (53)     NOT NULL,
    [Mayo]           FLOAT (53)     NOT NULL,
    [Junio]          FLOAT (53)     NOT NULL,
    [Julio]          FLOAT (53)     NOT NULL,
    [Agosto]         FLOAT (53)     NOT NULL,
    [Septiembre]     FLOAT (53)     NOT NULL,
    [Octubre]        FLOAT (53)     NOT NULL,
    [Noviembre]      FLOAT (53)     NOT NULL,
    [Diciembre]      FLOAT (53)     NOT NULL,
    [Total]          FLOAT (53)     NOT NULL,
    [StockActual]    FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_in_SPINV_010] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdUsuario] ASC, [IdAnio] ASC, [IdProducto] ASC),
    CONSTRAINT [FK_in_SPINV_010_in_Marca] FOREIGN KEY ([IdEmpresa], [IdMarca]) REFERENCES [dbo].[in_Marca] ([IdEmpresa], [IdMarca]),
    CONSTRAINT [FK_in_SPINV_010_in_presentacion] FOREIGN KEY ([IdEmpresa], [IdPresentacion]) REFERENCES [dbo].[in_presentacion] ([IdEmpresa], [IdPresentacion]),
    CONSTRAINT [FK_in_SPINV_010_in_subgrupo] FOREIGN KEY ([IdEmpresa], [IdCategoria], [IdLinea], [IdGrupo], [IdSubGrupo]) REFERENCES [dbo].[in_subgrupo] ([IdEmpresa], [IdCategoria], [IdLinea], [IdGrupo], [IdSubgrupo]),
    CONSTRAINT [FK_in_SPINV_010_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);




GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20180818-221520]
    ON [web].[in_SPINV_010]([IdEmpresa] ASC, [IdUsuario] ASC, [IdAnio] ASC, [IdCategoria] ASC, [IdLinea] ASC, [IdGrupo] ASC, [IdSubGrupo] ASC, [IdMarca] ASC);

