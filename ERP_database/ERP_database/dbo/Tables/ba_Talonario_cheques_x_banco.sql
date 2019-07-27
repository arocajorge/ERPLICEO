CREATE TABLE [dbo].[ba_Talonario_cheques_x_banco] (
    [IdEmpresa]                 INT           NOT NULL,
    [IdBanco]                   INT           NOT NULL,
    [Num_cheque]                VARCHAR (20)  NOT NULL,
    [secuencia]                 NUMERIC (18)  NULL,
    [Usado]                     BIT           NOT NULL,
    [Estado]                    CHAR (1)      NOT NULL,
    [IdEmpresa_cbtecble_Usado]  INT           NULL,
    [IdCbteCble_cbtecble_Usado] NUMERIC (18)  NULL,
    [IdTipoCbte_cbtecble_Usado] INT           NULL,
    [Fecha_uso]                 DATETIME      NULL,
    [FechaAnulacion]            DATETIME      NULL,
    [MotivoAnulacion]           VARCHAR (150) NULL,
    [IdUsuario_Anu]             VARCHAR (20)  NULL,
    CONSTRAINT [PK_tb_Talonario_cheques_x_banco] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdBanco] ASC, [Num_cheque] ASC),
    CONSTRAINT [FK_ba_Talonario_cheques_x_banco_ba_Banco_Cuenta] FOREIGN KEY ([IdEmpresa], [IdBanco]) REFERENCES [dbo].[ba_Banco_Cuenta] ([IdEmpresa], [IdBanco])
);

