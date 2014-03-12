CREATE TABLE [dbo].[StationMetaData] (
    [ID]              INT               IDENTITY (1, 1) NOT NULL,
    [StationName]     NCHAR (50)        UNIQUE NOT NULL,
    [StationLocation] [sys].[geography] NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Stores the friendly name of the station.  The current max length is 50 characters, which matches the format used by the SRBC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'StationMetaData', @level2type = N'COLUMN', @level2name = N'StationName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Stores the Latitude and Longitude of the station', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'StationMetaData', @level2type = N'COLUMN', @level2name = N'StationLocation';

