CREATE TABLE [web].[ct_CONTA_004] (
    [IdUsuario]            VARCHAR (50)    NOT NULL,
    [IdEmpresa]            INT             NOT NULL,
    [IdCtaCble]            VARCHAR (20)    NOT NULL,
    [pc_Cuenta]            VARCHAR (1000)  NOT NULL,
    [IdCtaCblePadre]       VARCHAR (20)    NULL,
    [EsCtaUtilidad]        BIT             NOT NULL,
    [IdNivelCta]           INT             NOT NULL,
    [IdGrupoCble]          VARCHAR (5)     NOT NULL,
    [gc_GrupoCble]         VARCHAR (50)    NOT NULL,
    [gc_estado_financiero] VARCHAR (50)    NOT NULL,
    [gc_Orden]             INT             NOT NULL,
    [EsCuentaMovimiento]   BIT             NOT NULL,
    [Naturaleza]           VARCHAR (2)     NOT NULL,
    [Valor1]               NUMERIC (18, 2) NOT NULL,
    [Valor2]               NUMERIC (18, 2) NOT NULL,
    [Variacion]            NUMERIC (18, 2) NOT NULL,
    [Signo]                VARCHAR (1)     NOT NULL,
    CONSTRAINT [PK_ct_CONTA_004] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [IdEmpresa] ASC, [IdCtaCble] ASC)
);

