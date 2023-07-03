Create Database LastHopeDatabase
go
Use LastHopeDatabase

Create Table UserAccount ( 
	ID int PRIMARY KEY,
	Phone nvarchar(50), 
	Password nvarchar(150),
	Fullname nvarchar(200),
	DayOfBirth Date,
	Gender bit,
	Email nvarchar(150),
	[Address] nvarchar(255),
	CitizenID nvarchar(50),
	DateJoin Date,
	[Status] int,
	RoleUser int,
)

Create Table Building (
	ID int PRIMARY KEY, 
	Name nvarchar(200),
	[Address] nvarchar(255), 
	[Status] int,
	Capacity int,
)

Create Table FlatType (
	ID int PRIMARY KEY, 
	Name nvarchar(200),
	Description nvarchar(255),
)

Create Table Flat (
	ID int PRIMARY KEY,
	Detail nvarchar(255),
	Price decimal,
	[Status] int, 
	BuildingID int FOREIGN KEY REFERENCES Building(ID),
	FlatTypeID int FOREIGN KEY REFERENCES FlatType(ID), 
	RoomNumber int, 
)

Create Table RentContract (
	ID int PRIMARY KEY,
	CustomerID int FOREIGN KEY REFERENCES UserAccount(ID),
	FlatID int FOREIGN KEY REFERENCES Building(ID), 
	[Value] decimal,
	StartDate Date,
	ExpiryDate Date,
	[Status] int,
	Contract nvarchar(100),
	Title nvarchar(200),
)

Create Table Term (
	ID int PRIMARY KEY,
	RentContractID int FOREIGN KEY REFERENCES RentContract(ID),
	Title nvarchar(200),
	Content nvarchar(255),
)

Create Table Bill (
	ID int PRIMARY KEY,
	RentContractID int FOREIGN KEY REFERENCES RentContract(ID),
	[Date] Date,
	[Value] decimal,
	[Status] int,
	Sender nvarchar(200),
	Receiver nvarchar(200),
	Content nvarchar(255),
)
Create Table [Service] (
	ID int PRIMARY KEY,
	Code nvarchar(100),
	Name nvarchar(200),
	Description nvarchar(255),
	Price decimal,
	[Status] int,
)
Create Table BillItem (
	ID int PRIMARY KEY,
	BillID int FOREIGN KEY REFERENCES Bill(ID),
	ServiceID int FOREIGN KEY REFERENCES [Service](ID),
	Quantity int,
	[Value] decimal,
)

