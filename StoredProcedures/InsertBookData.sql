ALTER PROCEDURE [dbo].[InsertBookData]

	@Title		varchar(255),
	@AuthorName		varchar(255),
	@Price		int,
	@Rating   int,
	@BookDetail varchar(max),
	@BookImage varchar(max),
	@BookQuantity int
AS
BEGIN
Insert into [Books] (Title,AuthorName,Price,Rating,BookDetail,BookImage,BookQuantity)
	VALUES(@Title,@AuthorName,@Price,@Rating,@BookDetail,@BookImage,@BookQuantity);
END

 