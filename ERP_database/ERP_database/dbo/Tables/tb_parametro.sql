CREATE TABLE [dbo].[tb_parametro] (
    [IdEmpresa]      INT          NOT NULL,
    [IdCod_Impuesto] VARCHAR (25) NOT NULL,
    [Porcentaje]     FLOAT (53)   NOT NULL,
    [EsMultiSucursal] BIT NOT NULL, 
    CONSTRAINT [PK_tb_parametro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC),
    CONSTRAINT [FK_tb_parametro_tb_sis_Impuesto] FOREIGN KEY ([IdCod_Impuesto]) REFERENCES [dbo].[tb_sis_Impuesto] ([IdCod_Impuesto])
);



