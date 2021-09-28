alter PROCEDURE [dbo].[GetPriceSortBooks]
	@PriceSort BIT
AS
BEGIN
	if(@PriceSort=0)
		begin
			select * from [Books] order by Price desc;
			end;
	else
		begin
			select * from [Books] order by Price asc;
		end
END