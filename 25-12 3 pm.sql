create database Milestone2;

use Milestone2;

go;

CREATE proc createAllTables AS


create table SystemUser(
username varchar(20) primary key,
Password varchar(20)
)

create table Fan(
national_ID varchar(20) primary key,
name varchar(20),
birthdate date,
address varchar(20),
phone_no int,
status bit,
username varchar(20) foreign key references SystemUser(username) on delete cascade on update cascade
)


create table SportsAssociationManager(
    ID int identity primary key ,
    name varchar(20),
     username varchar(20) foreign key REFERENCES SystemUser(username)  on delete cascade on update cascade
)


create table Systemadmin(
      ID int identity primary key,
    name varchar(20),
     username varchar(20) foreign key REFERENCES SystemUser(username)  on delete cascade on update cascade
)

create table Stadium(
    Id int  identity primary key,
     name varchar(20),
    location VARCHAR(20),
     capacity int,
    status bit
   
   
)
create table StadiumManager(
   ID int identity primary key,
    name varchar(20),
    stadium_id int foreign key references Stadium(Id)   on delete cascade on update cascade,
    username varchar(20) foreign key REFERENCES SystemUser(username)  on delete cascade on update cascade

)

create table Club(
    Id int  identity primary key,
    name varchar(20),
    location VARCHAR(20),
)
create table ClubRepresentative(
    ID int identity primary key,
    name varchar(20),
    club_id int foreign key references Club(Id)  on delete cascade on update cascade,
    username varchar(20) foreign key REFERENCES SystemUser(username)  on delete cascade on update cascade

)


create table Match(
        Id int  identity primary key,
        start_time datetime,
        end_time datetime,
        club_host int foreign key references Club(Id)  on delete cascade on update cascade,
        club_guest int foreign key references Club(Id),
         stadium_id int foreign key references Stadium(Id)  on delete set null on update cascade
)
create table Ticket(
  Id int  identity primary key,
  status bit,
  match_id int foreign key references Match(Id)  on delete cascade on update cascade

)

create table Ticket_Buying_Trans (
fan_national_id varchar(20) foreign key references Fan(National_id)  on delete cascade on update cascade,
ticket_id int foreign key references Ticket(Id)  on delete cascade on update cascade
)

truncate table Ticket_Buying_Trans
drop table Ticket_Buying_Trans
create table Host_Request (
id int primary key identity,
rep_id int foreign key references ClubRepresentative(Id) ,
manager_id int foreign key references StadiumManager(id) ,
match_id int foreign key references Match(Id)  on delete cascade on update cascade,
status varchar(20)
)
go;


go;
create PROC dropAllTables AS

drop table host_request;
drop table Ticket_Buying_Trans;
drop table Ticket;
drop table Match;
drop table Fan;
drop table StadiumManager; 
drop table ClubRepresentative;
drop table Stadium;
drop table Club;
drop table SportsAssociationManager;
drop table SystemAdmin;
drop table SystemUser;

go;




create PROC dropAllProceduresFunctionsViews AS 
drop procedure createAllTables;
drop procedure  dropAllTables;
drop procedure  clearAllTables;
drop procedure  addAssociationManager;
drop procedure  addNewMatch;
drop procedure  deleteMatch;
drop procedure  deleteMatchesOnStadium;
drop procedure  addClub;
drop procedure  addTicket;
drop procedure  deleteClub;
drop procedure  addStadium;
drop procedure  deleteStadium;
drop procedure  blockFan;
drop procedure  unblockFan;
drop procedure addRepresentative;
drop procedure addHostRequest;
drop procedure addStadiumManager;
drop procedure acceptRequest;
drop procedure rejectRequest;
drop procedure addFan;
drop procedure purchaseTicket;
drop procedure updateMatchHost;
drop procedure deleteMatchesOnStadium;

drop function viewAvailableStadiumsOn
drop function allUnassignedMatches
drop function allPendingRequests
drop function upcomingMatchesOfClub
drop function availableMatchesToAttend
drop function clubsNeverPlayed
drop function matchWithHighestAttendance
drop function matchesRankedByAttendance
drop function requestsFromClub

drop view allAssocManagers
drop view allClubRepresentatives
drop view allStadiumManagers
drop view allFans
drop view allMatches
drop view allTickets
drop view allCLubs
drop view allStadiums
drop view allRequests
drop view clubsWithNoMatches
drop view matchesPerTeam
drop view clubsNeverMatched

