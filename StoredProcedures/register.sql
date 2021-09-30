alter PROCEDURE [dbo].[Registration]
	@FullName		varchar(50),
	@EmailId		varchar(50),
	@Password		varchar(50),
	@MobileNumber   bigint,
	 @result int output
AS
BEGIN
if((select count(*) from [User] where EmailId = @EmailId ) = 0)
begin
  Insert into [User](FullName,EmailId,Password,MobileNumber)
	VALUES(@FullName,@EmailId,@Password,@MobileNumber);
	set @result =1;
	end
else
begin
   set @result=0;
   end
END