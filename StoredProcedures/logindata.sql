alter PROCEDURE [dbo].[Login]
	@EmailId varchar(50),
	@Password varchar(50)
AS
BEGIN
BEGIN TRANSACTION;
	DECLARE @result int=5;
	if((select count(EmailId) from [User] where EmailId = @EmailId) = 1)
		begin;
		set @result = 6;
		end
	if((select count(EmailId) from [User] where EmailId = @EmailId and Password = @Password) = 1)
	begin;
		set @result = 3;
	end
COMMIT TRANSACTION
return @result;
END
