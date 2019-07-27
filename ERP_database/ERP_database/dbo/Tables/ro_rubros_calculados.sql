CREATE TABLE [dbo].[ro_rubros_calculados] (
    [IdEmpresa]                    INT          NOT NULL,
    [IdRubro_dias_trabajados]      VARCHAR (50) NOT NULL,
    [IdRubro_tot_ing]              VARCHAR (50) NOT NULL,
    [IdRubro_tot_egr]              VARCHAR (50) NOT NULL,
    [IdRubro_iess_perso]           VARCHAR (50) NOT NULL,
    [IdRubro_sueldo]               VARCHAR (50) NOT NULL,
    [IdRubro_tot_pagar]            VARCHAR (50) NOT NULL,
    [IdRubro_aporte_patronal]      VARCHAR (50) NOT NULL,
    [IdRubro_fondo_reserva]        VARCHAR (50) NOT NULL,
    [IdRubro_prov_vac]             VARCHAR (50) NOT NULL,
    [IdRubro_prov_DIII]            VARCHAR (50) NOT NULL,
    [IdRubro_prov_DIV]             VARCHAR (50) NOT NULL,
    [IdRubro_prov_FR]              VARCHAR (50) NOT NULL,
    [IdRubro_DIII]                 VARCHAR (50) NOT NULL,
    [IdRubro_DIV]                  VARCHAR (50) NOT NULL,
    [IdRubro_IR]                   VARCHAR (50) NULL,
    [IdRubro_horas_matutina]       VARCHAR (50) NULL,
    [IdRubro_horas_vespertina]     VARCHAR (50) NULL,
    [IdRubro_horas_brigadas]       VARCHAR (50) NULL,
    [IdRubro_horas_adicionales]    VARCHAR (50) NULL,
    [IdRubro_horas_control_salida] VARCHAR (50) NULL,
    [IdRubro_horas_recargo]        VARCHAR (50) NULL,
    [IdRubro_bono_x_antiguedad]    VARCHAR (50) NULL,
    [IdRubro_anticipo]             VARCHAR (50) NULL,
    [IdRubro_novedad_proceso]      VARCHAR (50) NULL,
    [IdRubro_primaria_vespertina]  VARCHAR (50) NULL,
    CONSTRAINT [PK_ro_rubros_calculados] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubro_horas_recargo]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo1] FOREIGN KEY ([IdEmpresa], [IdRubro_horas_adicionales]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo10] FOREIGN KEY ([IdEmpresa], [IdRubro_prov_FR]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo11] FOREIGN KEY ([IdEmpresa], [IdRubro_prov_DIV]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo12] FOREIGN KEY ([IdEmpresa], [IdRubro_prov_DIII]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo13] FOREIGN KEY ([IdEmpresa], [IdRubro_prov_vac]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo14] FOREIGN KEY ([IdEmpresa], [IdRubro_fondo_reserva]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo15] FOREIGN KEY ([IdEmpresa], [IdRubro_aporte_patronal]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo16] FOREIGN KEY ([IdEmpresa], [IdRubro_tot_pagar]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo17] FOREIGN KEY ([IdEmpresa], [IdRubro_sueldo]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo18] FOREIGN KEY ([IdEmpresa], [IdRubro_iess_perso]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo19] FOREIGN KEY ([IdEmpresa], [IdRubro_tot_egr]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo20] FOREIGN KEY ([IdEmpresa], [IdRubro_tot_ing]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo21] FOREIGN KEY ([IdEmpresa], [IdRubro_dias_trabajados]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo6] FOREIGN KEY ([IdEmpresa], [IdRubro_horas_matutina]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo8] FOREIGN KEY ([IdEmpresa], [IdRubro_DIV]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubros_calculados_ro_rubro_tipo9] FOREIGN KEY ([IdEmpresa], [IdRubro_DIII]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro])
);













