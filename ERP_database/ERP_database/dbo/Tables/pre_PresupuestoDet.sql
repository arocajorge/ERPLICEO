CREATE TABLE [dbo].[pre_PresupuestoDet] (
    [IdEmpresa]     INT          NOT NULL,
    [IdPresupuesto] NUMERIC (18) NOT NULL,
    [Secuencia]     INT          NOT NULL,
    [IdRubro]       INT          NOT NULL,
    [IdCtaCble]     VARCHAR (20) NULL,
    [Monto]         FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_pre_PresupuestoDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPresupuesto] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_pre_Presupuesto_x_grupo_det_pre_rubro] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[pre_Rubro] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_pre_PresupuestoDet_pre_Presupuesto] FOREIGN KEY ([IdEmpresa], [IdPresupuesto]) REFERENCES [dbo].[pre_Presupuesto] ([IdEmpresa], [IdPresupuesto])
);





