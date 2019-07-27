CREATE TABLE [dbo].[ct_grupocble_Mayor] (
    [IdGrupo_Mayor]   VARCHAR (50)  NOT NULL,
    [nom_grupo_mayor] VARCHAR (150) NOT NULL,
    [orden]           INT           NOT NULL,
    CONSTRAINT [PK_ct_grupocble_Mayor] PRIMARY KEY CLUSTERED ([IdGrupo_Mayor] ASC)
);

