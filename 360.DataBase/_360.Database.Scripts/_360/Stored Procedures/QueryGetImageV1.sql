-- =============================================
-- Author:		Gerson Sánchez
-- Create date: 2023 - 12 - 09
-- Description:	QueryGetImage
-- =============================================
CREATE PROCEDURE [SZG].[QueryGetImageV1]
-- SZG.QueryGetImageV1 1,1,2020
	-- Add the parameters for the stored procedure here
	@SensorId int = 0,
	@ClasificationId int = 0,
	@Year int = 0
AS
BEGIN
	DECLARE @Response AS NVARCHAR(50) = 'BatchClasification'
	DECLARE @Status AS INTEGER = 200;
	DECLARE @Data AS NVARCHAR(MAX) = 'null';

	SET @Data = (SELECT
			bc.BatchClasification AS batchClasification, bc.Year AS year, bc.ClasificationId AS clasificationId
			,bc.SensorId AS sensorId, bc.CreatedOn AS createdOn, bc.ModifyOn AS modifyOn,
			fields.BatchFileId AS batchFileId, fields.FilePath AS filePath
		FROM dbo.BatchClasification AS bc 
		LEFT JOIN BatchFiles  fields ON bc.BatchClasification = fields.BatchClasification
		WHERE bc.Year = @Year OR bc.ClasificationId = @ClasificationId OR bc.SensorId = @SensorId 
		ORDER BY bc.Year DESC
		FOR JSON AUTO)

	if(@Data IS NULL) begin SET @Data = 'null'end

	SELECT '{"Response":"' + @Response
		 +'","Status":' + CAST(@Status AS NVARCHAR)
		 + ',"Data":' + @Data + '}' AS Result;
END