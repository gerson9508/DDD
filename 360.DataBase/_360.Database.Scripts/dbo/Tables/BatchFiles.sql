CREATE TABLE [dbo].[BatchFiles] (
    [BatchFileId]        INT           IDENTITY (1, 1) NOT NULL,
    [BatchClasification] INT           NULL,
    [FilePath]           VARCHAR (MAX) NULL,
    [Status]             INT           NULL,
    [FileName]           VARCHAR (MAX) NULL,
    [Extension]          VARCHAR (50)  NULL,
    [CreatedOn]          DATETIME      CONSTRAINT [DF_BatchFiles_CreatedOn] DEFAULT (getdate()) NULL,
    [ModifyOn]           DATETIME      NULL,
    [UserModifier]       VARCHAR (50)  NULL,
    [Hash]               VARCHAR (MAX) NULL,
    [Active]             BIT           CONSTRAINT [DF_BatchFiles_Active] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_BatchFiles] PRIMARY KEY CLUSTERED ([BatchFileId] ASC),
    CONSTRAINT [FK_BatchFiles_BatchClasification] FOREIGN KEY ([BatchClasification]) REFERENCES [dbo].[BatchClasification] ([BatchClasification]),
    CONSTRAINT [FK_BatchFiles_ImportStatus] FOREIGN KEY ([Status]) REFERENCES [dbo].[ImportStatus] ([StatusId])
);

