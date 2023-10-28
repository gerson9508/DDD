CREATE TABLE [dbo].[BatchStatus] (
    [BatchStatusId]     INT          IDENTITY (1, 1) NOT NULL,
    [StatusDescription] VARCHAR (50) NULL,
    [Active]            BIT          CONSTRAINT [DF_BatchStatus_Active] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_BatchStatus] PRIMARY KEY CLUSTERED ([BatchStatusId] ASC)
);

