create PROCEDURE [dbo].[AddOrder]
	@UserId int,
	@BookId int,
	@AddressId int,
	@OrderDate varchar(20),
	@TotalCost int
	AS
BEGIN
insert into MyOrders(UserId, BookId, AddressId,OrderDate,TotalCost) 
				values(@UserId, @BookId,@AddressId,@OrderDate,@TotalCost);
END