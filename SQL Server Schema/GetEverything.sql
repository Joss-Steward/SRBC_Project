USE SRBC_DB;

GO
SELECT * FROM WaterQualityData 
WHERE 
StationID = (SELECT a.ID FROM dbo.StationMetaData a WHERE (a.StationName = 'TESTSTATION2'));