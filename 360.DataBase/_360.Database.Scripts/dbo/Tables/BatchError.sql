CREATE TABLE [dbo].[BatchError] (
    [BatchErrorId] INT           IDENTITY (1, 1) NOT NULL,
    [FilePath]     VARCHAR (MAX) NOT NULL,
    [Descripcion]  VARCHAR (255) NOT NULL,
    [BatchId]      INT           NOT NULL,
    CONSTRAINT [PK_BatchError] PRIMARY KEY CLUSTERED ([BatchErrorId] ASC),
    CONSTRAINT [FK_BatchError_BatchHeader] FOREIGN KEY ([BatchId]) REFERENCES [dbo].[BatchHeader] ([BatchId])
);

