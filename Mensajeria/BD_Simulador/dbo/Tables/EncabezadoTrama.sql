CREATE TABLE [dbo].[EncabezadoTrama] (
    [encID]           NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [enc_mti]         VARCHAR (4)   NULL,
    [enc_descripcion] VARCHAR (100) NULL,
    [enc_usu_crea]    NUMERIC (18)  NULL,
    [enc_fec_crea]    DATETIME      NULL,
    CONSTRAINT [PK_EncabezadoTrama] PRIMARY KEY CLUSTERED ([encID] ASC)
);

