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
