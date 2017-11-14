use [SHGuests]

SELECT * FROM
(
select year(Discharged) as [Year],
       LEFT(DATENAME(month, Discharged), 3) as [Month],
	   count(*) as [Guests]
	from Visits
	WHERE Roster = 'D' and Discharged >= '2011-06-05'
	group by Discharged
) AS d
pivot
(
	sum([Guests]) for [Month] in (Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec)
) as pvt order by [Year];
