

CREATE TABLE [dbo].[ro_prestamo_detalle](
	[IdEmpresa] [int] NOT NULL,
	[IdPrestamo] [numeric](18, 0) NOT NULL,
	[NumCuota] [int] NOT NULL,
	[SaldoInicial] [float] NOT NULL,
	[TotalCuota] [float] NOT NULL,
	[Saldo] [float] NOT NULL,
	[FechaPago] [datetime] NOT NULL,
	[EstadoPago] [varchar](10) NOT NULL,
	[Estado] [bit] NOT NULL,
	[Observacion_det] [varchar](max) NOT NULL,
	[IdNominaTipoLiqui] [int] NOT NULL,
 CONSTRAINT [PK_ro_prestamo_detalle] PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC,
	[IdPrestamo] ASC,
	[NumCuota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ro_prestamo_detalle]  WITH NOCHECK ADD  CONSTRAINT [FK_ro_prestamo_detalle_ro_prestamo] FOREIGN KEY([IdEmpresa], [IdPrestamo])
REFERENCES [dbo].[ro_prestamo] ([IdEmpresa], [IdPrestamo])
GO

ALTER TABLE [dbo].[ro_prestamo_detalle] CHECK CONSTRAINT [FK_ro_prestamo_detalle_ro_prestamo]
GO


