CREATE TABLE [dbo].[Sesion] (
    [sesID]                   VARCHAR (50)   NOT NULL,
    [usuID]                   INT            NOT NULL,
    [ses_fecha]               DATETIME       NOT NULL,
    [ses_remote_addr]         VARCHAR (100)  NULL,
    [ses_remote_user]         VARCHAR (100)  NULL,
    [ses_remote_host]         VARCHAR (100)  NULL,
    [ses_user_agent]          VARCHAR (250)  NULL,
    [ses_language]            VARCHAR (250)  NULL,
    [ses_referer]             VARCHAR (250)  NULL,
    [ses_cookie]              VARCHAR (1000) NULL,
    [ses_host]                VARCHAR (250)  NULL,
    [ses_estado]              VARCHAR (20)   CONSTRAINT [DF_Sesion_ses_estado] DEFAULT ('ACTIVO') NOT NULL,
    [ses_menu]                XML            NULL,
    [ses_ultimo_ingreso]      DATETIME       NULL,
    [ses_ultima_fecha_activa] DATETIME       NULL,
    [ses_ultima_validacion]   DATETIME       NULL,
    CONSTRAINT [PK_Sesion] PRIMARY KEY CLUSTERED ([sesID] ASC)
);