go;
create PROC clearAllTables AS

delete from  host_request;
delete from  Ticket_Buying_Trans;
delete from  Ticket;
delete from  Match;
delete from  Fan;
delete from  StadiumManager;
delete from  ClubRepresentative;
delete from  Stadium;
delete from  Club;
delete from  SportsAssociationManager;
delete from  SystemAdmin;
delete from  SystemUser;

go;

go
create view allAssocManagers as (
select u.username,u.password,m.name
from SportsAssociationManager m inner join SystemUser u on u.username=m.username
)

go
create view allClubRepresentatives as (
    select  u.username,u.password,cr.name,cl.name as club 
    from SystemUser u inner join ClubRepresentative cr on cr.username=u.username 
                      inner join Club cl on cl.Id=cr.club_id

)
go
 
create view allStadiumManagers as (
   select u.username,u.password,s.name,st.name as managedstadname
    from SystemUser u inner join StadiumManager s on s.username=u.username
                      inner join Stadium st on st.Id=s.stadium_id
)
go

create view allFans as(
    select f.username,s.password,f.NAME,f.national_ID,f.birthdate,f.status
    from Fan f inner join SystemUser s on s.username = f.username
)

go

Create view allMatches as (
Select c2.name as hostname ,c1.name as guestname,m.start_time
From Match m inner join club c1 on m.club_guest = c1.id 
inner join club c2 on c2.id = m.club_host
Where c1.id <> c2.id 
)



go
create view allTickets as(
Select c2.name as hostclub,c1.name as guestclub,s.name,m1.start_time
From Ticket t inner join Match m1 on  t.match_ID = m1.ID 
inner join Club c1 on  m1.club_guest = c1.id 
inner join Club c2 on m1.club_host = c2.id 
inner join Stadium s on s.ID = m1.stadium_id
where c1.id<>c2.id 
)
go

create view allCLubs as (
    select c.name,c.location
    from Club c
)

go
create view allStadiums as(
    select s.name,s.location,s.capacity,s.status
    From Stadium s

)
go

create view allRequests as (
    select cc.username as ClubRepresentative ,ss.username as stadium_manager, h.status
    from Host_Request h, ClubRepresentative cc,StadiumManager ss 
    where h.rep_id = cc.ID  and ss.ID=h.manager_ID
)
go

go;
CREATE PROC addAssociationManager
@name varchar(20),
@username VARCHAR(20),
@password VARCHAR(20)
 AS 
 if  not exists (select username from SystemUser where username=@username) begin
insert into SystemUser values(@username,@password);--SystemUser
insert into SportsAssociationManager(name,username) values(@name,@username);--Sports_Association_Manager
end
go;

create proc addNewMatch 
@hostname varchar(20),
@guestname varchar(20),
@starttime datetime,
@endtime datetime
AS
insert into match values(@starttime,
@endtime,
(select id from Club where name=@hostname),
(select id from Club where name=@guestname),null)
go;
create view clubsWithNoMatches  
AS
select c.name
from Club c left outer join match m on C.id = m.club_host or C.id = m.club_guest
where m.club_host is null and m.club_guest is null
go;
create proc deleteMatch  
@hostname varchar(20),
@guestname varchar(20)
AS 
declare @c1id int , @c2id int 
select @c1id=c1.Id from Club c1 where c1.name=@hostname 
select @c2id=c2.Id from Club c2 where c2.name=@guestname 
delete from Match where club_host=@c1id and club_guest=@c2id 

go;

create proc deleteMatchesOnStadium 
@stadiumname varchar(20)
as 
 
delete m
from match m inner join stadium s on m.stadium_id=s.id
where s.name = @stadiumname and m.start_time>CURRENT_TIMESTAMP

go;

create proc addClub 
@namec varchar(20),
@location varchar(20)
AS 
insert into Club values (@namec,@location)

go;

create proc addTicket 
@clubh varchar(20),
@clubg varchar(20),
@timest datetime 
AS 
insert into Ticket values (1,
(select M.id 
from Match M
where M.club_guest=(select id from Club where name=@clubg)
and M.club_host=(select id from Club where name=@clubh)
and M.start_time=@timest))

go;

create proc deleteClub 
@name varchar(20)
AS
delete from Match
where club_guest=(select id from club where name=@name) 
delete from Club 
where name=@name

