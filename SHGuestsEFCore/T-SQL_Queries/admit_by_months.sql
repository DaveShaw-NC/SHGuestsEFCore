use [SHGuests]

SELECT * FROM
(
select year(AdmitDate) as [Year],
       LEFT(DATENAME(month, AdmitDate), 3) as [Month],
	   count(*) as [Guests]
	from Visits
	WHERE AdmitDate >= '2011-06-05'
	group by AdmitDate
) AS d
pivot
(
	sum([Guests]) for [Month] in (Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec)
) as pvt order by [Year];
