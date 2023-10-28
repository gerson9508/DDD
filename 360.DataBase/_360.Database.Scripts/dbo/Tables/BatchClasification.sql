CREATE TABLE [dbo].[BatchClasification] (
    [BatchClasification] INT          IDENTITY (1, 1) NOT NULL,
    [Year]               INT          NULL,
    [ClasificationId]    INT          NULL,
    [SensorId]           INT          NULL,
    [CreatedOn]          DATETIME     CONSTRAINT [DF_BatchClasification_CreatedOn] DEFAULT (getdate()) NULL,
    [ModifyOn]           DATETIME     NULL,
    [UserModifier]       VARCHAR (50) NULL,
    [Active]             BIT          CONSTRAINT [DF_BatchClasification_Active] DEFAULT ((1)) NULL,
    [BatchId]            INT          NULL,
    CONSTRAINT [PK_BatchClasification] PRIMARY KEY CLUSTERED ([BatchClasification] ASC),
    CONSTRAINT [FK_BatchClasification_BatchHeader] FOREIGN KEY ([BatchId]) REFERENCES [dbo].[BatchHeader] ([BatchId]),
    CONSTRAINT [FK_BatchClasification_ImageClasification] FOREIGN KEY ([ClasificationId]) REFERENCES [dbo].[ImageClasification] ([ClasificationId]),
    CONSTRAINT [FK_BatchClasification_Sensor] FOREIGN KEY ([SensorId]) REFERENCES [dbo].[Sensor] ([SensorId])
);

