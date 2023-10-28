CREATE TABLE [dbo].[BatchHeader] (
    [BatchId]      INT           IDENTITY (1, 1) NOT NULL,
    [TimeStamp]    DATETIME      CONSTRAINT [DF_BatchHeader_TimeStamp] DEFAULT (getdate()) NULL,
    [BatchName]    VARCHAR (100) NULL,
    [Status]       INT           NULL,
    [FileCount]    INT           NULL,
    [ProviderId]   INT           NULL,
    [CreatedOn]    DATETIME      CONSTRAINT [DF_BatchHeader_CreatedOn] DEFAULT (getdate()) NULL,
    [ModifyOn]     DATETIME      NULL,
    [UserModifier] VARCHAR (50)  NULL,
    [Active]       BIT           CONSTRAINT [DF_BatchHeader_Active] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_BatchHeader] PRIMARY KEY CLUSTERED ([BatchId] ASC),
    CONSTRAINT [FK_BatchHeader_BatchStatus] FOREIGN KEY ([Status]) REFERENCES [dbo].[BatchStatus] ([BatchStatusId]),
    CONSTRAINT [FK_BatchHeader_InfoProviders] FOREIGN KEY ([ProviderId]) REFERENCES [dbo].[InfoProviders] ([ProviderId])
);

