CREATE TABLE [dbo].[DetalleTramaTemp] (
    [dttID]                    NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [dtt_guid]                 VARCHAR (50)  NULL,
    [dtt_campo]                INT           NOT NULL,
    [dtt_nombre]               VARCHAR (100) NULL,
    [dtt_descripcion]          VARCHAR (200) NULL,
    [ddt_estatico]             VARCHAR (1)   NULL,
    [ddt_estatico_informacion] VARCHAR (999) NULL
);

