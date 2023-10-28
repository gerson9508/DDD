

-- =============================================
-- Author:		Gerson Sánchez
-- Create date: 2023 - 12 - 09
-- Description:	QueryGetImage
-- =============================================
CREATE PROCEDURE [SZG].[QueryGetImage]
-- SZG.QueryGetImage 1,1,1
	-- Add the parameters for the stored procedure here
	@SensorId int = 0,
	@ClasificationId int = 0,
	@BatchId int = 0
AS
BEGIN
	--DECLARE @Type AS NVARCHAR(75) = 'https://tools.ietf.org/html/rfc7231#section-6.5.1';
	DECLARE @Title AS NVARCHAR(50) ='BatchId incorrecto';
	DECLARE @Status AS INTEGER = 400;
	--DECLARE @TraceId AS UNIQUEIDENTIFIER = NEWID();
	DECLARE @Result AS NVARCHAR(MAX) = 'null';

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@SensorId = 0)
	BEGIN
		SET @Title = 'SensorId incorrecto';
	END
    ELSE IF (@ClasificationId = 0)
	BEGIN
		SET @Title = 'ClasificationId incorrecto';
	END
	ELSE IF EXISTS (SELECT bc.BatchClasification
					FROM dbo.BatchClasification bc
						INNER JOIN dbo.Sensor s ON s.SensorId = bc.SensorId 
						INNER JOIN dbo.ImageClasification ic ON ic.ClasificationId = bc.ClasificationId
						INNER JOIN dbo.BatchHeader bh ON bh.BatchId = bc.BatchId
					WHERE s.SensorId = @SensorId 
						AND ic.ClasificationId = @ClasificationId
						AND bh.BatchId = @BatchId)
	BEGIN
		DECLARE @batchName NVARCHAR(MAX) = (SELECT TOP(1) bh.BatchName 
						FROM dbo.BatchClasification bc 
							INNER JOIN dbo.Sensor s ON s.SensorId = bc.SensorId 
							INNER JOIN dbo.ImageClasification cd ON cd.ClasificationId = bc.ClasificationId
							INNER JOIN dbo.BatchHeader bh ON bh.BatchId = bc.BatchId
							INNER JOIN dbo.InfoProviders infp ON infp.ProviderId = bh.ProviderId
							INNER JOIN dbo.BatchStatus bs ON bs.BatchStatusId = bh.Status
						WHERE s.SensorId = @SensorId 
							AND cd.ClasificationId = @ClasificationId 
							AND bh.BatchId = @BatchId)
		DECLARE @sensorDescription NVARCHAR(MAX) = (SELECT TOP(1) s.SensorDescription
						FROM dbo.BatchClasification bc 
							INNER JOIN dbo.Sensor s ON s.SensorId = bc.SensorId 
							INNER JOIN dbo.ImageClasification cd ON cd.ClasificationId = bc.ClasificationId
							INNER JOIN dbo.BatchHeader bh ON bh.BatchId = bc.BatchId
							INNER JOIN dbo.InfoProviders infp ON infp.ProviderId = bh.ProviderId
							INNER JOIN dbo.BatchStatus bs ON bs.BatchStatusId = bh.Status
						WHERE s.SensorId = @SensorId 
							AND cd.ClasificationId = @ClasificationId 
							AND bh.BatchId = @BatchId)
		DECLARE @clasificationDescription NVARCHAR(MAX) = (SELECT TOP(1) cd.ClasificationDescription
						FROM dbo.BatchClasification bc 
							INNER JOIN dbo.Sensor s ON s.SensorId = bc.SensorId 
							INNER JOIN dbo.ImageClasification cd ON cd.ClasificationId = bc.ClasificationId
							INNER JOIN dbo.BatchHeader bh ON bh.BatchId = bc.BatchId
							INNER JOIN dbo.InfoProviders infp ON infp.ProviderId = bh.ProviderId
							INNER JOIN dbo.BatchStatus bs ON bs.BatchStatusId = bh.Status
						WHERE s.SensorId = @SensorId 
							AND cd.ClasificationId = @ClasificationId 
							AND bh.BatchId = @BatchId)
		DECLARE @providerName NVARCHAR(MAX) = (SELECT TOP(1) infp.ProviderName
						FROM dbo.BatchClasification bc 
							INNER JOIN dbo.Sensor s ON s.SensorId = bc.SensorId 
							INNER JOIN dbo.ImageClasification cd ON cd.ClasificationId = bc.ClasificationId
							INNER JOIN dbo.BatchHeader bh ON bh.BatchId = bc.BatchId
							INNER JOIN dbo.InfoProviders infp ON infp.ProviderId = bh.ProviderId
							INNER JOIN dbo.BatchStatus bs ON bs.BatchStatusId = bh.Status
						WHERE s.SensorId = @SensorId 
							AND cd.ClasificationId = @ClasificationId 
							AND bh.BatchId = @BatchId)
		DECLARE @statusDescription NVARCHAR(MAX) = (SELECT TOP(1)
							  bs.StatusDescription
						FROM dbo.BatchClasification bc 
							INNER JOIN dbo.Sensor s ON s.SensorId = bc.SensorId 
							INNER JOIN dbo.ImageClasification cd ON cd.ClasificationId = bc.ClasificationId
							INNER JOIN dbo.BatchHeader bh ON bh.BatchId = bc.BatchId
							INNER JOIN dbo.InfoProviders infp ON infp.ProviderId = bh.ProviderId
							INNER JOIN dbo.BatchStatus bs ON bs.BatchStatusId = bh.Status
						WHERE s.SensorId = @SensorId 
							AND cd.ClasificationId = @ClasificationId 
							AND bh.BatchId = @BatchId)			
		DECLARE	@dates NVARCHAR(MAX) =(SELECT bc.Year as year
						FROM dbo.BatchClasification bc 
							INNER JOIN dbo.Sensor s ON s.SensorId = bc.SensorId 
							INNER JOIN dbo.ImageClasification ic ON ic.ClasificationId = bc.ClasificationId
							INNER JOIN dbo.BatchHeader bh ON bh.BatchId = bc.BatchId
						WHERE s.SensorId = @SensorId
							AND ic.ClasificationId = @ClasificationId 
							AND bh.BatchId = @BatchId
						ORDER BY bc.Year DESC
						FOR JSON PATH, Without_Array_Wrapper)
		
		SET @Result = '{'
			+'"sensorDescription":"' + @sensorDescription 
			+ '",'
			+'"clasificationDescription":"'+ @clasificationDescription 		
			+ '",'
			+'"batchName":"' + @batchName 
			+ '",'
			+'"statusDescription":"' + @statusDescription
			+ '",'
			+'"providerName":"' + @providerName
			+ '",'
			+'"dates":[' + @dates + ']'
			+'}';
		SET @Status = 200;
		--SET @Type ='https://tools.ietf.org/html/rfc7231#section-6.3.1';
		SET @Title = 'Request successful.';
	END
	ELSE
	BEGIN
		IF EXISTS (Select bf.BatchClasification
					FROM dbo.BatchClasification bf 
					INNER JOIN dbo.Sensor s ON s.SensorId = bf.SensorId 
					WHERE s.SensorId = @SensorId)
		BEGIN
			IF NOT EXISTS (Select bf.BatchClasification
					FROM dbo.BatchClasification bf 
					INNER JOIN dbo.Sensor s ON s.SensorId = bf.SensorId 
					INNER JOIN dbo.ImageClasification ic ON ic.ClasificationId = bf.ClasificationId
					WHERE s.SensorId = @SensorId and ic.ClasificationId = @ClasificationId)
			BEGIN
				SET @Title = 'SensorId no coincide con ClasificationId';
			END
			
		END

		IF EXISTS (Select bf.BatchClasification
					FROM dbo.BatchClasification bf 
					INNER JOIN dbo.ImageClasification ic ON ic.ClasificationId = bf.ClasificationId
					WHERE ic.ClasificationId = @ClasificationId)
		BEGIN
			IF NOT EXISTS (Select bf.BatchClasification
					FROM dbo.BatchClasification bf 
					INNER JOIN dbo.Sensor s ON s.SensorId = bf.SensorId 
					INNER JOIN dbo.ImageClasification ic ON ic.ClasificationId = bf.ClasificationId
					WHERE s.SensorId = @SensorId and ic.ClasificationId = @ClasificationId)
			BEGIN
				SET @Title = 'ClasificationId no coincide con SensorId';
			END
			
		END
		
	END 
	
	SELECT '{'
		--+'"Type":"' + @Type 
		+ '"Response":"' + @Title
		+'","Status":' + CAST(@Status AS NVARCHAR)
	    --+',"TraceId":"' +convert(nvarchar(50), @TraceId)
		+ ',"Data":' + @Result
		+ '}' AS Result;

		--SELECT '{'
		--+'"StatusCode":' + CAST(@StatusCode AS NVARCHAR)
		--+ ','
		--+'"Message":"' + @Message
		--+ '",'
		--+'"Result":' + @Result
		--+ '}' as Result;
END