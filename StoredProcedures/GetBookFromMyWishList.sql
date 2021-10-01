CREATE PROCEDURE [dbo].[GetBookFromMyWishList]
	@UserId int
AS
BEGIN
	select MyWishList.UserId, MyWishList.MyWishListId, MyWishList.BookId,
	Books.Title,Books.BookDetail,Books.AuthorName,
	Books.BookImage,Books.BookQuantity,Books.Price,Books.Rating
	from 
	MyWishList inner join Books on MyWishList.BookId = Books.BookId
	where MyWishList.UserId = @UserId;
END