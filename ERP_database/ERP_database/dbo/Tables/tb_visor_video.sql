
		CREATE TABLE [dbo].[tb_visor_video](
			[Cod_video] [varchar](50) NOT NULL,
			[Nombre_video] [varchar](500) NOT NULL,
			[Estado] [bit] NOT NULL,
			[IdUsuario] [varchar](50) NULL,
			[FechaTransaccion] [date] NULL,
			[FechaModificacion] [date] NULL,
			[FechaAnulacion] [date] NULL,
			[IdUsuarioModifica] [varchar](50) NULL,
			[IdUsuarioAnulacion] [varchar](50) NULL,
		 CONSTRAINT [PK_Visor_video] PRIMARY KEY CLUSTERED 
		(
			[Cod_video] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		GO


