CREATE TABLE [dbo].[aca_AnioLectivo] (
    [IdEmpresa]               INT           NOT NULL,
    [IdAnio]                  INT           NOT NULL,
    [Descripcion]             VARCHAR (50)  NOT NULL,
    [FechaDesde]              DATE          NOT NULL,
    [FechaHasta]              DATE          NOT NULL,
    [Estado]                  BIT           NOT NULL,
    [EnCurso]                 BIT           NOT NULL,
    [BloquearMatricula]       BIT           NOT NULL,
    [IdAnioLectivoAnterior]   INT           NULL,
    [PromedioMinimoParcial]   FLOAT (53)    NULL,
    [PromedioMinimoPromocion] FLOAT (53)    NULL,
    [IdCursoBachiller]        INT           NULL,
    [IdUsuarioCreacion]       VARCHAR (200) NULL,
    [FechaCreacion]           DATETIME      NULL,
    [IdUsuarioModificacion]   VARCHAR (200) NULL,
    [FechaModificacion]       DATETIME      NULL,
    [IdUsuarioAnulacion]      VARCHAR (200) NULL,
    [FechaAnulacion]          DATETIME      NULL,
    [MotivoAnulacion]         VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_AnioLectivo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC),
    CONSTRAINT [FK_aca_AnioLectivo_aca_Curso] FOREIGN KEY ([IdEmpresa], [IdCursoBachiller]) REFERENCES [dbo].[aca_Curso] ([IdEmpresa], [IdCurso])
);











