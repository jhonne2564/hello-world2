-- =============================================
-- Author:		Fedex
-- Create date: 27/09/2015
-- Description:	General para Conexión
-- =============================================
CREATE PROCEDURE [dbo].[sp_Conexion] 
   @StrXMLDatos NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		-- VARIABLES PARAMETROS
		DECLARE @Tipo						VARCHAR(50)
				,@sesID						VARCHAR(50)
				,@con_descripcion			VARCHAR(100)
				,@con_ip					VARCHAR(15)
				,@con_puerto				NUMERIC(5,0)
				,@con_time_out_envio		NUMERIC(18,0)
				,@con_time_out_recepcion	NUMERIC(18,0)
				,@CampoOrden				VARCHAR(100)
				,@Ordenamiento				VARCHAR(10)
				,@Pag						INT
				,@RegPag					INT
				,@sqlSentencia				VARCHAR(MAX)

		-- VARIABLES DE SESION
		DECLARE	@idoc			INT
				,@usuID			INT
				,@CanReg	INT
				,@RegIni	INT
				,@RegFin	INT
				,@CanPag	INT
		
		EXEC sp_xml_preparedocument @idoc OUTPUT, @StrXMLDatos

		-- Lee documento XML de entrada
		SELECT	
				@sesID=sesID
				,@Tipo=Tipo
				,@CampoOrden=CampoOrden
				,@Ordenamiento=Ordenamiento
				,@Pag=Pag
				,@RegPag=RegPag
				,@con_descripcion=con_descripcion
				,@con_ip=con_ip
				,@con_puerto=con_puerto
				,@con_time_out_envio=con_time_out_envio
				,@con_time_out_recepcion=con_time_out_recepcion
		FROM OPENXML (@idoc, '/parametros',2)
		WITH	
			(
				sesID						VARCHAR(50)
				,Tipo						VARCHAR(50)
				,CampoOrden					VARCHAR(100)
				,Ordenamiento				VARCHAR(10)
				,Pag						INT
				,RegPag						INT
				,con_descripcion			VARCHAR(100)
				,con_ip						VARCHAR(15)
				,con_puerto					NUMERIC(5,0)
				,con_time_out_envio			NUMERIC(18,0)
				,con_time_out_recepcion		NUMERIC(18,0)
			 )

		EXEC sp_xml_removedocument @idoc
		
		SELECT  TOP 1 @usuID = usuID
		FROM Sesion (NOLOCK) 
		--WHERE sesID = @sesID AND ses_estado = 'ACTIVO'
		
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

			IF @Tipo = 'BuscarConexion'
			BEGIN
			
				CREATE TABLE #tmpBuscarConexion(tmpID BIGINT IDENTITY(1,1)
												,conID NUMERIC(18,0)
												,con_descripcion VARCHAR(100)
												,con_ip VARCHAR(15)
												,con_puerto NUMERIC(5,0)
												,con_time_out_envio NUMERIC(18,0)
												,con_time_out_recepcion NUMERIC(18,0))
										
					SET @sqlSentencia = 'INSERT INTO #tmpBuscarConexion '
									  + ' SELECT conID'
									  + ',con_descripcion'
									  + ',con_ip'
									  + ',con_puerto'
									  + ',con_time_out_envio'
									  + ',con_time_out_recepcion'
									  + ' FROM Conexion CON (NOLOCK)'
									  + ' WHERE 1=1'
					IF @con_descripcion <> ''
						SET @sqlSentencia = @sqlSentencia + ' AND CON.con_descripcion LIKE ''%' + CAST(@con_descripcion AS VARCHAR) + '%'''
					IF (@CampoOrden <> '') AND (@Ordenamiento <> '')
						SET @sqlSentencia = @sqlSentencia + ' ORDER BY ' + @CampoOrden + ' ' + @Ordenamiento
				
					PRINT (@sqlSentencia)
					EXEC (@sqlSentencia)
				
					SELECT @CanReg = COUNT(1) 
					FROM #tmpBuscarConexion (NOLOCK)

					IF @Pag = 0
						BEGIN
							SET @RegIni = 1;
							SET @RegFin = @CanReg;
							SET @CanPag = 0;
						END
					ELSE
						BEGIN
							SET @RegIni = (@RegPag * (@Pag - 1)) + 1;
							SET @RegFin = @RegIni + @RegPag - 1;
							SET @CanPag = CAST(@CanReg / @RegPag AS INT);
						
							IF @CanPag * @RegPag <> @CanReg 
							   SET @CanPag = @CanPag + 1;
						END
				
					SELECT	*
					FROM	#tmpBuscarConexion
						
					WHERE	tmpID BETWEEN @RegIni AND @RegFin
						
					SELECT	'CanReg'		= @CanReg,
							'CanPag'		= @CanPag
						
					DROP TABLE #tmpBuscarConexion

			END

			IF @Tipo = 'CrearConexion'
			BEGIN

				IF EXISTS(SELECT 1 FROM Conexion (NOLOCK) 
						  WHERE 
							con_ip= @con_ip AND
							con_puerto = @con_puerto)
				BEGIN

					SELECT '1' AS cod_error,'La conexión ya existe.' AS gls_error

				END
				ELSE
				BEGIN

					INSERT INTO Conexion
					(con_descripcion
					,con_ip
					,con_puerto
					,con_time_out_envio
					,con_time_out_recepcion)
					VALUES
					(@con_descripcion
					,@con_ip
					,@con_puerto
					,@con_time_out_envio
					,@con_time_out_recepcion)

					SELECT @@IDENTITY AS conID

				END

			END
			
		END
	END TRY
	BEGIN CATCH
		EXEC spGenerico_LogTryCatch @StrXMLDatos
	END	CATCH
END
