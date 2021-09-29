
----------------------User---------------------------

create Table [User]
(
  UserId INT PRIMARY KEY IDENTITY(1,1),
  FullName varchar(50) NOT NULL,
  EmailId varchar(50) NOT NULL,
  Password varchar(50) NOT NULL,
  MobileNumber varchar(50) NOT NULL
);

-------------------UserAddress---------------------------

create Table [UserAddress]
(
  AddressId INT IDENTITY(1,1) Primary KEY ,
  UserId INT NOT NULL,
  Address varchar(250) NOT NULL,
  Type varchar(50) NOT NULL,
  City varchar(50) NOT NULL,
  State varchar(50) NOT NULL,
  FOREIGN KEY (UserId) references  [User](UserId)
);

------------------------Books--------------------

  Create Table Books(
  BookId INT PRIMARY KEY IDENTITY(1,1),
  Title varchar(255) NOT NULL,
  AuthorName varchar(255) NOT NULL,
  Price int NOT NULL,
  Rating int DEFAULT 0,
  BookDetail varchar(max) NOT NULL,
  BookImage varchar(max) NOT NULL,
  BookQuantity int DEFAULT 0 
  )

  -----------------------Cart---------------------------

CREATE TABLE Cart
(
  CartId INT PRIMARY KEY IDENTITY(1,1),
  UserId INT NOT NULL,
  BookId INT NOT NULL,
  BookCount INT NOT NULL,
  TotalCost INT,
   FOREIGN KEY (UserId) references [BookStore].[dbo].[User] (UserId),
  FOREIGN KEY (BookId) references  [BookStore].[dbo].[Books] (BookId)
);

--------------------------MyOrders--------------------

CREATE TABLE MyOrders
(
  OrderId INT PRIMARY KEY IDENTITY(1,1),
  UserId INT NOT NULL,
  BookId INT NOT NULL,
  AddressId INT NOT NULL,
  OrderDate varchar(100) NOT NULL,
  TotalCost INT NOT NULL,
  FOREIGN KEY (UserId) references [BookStore].[dbo].[User] (UserId),
  FOREIGN KEY (BookId) references  [BookStore].[dbo].[Books] (BookId),
  FOREIGN KEY (AddressId) references  [BookStore].[dbo].[UserAddress](AddressId)
);

------------------------------MyWishList----------------------------
 
CREATE TABLE MyWishList
(
  MyWishListId INT PRIMARY KEY IDENTITY(1,1),
  UserId INT NOT NULL,
  BookId INT NOT NULL,
   FOREIGN KEY (UserId) references [BookStore].[dbo].[User] (UserId),
  FOREIGN KEY (BookId) references  [BookStore].[dbo].[Books] (BookId)
);
