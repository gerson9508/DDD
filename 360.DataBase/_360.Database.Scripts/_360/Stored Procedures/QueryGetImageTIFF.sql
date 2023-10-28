
-- =============================================
-- Author:		Gerson Sánchez
-- Create date: 2023 - 12 - 09
-- Description:	[QueryGetImageTIFF]
-- =============================================
CREATE  PROCEDURE [SZG].[QueryGetImageTIFF]
-- EXEC SZG.[QueryGetImageTIFF] @BatchFileId = 10
	-- Add the parameters for the stored procedure here
	@BatchFileId INT = 0
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

	IF (@BatchFileId = 0)
		BEGIN
			SET @Title = 'BatchFileId incorrecto';
		END

	IF EXISTS (SELECT FilePath FROM dbo.BatchFiles  WHERE BatchFileId = @BatchFileId)
		BEGIN
			SET @Result = (SELECT TOP(1) (bf.FilePath + '/'+bf.FileName +'.'+ bf.Extension) AS filePath
			FROM dbo.BatchFiles bf 
			WHERE bf.BatchFileId = @BatchFileId
			FOR JSON AUTO, Without_Array_Wrapper)

			SET @Status = 200;
			SET @Title = 'Request successful.';
			--SET @Type ='https://tools.ietf.org/html/rfc7231#section-6.3.1';
		END
	

		SELECT '{'
	--	+'"Type":"' + @Type 
		+ '"Title":"' + @Title
		+'","Status":' + CAST(@Status AS NVARCHAR)
	   -- +',"TraceId":"' +convert(nvarchar(50), @TraceId)
		+ ',"Result":' + @Result
		+ '}' as Result;

	--SELECT '{'
	--	+'"StatusCode":' + CAST(@StatusCode AS NVARCHAR)
	--	+ ','
	--	+'"Message":"' + @Message
	--	+ '",'
	--	+'"Result":' + @Result
	--	+ '}' as Result;
END