USE SRBC_DB;

GO
INSERT INTO dbo.WaterQualityData 
(StationID,
 SampleTime,
 Temperature,
 SpecificConductivity,
 PH,
 Turbidity,
 DissolvedOxygen) 
VALUES 
((SELECT a.ID FROM StationMetaData a WHERE a.StationName = @_stationname),
 @_sampletime,
 @_temperature,
 @_specificconductivity,
 @_ph,
 @_turbidity,
 @_dissolvedoxygen);