go;
drop proc  deleteClub 
exec deleteClub 'Sevilla'
go;
create proc addStadium
@name varchar(20),
@location varchar(20),
@cap int 
AS
insert into Stadium values (@name,@location,@cap,1)
go;


create proc deleteStadium
@name varchar(20)
AS
delete from Host_Request --didn't work without it 
where exists 
(select * from match m inner join stadium s on m.stadium_id=s.Id
where s.name=@name) 

delete from Ticket
where exists
(select * from match m inner join stadium s on m.stadium_id=s.Id
where s.name=@name ) 

delete from Stadium where name=@name
go;
exec deleteStadium 'Santiago'
select * from Stadium

go;
create proc blockFan 
@national_ID varchar(20)  
as 
update Fan 
set status=0 
where national_ID=@national_ID;


go;

create proc unblockFan 
@national_ID varchar(20)  
as 
update Fan 
set status=1 
where national_ID=@national_ID;

go;

create proc addRepresentative 
@crname varchar(20),
@cname varchar(20),
@suname varchar(20),
@password varchar(20) 
as 
 if  not exists (select username from SystemUser where username=@suname) begin
insert into SystemUser values( @suname,@password) 
declare @cid int 
select @cid=Id from Club where name=@cname 
insert into ClubRepresentative values (@crname, @cid ,@suname) 
end

go;
create function viewAvailableStadiumsOn (@time datetime)
returns table 
as 
return( select distinct s.name ,s.location ,s.capacity
from Stadium s left outer join Match m on m.stadium_id =s.Id 
         where s.status=1 and ((m.id is null ) or (
         s.id not in( select s.id from Stadium s inner join Match m on m.stadium_id =s.Id
        where  (@time>=m.start_time and @time<=m.end_time)))))
         
go;
drop function viewAvailableStadiumsOn

select * from dbo.viewAvailableStadiumsOn('')
select * from match
select * from club
insert into match values ('','',1,3,2)

go;
create proc addHostRequest 
@cname varchar(20) ,
@sname varchar(20) ,
@start_time datetime
as
declare 
@rep_id int ,
@manager_id int,
@match_id int
select @manager_id=sm.ID from  StadiumManager sm inner join Stadium s on sm.stadium_id=s.Id  where s.name = @sname
select @rep_id=cr.ID ,@match_id=m.Id 
          from ClubRepresentative cr inner join Club c on cr.club_id = c.Id 
                                     inner join  Match m on m.club_host = c.Id
           where c.name =@cname  and m.start_time=@start_time 

                       
insert into Host_Request values(@rep_id,@manager_id,@match_id,'unhandled')


go;

create function allUnassignedMatches(@clubname varchar(20))  
returns TABLE
as 
return select cl.name, m.start_time
from match m inner join club cl on cl.id = m.club_guest
inner join club c2 on c2.id=m.club_host

where m.stadium_id is null and c2.name = @clubname
go;


create proc addStadiumManager 
@name varchar(20),
@stadiumname VARCHAR(20),
@username VARCHAR(20),
@password VARCHAR(20)
as 
 if  not exists (select username from SystemUser where username=@username) begin
insert into SystemUser values(@username,@password)
declare @r INT
select @r = s.Id
from Stadium s 
where s.name=@stadiumname

insert into StadiumManager values(@name,@r,@username)
end

go;

create function allPendingRequests(@username varchar(20)) 
returns table
AS 
return( select c.name as ClubRepresentativename ,cl.name as Guest_club,m.start_time 
from Host_Request h inner join ClubRepresentative c on c.ID =h.rep_id 
inner join match m on h.match_id=m.id
inner join club cl on cl.id = m.club_guest
inner join club c2 on c2.id = m.club_host
inner join StadiumManager sm on sm.id = h.manager_id
where sm.username = @Username and c.club_id=c2.Id and h.status='unhandled')
go

create proc acceptRequest 
@username varchar(20), 
@hostingclubname varchar(20), 
@guestname varchar(20), 
@starttime datetime
As
declare @capacity varchar(20) , @stadiumid int ,@hostid int , @guestid int
update h
set h.status ='accepted'
from Stadium s inner join StadiumManager sm on sm.stadium_id = s.id 
               inner join Host_Request h on h.manager_id = sm.id 
               inner join match m on m.id=h.match_id
               inner JOIN club cg on cg.id = m.club_guest 
               inner join club ch on ch.id = m.club_host
