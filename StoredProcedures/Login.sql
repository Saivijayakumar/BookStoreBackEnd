USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 28-09-2021 07:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Login]
	@EmailId varchar(50),
	@Password varchar(50)
AS
BEGIN
	
	if((select count(EmailId) from [User] where EmailId = @EmailId) = 1)
		begin;
		select * from [User] where EmailId = @EmailId;
		end	
END
