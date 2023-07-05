Create Database LastHopeDatabase
go
Use LastHopeDatabase

Create Table UserAccount ( 
	ID int identity(1,1) PRIMARY KEY,
	Phone nvarchar(50) NOT NULL, 
	Password nvarchar(150) NOT NULL,
	Fullname nvarchar(200),
	DayOfBirth Date,
	Gender bit,
	Email nvarchar(150),
	[Address] nvarchar(255),
	CitizenID nvarchar(50),
	DateJoin Date,
	[Status] int,
	RoleUser int NOT NULL,
)

Create Table Building (
	ID int identity(1,1) PRIMARY KEY, 
	Name nvarchar(200),
	[Address] nvarchar(255), 
	[Status] int,
	Capacity int,
)

Create Table FlatType (
	ID int identity(1,1) PRIMARY KEY, 
	Name nvarchar(200),
	Description nvarchar(255),
)

Create Table Flat (
	ID int identity(1,1) PRIMARY KEY,
	Detail nvarchar(255),
	Price decimal,
	[Status] int, 
	BuildingID int FOREIGN KEY REFERENCES Building(ID),
	FlatTypeID int FOREIGN KEY REFERENCES FlatType(ID), 
	RoomNumber int, 
)

Create Table RentContract (
	ID int identity(1,1) PRIMARY KEY,
	CustomerID int FOREIGN KEY REFERENCES UserAccount(ID),
	FlatID int FOREIGN KEY REFERENCES Building(ID), 
	[Value] decimal,
	StartDate Date,
	ExpiryDate Date,
	[Status] int,
	Contract nvarchar(MAX),
	Title nvarchar(200),
)

Create Table Term (
	ID int identity(1,1) PRIMARY KEY,
	RentContractID int FOREIGN KEY REFERENCES RentContract(ID),
	Title nvarchar(200),
	Content nvarchar(255),
)

Create Table Bill (
	ID int identity(1,1) PRIMARY KEY,
	RentContractID int FOREIGN KEY REFERENCES RentContract(ID),
	[Date] Date,
	[Value] decimal,
	[Status] int,
	Sender nvarchar(200),
	Receiver nvarchar(200),
	Content nvarchar(255),
	[Type] int
)

Create Table [Service] (
	ID int identity(1,1) PRIMARY KEY,
	Code nvarchar(100),
	Name nvarchar(200),
	Description nvarchar(255),
	Price decimal,
	[Status] int,
)

Create Table BillItem (
	BillID int FOREIGN KEY REFERENCES Bill(ID),
	ServiceID int FOREIGN KEY REFERENCES [Service](ID),
	Quantity int,
	[Value] decimal,
	Primary key(BillID,ServiceID)
)

