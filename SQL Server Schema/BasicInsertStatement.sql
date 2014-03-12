USE SRBC_DB;

GO
DECLARE @_stationname nchar(50)
DECLARE @_sampletime timestamp
DECLARE @_temperature float
DECLARE @_specificconductivity float
DECLARE @_ph float
DECLARE @_turbidity float
DECLARE @_dissolvedoxygen float

SET @_stationname = 'TESTSTATION2'
SET @_sampletime = CONVERT(timestamp, CURRENT_TIMESTAMP)
SET @_temperature = 3.423
SET @_specificconductivity = 52.3242
SET @_ph = 3
SET @_turbidity = 621
SET @_dissolvedoxygen = 0.392778

INSERT INTO dbo.WaterQualityData 
(StationID,
 SampleTime,
 Temperature,
 SpecificConductivity,
 PH,
 Turbidity,
 DisolvedOxygen) 
VALUES 
((SELECT a.ID FROM StationMetaData a WHERE a.StationName = @_stationname),
 @_sampletime,
 @_temperature,
 @_specificconductivity,
 @_ph,
 @_turbidity,
 @_dissolvedoxygen);