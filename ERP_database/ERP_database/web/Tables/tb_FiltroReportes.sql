CREATE TABLE [web].[tb_FiltroReportes] (
    [IdEmpresa]  INT          NOT NULL,
    [IdUsuario]  VARCHAR (50) NOT NULL,
    [IdSucursal] INT          NOT NULL,
    [Fecha]      DATETIME     NULL,
    CONSTRAINT [PK_tb_FiltroReportes] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdUsuario] ASC, [IdSucursal] ASC)
);

