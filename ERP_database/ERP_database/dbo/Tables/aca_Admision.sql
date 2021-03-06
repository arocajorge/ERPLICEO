﻿CREATE TABLE [dbo].[aca_Admision] (
    [IdEmpresa]                            INT           NOT NULL,
    [IdAdmision]                           NUMERIC (18)  NOT NULL,
    [IdSede]                               INT           NOT NULL,
    [IdAnio]                               INT           NOT NULL,
    [IdJornada]                            INT           NOT NULL,
    [IdNivel]                              INT           NOT NULL,
    [IdCurso]                              INT           NOT NULL,
    [Naturaleza_Aspirante]                 VARCHAR (25)  NOT NULL,
    [IdTipoDocumento_Aspirante]            VARCHAR (25)  NOT NULL,
    [CedulaRuc_Aspirante]                  VARCHAR (50)  NOT NULL,
    [Nombres_Aspirante]                    VARCHAR (100) NOT NULL,
    [Apellidos_Aspirante]                  VARCHAR (100) NOT NULL,
    [NombreCompleto_Aspirante]             VARCHAR (200) NOT NULL,
    [Direccion_Aspirante]                  VARCHAR (150) NULL,
    [Telefono_Aspirante]                   VARCHAR (50)  NULL,
    [Celular_Aspirante]                    VARCHAR (50)  NULL,
    [Correo_Aspirante]                     VARCHAR (100) NULL,
    [Sexo_Aspirante]                       VARCHAR (25)  NULL,
    [FechaNacimiento_Aspirante]            DATETIME      NULL,
    [CodCatalogoSangre_Aspirante]          VARCHAR (25)  NULL,
    [CodCatalogoCONADIS_Aspirante]         VARCHAR (25)  NULL,
    [PorcentajeDiscapacidad_Aspirante]     FLOAT (53)    NULL,
    [NumeroCarnetConadis_Aspirante]        VARCHAR (200) NULL,
    [IdGrupoEtnico_Aspirante]              INT           NULL,
    [IdReligion_Aspirante]                 INT           NULL,
    [AsisteCentroCristiano_Aspirante]      BIT           NOT NULL,
    [LugarNacimiento_Aspirante]            VARCHAR (300) NULL,
    [IdPais_Aspirante]                     VARCHAR (10)  NULL,
    [Cod_Region_Aspirante]                 VARCHAR (10)  NULL,
    [IdProvincia_Aspirante]                VARCHAR (25)  NULL,
    [IdCiudad_Aspirante]                   VARCHAR (25)  NULL,
    [IdParroquia_Aspirante]                VARCHAR (25)  NULL,
    [Sector_Aspirante]                     VARCHAR (500) NULL,
    [FechaIngreso_Aspirante]               DATE          NOT NULL,
    [IdCatalogoFichaTipoViv_Aspirante]     INT           NOT NULL,
    [IdCatalogoFichaViv_Aspirante]         INT           NOT NULL,
    [IdCatalogoFichaAgua_Aspirante]        INT           NOT NULL,
    [TieneElectricidad_Aspirante]          BIT           NOT NULL,
    [TieneHermanos_Aspirante]              BIT           NOT NULL,
    [CantidadHermanos]                     INT           NULL,
    [IdCatalogoFichaMotivo_Aspirante]      INT           NOT NULL,
    [IdCatalogoFichaInst_Aspirante]        INT           NOT NULL,
    [IdCatalogoFichaFinanc_Aspirante]      INT           NOT NULL,
    [IdCatalogoFichaVive_Aspirante]        INT           NOT NULL,
    [OtroMotivoIngreso_Aspirante]          VARCHAR (MAX) NULL,
    [OtroInformacionInst_Aspirante]        VARCHAR (MAX) NULL,
    [OtroFinanciamiento_Aspirante]         VARCHAR (MAX) NULL,
    [Dificultad_Lectura]                   BIT           NOT NULL,
    [Dificultad_Escritura]                 BIT           NOT NULL,
    [Dificultad_Matematicas]               BIT           NOT NULL,
    [AceptaTerminos]                       BIT           NOT NULL,
    [Naturaleza_Padre]                     VARCHAR (25)  NULL,
    [IdTipoDocumento_Padre]                VARCHAR (25)  NULL,
    [CedulaRuc_Padre]                      VARCHAR (50)  NULL,
    [Nombres_Padre]                        VARCHAR (100) NULL,
    [Apellidos_Padre]                      VARCHAR (100) NULL,
    [NombreCompleto_Padre]                 VARCHAR (200) NOT NULL,
    [RazonSocial_Padre]                    VARCHAR (150) NULL,
    [Direccion_Padre]                      VARCHAR (150) NULL,
    [Telefono_Padre]                       VARCHAR (50)  NULL,
    [Celular_Padre]                        VARCHAR (50)  NULL,
    [Correo_Padre]                         VARCHAR (100) NULL,
    [Sexo_Padre]                           VARCHAR (25)  NULL,
    [FechaNacimiento_Padre]                DATETIME      NULL,
    [CodCatalogoCONADIS_Padre]             VARCHAR (25)  NULL,
    [PorcentajeDiscapacidad_Padre]         FLOAT (53)    NULL,
    [NumeroCarnetConadis_Padre]            VARCHAR (200) NULL,
    [IdGrupoEtnico_Padre]                  INT           NULL,
    [IdReligion_Padre]                     INT           NULL,
    [IdEstadoCivil_Padre]                  VARCHAR (25)  NULL,
    [AsisteCentroCristiano_Padre]          BIT           NOT NULL,
    [IdPais_Padre]                         VARCHAR (10)  NULL,
    [Cod_Region_Padre]                     VARCHAR (10)  NULL,
    [IdProvincia_Padre]                    VARCHAR (25)  NULL,
    [IdCiudad_Padre]                       VARCHAR (25)  NULL,
    [IdParroquia_Padre]                    VARCHAR (25)  NULL,
    [Sector_Padre]                         VARCHAR (500) NULL,
    [IdCatalogoPAREN_Padre]                INT           NOT NULL,
    [IdCatalogoFichaInst_Padre]            INT           NULL,
    [EmpresaTrabajo_Padre]                 VARCHAR (200) NULL,
    [DireccionTrabajo_Padre]               VARCHAR (500) NULL,
    [TelefonoTrabajo_Padre]                VARCHAR (200) NULL,
    [CargoTrabajo_Padre]                   VARCHAR (200) NULL,
    [AniosServicio_Padre]                  INT           NULL,
    [IngresoMensual_Padre]                 FLOAT (53)    NULL,
    [VehiculoPropio_Padre]                 BIT           NOT NULL,
    [Marca_Padre]                          VARCHAR (50)  NULL,
    [Modelo_Padre]                         VARCHAR (50)  NULL,
    [AnioVehiculo_Padre]                   INT           NULL,
    [CasaPropia_Padre]                     BIT           NOT NULL,
    [EstaFallecido_Padre]                  BIT           NOT NULL,
    [IdProfesion_Padre]                    INT           NULL,
    [SeFactura_Padre]                      BIT           NOT NULL,
    [Idtipo_cliente_Padre]                 INT           NULL,
    [IdTipoCredito_Padre]                  VARCHAR (20)  NULL,
    [IdCiudad_Padre_Fact]                  VARCHAR (25)  NULL,
    [IdParroquia_Padre_Fact]               VARCHAR (25)  NULL,
    [Naturaleza_Madre]                     VARCHAR (25)  NULL,
    [IdTipoDocumento_Madre]                VARCHAR (25)  NULL,
    [CedulaRuc_Madre]                      VARCHAR (50)  NULL,
    [Nombres_Madre]                        VARCHAR (100) NULL,
    [Apellidos_Madre]                      VARCHAR (100) NULL,
    [NombreCompleto_Madre]                 VARCHAR (200) NOT NULL,
    [RazonSocial_Madre]                    VARCHAR (150) NULL,
    [Direccion_Madre]                      VARCHAR (150) NULL,
    [Telefono_Madre]                       VARCHAR (50)  NULL,
    [Celular_Madre]                        VARCHAR (50)  NULL,
    [Correo_Madre]                         VARCHAR (100) NULL,
    [Sexo_Madre]                           VARCHAR (25)  NULL,
    [FechaNacimiento_Madre]                DATETIME      NULL,
    [CodCatalogoCONADIS_Madre]             VARCHAR (25)  NULL,
    [PorcentajeDiscapacidad_Madre]         FLOAT (53)    NULL,
    [NumeroCarnetConadis_Madre]            VARCHAR (200) NULL,
    [IdGrupoEtnico_Madre]                  INT           NULL,
    [IdReligion_Madre]                     INT           NULL,
    [IdEstadoCivil_Madre]                  VARCHAR (25)  NULL,
    [AsisteCentroCristiano_Madre]          BIT           NOT NULL,
    [IdPais_Madre]                         VARCHAR (10)  NULL,
    [Cod_Region_Madre]                     VARCHAR (10)  NULL,
    [IdProvincia_Madre]                    VARCHAR (25)  NULL,
    [IdCiudad_Madre]                       VARCHAR (25)  NULL,
    [IdParroquia_Madre]                    VARCHAR (25)  NULL,
    [Sector_Madre]                         VARCHAR (500) NULL,
    [IdCatalogoPAREN_Madre]                INT           NOT NULL,
    [IdCatalogoFichaInst_Madre]            INT           NULL,
    [EmpresaTrabajo_Madre]                 VARCHAR (200) NULL,
    [DireccionTrabajo_Madre]               VARCHAR (500) NULL,
    [TelefonoTrabajo_Madre]                VARCHAR (200) NULL,
    [CargoTrabajo_Madre]                   VARCHAR (200) NULL,
    [AniosServicio_Madre]                  INT           NULL,
    [IngresoMensual_Madre]                 FLOAT (53)    NULL,
    [VehiculoPropio_Madre]                 BIT           NOT NULL,
    [Marca_Madre]                          VARCHAR (50)  NULL,
    [Modelo_Madre]                         VARCHAR (50)  NULL,
    [AnioVehiculo_Madre]                   INT           NULL,
    [CasaPropia_Madre]                     BIT           NOT NULL,
    [EstaFallecido_Madre]                  BIT           NOT NULL,
    [IdProfesion_Madre]                    INT           NULL,
    [SeFactura_Madre]                      BIT           NOT NULL,
    [Idtipo_cliente_Madre]                 INT           NULL,
    [IdTipoCredito_Madre]                  VARCHAR (20)  NULL,
    [IdCiudad_Madre_Fact]                  VARCHAR (25)  NULL,
    [IdParroquia_Madre_Fact]               VARCHAR (25)  NULL,
    [Naturaleza_Representante]             VARCHAR (25)  NOT NULL,
    [IdTipoDocumento_Representante]        VARCHAR (25)  NOT NULL,
    [CedulaRuc_Representante]              VARCHAR (50)  NOT NULL,
    [Nombres_Representante]                VARCHAR (100) NOT NULL,
    [Apellidos_Representante]              VARCHAR (100) NOT NULL,
    [NombreCompleto_Representante]         VARCHAR (200) NOT NULL,
    [RazonSocial_Representante]            VARCHAR (150) NULL,
    [Direccion_Representante]              VARCHAR (150) NULL,
    [Telefono_Representante]               VARCHAR (50)  NULL,
    [Celular_Representante]                VARCHAR (50)  NULL,
    [Correo_Representante]                 VARCHAR (100) NULL,
    [Sexo_Representante]                   VARCHAR (25)  NULL,
    [FechaNacimiento_Representante]        DATETIME      NULL,
    [CodCatalogoCONADIS_Representante]     VARCHAR (25)  NULL,
    [PorcentajeDiscapacidad_Representante] FLOAT (53)    NULL,
    [NumeroCarnetConadis_Representante]    VARCHAR (200) NULL,
    [IdGrupoEtnico_Representante]          INT           NULL,
    [IdReligion_Representante]             INT           NULL,
    [IdEstadoCivil_Representante]          VARCHAR (25)  NULL,
    [AsisteCentroCristiano_Representante]  BIT           NOT NULL,
    [IdPais_Representante]                 VARCHAR (10)  NULL,
    [Cod_Region_Representante]             VARCHAR (10)  NULL,
    [IdProvincia_Representante]            VARCHAR (25)  NULL,
    [IdCiudad_Representante]               VARCHAR (25)  NULL,
    [IdParroquia_Representante]            VARCHAR (25)  NULL,
    [Sector_Representante]                 VARCHAR (500) NULL,
    [IdCatalogoPAREN_Representante]        INT           NOT NULL,
    [IdCatalogoFichaInst_Representante]    INT           NULL,
    [EmpresaTrabajo_Representante]         VARCHAR (200) NULL,
    [DireccionTrabajo_Representante]       VARCHAR (500) NULL,
    [TelefonoTrabajo_Representante]        VARCHAR (200) NULL,
    [CargoTrabajo_Representante]           VARCHAR (200) NULL,
    [AniosServicio_Representante]          INT           NULL,
    [IngresoMensual_Representante]         FLOAT (53)    NULL,
    [VehiculoPropio_Representante]         BIT           NOT NULL,
    [Marca_Representante]                  VARCHAR (50)  NULL,
    [Modelo_Representante]                 VARCHAR (50)  NULL,
    [AnioVehiculo_Representante]           INT           NULL,
    [CasaPropia_Representante]             BIT           NOT NULL,
    [EstaFallecido_Representante]          BIT           NOT NULL,
    [IdProfesion_Representante]            INT           NULL,
    [SeFactura_Representante]              BIT           NOT NULL,
    [Idtipo_cliente_Representante]         INT           NULL,
    [IdTipoCredito_Representante]          VARCHAR (20)  NULL,
    [IdCiudad_Representante_Fact]          VARCHAR (25)  NULL,
    [IdParroquia_Representante_Fact]       VARCHAR (25)  NULL,
    [SueldoPadre]                          FLOAT (53)    NOT NULL,
    [SueldoMadre]                          FLOAT (53)    NOT NULL,
    [OtroIngresoPadre]                     FLOAT (53)    NOT NULL,
    [OtroIngresoMadre]                     FLOAT (53)    NOT NULL,
    [GastoAlimentacion]                    FLOAT (53)    NOT NULL,
    [GastoEducacion]                       FLOAT (53)    NOT NULL,
    [GastoServicioBasico]                  FLOAT (53)    NOT NULL,
    [GastoSalud]                           FLOAT (53)    NOT NULL,
    [GastoArriendo]                        FLOAT (53)    NOT NULL,
    [GastoPrestamo]                        FLOAT (53)    NOT NULL,
    [OtroGasto]                            FLOAT (53)    NOT NULL,
    [IdCatalogoESTADM]                     INT           NOT NULL,
    [Estado]                               BIT           NOT NULL,
    [IdUsuarioCreacion]                    VARCHAR (200) NULL,
    [FechaCreacion]                        DATETIME      NULL,
    [IdUsuarioModificacion]                VARCHAR (200) NULL,
    [FechaModificacion]                    DATETIME      NULL,
    [IdUsuarioRevision]                    VARCHAR (200) NULL,
    [FechaRevision]                        DATETIME      NULL,
    [IdUsuarioAnulacion]                   VARCHAR (200) NULL,
    [FechaAnulacion]                       DATETIME      NULL,
    [MotivoAnulacion]                      VARCHAR (MAX) NULL,
    [FechaPreMatriculacion]                DATETIME      NULL,
    CONSTRAINT [PK_aca_Admision] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAdmision] ASC),
    CONSTRAINT [FK_aca_Admision_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio]),
    CONSTRAINT [FK_aca_Admision_aca_Catalogo] FOREIGN KEY ([IdCatalogoPAREN_Padre]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_aca_Admision_aca_Catalogo1] FOREIGN KEY ([IdCatalogoPAREN_Madre]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_aca_Admision_aca_Catalogo2] FOREIGN KEY ([IdCatalogoPAREN_Representante]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_aca_Admision_aca_Catalogo3] FOREIGN KEY ([IdCatalogoESTADM]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_aca_Admision_aca_CatalogoFicha] FOREIGN KEY ([IdCatalogoFichaTipoViv_Aspirante]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_Admision_aca_CatalogoFicha1] FOREIGN KEY ([IdCatalogoFichaViv_Aspirante]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_Admision_aca_CatalogoFicha10] FOREIGN KEY ([IdCatalogoFichaInst_Representante]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_Admision_aca_CatalogoFicha2] FOREIGN KEY ([IdCatalogoFichaAgua_Aspirante]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_Admision_aca_CatalogoFicha3] FOREIGN KEY ([IdCatalogoFichaMotivo_Aspirante]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_Admision_aca_CatalogoFicha4] FOREIGN KEY ([IdCatalogoFichaInst_Aspirante]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_Admision_aca_CatalogoFicha5] FOREIGN KEY ([IdCatalogoFichaFinanc_Aspirante]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_Admision_aca_CatalogoFicha6] FOREIGN KEY ([IdCatalogoFichaVive_Aspirante]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_Admision_aca_CatalogoFicha7] FOREIGN KEY ([IdCatalogoFichaVive_Aspirante]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_Admision_aca_CatalogoFicha8] FOREIGN KEY ([IdCatalogoFichaInst_Padre]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_Admision_aca_CatalogoFicha9] FOREIGN KEY ([IdCatalogoFichaInst_Madre]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_Admision_aca_Curso] FOREIGN KEY ([IdEmpresa], [IdCurso]) REFERENCES [dbo].[aca_Curso] ([IdEmpresa], [IdCurso]),
    CONSTRAINT [FK_aca_Admision_aca_Jornada] FOREIGN KEY ([IdEmpresa], [IdJornada]) REFERENCES [dbo].[aca_Jornada] ([IdEmpresa], [IdJornada]),
    CONSTRAINT [FK_aca_Admision_aca_NivelAcademico] FOREIGN KEY ([IdEmpresa], [IdNivel]) REFERENCES [dbo].[aca_NivelAcademico] ([IdEmpresa], [IdNivel]),
    CONSTRAINT [FK_aca_Admision_aca_Sede] FOREIGN KEY ([IdEmpresa], [IdSede]) REFERENCES [dbo].[aca_Sede] ([IdEmpresa], [IdSede]),
    CONSTRAINT [FK_aca_Admision_fa_cliente_tipo] FOREIGN KEY ([IdEmpresa], [Idtipo_cliente_Padre]) REFERENCES [dbo].[fa_cliente_tipo] ([IdEmpresa], [Idtipo_cliente]),
    CONSTRAINT [FK_aca_Admision_fa_cliente_tipo1] FOREIGN KEY ([IdEmpresa], [Idtipo_cliente_Madre]) REFERENCES [dbo].[fa_cliente_tipo] ([IdEmpresa], [Idtipo_cliente]),
    CONSTRAINT [FK_aca_Admision_fa_cliente_tipo2] FOREIGN KEY ([IdEmpresa], [Idtipo_cliente_Representante]) REFERENCES [dbo].[fa_cliente_tipo] ([IdEmpresa], [Idtipo_cliente]),
    CONSTRAINT [FK_aca_Admision_fa_TerminoPago] FOREIGN KEY ([IdTipoCredito_Representante]) REFERENCES [dbo].[fa_TerminoPago] ([IdTerminoPago]),
    CONSTRAINT [FK_aca_Admision_fa_TerminoPago1] FOREIGN KEY ([IdTipoCredito_Madre]) REFERENCES [dbo].[fa_TerminoPago] ([IdTerminoPago]),
    CONSTRAINT [FK_aca_Admision_fa_TerminoPago2] FOREIGN KEY ([IdTipoCredito_Padre]) REFERENCES [dbo].[fa_TerminoPago] ([IdTerminoPago]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo] FOREIGN KEY ([IdTipoDocumento_Aspirante]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo1] FOREIGN KEY ([Naturaleza_Aspirante]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo10] FOREIGN KEY ([IdTipoDocumento_Madre]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo11] FOREIGN KEY ([Naturaleza_Madre]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo12] FOREIGN KEY ([CodCatalogoCONADIS_Madre]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo13] FOREIGN KEY ([IdEstadoCivil_Madre]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo14] FOREIGN KEY ([Naturaleza_Representante]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo15] FOREIGN KEY ([IdTipoDocumento_Representante]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo16] FOREIGN KEY ([CodCatalogoCONADIS_Representante]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo17] FOREIGN KEY ([IdEstadoCivil_Representante]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo2] FOREIGN KEY ([Sexo_Aspirante]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo3] FOREIGN KEY ([CodCatalogoSangre_Aspirante]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo4] FOREIGN KEY ([CodCatalogoCONADIS_Aspirante]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo5] FOREIGN KEY ([Naturaleza_Padre]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo6] FOREIGN KEY ([IdTipoDocumento_Padre]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo7] FOREIGN KEY ([Sexo_Padre]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo8] FOREIGN KEY ([CodCatalogoCONADIS_Padre]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_Catalogo9] FOREIGN KEY ([IdEstadoCivil_Padre]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_aca_Admision_tb_ciudad] FOREIGN KEY ([IdCiudad_Aspirante]) REFERENCES [dbo].[tb_ciudad] ([IdCiudad]),
    CONSTRAINT [FK_aca_Admision_tb_ciudad1] FOREIGN KEY ([IdCiudad_Padre]) REFERENCES [dbo].[tb_ciudad] ([IdCiudad]),
    CONSTRAINT [FK_aca_Admision_tb_ciudad2] FOREIGN KEY ([IdCiudad_Madre]) REFERENCES [dbo].[tb_ciudad] ([IdCiudad]),
    CONSTRAINT [FK_aca_Admision_tb_ciudad3] FOREIGN KEY ([IdCiudad_Representante]) REFERENCES [dbo].[tb_ciudad] ([IdCiudad]),
    CONSTRAINT [FK_aca_Admision_tb_ciudad4] FOREIGN KEY ([IdCiudad_Representante_Fact]) REFERENCES [dbo].[tb_ciudad] ([IdCiudad]),
    CONSTRAINT [FK_aca_Admision_tb_ciudad5] FOREIGN KEY ([IdCiudad_Madre_Fact]) REFERENCES [dbo].[tb_ciudad] ([IdCiudad]),
    CONSTRAINT [FK_aca_Admision_tb_ciudad6] FOREIGN KEY ([IdCiudad_Padre_Fact]) REFERENCES [dbo].[tb_ciudad] ([IdCiudad]),
    CONSTRAINT [FK_aca_Admision_tb_GrupoEtnico] FOREIGN KEY ([IdGrupoEtnico_Aspirante]) REFERENCES [dbo].[tb_GrupoEtnico] ([IdGrupoEtnico]),
    CONSTRAINT [FK_aca_Admision_tb_GrupoEtnico1] FOREIGN KEY ([IdGrupoEtnico_Padre]) REFERENCES [dbo].[tb_GrupoEtnico] ([IdGrupoEtnico]),
    CONSTRAINT [FK_aca_Admision_tb_GrupoEtnico2] FOREIGN KEY ([IdGrupoEtnico_Madre]) REFERENCES [dbo].[tb_GrupoEtnico] ([IdGrupoEtnico]),
    CONSTRAINT [FK_aca_Admision_tb_GrupoEtnico3] FOREIGN KEY ([IdGrupoEtnico_Representante]) REFERENCES [dbo].[tb_GrupoEtnico] ([IdGrupoEtnico]),
    CONSTRAINT [FK_aca_Admision_tb_pais] FOREIGN KEY ([IdPais_Aspirante]) REFERENCES [dbo].[tb_pais] ([IdPais]),
    CONSTRAINT [FK_aca_Admision_tb_pais1] FOREIGN KEY ([IdPais_Padre]) REFERENCES [dbo].[tb_pais] ([IdPais]),
    CONSTRAINT [FK_aca_Admision_tb_pais2] FOREIGN KEY ([IdPais_Madre]) REFERENCES [dbo].[tb_pais] ([IdPais]),
    CONSTRAINT [FK_aca_Admision_tb_pais3] FOREIGN KEY ([IdPais_Representante]) REFERENCES [dbo].[tb_pais] ([IdPais]),
    CONSTRAINT [FK_aca_Admision_tb_parroquia] FOREIGN KEY ([IdParroquia_Aspirante]) REFERENCES [dbo].[tb_parroquia] ([IdParroquia]),
    CONSTRAINT [FK_aca_Admision_tb_parroquia1] FOREIGN KEY ([IdParroquia_Padre]) REFERENCES [dbo].[tb_parroquia] ([IdParroquia]),
    CONSTRAINT [FK_aca_Admision_tb_parroquia2] FOREIGN KEY ([IdParroquia_Madre]) REFERENCES [dbo].[tb_parroquia] ([IdParroquia]),
    CONSTRAINT [FK_aca_Admision_tb_parroquia3] FOREIGN KEY ([IdParroquia_Representante]) REFERENCES [dbo].[tb_parroquia] ([IdParroquia]),
    CONSTRAINT [FK_aca_Admision_tb_parroquia4] FOREIGN KEY ([IdParroquia_Representante_Fact]) REFERENCES [dbo].[tb_parroquia] ([IdParroquia]),
    CONSTRAINT [FK_aca_Admision_tb_parroquia5] FOREIGN KEY ([IdParroquia_Madre_Fact]) REFERENCES [dbo].[tb_parroquia] ([IdParroquia]),
    CONSTRAINT [FK_aca_Admision_tb_parroquia6] FOREIGN KEY ([IdParroquia_Padre_Fact]) REFERENCES [dbo].[tb_parroquia] ([IdParroquia]),
    CONSTRAINT [FK_aca_Admision_tb_profesion] FOREIGN KEY ([IdProfesion_Padre]) REFERENCES [dbo].[tb_profesion] ([IdProfesion]),
    CONSTRAINT [FK_aca_Admision_tb_profesion1] FOREIGN KEY ([IdProfesion_Madre]) REFERENCES [dbo].[tb_profesion] ([IdProfesion]),
    CONSTRAINT [FK_aca_Admision_tb_profesion2] FOREIGN KEY ([IdProfesion_Representante]) REFERENCES [dbo].[tb_profesion] ([IdProfesion]),
    CONSTRAINT [FK_aca_Admision_tb_provincia] FOREIGN KEY ([IdProvincia_Aspirante]) REFERENCES [dbo].[tb_provincia] ([IdProvincia]),
    CONSTRAINT [FK_aca_Admision_tb_provincia1] FOREIGN KEY ([IdProvincia_Padre]) REFERENCES [dbo].[tb_provincia] ([IdProvincia]),
    CONSTRAINT [FK_aca_Admision_tb_provincia2] FOREIGN KEY ([IdProvincia_Madre]) REFERENCES [dbo].[tb_provincia] ([IdProvincia]),
    CONSTRAINT [FK_aca_Admision_tb_provincia3] FOREIGN KEY ([IdProvincia_Representante]) REFERENCES [dbo].[tb_provincia] ([IdProvincia]),
    CONSTRAINT [FK_aca_Admision_tb_region] FOREIGN KEY ([Cod_Region_Aspirante]) REFERENCES [dbo].[tb_region] ([Cod_Region]),
    CONSTRAINT [FK_aca_Admision_tb_region1] FOREIGN KEY ([Cod_Region_Padre]) REFERENCES [dbo].[tb_region] ([Cod_Region]),
    CONSTRAINT [FK_aca_Admision_tb_region2] FOREIGN KEY ([Cod_Region_Madre]) REFERENCES [dbo].[tb_region] ([Cod_Region]),
    CONSTRAINT [FK_aca_Admision_tb_region3] FOREIGN KEY ([Cod_Region_Representante]) REFERENCES [dbo].[tb_region] ([Cod_Region]),
    CONSTRAINT [FK_aca_Admision_tb_Religion] FOREIGN KEY ([IdReligion_Aspirante]) REFERENCES [dbo].[tb_Religion] ([IdReligion]),
    CONSTRAINT [FK_aca_Admision_tb_Religion1] FOREIGN KEY ([IdReligion_Padre]) REFERENCES [dbo].[tb_Religion] ([IdReligion]),
    CONSTRAINT [FK_aca_Admision_tb_Religion2] FOREIGN KEY ([IdReligion_Representante]) REFERENCES [dbo].[tb_Religion] ([IdReligion]),
    CONSTRAINT [FK_aca_Admision_tb_Religion3] FOREIGN KEY ([IdReligion_Madre]) REFERENCES [dbo].[tb_Religion] ([IdReligion])
);

















