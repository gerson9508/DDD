-- =============================================
-- Author:		Gerson Sánchez
-- Create date: 2023 - 12 - 09
-- Description:	[QueryGetImageTIFF]
-- =============================================
CREATE  PROCEDURE [SZG].[QueryGetImageTIFFV1]
-- SZG.[QueryGetImageTIFFV1] 10
	-- Add the parameters for the stored procedure here
	@BatchFileId INT = 0
AS
BEGIN
	DECLARE @Response AS NVARCHAR(50) ='BatchId incorrecto';
	DECLARE @Status AS INTEGER = 400;
	DECLARE @Data AS NVARCHAR(MAX) = 'null';
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;
    -- Insert statements for procedure here
	
	IF EXISTS (SELECT FilePath FROM dbo.BatchFiles  WHERE BatchFileId = @BatchFileId)
		BEGIN
			SET @Data = (SELECT TOP(1) bf.FilePath 
			FROM dbo.BatchFiles bf 
			WHERE bf.BatchFileId = @BatchFileId
			FOR JSON PATH, Without_Array_Wrapper)

			SET @Status = 200;
			SET @Response = 'Request successful.';
	END

		SELECT '{'
		+ '"Response":"' + @Response
		+'","Status":' + CAST(@Status AS NVARCHAR)
		+ ',"Data":' + @Data + '}' as Result;
END