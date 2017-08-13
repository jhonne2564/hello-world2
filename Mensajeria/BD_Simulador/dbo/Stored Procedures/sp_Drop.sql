-- ==================================================================    
-- Author:  Fedex    
-- Create date: 06/10/2015    
-- Description: Se realiza todos los cargues de drop en la aplicación    
-- ==================================================================    
CREATE PROCEDURE [dbo].[sp_Drop]
  @StrXMLDatos VARCHAR(MAX)    
AS    
 BEGIN    
	SET NOCOUNT ON;    
	BEGIN TRY
	  -- VARIABLES PARAMETROS    
	  DECLARE	@Tipo		VARCHAR(50)    
				,@sesID		VARCHAR(50)    
				,@Valor		VARCHAR(100)
				,@Valor2	VARCHAR(100)  
	        
	        
	  -- VARIABLES POR SP    
	   DECLARE	@idoc   INT   
				,@usuID  INT    
				,@empID  INT   
				,@grpID  INT
	     
	     
	  EXEC sp_xml_preparedocument @idoc OUTPUT, @StrXMLDatos    
	      
	  -- Lee documento XML de entrada    
	  SELECT    
		 @sesID=sesID
		 ,@Tipo=Tipo
		 ,@Valor=Valor
		 ,@Valor2=Valor2  
	  FROM OPENXML (@idoc, '/parametros',2)    
	     
	   WITH     
		 (    
			sesID   VARCHAR(50),    
			Tipo	VARCHAR(50),    
			Valor   VARCHAR(100),
			Valor2	VARCHAR(100)  
		)    
	      
	EXEC sp_xml_removedocument @idoc    
	     
	SELECT  TOP 1 @usuID = usuID
	FROM Sesion (NOLOCK)     
	    
	IF @@ROWCOUNT = 0    
	BEGIN   
	      
		-- ERROR DE SESION... FUERA    
		SELECT RET = 'NOK', MSG = 'SESION INVALIDA'
		SET    @sesID = ''	

	END    
	ELSE     
	BEGIN    
	      
		-- OK LA SESION, CONTINUAMOS    
		SELECT RET = 'OK', MSG = ''    
	       
		 -- AHORA LAS ACCIONES DEL NEGOCIO     
	         
		IF @Tipo = 'CONEXION'    
		BEGIN

			SELECT  'Value' = CAST(con_ip AS VARCHAR) + '|' + CAST(con_puerto AS VARCHAR) + '|' + CAST(con_time_out_envio AS VARCHAR) + '|' + CAST(con_time_out_recepcion AS VARCHAR),    
					'Texto' = con_descripcion    
			FROM  Conexion (NOLOCK)      
			ORDER BY con_descripcion   
		   
		END    
		
	END
	END TRY
	BEGIN CATCH
		EXEC spGenerico_LogTryCatch @StrXMLDatos
	END	CATCH 
END    