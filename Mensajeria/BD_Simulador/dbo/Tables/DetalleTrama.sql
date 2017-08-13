CREATE TABLE [dbo].[DetalleTrama] (
    [detID]                    NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [encID]                    NUMERIC (18)  NULL,
    [det_campo]                INT           NULL,
    [det_nombre]               VARCHAR (100) NULL,
    [det_descripcion]          VARCHAR (200) NULL,
    [det_estatico]             VARCHAR (1)   NULL,
    [det_estatico_informacion] VARCHAR (999) NULL,
    CONSTRAINT [PK_DetalleTrama] PRIMARY KEY CLUSTERED ([detID] ASC)
);

