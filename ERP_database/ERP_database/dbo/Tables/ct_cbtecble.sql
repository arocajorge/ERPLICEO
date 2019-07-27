CREATE TABLE [dbo].[ct_cbtecble] (
    [IdEmpresa]        INT           NOT NULL,
    [IdTipoCbte]       INT           NOT NULL,
    [IdCbteCble]       NUMERIC (18)  NOT NULL,
    [IdSucursal]       INT           NOT NULL,
    [CodCbteCble]      VARCHAR (20)  NULL,
    [IdPeriodo]        INT           NOT NULL,
    [cb_Fecha]         DATETIME      NOT NULL,
    [cb_Valor]         FLOAT (53)    NOT NULL,
    [cb_Observacion]   VARCHAR (MAX) NULL,
    [cb_Estado]        CHAR (1)      NOT NULL,
    [IdUsuario]        VARCHAR (20)  NULL,
    [IdUsuarioAnu]     VARCHAR (20)  NULL,
    [cb_MotivoAnu]     VARCHAR (100) NULL,
    [IdUsuarioUltModi] VARCHAR (20)  NULL,
    [cb_FechaAnu]      DATETIME      NULL,
    [cb_FechaTransac]  DATETIME      NULL,
    [cb_FechaUltModi]  DATETIME      NULL,
    CONSTRAINT [PK_ct_cbtecble] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipoCbte] ASC, [IdCbteCble] ASC),
    CONSTRAINT [FK_ct_cbtecble_ct_cbtecble_tipo] FOREIGN KEY ([IdEmpresa], [IdTipoCbte]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_ct_cbtecble_ct_periodo] FOREIGN KEY ([IdEmpresa], [IdPeriodo]) REFERENCES [dbo].[ct_periodo] ([IdEmpresa], [IdPeriodo]),
    CONSTRAINT [FK_ct_cbtecble_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);




GO
CREATE NONCLUSTERED INDEX [IX_ct_cbtecble]
    ON [dbo].[ct_cbtecble]([IdEmpresa] ASC, [IdCbteCble] ASC, [IdTipoCbte] ASC);

