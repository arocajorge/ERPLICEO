CREATE TABLE [dbo].[tb_ColaImpresionDirecta] (
    [IdEmpresa]      INT            NOT NULL,
    [IdImpresion]    NUMERIC (18)   NOT NULL,
    [CodReporte]     VARCHAR (50)   NOT NULL,
    [IPUsuario]      VARCHAR (1000) NOT NULL,
    [IPImpresora]    VARCHAR (1000) NULL,
    [Parametros]     VARCHAR (1000) NULL,
    [Usuario]        VARCHAR (1000) NULL,
    [NombreEmpresa]  VARCHAR (1000) NULL,
    [FechaEnvio]     DATETIME       NOT NULL,
    [FechaImpresion] DATETIME       NULL,
    [Comentario]     VARCHAR (MAX)  NULL,
    [NumCopias]      INT            CONSTRAINT [DF_tb_ColaImpresionDirecta_NumCopias] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_tb_ColaImpresionDirecta] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdImpresion] ASC)
);





