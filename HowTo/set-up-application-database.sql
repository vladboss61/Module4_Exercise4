use [Application]

select * from [Car]

insert into dbo.[Product] values ('SuperCar')
insert into dbo.[Product] values ('Phone')

select * from dbo.[Product] as p join dbo.[Company] as c on p.ProductId = c.ProductId 

insert into dbo.[Company] values ('Audi', 120000, GETDATE(), 1)
insert into dbo.[Company] values ('Apple', 120000, GETDATE(), 2)

insert into dbo.[User] values ('FirstName', 'LastName', GETDATE(), 1)
insert into dbo.[User] values ('Vlad', 'Delas', GETDATE(), 1)
insert into dbo.[User] values ('Don', 'Joe', GETDATE(), 2)

select * from dbo.[Product]
select * from dbo.[Company]
select * from dbo.[User]