CREATE TABLE [dbo].[ro_cargaFamiliar] (
    [IdEmpresa]              INT           NOT NULL,
    [IdCargaFamiliar]        INT           NOT NULL,
    [IdEmpleado]             NUMERIC (18)  NOT NULL,
    [Cedula]                 VARCHAR (20)  NOT NULL,
    [Sexo]                   VARCHAR (25)  NOT NULL,
    [TipoFamiliar]           VARCHAR (10)  NOT NULL,
    [Nombres]                VARCHAR (200) NOT NULL,
    [FechaNacimiento]        DATETIME      NULL,
    [Estado]                 CHAR (1)      NOT NULL,
    [FechaDefucion]          DATETIME      NULL,
    [capacidades_especiales] BIT           NOT NULL,
    CONSTRAINT [PK_ro_CargaFamiliar] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCargaFamiliar] ASC, [IdEmpleado] ASC),
    CONSTRAINT [FK_ro_CargaFamiliar_ro_Catalogo] FOREIGN KEY ([TipoFamiliar]) REFERENCES [dbo].[ro_catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_ro_CargaFamiliar_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_CargaFamiliar_tb_Catalogo] FOREIGN KEY ([Sexo]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo])
);

