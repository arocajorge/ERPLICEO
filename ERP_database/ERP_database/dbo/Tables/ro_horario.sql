CREATE TABLE [dbo].[ro_horario] (
    [IdEmpresa]          INT           NOT NULL,
    [IdHorario]          NUMERIC (18)  NOT NULL,
    [HoraIni]            TIME (7)      NOT NULL,
    [HoraFin]            TIME (7)      NOT NULL,
    [ToleranciaEnt]      INT           NOT NULL,
    [ToleranciaReg_lunh] INT           NOT NULL,
    [SalLunch]           TIME (7)      NOT NULL,
    [RegLunch]           TIME (7)      NOT NULL,
    [Descripcion]        VARCHAR (50)  NOT NULL,
    [Estado]             CHAR (1)      NULL,
    [IdUsuario]          VARCHAR (20)  NULL,
    [Fecha_Transac]      DATETIME      NULL,
    [IdUsuarioUltMod]    VARCHAR (20)  NULL,
    [Fecha_UltMod]       DATETIME      NULL,
    [IdUsuarioUltAnu]    VARCHAR (20)  NULL,
    [Fecha_UltAnu]       DATETIME      NULL,
    [nom_pc]             VARCHAR (50)  NULL,
    [ip]                 VARCHAR (25)  NULL,
    [MotiAnula]          VARCHAR (200) NULL,
    CONSTRAINT [PK_ro_turno] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdHorario] ASC),
    CONSTRAINT [FK_ro_horario_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);

