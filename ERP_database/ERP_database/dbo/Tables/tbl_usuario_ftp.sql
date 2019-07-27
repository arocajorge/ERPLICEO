CREATE TABLE [dbo].[tbl_usuario_ftp](
	[Id] [int] NOT NULL,
	[ftp_usuario] [varchar](100) NOT NULL,
	[ftp_contrasenia] [varchar](100) NOT NULL,
	[ftp_url] [varchar](500) NOT NULL,
 CONSTRAINT [PK_tbl_usuario_ftp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]