Create PROCEDURE [dbo].[ForgotPassword]
	@EmailId varchar(50)
AS
BEGIN
	
	if((select count(EmailId) from [User] where EmailId = @EmailId) = 1)
		begin;
		select UserId,EmailId from [User] where EmailId = @EmailId;
		end	
END
