Create PROCEDURE [dbo].[GetAllUserAddress]
	@UserId INT
AS
BEGIN
	
		select * from [UserAddress] where UserId = @UserId;
END