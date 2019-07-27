CREATE TABLE [dbo].[ct_plancta_nivel] (
    [IdEmpresa]         INT           NOT NULL,
    [IdNivelCta]        INT           NOT NULL,
    [nv_NumDigitos]     INT           NOT NULL,
    [nv_Descripcion]    VARCHAR (50)  NOT NULL,
    [Estado]            CHAR (1)      NULL,
    [IdUsuario]         VARCHAR (20)  NULL,
    [Fecha_Transaccion] DATETIME      NULL,
    [IdUsuarioUltModi]  VARCHAR (20)  NULL,
    [Fecha_UltMod]      DATETIME      NULL,
    [IdUsuarioUltAnu]   VARCHAR (20)  NULL,
    [Fecha_UltAnu]      DATETIME      NULL,
    [MotivoAnulacion]   VARCHAR (100) NULL,
    CONSTRAINT [PK_ct_nivel_cuenta] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdNivelCta] ASC)
);

