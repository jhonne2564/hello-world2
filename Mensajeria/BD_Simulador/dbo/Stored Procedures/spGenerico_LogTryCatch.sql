/*
====================================================================================
Nombre           :	spGenerico_LogTryCatch
Función          :	Graba los errores que ocurren dentro de un stored procedure
Fecha de Creación:	26-09-2015
Autor            :	Fedex

Ejecución:
	EXEC spPYME_LogTryCatch @StrXMLDatos
Modificaciones:
---------------
Modificación										Fecha			Realizado por
====================================================================================*/


CREATE PROCEDURE [dbo].[spGenerico_LogTryCatch]
   @StrXMLDatos VARCHAR(MAX)
AS
BEGIN
	INSERT INTO ErrorProcedure
				([ErrorNumber]
				,[ErrorSeverity]
				,[ErrorState]
				,[ErrorProcedure]
				,[ErrorLine]
				,[ErrorMessage]
				,[DatosEntrada])
	VALUES
				(ERROR_NUMBER()
				,ERROR_SEVERITY()
				,ERROR_STATE()
				,ERROR_PROCEDURE()
				,ERROR_LINE()
				,ERROR_MESSAGE()
				,@StrXMLDatos)
END
