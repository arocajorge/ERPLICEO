CREATE TABLE [dbo].[aca_Menu] (
    [IdMenu]             INT           NOT NULL,
    [IdMenuPadre]        INT           NULL,
    [DescripcionMenu]    VARCHAR (500) NOT NULL,
    [PosicionMenu]       INT           NOT NULL,
    [Estado]             BIT           NOT NULL,
    [nivel]              INT           NULL,
    [web_nom_Area]       VARCHAR (200) NULL,
    [web_nom_Controller] VARCHAR (200) NULL,
    [web_nom_Action]     VARCHAR (300) NULL,
    CONSTRAINT [PK_aca_Menu] PRIMARY KEY CLUSTERED ([IdMenu] ASC)
);

