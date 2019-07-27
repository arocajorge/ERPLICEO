CREATE VIEW vwtb_ciudad_id
AS
select  isnull(cast(IdCiudad as int),0) as IdCiudad from tb_ciudad