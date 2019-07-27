CREATE TABLE [dbo].[imp_gasto] (
    [IdGasto_tipo]   INT           NOT NULL,
    [gt_descripcion] VARCHAR (200) NOT NULL,
    [estado]         BIT           NOT NULL,
    [observacion]    VARCHAR (10)  NULL,
    [gt_orden]       INT           NULL,
    CONSTRAINT [PK_imp_gasto] PRIMARY KEY CLUSTERED ([IdGasto_tipo] ASC)
);



