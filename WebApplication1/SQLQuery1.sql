
select * from((
select WaterQualityData.Temperature,row_number() over(order by WaterQualityData.Temperature asc) as 'row' from WaterQualityData where WaterQualityData.StationID=1)) as temp,
(
select count(*) as cnt from WaterQualityData where WaterQualityData.StationID=1) as temp1
where temp.row =temp1.cnt/2


select * from
(select WaterQualityData.Temperature,row_number() over(order by WaterQualityData.Temperature asc) as 'row' from WaterQualityData where WaterQualityData.StationID=1) as temp
