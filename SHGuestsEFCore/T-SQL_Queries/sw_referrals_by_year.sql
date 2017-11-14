use [SHGuests]

CREATE TABLE #bd_month 
	(year_admit int, 
	month_num int,
	month_name nvarchar(12),
	agency nvarchar(35),
	social nvarchar(25),
	guests int);

insert into #bd_month 
select YEAR(AdmitDate), 
	month(AdmitDate), 
	Left(datename(month, AdmitDate), 3),
	Agency,
	Worker, 
    count(Worker)
	from Visits
	group by AdmitDate, Agency, Worker;

select * from
(
select coalesce(year_admit, ' ') as [Year],
	   coalesce(agency, ' ') as [Agency], 
	   coalesce(social, ' ') as [Referrer],
	   sum(ISNULL(guests, 0)) as [Guests] 
	   from #bd_month 
	   group by year_admit, agency, social, month_num, month_name
) as d
PIVOT
(
	sum([Guests]) for [Year] in ([2005], [2006], [2007], [2008], [2009], [2010], [2011], [2012], [2013], [2014], [2015], [2016])
) as pvt order by [Agency], [Referrer];

drop table #bd_month;