where ch.id<>cg.id and ch.name=@hostingclubname and sm.username = @username and cg.name =@guestname
                    and m.start_time = @starttime


select @stadiumid=sm.stadium_id 
from StadiumManager sm
where sm.username=@username

select @hostid=id
from club 
where name=@hostingclubname

update match 
set stadium_id=@stadiumid
where club_host=@hostid and start_time=@starttime



select @capacity =s.capacity
from stadium s
where s.id=@stadiumid

declare @i int = 1
while @i <=@capacity
begin 
    exec addTicket @hostingclubname,@guestname,@starttime

    set @i=@i+1
end

go

create proc rejectRequest  
@username varchar(20), 
@hostingclubname varchar(20), 
@guestname varchar(20), 
@starttime datetime
AS
update h
set h.status ='rejected'
from 
StadiumManager sm inner join Host_Request h on h.manager_id = sm.id 
inner join match m on m.id = h.match_id 
inner JOIN club cg on cg.id = m.club_guest 
inner join club ch on ch.id = m.club_host
where ch.name=@hostingclubname and cg.name =@guestname and sm.username = @username 
and m.start_time = @starttime

go;
create proc addFan
@name varchar(20),
@username varchar(20),
@pass varchar(20),
@nationalid varchar(20),
@birthdate  datetime,
@address varchar(20),
@phone int 
AS 
if  not exists (select username from SystemUser where username=@username) begin
insert into SystemUser values (@username,@pass)
insert into Fan values (@nationalid,@name,@birthdate,@address,@phone,1,@username)
end
go;

create function upcomingMatchesOfClub (@clubname varchar(20)) 
returns table 
as
return
select C1.name as Host_Club , C2.name as Guest_Club ,M.start_time,S.name as Stadium_Name
from
Club C1 inner join Match M on M.club_host=C1.id
inner join Club C2 on M.club_guest= C2.id
inner join Stadium S on S.Id=M.stadium_id
where (C1.name = @clubname or C2.name=@clubname ) and M.start_time>CURRENT_TIMESTAMP
go;

create function availableMatchesToAttend(@time datetime)  
returns table 
as 
return 
select C1.name as Host_Club , C2.name as Guest_Club,M.start_time,S.name as Stadium_Name
from
Club C1 inner join Match M on M.club_host=C1.id
inner join Club C2 on M.club_guest= C2.id
inner join Stadium S on S.Id=M.stadium_id
where M.start_time>=@time and exists
(select count(*) 
from ticket
where match_id=M.id and status=1
having count(*)>0)

go;

create proc purchaseTicket  
@fnationalid varchar(20),
@clubh varchar(20),
@clubg varchar(20),
@timest datetime
AS
--declare @ticketid int
insert into Ticket_Buying_Trans values (@fnationalid,
(select  top 1 T.id 
from Ticket T inner join Match M on T.match_id=M.id 
where M.club_guest=(select id from Club where name=@clubg)
and M.club_host=(select id from Club where name=@clubh)
and M.start_time=@timest and status=1))

update Ticket
set status =0
where id=(select top 1 T.id 
from Ticket T inner join Match M on T.match_id=M.id 
where M.club_guest=(select id from Club where name=@clubg)
and M.club_host=(select id from Club where name=@clubh)
and M.start_time=@timest and status=1)
go;


create proc updateMatchHost 
@clubh varchar(20),
@clubg varchar(20),
@timest datetime
AS
update Match 
set club_guest=(select id from Club where name=@clubh),
club_host=(select id from Club where name=@clubg),
stadium_id=null 
where club_guest=(select id from Club where name=@clubg)
and club_host=(select id from Club where name=@clubh)
and start_time=@timest


go;

create view matchesPerTeam AS  --left outer
select  c.name,count(M.id) as count
from Club C left outer join Match M on ( C.id=M.club_host or C.id=M.club_guest)
where M.end_time<current_timestamp and( C.id=M.club_host or C.id=M.club_guest)
group by C.name
go;

create view clubsNeverMatched (firstClubName,secondClubName)as 
select distinct c1.name , c2.name
from Club c1 inner join Club c2 on c1.Id>c2.Id
where c1.Id<>c2.Id and not exists (
select * from Match m where (m.club_host=c1.Id and m.club_guest=c2.Id) or (m.club_host=c2.Id and m.club_guest=c1.Id) ) 

