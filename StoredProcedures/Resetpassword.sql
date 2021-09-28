create PROCEDURE [dbo].[RestPassword]
	@UserId		int,
	@Password		varchar(50)
AS
BEGIN
	UPDATE [User] SET Password = @Password WHERE UserId = @UserId;
END