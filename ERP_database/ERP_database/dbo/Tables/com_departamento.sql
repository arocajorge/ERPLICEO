CREATE TABLE [dbo].[com_departamento] (
    [IdEmpresa]        INT           NOT NULL,
    [IdDepartamento]   NUMERIC (18)  NOT NULL,
    [nom_departamento] VARCHAR (500) NOT NULL,
    [Estado]           VARCHAR (1)   NOT NULL,
    [IdUsuario]        VARCHAR (50)  NULL,
    [Fecha_Transac]    DATETIME      NULL,
    [IdUsuarioUltMod]  VARCHAR (50)  NULL,
    [Fecha_UltMod]     DATETIME      NULL,
    [IdUsuarioUltAnu]  VARCHAR (50)  NULL,
    [Fecha_UltAnu]     DATETIME      NULL,
    [MotiAnula]        VARCHAR (150) NULL,
    CONSTRAINT [PK_com_departamento] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdDepartamento] ASC)
);





