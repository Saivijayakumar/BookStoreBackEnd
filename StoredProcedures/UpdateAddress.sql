create PROCEDURE [dbo].[UpdateAddress]
    @AddressId  int,
	@Address		varchar(50),
	@Type		varchar(50),
	@City varchar(50),
	@State varchar(50)
AS
BEGIN
UPDATE UserAddress set Address = @Address, Type = @Type,
City = @City,State=@State where AddressId = @AddressId;
END
