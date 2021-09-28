create Table [User]
(
  UserId INT PRIMARY KEY IDENTITY(1,1),
  FullName varchar(50) NOT NULL,
  EmailId varchar(50) NOT NULL,
  Password varchar(50) NOT NULL,
  MobileNumber varchar(50) NOT NULL
);

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