Create PROCEDURE [dbo].[AddAddress]
	@UserId		INT,
	@Address    varchar(50),
	@Type		varchar(50),
	@City		varchar(50),
	@State	    varchar(50)
AS
BEGIN
Insert into [UserAddress] (UserId,Address,Type,City,State)
	VALUES(@UserId,@Address,@Type,@City,@State);
END