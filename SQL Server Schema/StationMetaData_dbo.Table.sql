CREATE TABLE [dbo].[Table]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [StationName] NCHAR(50) NOT NULL, 
    [StationLocation] [sys].[geography] NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stores the friendly name of the station.  The current max length is 50 characters, which matches the format used by the SRBC',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Table',
    @level2type = N'COLUMN',
    @level2name = N'StationName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stores the Latitude and Longitude of the station',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Table',
    @level2type = N'COLUMN',
    @level2name = N'StationLocation'