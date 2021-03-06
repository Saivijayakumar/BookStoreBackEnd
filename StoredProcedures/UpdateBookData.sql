USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateBookData]    Script Date: 28-09-2021 15:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateBookData]
    @BookId  int,
	@Title		varchar(255),
	@AuthorName		varchar(255),
	@Price		int,
	@Rating   int,
	@BookDetail varchar(max),
	@BookImage varchar(max),
	@BigBookImage varchar(max),
	@BookQuantity int
AS
BEGIN
	if((select count(*) from [Books] where BookId = @BookId) = 1)
	begin
		UPDATE Books set Title = @Title, AuthorName = @AuthorName,
	  Price = @Price,
	  Rating = @Rating, BookDetail = @BookDetail, BookImage = @BookImage,BigImage = @BigBookImage,
	 BookQuantity=@BookQuantity
	 where BookId = @BookId;
	 select * from [Books] where BookId=@BookId;
	end;

END
