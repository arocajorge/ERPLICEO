CREATE TABLE [dbo].[cp_cuotas_x_doc] (
    [IdEmpresa]     INT           NOT NULL,
    [IdCuota]       NUMERIC (18)  NOT NULL,
    [IdEmpresa_ct]  INT           NULL,
    [IdTipoCbte]    INT           NULL,
    [IdCbteCble]    NUMERIC (18)  NULL,
    [Total_a_pagar] FLOAT (53)    NOT NULL,
    [Num_cuotas]    INT           NOT NULL,
    [Dias_plazo]    INT           NOT NULL,
    [Fecha_inicio]  DATETIME      NOT NULL,
    [Estado]        BIT           NOT NULL,
    [Observacion]   VARCHAR (500) NULL,
    CONSTRAINT [PK_cp_cuotas_x_doc] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCuota] ASC),
    CONSTRAINT [FK_cp_cuotas_x_doc_ct_cbtecble1] FOREIGN KEY ([IdEmpresa_ct], [IdTipoCbte], [IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble])
);

