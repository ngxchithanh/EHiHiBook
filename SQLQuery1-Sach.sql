CREATE DATABASE Sach
GO
USE Sach

GO

CREATE TABLE Category
(
	CategoryID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	CateoryName NVARCHAR(50) NULL,
)

CREATE TABLE Supplier
(
	SupplierID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	SupplierName NVARCHAR(50) NULL,
	Addr NVARCHAR(255) NULL,
	Email NVARCHAR(255) NULL,
	Phone NVARCHAR(10) NULL
)

CREATE TABLE Publisher
(
	PublisherID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	PublisherName NVARCHAR(50) NULL,
	Info NVARCHAR(255) NULL
)

CREATE TABLE Book
(
	BookID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	BookName NVARCHAR(255) NULL,
	Price DECIMAL(18) NULL,
	Author NVARCHAR(50) NULL,
	Descriptions NVARCHAR(500) NULL,
	Images NVARCHAR(50) NULL,
	Quantity INT NULL,
	SupplierID INT NULL,
	PublisherID INT NULL,
	CategoryID INT NULL,
	FOREIGN KEY (CategoryID) REFERENCES Category (CategoryID),
    FOREIGN KEY (PublisherID) REFERENCES Publisher (PublisherID),
    FOREIGN KEY (SupplierID) REFERENCES Supplier (SupplierID)
)

CREATE TABLE MembershipType
(
	MembershipTypeID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	MembershipTypeName NVARCHAR(50) NULL,

)

CREATE TABLE Membership
(
	MembershipID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	UserName NVARCHAR(100) NULL,
	Pass NVARCHAR(100) NULL,
	FirstName NVARCHAR(100) NULL,
	LastName NVARCHAR(100) NULL,
	Addr1 NVARCHAR(255) NULL,
	Addr2 NVARCHAR(255) NULL,
	Email NVARCHAR(255) NULL,
	Phone NVARCHAR(10) NULL,
	Birthday DATETIME NULL,
	Questions NVARCHAR(500) NULL,
	Answers NVARCHAR(500) NULL,
	MembershipTypeID INT NULL,
	CumulativePoints INT NULL,
	FOREIGN KEY (MembershipTypeID) REFERENCES MembershipType (MembershipTypeID) ON DELETE CASCADE
)

CREATE TABLE Users
(
    UserID INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    UserName NVARCHAR(100) NULL,
	Pass NVARCHAR(100) NULL,
	FirstName NVARCHAR(100) NULL,
	LastName NVARCHAR(100) NULL,
    Addr NVARCHAR(255) NULL,
	Email NVARCHAR(255) NULL,
	Phone NVARCHAR(10) NULL,
	Birthday DATETIME NULL,
	NgaySinh DATETIME NULL,
	MembershipID INT NULL,
    FOREIGN KEY (MembershipID) REFERENCES Membership (MembershipID) ON DELETE CASCADE
)

CREATE TABLE Orders (
	OrderID INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    PurchaseDay DATETIME NULL,
    OrderStates BIT NULL, /* Da giao (True) Chua giao (False)*/
    DeliveryDay DATETIME NULL,
    Paid BIT NULL, /* Da thanh toan (True) Chua thanh toan (False)*/
	AddrUser NVARCHAR(100) NULL,
    UserID INT NULL,
    FOREIGN KEY (UserID) REFERENCES Users (UserID) ON DELETE CASCADE
)

CREATE TABLE OrderDetail(
    OrderDetailID INT IDENTITY (1, 1) PRIMARY KEY NOT NULL,
    BookID INT NULL,
    OrderID INT NULL,
    BookName NVARCHAR (255) NULL,
    Amount INT NULL,
    Price DECIMAL (18) NULL,
    FOREIGN KEY (BookID) REFERENCES Book (BookID),
    FOREIGN KEY (OrderID) REFERENCES Orders (OrderID)
)


CREATE TABLE Comment (
    CommentID INT IDENTITY (1, 1) PRIMARY KEY NOT NULL,
    Content   NVARCHAR (255) null,
    MembershipID INT NULL,
    BookID INT NULL,
    FOREIGN KEY (BookID) REFERENCES Book (BookID),
    FOREIGN KEY (MembershipID) REFERENCES Membership (MembershipID)
)

ALTER TABLE Users
   ADD ConfirmPassword NVARCHAR (100) NULL

ALTER TABLE Category
   ADD IsActive BIT NULL

ALTER TABLE Category
   ADD IsDelete BIT NULL

   ALTER TABLE Book
   ADD IsActive BIT NULL

ALTER TABLE Book
   ADD IsDelete BIT NULL

   ALTER TABLE Category
   ADD IsFeatured BIT NULL

CREATE TABLE CartStatus
(
	CartStatusID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	CartStatus NVARCHAR(100) NULL
)

CREATE TABLE Cart
(
	CartID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	BookID INT NULL,
	MembershipID INT NULL,
	CartStatusID INT NULL,
	FOREIGN KEY (MembershipID) REFERENCES Membership (MembershipID),
	FOREIGN KEY (CartStatusID) REFERENCES CartStatus (CartStatusID)
)

CREATE TABLE ShippingDetail
(
	ShippingDetailID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	MembershipID INT NULL,
	Addr NVARCHAR(255) NULL,
	City NVARCHAR(100) NULL,
	ZipCode NVARCHAR(10) NULL,
	OrderID INT NULL,
	AmountPaid DECIMAL(18) NULL,
	PaymentType NVARCHAR(50) NULL,
	FOREIGN KEY (MembershipID) REFERENCES Membership (MembershipID),
	FOREIGN KEY (OrderID) REFERENCES Orders (OrderID)
)
   ALTER TABLE Book
   ADD IsFeatured BIT NULL