go;

create function clubsNeverPlayed(@cname varchar(20)) 
returns table 
as 
return(
select  c1.name 
from Club c1 inner join Club c2 on c1.Id<>c2.Id 
where c1.Id<>c2.Id and not exists (
select * from Match m where ((m.club_host=c1.Id and m.club_guest=c2.Id) or (m.club_host=c2.Id and m.club_guest=c1.Id) )) and c2.name=@cname
)
 
go ;

create function matchWithHighestAttendance ()
returns table 
as
return(
select distinct c1.name as hostname ,
       c2.name as guestname 
from Club c1 inner join Club c2 on c1.Id<>c2.Id
             inner join Match m on c1.Id=m.club_host and c2.Id=m.club_guest
             inner join Ticket t on t.match_id=m.Id 
             inner join (SELECT t.match_id as maxmatchid ,  COUNT(*) tickets 
               FROM Ticket t
               GROUP BY  t.match_id
               HAVING COUNT(*)=
              (SELECT MAX(tickets)
                FROM
              (SELECT COUNT(*) tickets
               FROM Ticket
               where status =0 
               GROUP BY match_id) as intable) ) as maxtickects(t_match_id,tickets) on t_match_id=m.Id 
)

go;
create function matchesRankedByAttendance ()
returns table
as
return(
 select c1.name as hostname , c2.name as guestname --offset 0 rows 
      from Club c1 inner join Club c2 on c1.Id<>c2.Id 
                   inner join Match m on m.club_host=c1.Id and m.club_guest=c2.Id
                   inner join Ticket t on t.match_id=m.Id 
     where t.status=0
    group by c1.name , c2.name  
    order by count(t.match_id) desc offset 0 rows )



go;

create function requestsFromClub( @sname varchar(20),@cname varchar(20))
returns table 
as
return(
select c1.name as hostname ,c2.name as guestname
from Club c1 inner join Club c2 on c1.Id<>c2.Id
                        inner join Match m on m.club_host=c1.Id and m.club_guest=c2.Id
                        inner join Host_Request hr on hr.match_id=m.Id 
                        inner join ClubRepresentative cr on cr.ID=hr.rep_id and cr.club_id=c1.Id
                        inner join StadiumManager sm on sm.ID=hr.manager_id 
                        inner join Stadium s on s.Id=sm.stadium_id 
where s.name=@sname and c1.name=@cname and hr.status='unhandled'
)
go;

create proc login 
@username varchar(20),
@password varchar(20),
@status int output ,
@type varchar(30) output 
as 
if exists (select username , password from SystemUser where username=@username and Password=@password)
begin
set @status=1 
if exists (select username from Fan where username=@username )
begin 
set @type='Fan';
end 
if exists  (select username from ClubRepresentative where username=@username )
begin 
set @type='ClubRepresentative';
end 
if exists  (select username from StadiumManager where username=@username )
begin 
set @type='StadiumManager';
end 
if exists  (select username from SportsAssociationManager where username=@username )
begin 
set @type='SportsAssociationManager';
end 
if exists  (select username from Systemadmin where username=@username )
begin 
set @type='Systemadmin';
end 
end
else 
begin 
set @status=0 ;
print 'Not valid username & password'
end 

declare @s bit , @t varchar(30)
exec login @username='sara1' , @password='1234' ,@status=@s output ,@type =@t output 
print @s 
print @t
select * from SystemUser
select * from SportsAssociationManager
drop proc login

go;

create proc Allusers
@username varchar(20),
@stat int output
as
if exists (select * from SystemUser where username=@username)
begin 
set @stat=1
end
else
begin 
set @stat=0
end
go;

create proc checkClubname
@cname varchar(20),
@stat int output
as
if exists (select * from club where name=@cname)
begin 
set @stat=1
end
else
begin 
set @stat=0
end
go;


create proc checkStadiumname
@sname varchar(20),
@stat int output
as
if exists (select * from Stadium where name=@sname)
begin 
set @stat=1
end
else
begin 
set @stat=0
end
go;
declare @r bit
exec checkStadiumname 'sss' , @r output
print @r 
drop proc checkStadiumname
drop proc checkClubname
drop proc Allusers
drop proc login

select * from stadium
select * from systemuser s inner join ClubRepresentative c on s.username=c.username
select * from fan

