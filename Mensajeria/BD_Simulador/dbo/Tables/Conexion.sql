CREATE TABLE [dbo].[Conexion] (
    [conID]                  NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [con_descripcion]        VARCHAR (100) NULL,
    [con_ip]                 VARCHAR (15)  NULL,
    [con_puerto]             NUMERIC (5)   NULL,
    [con_time_out_envio]     NUMERIC (18)  NULL,
    [con_time_out_recepcion] NUMERIC (18)  NULL,
    CONSTRAINT [PK_Conexion] PRIMARY KEY CLUSTERED ([conID] ASC)
);

