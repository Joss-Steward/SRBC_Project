CREATE TABLE [dbo].[WaterQualityData] (
    [ID]                   BIGINT     NOT NULL IDENTITY,
    [StationID]            INT        NOT NULL,
    [SampleTime]           DATETIME NOT NULL,
    [Temperature]          FLOAT (53) NULL,
    [SpecificConductivity] FLOAT (53) NULL,
    [PH]                   FLOAT (53) NULL,
    [Turbidity]            FLOAT (53) NULL,
    [DisolvedOxygen]       FLOAT (53) NULL,
    PRIMARY KEY NONCLUSTERED ([ID] ASC),
    CONSTRAINT [FK_WaterQualityData_ToStationID] FOREIGN KEY ([StationID]) REFERENCES [dbo].[StationMetaData] ([ID])
);


GO
CREATE CLUSTERED INDEX [IX_WaterQualityData_SampleTime]
    ON [dbo].[WaterQualityData]([SampleTime] ASC, [StationID] ASC);