go;

create proc deleteMatchOnTime  
@hostname varchar(20),
@guestname varchar(20),
@start datetime,
@end datetime
AS 
declare @c1id int , @c2id int 
select @c1id=c1.Id from Club c1 where c1.name=@hostname 
select @c2id=c2.Id from Club c2 where c2.name=@guestname 
delete from Match where club_host=@c1id and club_guest=@c2id and start_time=@start and end_time=@end

go;
exec deleteMatchOnTime 'atletico madrid','real madrid','2022-12-16 20:00:00.000' ,'2022-12-16 22:00:00.000'

select * from match
select * from Ticket
go;

create view Alreadyplayed
as
select (select name from club where id=club_host) as host_club,  (select name from club where id=club_guest) as guest_club , start_time,end_time from Match 
where end_time<CURRENT_TIMESTAMP


go;

create view AllUpcoming
as
select (select name from club where id=club_host) as host_club,  (select name from club where id=club_guest) as guest_club , start_time,end_time from Match 
where start_time>CURRENT_TIMESTAMP

go;
select * from AllUpcoming

select * from ClubRepresentative

go;
create proc clubinfo 
@username varchar(20),
@name varchar(20) output, 
@location VARCHAR(20) output
as
select @name=c.name ,@location=c.location from Club c inner join ClubRepresentative cr on c.Id =cr.club_id where cr.username=@username 
go;
declare @n varchar(20), @l varchar(20)
exec clubinfo  'Ahmed01', @n output , @l output
print @n
print @l

select * from Club
go;

create proc MyClubName
@username varchar(20),
@clubname varchar(20) output 
as
select @clubname=c.name from  Club c, ClubRepresentative cr
where cr.club_id=c.Id and cr.username=@username 
go;
declare @cn varchar(20)
exec MyClubName @username='Ahmed01' ,@clubname=@cn output
print @cn

go;
create view AllUpcoming2
as
select c1.name as host_club,  c2.name guest_club , m.start_time,m.end_time ,s.name as Stadium_name
from Match m inner join club c2 on c2.Id=m.club_guest 
             inner join club c1 on c1.Id=m.club_host
             left outer join Stadium s on s.Id=m.stadium_id 
where m.start_time>CURRENT_TIMESTAMP 

go;
drop view AllUpcoming2
select * from AllUpcoming2 where host_club='real madrid' or guest_club='real madrid'
select * from match ;
select * from Stadium
select * from Host_Request
go;
create proc stadiuminfo 
@username varchar(20),
@name varchar(20) output, 
@location VARCHAR(20) output,
@capacity VARCHAR(20) output,
@status VARCHAR(20) output
as
declare @s bit
declare @stat varchar(20)
select @s=s.status from Stadium s inner join StadiumManager sm on sm.stadium_id = s.Id where @username=sm.username 

if (@s=0) set @stat="Available" 
else set @stat="Unavailable"

select @name=s.name ,@location=s.location , @capacity=s.capacity ,@status=@stat  from Stadium s inner join StadiumManager sm on sm.stadium_id = s.Id 
where @username=sm.username 

go;
declare @n varchar(20), @l varchar(20)
exec clubinfo  'Ahmed01', @n output , @l output
print @n
print @l

select * from Club
go;
create proc stadiuminfo 
@username varchar(20),
@name varchar(20) output, 
@location VARCHAR(20) output,
@capacity VARCHAR(20) output,
@status bit output
as

select @name=s.name ,@location=s.location , @capacity=s.capacity ,@status=s.status  from Stadium s inner join StadiumManager sm on sm.stadium_id = s.Id 
where @username=sm.username 

go;
declare @n varchar(20), @l varchar(20),@c varchar(20),@s bit
exec stadiuminfo  'lolo', @n output , @l output ,@c output,@s output
print @n
print @l
print @c
print @s

