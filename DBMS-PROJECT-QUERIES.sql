create database [Hotel Management System]

-----------1st-Table-Customers Table------------
create table Customers
(
	[Customer ID] int primary key identity(10000, 1),
	[Customer First name] nvarchar(MAX),
	[Customer Last name] nvarchar(MAX),
	[Customer Email-Address] nvarchar(MAX),
	[Customer Address] nvarchar(MAX),
	[No. of Peoples] int not null
)
create table [Customer's Phone No.s]
(
	[CPID] int primary key identity(1, 1),
	[Customer ID] int, foreign key([Customer ID]) references Customers([Customer ID]),
	[Customer Phone No.s] bigint unique
)
Select * from Customers
Select * from [Customer's Phone No.s]
-----------Customers Final Table output------------
CREATE VIEW [CUSTOMERS VIEW]
AS
	Select Customers.[Customer ID], [Customer First name], [Customer Last name], [Customer Address], [No. of Peoples], [Customer Phone No.s], [Username], [Password]
	from Customers 
	INNER JOIN [Customer's Phone No.s]
		ON Customers.[Customer ID] = [Customer's Phone No.s].[Customer ID]
GO
Select * from [CUSTOMERS VIEW]
-----------Customers Final Table output------------
-----------Customers Table------------

-----------2nd-Table-Payments Table-------------
create table Payments
(
	[Payment ID] int primary key identity(90000, 1),
	[Customer ID] int, foreign key([Customer ID]) references Customers([Customer ID]),
	[Transaction ID] int, foreign key([Transaction ID]) references Transactions([Transaction ID]),
	[Amount] bigint,
	[Payment Date] Date,
	[No. of Days Stay] bigint not null,
)
Select * from Payments
-----------2nd-Table-Payments Table-------------

-----------3rd-Table-Payments Table-------------
create table Reviews
(
	[RID] int primary key identity(1, 1),
	[Customer ID-Name] int, foreign key([Customer ID-Name]) references Customers([Customer ID]),
	[Review Details] nvarchar(MAX),
	[Review Date] Date
)
Select * from Reviews
-----------Reviews Final Table output------------
CREATE VIEW [CUSTOMERS REVIEWS]
AS
	Select Customers.[Customer ID], [Customer First name], [Customer Last name], Reviews.[Review Details], Reviews.[Review Date] 
	from Customers 
	RIGHT JOIN [Reviews] 
		ON Customers.[Customer ID] = [Reviews].[Customer ID-Name]
GO
Select * from [CUSTOMERS REVIEWS]
-----------Reviews Final Table output------------
-----------3rd-Table-Payments Table-------------


-----------4th-Table-Employees Table-------------
create table Employees
(
	[Employee ID] int primary key identity(2000, 1),
	[Employee First Name] nvarchar(MAX),
	[Employee Last Name] nvarchar(MAX),
	[Username] nvarchar(50),
	[Password] nvarchar(50),
	[Employee Department] nvarchar(MAX),
	[Employee Address] nvarchar(MAX),
	[Employee Salary] money
)
create table [Employees Contact No.s]
(
	ECID int primary key identity(1, 1),
	[Employee ID] int, foreign key([Employee ID]) references Employees([Employee ID]),
	[Employee Contact No.s] bigint unique
)
Select * from Employees
Select * from [Employees Contact No.s]
----------------Employees Final Table output---------
CREATE VIEW [EMPLOYEES VIEW]
AS
	Select Employees.[Employee ID], [Employee First name], [Employee Last name], [Username], [Password], [Employee Department], [Employee Address], [Employee Salary], [Employees Contact No.s].[Employee Contact No.s]
	from Employees
	INNER JOIN [Employees Contact No.s]
		ON Employees.[Employee ID] = [Employees Contact No.s].[Employee ID]
GO
Select * from [EMPLOYEES VIEW]
----------------Employees Final Table output---------
-----------4th-Table-Employees Table-------------


-----------5th-Table-Transactions Table-------------
create table Transactions
(
	[Transaction ID] int primary key identity(1, 1),
	[Transaction Name] nvarchar(50),
	[Customer ID] int, foreign key([Customer ID]) references Customers([Customer ID]),
	[Employee ID] int, foreign key([Employee ID]) references Employees([Employee ID]),
	[Transaction Date] Date
)
-----------5th-Table-Transactions Table-------------


-----------6th-Table-Room Table-------------
create table Rooms
(
	[Room No] int primary key identity(100, 1),
	[Room Category] nvarchar(MAX),
	[Rent] bigint unique,
	[Reservation ID] int, foreign key([Reservation ID]) references Reservations([Reservation ID])
)
Select * from Rooms
----------------Transaction Final Table output for slip---------
CREATE VIEW [TRANSACTIONS VIEW]
AS
	Select Transactions.[Transaction ID],Transactions.[Transaction Name],Transactions.[Transaction Date], Transactions.[Employee ID], Transactions.[Customer ID], Payments.[Amount], Payments.[No. of Days Stay]
	from Transactions
	LEFT JOIN [Payments] 
		ON Transactions.[Transaction ID]=[Payments].[Transaction ID]
GO

Select * from [TRANSACTIONS VIEW]
Select * from Payments
Select * from Transactions
----------------Transaction Final Table output for slip---------
-----------7th-Table-Reservation Table------
create table Reservations
(
	[Reservation ID] int primary key identity(1000, 1),
	[Transaction ID] int, foreign key([Transaction ID]) references Transactions([Transaction ID]),
	[Customer ID] int, foreign key([Customer ID]) references Customers([Customer ID]),
	[Date In] Date,
	[Date Out] Date,
	[Days/Interval of Stay] bigint
)
Select * from Reservations
select * from Customers
Select * from Rooms
----------------Reservation Final Table output for slip---------
CREATE VIEW [RESERVATIONS VIEW]
AS
	Select Reservations.[Reservation ID], Reservations.[Transaction ID], Reservations.[Customer ID], Reservations.[Date In], Reservations.[Date Out], Reservations.[Days/Interval of Stay], Rooms.[Room No], Rooms.[Room Category]
	from Reservations
	RIGHT JOIN Rooms
		ON Reservations.[Reservation ID] = Rooms.[Reservation ID]
GO

Select * from [RESERVATIONS VIEW]
----------------Reservation Final Table output for slip---------

---------------Check-Constraint------------------
alter table Reservations add constraint CKC_Reservations_IOS
check(([Days/Interval of Stay] IS NOT Null) and ([Days/Interval of Stay] != 0) and ([Days/Interval of Stay] > 0))

alter table Customers add constraint NOT_ZERO_CKC_Customers_NOP
check(([No. of Peoples] IS NOT NULL) and ([No. of Peoples] != 0) and ([No. of Peoples] > 0))
---------------Check-Constraint------------------




--------------Cascade-Referential-Integrity-------------------------Cascade-Referential-Integrity-------------------------Cascade-Referential-Integrity-----------
---------------------------Cascade-Referential-Integrity-of-on-delete-cascade------------------------Cascade-Referential-Integrity-of-on-delete-cascade-----------
--------------ON-CUSTOMER-ID's-----------
alter table [Customer's Phone No.s] add constraint CRI_FK_customer_id
foreign key ([Customer ID]) references [Customers]([Customer ID])on delete cascade

alter table [Payments] add constraint CRI_ON_PAYMENTS_FK_CUSTOMER_ID
foreign key ([Customer ID]) references [Customers]([Customer ID])on delete cascade

alter table [Reservations] add constraint CRI_ON_RESERVATIONS_FK_CUSTOMER_ID
foreign key ([Customer ID]) references [Customers]([Customer ID])on delete cascade

alter table [Reviews] add constraint CRI_ON_REVIEWS_FK_CUSTOMER_ID
foreign key ([Customer ID-Name]) references [Customers]([Customer ID])on delete cascade
--------------ON-CUSTOMER-ID's-----------
--------------ON-EMPLOYEE-ID's-----------
alter table [Employees Contact No.s] add constraint CRI_ON_EMPLOYEECONTACTNOS_FK_EMPLOYEE_ID
foreign key ([Employee ID]) references [Employees]([Employee ID])on delete cascade
--------------ON-EMPLOYEE-ID's-----------
---------------------------Cascade-Referential-Integrity-of-on-delete-cascade------------------------Cascade-Referential-Integrity-of-on-delete-cascade-----------


---------------------------Cascade-Referential-Integrity-of-on-delete-set-null-----------------------Cascade-Referential-Integrity-of-on-delete-set-null----------
--------------ON-RESERVATION-ID's-----------
alter table [Rooms] add constraint CRI_ON_ROOMS_FK_RESERVATION_ID
foreign key ([Reservation ID]) references [Reservations]([Reservation ID]) on delete set null
--------------ON-RESERVATION-ID's-----------
--------------ON-TRANSACTION-ID's-----------
alter table [Reservations] add constraint CRI_ON_RESERVATIONS_FK_TRANSACTION_ID
foreign key ([Transaction ID]) references [Transactions]([Transaction ID]) on delete set null
--------------ON-TRANSACTION-ID's-----------
--------------ON-CUSTOMER-ID's--------------
alter table [Transactions] add constraint CRI_ON_TRANSACTIONS_FK_CUSTOMER_ID
foreign key ([Customer ID]) references [Customers]([Customer ID])on delete set null
--------------ON-CUSTOMER-ID's--------------
--------------ON-EMPLOYEE-ID's--------------
alter table [Transactions] add constraint CRI_ON_TRANSACTIONS_FK_EMPLOYEE_ID
foreign key ([Employee ID]) references [Employees]([Employee ID]) on delete set null
--------------ON-EMPLOYEE-ID's--------------
---------------------------Cascade-Referential-Integrity-of-on-delete-set-null-----------------------Cascade-Referential-Integrity-of-on-delete-set-null----------
--------------Cascade-Referential-Integrity-------------------------Cascade-Referential-Integrity-------------------------Cascade-Referential-Integrity-----------




--------------Stored-Procedure------------------------
create procedure sp_IOS
@Transaction_ID int,
@Customer_ID int,
@Date_In date,
@Date_Out date
as
begin
		insert into Reservations values (@Transaction_ID, @Customer_ID, @Date_In, @Date_Out, DATEDIFF(DAY, @Date_In, @Date_Out))
End

exec sp_IOS @Transaction_ID = 2, @Customer_ID = 10003, @Date_In = '2022-09-25', @Date_Out = '2022-10-06'
exec sp_IOS @Transaction_ID = 2, @Customer_ID = 10003, @Date_In = '2022-09-06', @Date_Out = '2022-09-28'
Select * from Reservations
--------------Stored-Procedure------------------------

---------------------SQL-Triggers--------------------------------
create trigger tr_customers
on Customers
after insert 
as
begin
select * from inserted
end
---------------------SQL-Triggers--------------------------------

-----------------------Table-Selection-Fetching-Queries--------------
Select * from Customers
Select * from [Customer's Phone No.s]
Select * from [CUSTOMERS VIEW]

Select * from Payments

Select * from Reviews
Select * from [CUSTOMERS REVIEWS]

Select * from Employees
Select * from [Employees Contact No.s]
Select * from [EMPLOYEES VIEW]

Select * from Rooms

Select * from Transactions
Select * from [TRANSACTIONS VIEW]

Select * from Reservations
Select * from [RESERVATIONS VIEW]
-----------------------Table-Selection-Fetching-Queries--------------

---------------Later-Might-Useable-Queries----------------------
ALTER TABLE Customers DROP COLUMN [No. of Peoples]
ALTER TABLE Customers ADD [Username] nvarchar(MAX)
ALTER TABLE Customers ADD [Password] nvarchar(MAX)
ALTER TABLE Rooms DROP constraint UQ__Rooms__DC85F39AE874F43D
ALTER TABLE Reservations DROP constraint CKC_Reservations_IOS
---------------Later-Might-Useable-Queries----------------------


--------------UML-Diagram-Query-------------------------UML-Diagram-Query-------------------------UML-Diagram-Query-----------
SELECT 
FROM     Payments INNER JOIN
                  Customers INNER JOIN
                  [Customer's Phone No.s] ON Customers.[Customer ID] = [Customer's Phone No.s].[Customer ID] AND Customers.[Customer ID] = [Customer's Phone No.s].[Customer ID] ON Payments.[Customer ID] = Customers.[Customer ID] AND 
                  Payments.[Customer ID] = Customers.[Customer ID] INNER JOIN
                  Reservations ON Customers.[Customer ID] = Reservations.[Customer ID] AND Customers.[Customer ID] = Reservations.[Customer ID] INNER JOIN
                  Reviews ON Customers.[Customer ID] = Reviews.[Customer ID-Name] AND Customers.[Customer ID] = Reviews.[Customer ID-Name] INNER JOIN
                  Rooms ON Reservations.[Reservation ID] = Rooms.[Reservation ID] AND Reservations.[Reservation ID] = Rooms.[Reservation ID] INNER JOIN
                  Transactions ON Payments.[Transaction ID] = Transactions.[Transaction ID] AND Customers.[Customer ID] = Transactions.[Customer ID] AND Customers.[Customer ID] = Transactions.[Customer ID] AND 
                  Reservations.[Transaction ID] = Transactions.[Transaction ID] AND Reservations.[Transaction ID] = Transactions.[Transaction ID] INNER JOIN
                  [Employees Contact No.s] INNER JOIN
                  Employees ON [Employees Contact No.s].[Employee ID] = Employees.[Employee ID] AND [Employees Contact No.s].[Employee ID] = Employees.[Employee ID] ON Transactions.[Employee ID] = Employees.[Employee ID] AND 
                  Transactions.[Employee ID] = Employees.[Employee ID]
--------------UML-Diagram-Query-------------------------UML-Diagram-Query-------------------------UML-Diagram-Query-----------