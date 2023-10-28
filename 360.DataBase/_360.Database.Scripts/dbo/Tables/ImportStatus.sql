CREATE TABLE [dbo].[ImportStatus] (
    [StatusId]          INT          IDENTITY (1, 1) NOT NULL,
    [StatusDescription] VARCHAR (50) NULL,
    [Active]            BIT          NOT NULL DEFAULT 1,
    CONSTRAINT [PK_ImportStatus] PRIMARY KEY CLUSTERED ([StatusId] ASC)
);

