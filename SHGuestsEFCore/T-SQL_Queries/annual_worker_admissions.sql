use [SHGuests]

select * from
(
select Year(AdmitDate) as [Year],
	   Agency [Agency], 
	   Worker as [Referrer],
	   Count(Worker) as [Guests] 
	   from Visits
	   WHERE AdmitDate >= '2011-06-05'
	   group by AdmitDate, Agency, Worker
) as d
PIVOT
(
	sum([Guests]) for [Year] in ([2011], [2012], [2013], [2014], [2015], [2016], [2017], [2018], [2019], [2020])
) as pvt order by [Agency], [Referrer];

