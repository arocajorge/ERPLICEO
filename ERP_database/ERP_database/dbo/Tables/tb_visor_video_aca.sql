CREATE TABLE [dbo].[tb_visor_video_aca] (
    [Cod_video]          VARCHAR (50)  NOT NULL,
    [Nombre_video]       VARCHAR (500) NOT NULL,
    [Estado]             BIT           NOT NULL,
    [IdUsuario]          VARCHAR (50)  NULL,
    [FechaTransaccion]   DATE          NULL,
    [FechaModificacion]  DATE          NULL,
    [FechaAnulacion]     DATE          NULL,
    [IdUsuarioModifica]  VARCHAR (50)  NULL,
    [IdUsuarioAnulacion] VARCHAR (50)  NULL,
    CONSTRAINT [PK_tb_visor_video_aca] PRIMARY KEY CLUSTERED ([Cod_video] ASC)
);