select * from StadiumManager 
select * from SystemUser
go;
create function Requests (@username varchar(20))
returns table
as 
return(
    
    select c.name as ClubRepresentative_name , c2.name as Host_club , c1.name as Guest_club ,m.start_time as Start_Time , m.end_time as End_Time , h.status as Status
    from Host_Request h inner join ClubRepresentative c on c.ID =h.rep_id 
                        inner join match m on h.match_id=m.id
                        inner join Club c1 on c1.id = m.club_guest
                        inner join Club c2 on c2.id = m.club_host
                        inner join StadiumManager sm on sm.id = h.manager_id
                        inner join Stadium s on s.Id=sm.stadium_id 
    where  c.club_id=c2.Id and @username=sm.username   
)
go
drop function Requests
select * from dbo.Requests('lolo')
select * from Host_Request
select* from Match 
select * from club
insert into Host_Request values (1,1,11,'accepted')
insert into Host_Request values (1,1,15,'unhandled')
update Host_Request set status='unhandled' where match_id=15 
go;
create proc MyStadiumName
@username varchar(20),
@sname varchar(20) output 
as
select @sname=s.name from  Stadium s, StadiumManager sm
where sm.stadium_id=s.Id and sm.username=@username 
go;
declare @sn varchar(20)
exec MyStadiumName @username='lolo' ,@sname=@sn output
print @sn
go;
create proc acceptRequest2 
@username varchar(20), 
@hostingclubname varchar(20), 
@guestname varchar(20), 
@starttime datetime
As
declare @capacity varchar(20) , @stadiumid int ,@hostid int , @guestid int
update h
set h.status ='accepted'
from Stadium s inner join StadiumManager sm on sm.stadium_id = s.id 
               inner join Host_Request h on h.manager_id = sm.id 
               inner join match m on m.id=h.match_id
               inner JOIN club cg on cg.id = m.club_guest 
               inner join club ch on ch.id = m.club_host
where ch.id<>cg.id and ch.name=@hostingclubname and sm.username = @username and cg.name =@guestname
                    and m.start_time = @starttime and h.status='unhandled'


select @stadiumid=sm.stadium_id 
from StadiumManager sm
where sm.username=@username

select @hostid=id
from club 
where name=@hostingclubname

update match 
set stadium_id=@stadiumid
where club_host=@hostid and start_time=@starttime



select @capacity =s.capacity
from stadium s
where s.id=@stadiumid

declare @i int = 1
while @i <=@capacity
begin 
    exec addTicket @hostingclubname,@guestname,@starttime

    set @i=@i+1
end

go
select * from host_request
select * from match
exec acceptrequest2 'lolo', 'Real madrid', 'sevilla' ,'2023-3-1 8:00';
exec addHostRequest 'Real Madrid', 'Camp nou' ,'2023-3-1 8:00'
go;

create proc checkST
@st datetime ,
@stat int output
as
if exists (select * from match where start_time=@st)
begin 
set @stat=1
end
else
begin 
set @stat=0
end
go;
drop proc checkST
go

create proc rejectRequest2  
@username varchar(20), 
@hostingclubname varchar(20), 
@guestname varchar(20), 
@starttime datetime
AS
update h
set h.status ='rejected'
from 
StadiumManager sm inner join Host_Request h on h.manager_id = sm.id 
inner join match m on m.id = h.match_id 
inner JOIN club cg on cg.id = m.club_guest 
inner join club ch on ch.id = m.club_host
where ch.name=@hostingclubname and cg.name =@guestname and sm.username = @username 
and m.start_time = @starttime and h.status='unhandled';

go;

create proc MyFanID
@username varchar(20),
@id varchar(20) output
as
select @id=national_ID from fan where username=@username

go;

create function availableMatchesToAttend2(@time datetime)  
returns table 
as 
return 
select C1.name as Host_Club , C2.name as Guest_Club,S.name as Stadium_Name, S.location as Stadium_location
from
Club C1 inner join Match M on M.club_host=C1.id
inner join Club C2 on M.club_guest= C2.id
inner join Stadium S on S.Id=M.stadium_id
where M.start_time>=@time and exists
(select count(*) 
from ticket
where match_id=M.id and status=1
having count(*)>0)

go;
use milestone2
select * from dbo.availableMatchesToAttend2('2022-12-12 2:00:00')
select * from match
select * from ticket

select * from SystemUser
select * from fan

select * from Ticket_Buying_Trans
go;
create view availableMatcheswizTickets
 
as 
 
select C1.name as Host_Club , C2.name as Guest_Club,M.start_time as Start_time ,S.name as Stadium_Name
from
Club C1 inner join Match M on M.club_host=C1.id
inner join Club C2 on M.club_guest= C2.id
inner join Stadium S on S.Id=M.stadium_id
where  exists
(select count(*) 
from ticket
where match_id=M.id and status=1
having count(*)>0)

