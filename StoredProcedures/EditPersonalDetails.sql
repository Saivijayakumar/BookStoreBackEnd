Alter PROCEDURE [dbo].[EditPersonalDetails]
	@UserId		int,
	@FullName	varchar(50),
	@EmailId	varchar(50),
	@Password	varchar(50),
	@MobileNumber   varchar(50)
AS
BEGIN
      if( (select count(*) from [User] where UserId = @UserId) = 1)
		BEGIN
			UPDATE [User] SET FullName=@FullName,EmailId=@EmailId,Password=@Password,MobileNumber=@MobileNumber
			where  UserId =@UserId;
			select * from [User] where UserId = @UserId;
		END
END