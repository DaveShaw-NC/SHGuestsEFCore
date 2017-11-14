use [SHGuests]

CREATE TABLE #bd_month 
	(year_admit int, 
	quarter_admit int,
	month_num int,
	month_name nvarchar(12),
	agency nvarchar(35),
	guests int);

insert into #bd_month 
select YEAR(Discharged), 
	datepart(quarter, Discharged),
	month(Discharged), 
	datename(month, Discharged),
	Agency,
    count(Worker)
	from Visits
	WHERE Roster = 'D' and Discharged >= '2011-06-05'
	group by Discharged, Agency;

select * from
(
select year_admit as [Year],
		month_num as [NMonth],
       coalesce(month_name, 'Total') as [Month], 
	   sum(ISNULL(guests, 0)) as [Guests] 
	   from #bd_month 
	   group by year_admit, month_num, month_name
) as d
PIVOT
(
	sum([Guests]) for [Year] in ([2011], [2012], [2013], [2014], [2015], [2016], [2017], [2018], [2019], [2020])
	--sum([Guests]) for [Year] in ([2005], [2006], [2007], [2008], [2009], [2010], [2011], 
	--[2012], [2013], [2014], [2015], [2016], [2017], [2018], [2019], [2020])
) as pvt order by [NMonth];

drop table #bd_month;