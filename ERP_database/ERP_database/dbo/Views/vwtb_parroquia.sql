CREATE VIEW vwtb_parroquia
AS
select isnull(cast(IdParroquia as int),0) IdParroquia from tb_parroquia