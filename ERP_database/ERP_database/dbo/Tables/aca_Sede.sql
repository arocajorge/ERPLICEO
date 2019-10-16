CREATE TABLE [dbo].[aca_Sede] (
    [IdEmpresa]             INT           NOT NULL,
    [IdSede]                INT           NOT NULL,
    [IdSucursal]            INT           NOT NULL,
    [NomSede]               VARCHAR (500) NOT NULL,
    [Direccion]             VARCHAR (MAX) NULL,
    [NombreRector]          VARCHAR (500) NULL,
    [NombreSecretaria]      VARCHAR (500) NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_Sede] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSede] ASC),
    CONSTRAINT [FK_aca_Sede_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);

