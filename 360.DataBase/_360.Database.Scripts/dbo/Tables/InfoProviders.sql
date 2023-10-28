CREATE TABLE [dbo].[InfoProviders]
(
	[ProviderId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProviderName] VARCHAR(50) NULL, 
    [Active] BIT NOT NULL DEFAULT 1
)
