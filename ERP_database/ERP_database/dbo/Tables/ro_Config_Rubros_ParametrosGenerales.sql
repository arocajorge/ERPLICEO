CREATE TABLE [dbo].[ro_Config_Rubros_ParametrosGenerales] (
    [IdTipoParametro] VARCHAR (10)    NOT NULL,
    [Descripcion]     VARCHAR (50)    NOT NULL,
    [IdRubro]         VARCHAR (10)    NULL,
    [IdMesPago]       INT             NULL,
    [Formula]         VARCHAR (50)    NULL,
    [Porcentaje]      FLOAT (53)      NULL,
    [Orden]           INT             NULL,
    [File]            VARBINARY (MAX) NULL,
    [FechaIni]        DATE            NULL,
    [FechaFin]        DATE            NULL,
    CONSTRAINT [PK_ro_Config_Rubros_ParametrosGenerales] PRIMARY KEY CLUSTERED ([IdTipoParametro] ASC)
);

