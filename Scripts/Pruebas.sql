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