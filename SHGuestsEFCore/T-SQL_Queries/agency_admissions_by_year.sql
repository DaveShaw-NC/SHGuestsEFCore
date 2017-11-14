use [SHGuests]

select * from
(
select YEAR(AdmitDate) as [Year],
	   Agency as [Agency],
	   Count(*) as [Guests] 
	   from Visits
	   WHERE AdmitDate >= '2011-06-05'
	   group by AdmitDate, Agency
) as d
PIVOT
(
	sum([Guests]) for [Year] in ([2011], [2012], [2013], [2014], [2015], [2016], [2017], [2018], [2019], [2020])
	--sum([Guests]) for [Year] in ([2005], [2006], [2007], [2008], [2009], [2010], [2011], 
	--[2012], [2013], [2014], [2015], [2016], [2017], [2018], [2019], [2020])
) as pvt order by [Agency];

