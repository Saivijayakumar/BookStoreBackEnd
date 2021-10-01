CREATE PROCEDURE [dbo].[RemoveBookFromMyWishList]

	@MyWishListId int

AS
BEGIN
		DELETE From MyWishList where @MyWishListId = MyWishListId;
END