alter PROCEDURE [dbo].[Registration]
	@FullName		varchar(50),
	@EmailId		varchar(50),
	@Password		varchar(50),
	@MobileNumber   bigint
AS
BEGIN
Insert into [User](FullName,EmailId,Password,MobileNumber)
	VALUES(@FullName,@EmailId,@Password,@MobileNumber);
END