go;

create proc checkmatch
@hostc varchar(20),
@guestc varchar(20),
@st datetime,
@end datetime,
@stat int output
as
if exists (select * from match where (select name from club where id=club_host)=@hostc and (select name from club where id=club_guest)=@guestc and start_time=@st and end_time=@end)
begin 
set @stat=1
end
else
begin 
set @stat=0
end

go;
drop proc checkmatch
select * from match
select * from SportsAssociationManager
select * from SystemUser
select * from club

insert into match values('2022-12-28 20:00:00.000','2022-12-28 22:00:00.000',1,3,2);
insert into match values ('2022-12-28 20:00:00.000','2022-12-28 22:00:00.000',3,1,2);

go;

create function sendR (@clubHost varchar(20))
returns table 
as
return(
select (select name from club where id=m.club_host) as Host ,(select name from club where id= m.club_guest) as Guest,m.start_time,m.end_time 
from Match m inner join club c1 on c1.Id =m.club_host 
               where c1.name=@clubHost and m.stadium_id is null

)

go
select * from dbo.sendR('Real Madrid')
drop function sendR
select * from match 
select * from stadiumManager

select * from ClubRepresentative cr inner join Club c  on cr.club_id=c.id
select * from SystemUser
insert into SystemUser values('Fezoeldakhlawy','12345')
insert into ClubRepresentative values ('Rozy',4,'Fezoeldakhlawy')
select * from ClubRepresentative
select * from Host_Request
go;
create function unhandledR (@smname varchar(20))
returns table 
as
return(
select (select name from club where id=m.club_host) as Host ,(select name from club where id= m.club_guest) as Guest,m.start_time,m.end_time 
from Match m inner join Host_Request h on h.match_id=m.Id
             inner join StadiumManager sm on h.manager_id =sm.ID 
             where  h.status='unhandled' and sm.username=@smname 

)

go
drop function unhandledR
use milestone2
go;
create proc checkfan 
@national varchar(20),
@stat int output
as
if exists (select * from Fan where status=0 and national_ID=@national)
begin 
set @stat=0
end
else
begin
set @stat=1
end
go;
drop proc checkfan 
select * from Systemadmin 
select * from fan

insert into SystemUser values ('Etsh01' ,'Nayera')
insert into Systemadmin values('Sayera','Etsh01')
select * from club
insert into match values ('2023-4-4 8:00:00','2023-4-4 10:00:00', 1,4,null)
select * from Host_Request
select * from match
delete from SystemUser where username='Ziko'
select *  from SystemUser
select * from ClubRepresentative
select * from StadiumManager
select * from Stadium
select * from fan
select * from Match
select * from SportsAssociationManager
delete from club where name='test'
select * from Ticket
select * from Ticket_Buying_Trans
-- 3020445888   1818
insert into Ticket_Buying_Trans values ('3020445888',1608)
insert into Ticket values (1,1009)
insert into Stadium values ('cairo','cairo',5,1)
update match set stadium_id=4006 where club_host=1 and club_guest=3
select * from SportsAssociationManager
delete from SystemUser where username ='dido'
delete from fan where national_ID='3020445888'
go;
create proc checkfan2 
@national varchar(20),
@stat int output
as
if exists (select * from Fan where national_ID=@national)
begin 
set @stat=1
end
else
begin
set @stat=0
end
go;
go;

create function availableMatchesToAttend3(@time datetime)  
returns table 
as 
return 
select C1.name as Host_Club , C2.name as Guest_Club,S.name as Stadium_Name, S.location as Stadium_location
from
Club C1 inner join Match M on M.club_host=C1.id
inner join Club C2 on M.club_guest= C2.id
inner join Stadium S on S.Id=M.stadium_id
where M.start_time=@time and exists
(select count(*) 
from ticket
where match_id=M.id and status=1
having count(*)>0)

go;


create proc deleteStadium2
@name varchar(20)
AS
delete h from Host_Request h inner join match m on h.match_id=m.Id 
                            inner join Stadium s on s.Id=m.stadium_id
                            where s.name=@name 


delete t from Ticket t inner join match m on t.match_id=m.Id inner join Stadium s on s.Id=m.stadium_id
                     where s.name=@name

delete from Stadium where name=@name
go;
drop proc deleteStadium2 
select * from club
select * from Ticket