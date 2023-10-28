
-- =============================================
-- Author:		Gerson Sánchez
-- Create date: 2023 - 12 - 09
-- Description:	[QueryGetSICY]
-- =============================================
CREATE  PROCEDURE [SZG].[QueryGetSICY]
-- SZG.[QueryGetSICY]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	DECLARE @Response AS NVARCHAR(50) ='BatchId incorrecto';
	DECLARE @Status AS INTEGER = 400;
	DECLARE @Data AS NVARCHAR(MAX) = 'null';
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;
    -- Insert statements for procedure here
	
	DECLARE @DataSensor AS NVARCHAR(MAX) = (SELECT *
	FROM dbo.Sensor 			
	ORDER BY SensorId 
	FOR JSON AUTO
	)

	DECLARE @DataImageClasification AS NVARCHAR(MAX) = (SELECT *
	FROM dbo.ImageClasification
	ORDER BY ClasificationId 
	FOR JSON AUTO
	)

	CREATE TABLE #temp (year INT)
     
    DECLARE @YEAR INT
     
    SET @YEAR=2000
    while @year<=YEAR(GETDATE()) 
    BEGIN
        INSERT INTO #temp VALUES (@YEAR)
        SET @YEAR=@YEAR+1       
    END

    DECLARE @Years AS NVARCHAR(MAX)= (SELECT * FROM #temp FOR JSON AUTO)
     
    DROP TABLE #temp

	SET @Status = 200;
	SET @Response = 'ImageClasification, sensor and year.';

	SELECT '{'
	+ '"Response":"' + @Response
	+'","Status":' + CAST(@Status AS NVARCHAR)
	+ ',"Data":{'+'"SensorList":' + @DataSensor 
	+ ',"ImageClasificationList":'+@DataImageClasification
	+',"YearList":'+@Years+'}}' as Result;
END