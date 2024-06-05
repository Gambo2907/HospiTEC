SELECT s.numsalon, s.nombre,s.piso,tp.descripcion as TipoMedicina, count(*) as CantCamas
FROM salon as s, tipo_medicina as tp, cama as c 
WHERE s.id_tipo_medicina = tp.id AND s.numsalon = c.num_salon
GROUP BY s.numsalon,s.nombre,s.piso,tp.descripcion
Order BY s.numsalon

select * from cama
	
Insert into salon
values(1,'Salon1',2,1);

Insert into salon
values(2,'Salon2',2,3);

Insert into cama
values(1,FALSE,1,1);

Insert into cama
values(2,FALSE,1,1);

Insert into cama
values(3,FALSE,1,1);

Insert into cama
values(4,false,1,2);

select c.numcama, c.uci, c.num_salon from reservacion as r
	RIGHT JOIN cama as c on r.numcama = c.numcama
	Where r.fechaingreso IS NULL OR r.fechaingreso != '2021-07-29'

SELECT 
    r.fechaingreso, r.cedpaciente, r.numcama,p.nombre, 
    r.fechaingreso + INTERVAL '1 day' * p.cantdias AS fecha_salida
FROM reservacion r
JOIN procedimiento_medico p ON r.idprocmed = p.id
WHERE cedpaciente = 2;

call crear_reservacion('2021-07-29',1,2,2);

delete from reservacion