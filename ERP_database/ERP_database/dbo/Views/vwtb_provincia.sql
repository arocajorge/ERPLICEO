CREATE VIEW vwtb_provincia
AS
select isnull(cast(IdProvincia as int),0)IdProvincia from tb_provincia