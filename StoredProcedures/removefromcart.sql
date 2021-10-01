Create PROCEDURE [dbo].[RemoveBookFromCart]
	@CartId int

AS
BEGIN
    DELETE  from Cart where  CartId=@CartId;
END