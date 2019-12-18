CREATE FUNCTION [dbo].[BankersRounding](@value decimal(36,11), @significantDigits INT)        
RETURNS MONEY        
AS        
BEGIN        
    -- if value = 12.345 and signficantDigits = 2...        

    -- base = 1000        
    declare @base int = power(10, @significantDigits + 1)        


    -- roundingValue = 12345        
    declare @roundingValue decimal(36,11) = floor(abs(@value) * @base)        
    -- roundingDigit = 5        
    declare @roundingDigit int = @roundingValue % 10        

    -- significantValue = 1234        
    declare @significantValue decimal(36,11) = floor(@roundingValue / 10)        
    -- lastSignificantDigit = 4        
    declare @lastSignificantDigit int = @significantValue % 10        


    -- awayFromZero = 12.35        
    declare @awayFromZero money = (@significantValue + 1) / (@base / 10)        
    -- towardsZero = 12.34        
    declare @towardsZero money = @significantValue / (@base / 10)        

    -- negative values handled slightly different        
    if @value < 0        
    begin        
        -- awayFromZero = -12.35        
        set @awayFromZero = ((-1 * @significantValue) - 1) / (@base / 10)        
        -- towardsZero = -12.34        
        set @towardsZero = (-1 * @significantValue) / (@base / 10)        
    end        

    -- default to towards zero (i.e. assume thousandths digit is 0-4)        
    declare @rv money = @towardsZero        
    if @roundingDigit >= 5        
        set @rv = @awayFromZero  -- 5-9 goes away from 0        
    else if @roundingDigit = 5         
    begin        
        -- 5 goes to nearest even number (towards zero if even, away from zero if odd)        
        set @rv = case when @lastSignificantDigit % 2 = 0 then @towardsZero else @awayFromZero end        
    end        

    return @rv        
	end