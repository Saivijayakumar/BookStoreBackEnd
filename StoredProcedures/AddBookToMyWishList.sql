Alter PROCEDURE [dbo].[AddBookToMyWishList]
	@UserId int,
	@BookId int
	AS
BEGIN

	if((select count(BookId) from [MyWishList] where BookId = @BookId and UserId=@UserId) = 0)
	BEGIN	   
		insert into MyWishList(UserId, BookId) values(@UserId, @BookId);
	END
END