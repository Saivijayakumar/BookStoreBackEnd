create PROCEDURE GetMyOrders
	@UserId int
AS
BEGIN

	select MyOrders.UserId, MyOrders.OrderId,MyOrders.OrderDate,MyOrders.TotalCost,Books.Title,
	Books.AuthorName,Books.BookImage,Books.BookId 
	from 
	MyOrders inner join Books on MyOrders.BookId = Books.BookId
	where MyOrders.UserId = 1;
END