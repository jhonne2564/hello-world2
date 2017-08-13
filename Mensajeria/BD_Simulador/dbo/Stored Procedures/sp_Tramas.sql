-- =============================================
-- Author:		Fedex
-- Create date: 27/09/2015
-- Description:	General para Tramas
-- =============================================
CREATE PROCEDURE [dbo].[sp_Tramas] 
   @StrXMLDatos NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		-- VARIABLES PARAMETROS
		DECLARE @Tipo						VARCHAR(50)
				,@sesID						VARCHAR(50)
				,@dtt_guid					VARCHAR(50)
				,@dtt_campo					INT
				,@dtt_nombre				VARCHAR(100)
				,@dtt_descripcion			VARCHAR(200)
				,@ddt_estatico				VARCHAR(1)
				,@ddt_estatico_informacion	VARCHAR(999)
				,@enc_mti					VARCHAR(4)
				,@enc_descripcion			VARCHAR(100)
				,@encID						NUMERIC(18,0)
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
				,@dtt_guid=dtt_guid
				,@dtt_campo=dtt_campo
				,@dtt_nombre=dtt_nombre
				,@dtt_descripcion=dtt_descripcion
				,@ddt_estatico=ddt_estatico
				,@ddt_estatico_informacion=ddt_estatico_informacion
				,@enc_mti=enc_mti
				,@enc_descripcion=enc_descripcion
				,@CampoOrden=CampoOrden
				,@Ordenamiento=Ordenamiento
				,@Pag=Pag
				,@RegPag=RegPag
				,@encID=encID
		FROM OPENXML (@idoc, '/parametros',2)
		WITH	
			(
				sesID						VARCHAR(50)
				,Tipo						VARCHAR(50)
				,dtt_guid					VARCHAR(50)
				,dtt_campo					INT
				,dtt_nombre					VARCHAR(100)
				,dtt_descripcion			VARCHAR(200)
				,ddt_estatico				VARCHAR(1)
				,ddt_estatico_informacion	VARCHAR(999)
				,enc_mti					VARCHAR(4)
				,enc_descripcion			VARCHAR(100)
				,CampoOrden					VARCHAR(100)
				,Ordenamiento				VARCHAR(10)
				,Pag						INT
				,RegPag						INT
				,encID						NUMERIC(18,0)
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

			IF @Tipo = 'BuscarTrama'
			BEGIN
			
			CREATE TABLE #tmpBuscarTrama(tmpID BIGINT IDENTITY(1,1),
										 encID NUMERIC(18,0),
										 enc_mti VARCHAR(4),
										 enc_descripcion VARCHAR(100))
										
				SET @sqlSentencia = 'INSERT INTO #tmpBuscarTrama '
								  + ' SELECT encID'
								  + ',enc_mti'
								  + ',enc_descripcion'
								  + ' FROM EncabezadoTrama ENC (NOLOCK)'
								  + ' WHERE 1=1'
				IF @enc_mti <> ''
					SET @sqlSentencia = @sqlSentencia + ' AND ENC.enc_mti = ''' + CAST(@enc_mti AS VARCHAR) + ''''
				IF (@CampoOrden <> '') AND (@Ordenamiento <> '')
					SET @sqlSentencia = @sqlSentencia + ' ORDER BY ' + @CampoOrden + ' ' + @Ordenamiento
				
				PRINT (@sqlSentencia)
				EXEC (@sqlSentencia)
				
				SELECT @CanReg = COUNT(1) 
				FROM #tmpBuscarTrama (NOLOCK)

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
				FROM	#tmpBuscarTrama
						
				WHERE	tmpID BETWEEN @RegIni AND @RegFin
						
				SELECT	'CanReg'		= @CanReg,
						'CanPag'		= @CanPag
						
				DROP TABLE #tmpBuscarTrama

			END
			
			IF @Tipo = 'EliminarTrama'
			BEGIN

				DELETE FROM DetalleTrama
				WHERE
					encID = @encID

				DELETE FROM EncabezadoTrama
				WHERE
					encID = @encID				

				SELECT @encID AS encID

			END
			
			IF @Tipo = 'CrearTrama'
			BEGIN

				INSERT INTO EncabezadoTrama
				(enc_mti
				,enc_descripcion
				,enc_usu_crea
				,enc_fec_crea)
				VALUES
				(@enc_mti
				,@enc_descripcion
				,@usuID
				,GETDATE())

				SELECT @encID = @@IDENTITY

				INSERT INTO DetalleTrama
				(encID
				,det_campo
				,det_nombre
				,det_descripcion
				,det_estatico
				,det_estatico_informacion)
				SELECT
				@encID
				,dtt_campo
				,dtt_nombre
				,dtt_descripcion
				,ddt_estatico
				,ddt_estatico_informacion
				FROM DetalleTramaTemp (NOLOCK)
				WHERE
					dtt_guid = @dtt_guid

				SELECT @encID AS encID

			END
			
			IF @Tipo = 'CrearDetalleTramaTemporal'
			BEGIN
			
				IF EXISTS(SELECT 1 FROM DetalleTramaTemp (NOLOCK) 
						  WHERE 
							dtt_guid= @dtt_guid AND
							dtt_campo = @dtt_campo)
				BEGIN

					SELECT '1' AS cod_error,'El campo ya ha sido adicionado.' AS gls_error

				END
				ELSE
				BEGIN

					INSERT INTO DetalleTramaTemp 
						(dtt_guid
						 ,dtt_campo
						 ,dtt_nombre
						 ,dtt_descripcion
						 ,ddt_estatico
						 ,ddt_estatico_informacion)
					VALUES
						(@dtt_guid
						 ,@dtt_campo
						 ,@dtt_nombre
						 ,@dtt_descripcion
						 ,@ddt_estatico
						 ,@ddt_estatico_informacion)

					SELECT @@IDENTITY AS dttID

				END			
				
			END

			IF @Tipo = 'BuscarDetalleTablaTemporal'
			BEGIN

				SELECT *
				FROM DetalleTramaTemp (NOLOCK)
				WHERE
					dtt_guid = @dtt_guid 

			END

			IF @Tipo = 'DetalleTrama'
			BEGIN

				SELECT *
				FROM DetalleTrama (NOLOCK)
				WHERE
					encID = @encID

			END
			
		END
	END TRY
	BEGIN CATCH
		EXEC spGenerico_LogTryCatch @StrXMLDatos
	END	CATCH
END
