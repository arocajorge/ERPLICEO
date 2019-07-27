CREATE TABLE [web].[tb_sis_reporte] (
    [CodReporte]                        VARCHAR (50)   NOT NULL,
    [nom_reporte]                       VARCHAR (1000) NOT NULL,
    [CodModulo]                         VARCHAR (20)   NOT NULL,
    [rpt_vista]                         VARCHAR (50)   NULL,
    [rpt_usa_store_procedure]           BIT            NOT NULL,
    [rpt_store_procedure]               VARCHAR (200)  NULL,
    [rpt_clase_info]                    VARCHAR (200)  NOT NULL,
    [rpt_clase_data]                    VARCHAR (50)   NOT NULL,
    [rpt_clase_bus]                     VARCHAR (50)   NOT NULL,
    [rpt_clase_rpt]                     VARCHAR (50)   NOT NULL,
    [mvc_area]                          VARCHAR (500)  NOT NULL,
    [mvc_controlador]                   VARCHAR (500)  NOT NULL,
    [mvc_accion]                        VARCHAR (500)  NOT NULL,
    [estado]                            BIT            NOT NULL,
    [se_muestra_administrador_reportes] BIT            NOT NULL,
    [orden]                             INT            NOT NULL,
    [observacion]                       VARCHAR (2000) NULL,
    [Se_Mustra_Disenador_rpt]           BIT            NOT NULL,
    CONSTRAINT [PK_tb_sis_reporte_1] PRIMARY KEY CLUSTERED ([CodReporte] ASC),
    CONSTRAINT [FK_tb_sis_reporte_tb_modulo] FOREIGN KEY ([CodModulo]) REFERENCES [dbo].[tb_modulo] ([CodModulo])
);

