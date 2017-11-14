use [SHGuests]

create table #visit_tab
	( roster char(10) not null,
	  visitnum int not null,
	  guests bigint not null,
	  bd bigint not null,
	  min_bd bigint not null,
	  max_bd bigint not null,
	  avg_bd decimal(12,2) not null); 

insert into #visit_tab
    select 'Discharged', VisitNumber, count(*), 
	sum(VisitDays),
    min(VisitDays), 
	max(VisitDays),
    avg(VisitDays)
	from Visits where Roster = 'D' group by VisitNumber;

insert into #visit_tab 
	select 'Current', VisitNumber, count(*), 
	sum(datediff(day, AdmitDate, dateadd(day, 1, getdate()))),
	min(datediff(day, AdmitDate, dateadd(day, 1, getdate()))), 
	max(datediff(day, AdmitDate, dateadd(day, 1, getdate()))),
	avg(datediff(day, AdmitDate, dateadd(day, 1, getdate())))
	from Visits where Roster = 'C' group by VisitNumber;

select coalesce(roster, 'Total') as 'Roster',
    coalesce(cast(visitnum as varchar(6)), 'Total') as '# of Visits',  
	sum(guests) as 'Guests', min(min_bd) as 'Min. Stay', 
	max(max_bd) as 'Max. Stay', 
	sum(bd) as 'Bed Days', 
    (1. * sum(bd)) / (1. * sum(guests)) as 'Avg. Stay'
	from #visit_tab 
	group by roster, visitnum with rollup;

drop table #visit_tab;
