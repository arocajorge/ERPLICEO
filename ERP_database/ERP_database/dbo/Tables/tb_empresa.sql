CREATE TABLE [dbo].[tb_empresa] (
    [IdEmpresa]               INT            NOT NULL,
    [codigo]                  VARCHAR (50)   NULL,
    [em_nombre]               VARCHAR (300)  NOT NULL,
    [RazonSocial]             VARCHAR (300)  NOT NULL,
    [NombreComercial]         VARCHAR (300)  NOT NULL,
    [ContribuyenteEspecial]   VARCHAR (5)    NOT NULL,
    [em_ruc]                  VARCHAR (50)   NOT NULL,
    [em_gerente]              VARCHAR (50)   NULL,
    [em_contador]             VARCHAR (150)  NULL,
    [em_rucContador]          VARCHAR (50)   NULL,
    [em_telefonos]            VARCHAR (200)  NULL,
    [em_direccion]            VARCHAR (1000) NOT NULL,
    [em_logo]                 IMAGE          NULL,
    [em_fechaInicioContable]  DATE           NOT NULL,
    [Estado]                  CHAR (1)       NOT NULL,
    [em_fechaInicioActividad] DATE           NOT NULL,
    [cod_entidad_dinardap]    VARCHAR (50)   NULL,
    [em_Email]                VARCHAR (300)  NULL,
    CONSTRAINT [PK_tb_empresa] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC)
);



