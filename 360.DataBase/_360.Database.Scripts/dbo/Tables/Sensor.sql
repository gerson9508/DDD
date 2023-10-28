CREATE TABLE [dbo].[Sensor] (
    [SensorId]          INT           IDENTITY (1, 1) NOT NULL,
    [SensorDescription] VARCHAR (150) NULL,
    [Metada]            VARCHAR (MAX) NULL,
    [Active]            BIT           CONSTRAINT [DF_Sensor_Active] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Sensor] PRIMARY KEY CLUSTERED ([SensorId] ASC)
);

