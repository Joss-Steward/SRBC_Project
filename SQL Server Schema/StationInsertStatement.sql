USE SRBC_DB;

GO
DECLARE @name nchar(50)

SET @name = 'TESTSTATION2'

IF NOT EXISTS 
    (   SELECT  1
        FROM    StationMetaData 
        WHERE   (StationMetaData.StationName = @name) 
    )
    BEGIN
        INSERT StationMetaData (StationName, StationLocation) 
        VALUES (@name, NULL) 
    END;