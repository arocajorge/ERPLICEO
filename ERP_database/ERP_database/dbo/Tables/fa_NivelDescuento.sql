CREATE TABLE [dbo].[fa_NivelDescuento] (
    [IdEmpresa]   INT            NOT NULL,
    [IdNivel]     INT            NOT NULL,
    [Descripcion] VARCHAR (5000) NOT NULL,
    [Observacion] VARCHAR (MAX)  NULL,
    [Porcentaje]  FLOAT (53)     NOT NULL,
    [Estado]      BIT            NOT NULL,
    CONSTRAINT [PK_fa_NivelDescuento] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdNivel] ASC)
);

