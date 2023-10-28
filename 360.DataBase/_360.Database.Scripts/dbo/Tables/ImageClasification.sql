CREATE TABLE [dbo].[ImageClasification] (
    [ClasificationId]          INT          IDENTITY (1, 1) NOT NULL,
    [ClasificationDescription] VARCHAR (50) NULL,
    [Active]                   BIT          CONSTRAINT [DF_ImageClasification_Active] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_ImageClasification] PRIMARY KEY CLUSTERED ([ClasificationId] ASC)
);

