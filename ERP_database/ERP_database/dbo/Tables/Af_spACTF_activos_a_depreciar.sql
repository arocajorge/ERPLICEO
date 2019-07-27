CREATE TABLE [dbo].[Af_spACTF_activos_a_depreciar] (
    [IdEmpresa]              INT          NOT NULL,
    [IdActivoFijo]           INT          NOT NULL,
    [IdUsuario]              VARCHAR (25) NOT NULL,
    [Af_costo_compra]        FLOAT (53)   NOT NULL,
    [Af_depreciacion_acum]   FLOAT (53)   NOT NULL,
    [Af_dias_a_depreciar]    INT          NOT NULL,
    [Af_valor_depr_diario]   FLOAT (53)   NOT NULL,
    [Af_valor_depreciacion]  FLOAT (53)   NOT NULL,
    [IdCtaCble_Activo]       VARCHAR (30) NULL,
    [IdCtaCble_Dep_Acum]     VARCHAR (30) NULL,
    [IdCtaCble_Gastos_Depre] VARCHAR (30) NULL
);

