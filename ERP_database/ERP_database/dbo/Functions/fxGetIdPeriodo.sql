
CREATE FUNCTION [dbo].[fxGetIdPeriodo]
(
	@Fecha date
)
returns int
AS
BEGIN
	declare @Retorno int = cast(YEAR(@Fecha) as varchar(4)) + right('00'+cast(MONTH(@Fecha) as varchar(2)),2);
	return @Retorno;
END