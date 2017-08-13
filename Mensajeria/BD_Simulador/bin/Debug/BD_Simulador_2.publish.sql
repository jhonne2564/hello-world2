/*
Script de implementación para BD_Simulador

Una herramienta generó este código.
Los cambios realizados en este archivo podrían generar un comportamiento incorrecto y se perderán si
se vuelve a generar el código.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "BD_Simulador"
:setvar DefaultFilePrefix "BD_Simulador"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detectar el modo SQLCMD y deshabilitar la ejecución del script si no se admite el modo SQLCMD.
Para volver a habilitar el script después de habilitar el modo SQLCMD, ejecute lo siguiente:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'El modo SQLCMD debe estar habilitado para ejecutar correctamente este script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creando $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE Latin1_General_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'No se puede modificar la configuración de la base de datos. Debe ser un administrador del sistema para poder aplicar esta configuración.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'No se puede modificar la configuración de la base de datos. Debe ser un administrador del sistema para poder aplicar esta configuración.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creando [dbo].[Campo]...';


GO
CREATE TABLE [dbo].[Campo] (
    [camID]    INT          NOT NULL,
    [cam_tipo] VARCHAR (50) NULL,
    CONSTRAINT [PK_Campo] PRIMARY KEY CLUSTERED ([camID] ASC)
);


GO
PRINT N'Creando [dbo].[Conexion]...';


GO
CREATE TABLE [dbo].[Conexion] (
    [conID]                  NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [con_descripcion]        VARCHAR (100) NULL,
    [con_ip]                 VARCHAR (15)  NULL,
    [con_puerto]             NUMERIC (5)   NULL,
    [con_time_out_envio]     NUMERIC (18)  NULL,
    [con_time_out_recepcion] NUMERIC (18)  NULL,
    CONSTRAINT [PK_Conexion] PRIMARY KEY CLUSTERED ([conID] ASC)
);


GO
PRINT N'Creando [dbo].[DetalleTrama]...';


GO
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


GO
PRINT N'Creando [dbo].[DetalleTramaTemp]...';


GO
CREATE TABLE [dbo].[DetalleTramaTemp] (
    [dttID]                    NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [dtt_guid]                 VARCHAR (50)  NULL,
    [dtt_campo]                INT           NOT NULL,
    [dtt_nombre]               VARCHAR (100) NULL,
    [dtt_descripcion]          VARCHAR (200) NULL,
    [ddt_estatico]             VARCHAR (1)   NULL,
    [ddt_estatico_informacion] VARCHAR (999) NULL
);


GO
PRINT N'Creando [dbo].[EncabezadoTrama]...';


GO
CREATE TABLE [dbo].[EncabezadoTrama] (
    [encID]           NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [enc_mti]         VARCHAR (4)   NULL,
    [enc_descripcion] VARCHAR (100) NULL,
    [enc_usu_crea]    NUMERIC (18)  NULL,
    [enc_fec_crea]    DATETIME      NULL,
    CONSTRAINT [PK_EncabezadoTrama] PRIMARY KEY CLUSTERED ([encID] ASC)
);


GO
PRINT N'Creando [dbo].[ErrorProcedure]...';


GO
CREATE TABLE [dbo].[ErrorProcedure] (
    [errID]          INT           IDENTITY (1, 1) NOT NULL,
    [err_fecha]      DATETIME      NOT NULL,
    [ErrorNumber]    INT           NULL,
    [ErrorSeverity]  INT           NULL,
    [ErrorState]     INT           NULL,
    [ErrorProcedure] VARCHAR (MAX) NULL,
    [ErrorLine]      INT           NULL,
    [ErrorMessage]   VARCHAR (MAX) NULL,
    [DatosEntrada]   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ErrorProcedure] PRIMARY KEY CLUSTERED ([errID] ASC)
);


GO
PRINT N'Creando [dbo].[Sesion]...';


GO
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
    [ses_estado]              VARCHAR (20)   NOT NULL,
    [ses_menu]                XML            NULL,
    [ses_ultimo_ingreso]      DATETIME       NULL,
    [ses_ultima_fecha_activa] DATETIME       NULL,
    [ses_ultima_validacion]   DATETIME       NULL,
    CONSTRAINT [PK_Sesion] PRIMARY KEY CLUSTERED ([sesID] ASC)
);


GO
PRINT N'Creando [dbo].[Usuario]...';


GO
CREATE TABLE [dbo].[Usuario] (
    [usuID]                      INT           IDENTITY (1, 1) NOT NULL,
    [usu_capa]                   VARCHAR (10)  NULL,
    [usu_tipo_id]                VARCHAR (10)  NULL,
    [usu_num_id]                 VARCHAR (15)  NULL,
    [usu_nombres]                VARCHAR (100) NULL,
    [usu_apellidos]              VARCHAR (100) NULL,
    [usu_direccion]              VARCHAR (100) NULL,
    [usu_telefono]               VARCHAR (100) NULL,
    [usu_celular]                VARCHAR (50)  NULL,
    [usu_email]                  VARCHAR (100) NULL,
    [usu_vigencia]               VARCHAR (10)  NULL,
    [usu_fec_crea]               DATETIME      NULL,
    [usu_usu_crea]               INT           NULL,
    [usu_fec_modif]              DATETIME      NULL,
    [usu_usu_modif]              INT           NULL,
    [usu_ultimo_ingreso]         DATETIME      NULL,
    [usu_ultimo_ingreso_fallido] DATETIME      NULL,
    [usu_activo]                 BIT           NOT NULL,
    [perID]                      NUMERIC (18)  NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([usuID] ASC)
);


GO
PRINT N'Creando [dbo].[DF_ErrorProcedure_err_fecha]...';


GO
ALTER TABLE [dbo].[ErrorProcedure]
    ADD CONSTRAINT [DF_ErrorProcedure_err_fecha] DEFAULT (getdate()) FOR [err_fecha];


GO
PRINT N'Creando [dbo].[DF_Sesion_ses_estado]...';


GO
ALTER TABLE [dbo].[Sesion]
    ADD CONSTRAINT [DF_Sesion_ses_estado] DEFAULT ('ACTIVO') FOR [ses_estado];


GO
PRINT N'Creando [dbo].[DF_Usuario_usu_activo]...';


GO
ALTER TABLE [dbo].[Usuario]
    ADD CONSTRAINT [DF_Usuario_usu_activo] DEFAULT ((1)) FOR [usu_activo];


GO
PRINT N'Creando [dbo].[spGenerico_LogTryCatch]...';


GO
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
GO
PRINT N'Creando [dbo].[sp_Conexion]...';


GO
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
GO
PRINT N'Creando [dbo].[sp_Drop]...';


GO
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
GO
PRINT N'Creando [dbo].[sp_Tramas]...';


GO
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
GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET IDENTITY_INSERT [dbo].[Usuario] ON 

GO
INSERT [dbo].[Usuario] ([usuID], [usu_capa], [usu_tipo_id], [usu_num_id], [usu_nombres], [usu_apellidos], [usu_direccion], [usu_telefono], [usu_celular], [usu_email], [usu_vigencia], [usu_fec_crea], [usu_usu_crea], [usu_fec_modif], [usu_usu_modif], [usu_ultimo_ingreso], [usu_ultimo_ingreso_fallido], [usu_activo], [perID]) VALUES (1, NULL, N'1', N'93237493', N'Federico', N'Vergara Giraldo', NULL, N'7024362', N'3016063260', N'fedex007@gmail.com', N'V', NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(1 AS Numeric(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO


INSERT [dbo].[Sesion] ([sesID], [usuID], [ses_fecha], [ses_remote_addr], [ses_remote_user], [ses_remote_host], [ses_user_agent], [ses_language], [ses_referer], [ses_cookie], [ses_host], [ses_estado], [ses_menu], [ses_ultimo_ingreso], [ses_ultima_fecha_activa], [ses_ultima_validacion]) VALUES (N'123456789', 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'ACTIVO', NULL, NULL, NULL, NULL)
GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Actualización completada.';


GO
