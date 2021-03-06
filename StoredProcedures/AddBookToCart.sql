USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[AddBookToCart]    Script Date: 29-09-2021 12:28:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[AddBookToCart]
	@UserId int,
	@BookId int
	AS
BEGIN

	DECLARE @BookPrice int = 0;
	DECLARE @BookCount int =1;
	set @BookPrice= (select Price from Books where BookId = @BookId); 
	if((select count(BookId) from [Cart] where BookId = @BookId and UserId=@UserId) = 1)
	BEGIN	   
		update Cart set BookCount = BookCount +1,
	   TotalCost = TotalCost +@BookPrice
	   where BookId = @BookId and UserId = @UserId;
	END

	else
	begin
		      insert into Cart(UserId, BookId, TotalCost,BookCount) 
				values(@UserId, @BookId, @BookPrice,@BookCount);
	end

END