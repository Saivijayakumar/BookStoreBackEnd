create Table [User]
(
  UserId INT PRIMARY KEY IDENTITY(1,1),
  FullName varchar(50) NOT NULL,
  EmailId varchar(50) NOT NULL,
  Password varchar(50) NOT NULL,
  MobileNumber varchar(50) NOT NULL
);