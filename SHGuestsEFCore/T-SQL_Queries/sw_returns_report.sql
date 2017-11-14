use [SHGuests]
DECLARE @retn as nvarchar(4)

select * from
(
select
	   coalesce(Agency, ' ') as [Agency], 
	   coalesce(Worker, ' ') as [Referrer],
	   case CanReturn
	       when 1 then 'Yes '
		   else 'No  '
       end as [Return],
	   COUNT(CanReturn) as [Guests]
	   from Visits
	   where Roster = 'D' AND Discharged >= '2011-06-05'
	   group by Agency, Worker, CanReturn
) as d
PIVOT
(
	sum([Guests]) for [Return] in ([Yes], [No])
) as pvt order by [Agency], [Referrer];

