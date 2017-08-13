CREATE TABLE [dbo].[ErrorProcedure] (
    [errID]          INT           IDENTITY (1, 1) NOT NULL,
    [err_fecha]      DATETIME      CONSTRAINT [DF_ErrorProcedure_err_fecha] DEFAULT (getdate()) NOT NULL,
    [ErrorNumber]    INT           NULL,
    [ErrorSeverity]  INT           NULL,
    [ErrorState]     INT           NULL,
    [ErrorProcedure] VARCHAR (MAX) NULL,
    [ErrorLine]      INT           NULL,
    [ErrorMessage]   VARCHAR (MAX) NULL,
    [DatosEntrada]   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ErrorProcedure] PRIMARY KEY CLUSTERED ([errID] ASC)
);

