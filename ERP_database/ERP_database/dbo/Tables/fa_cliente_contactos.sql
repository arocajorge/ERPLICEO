CREATE TABLE [dbo].[fa_cliente_contactos] (
    [IdEmpresa]   INT            NOT NULL,
    [IdCliente]   NUMERIC (18)   NOT NULL,
    [IdContacto]  INT            NOT NULL,
    [Nombres]     VARCHAR (1000) NOT NULL,
    [Telefono]    VARCHAR (200)  NULL,
    [Celular]     VARCHAR (200)  NULL,
    [Correo]      VARCHAR (1000) NULL,
    [Direccion]   VARCHAR (1000) NULL,
    [IdCiudad]    VARCHAR (25)   NOT NULL,
    [IdParroquia] VARCHAR (25)   NOT NULL,
    CONSTRAINT [PK_fa_cliente_contactos] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCliente] ASC, [IdContacto] ASC),
    CONSTRAINT [FK_fa_cliente_contactos_fa_cliente] FOREIGN KEY ([IdEmpresa], [IdCliente]) REFERENCES [dbo].[fa_cliente] ([IdEmpresa], [IdCliente]),
    CONSTRAINT [FK_fa_cliente_contactos_tb_ciudad] FOREIGN KEY ([IdCiudad]) REFERENCES [dbo].[tb_ciudad] ([IdCiudad]),
    CONSTRAINT [FK_fa_cliente_contactos_tb_parroquia] FOREIGN KEY ([IdParroquia]) REFERENCES [dbo].[tb_parroquia] ([IdParroquia])
);

