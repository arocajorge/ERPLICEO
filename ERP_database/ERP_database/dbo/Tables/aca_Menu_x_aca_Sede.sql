CREATE TABLE [dbo].[aca_Menu_x_aca_Sede] (
    [IdEmpresa]   INT         NOT NULL,
    [IdSede]      INT         NOT NULL,
    [IdMenu]      INT         NOT NULL,
    [Observacion] VARCHAR (1) NULL,
    CONSTRAINT [PK_aca_Menu_x_aca_Sede] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSede] ASC, [IdMenu] ASC),
    CONSTRAINT [FK_aca_Menu_x_aca_Sede_aca_Menu] FOREIGN KEY ([IdMenu]) REFERENCES [dbo].[aca_Menu] ([IdMenu]),
    CONSTRAINT [FK_aca_Menu_x_aca_Sede_aca_Sede] FOREIGN KEY ([IdEmpresa], [IdSede]) REFERENCES [dbo].[aca_Sede] ([IdEmpresa], [IdSede])
);

