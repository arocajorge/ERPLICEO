CREATE TABLE [dbo].[seg_Menu] (
    [IdMenu]                   INT           NOT NULL,
    [IdMenuPadre]              INT           NULL,
    [DescripcionMenu]          VARCHAR (255) NOT NULL,
    [PosicionMenu]             INT           NOT NULL,
    [Habilitado]               BIT           NOT NULL,
    [Tiene_FormularioAsociado] BIT           NOT NULL,
    [nom_Formulario]           VARCHAR (255) NULL,
    [nom_Asembly]              VARCHAR (200) NULL,
    [nivel]                    INT           NULL,
    [web_nom_Area]             VARCHAR (200) NULL,
    [web_nom_Controller]       VARCHAR (200) NULL,
    [web_nom_Action]           VARCHAR (300) NULL,
    [es_web]                   BIT           NOT NULL,
    [es_desktop]               BIT           NOT NULL,
    CONSTRAINT [PK_seg_Menu] PRIMARY KEY CLUSTERED ([IdMenu] ASC)
);

