CREATE VIEW [dbo].[vwAf_Catalogo_IdAuto_numeric]
AS
SELECT     RIGHT('00000' + CAST(ISNULL(MAX(CAST(IdCatalogo AS numeric)), 0) + 1 AS VARCHAR(4)), 4) AS IdCatalogo, '' AS observacion
FROM         dbo.fa_catalogo
WHERE     (ISNUMERIC(IdCatalogo) <> 0)