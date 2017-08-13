CREATE TABLE [dbo].[Usuario] (
    [usuID]                      INT           IDENTITY (1, 1) NOT NULL,
    [usu_capa]                   VARCHAR (10)  NULL,
    [usu_tipo_id]                VARCHAR (10)  NULL,
    [usu_num_id]                 VARCHAR (15)  NULL,
    [usu_nombres]                VARCHAR (100) NULL,
    [usu_apellidos]              VARCHAR (100) NULL,
    [usu_direccion]              VARCHAR (100) NULL,
    [usu_telefono]               VARCHAR (100) NULL,
    [usu_celular]                VARCHAR (50)  NULL,
    [usu_email]                  VARCHAR (100) NULL,
    [usu_vigencia]               VARCHAR (10)  NULL,
    [usu_fec_crea]               DATETIME      NULL,
    [usu_usu_crea]               INT           NULL,
    [usu_fec_modif]              DATETIME      NULL,
    [usu_usu_modif]              INT           NULL,
    [usu_ultimo_ingreso]         DATETIME      NULL,
    [usu_ultimo_ingreso_fallido] DATETIME      NULL,
    [usu_activo]                 BIT           CONSTRAINT [DF_Usuario_usu_activo] DEFAULT ((1)) NOT NULL,
    [perID]                      NUMERIC (18)  NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([usuID] ASC)
);

