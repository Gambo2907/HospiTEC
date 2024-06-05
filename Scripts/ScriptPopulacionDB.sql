INSERT INTO TIPO_PERSONAL
VALUES(DEFAULT,'Doctor');

INSERT INTO TIPO_PERSONAL
VALUES(DEFAULT,'Enfermero');

INSERT INTO TIPO_PERSONAL
VALUES(DEFAULT,'Administrativo');

INSERT INTO TIPO_MEDICINA
VALUES(1,'Hombres');

INSERT INTO TIPO_MEDICINA
VALUES(2,'Mujeres');

INSERT INTO TIPO_MEDICINA
VALUES(3,'Niños');

INSERT INTO ESTADO_CAMA
VALUES(1,'Disponible');

INSERT INTO ESTADO_CAMA
VALUES(2,'Reservado');

INSERT INTO EQUIPO_MEDICO
VALUES(1,'Luces Quirurgicas','Kalstein',50);

INSERT INTO EQUIPO_MEDICO
VALUES(2,'Ultrasonido','Eleinmsa',45);

INSERT INTO EQUIPO_MEDICO
VALUES(3,'Esterilizadores','Eleinmsa',25);

INSERT INTO EQUIPO_MEDICO
VALUES(4,'Desfibriladores','Eleinmsa',25);

INSERT INTO EQUIPO_MEDICO
VALUES(5,'Monitores','Eleinmsa',50);

INSERT INTO EQUIPO_MEDICO
VALUES(6,'Respiradores Artificiales','Eleinmsa',75);

INSERT INTO EQUIPO_MEDICO
VALUES(7,'Electrocardiografos','Eleinmsa',65);

INSERT INTO PROCEDIMIENTO_MEDICO
VALUES(1,'Apendicectomia',7);

INSERT INTO PROCEDIMIENTO_MEDICO
VALUES(2,'Biopsia de Mama',5);

INSERT INTO PROCEDIMIENTO_MEDICO
VALUES(3,'Cirugia de Cataratas',2);

INSERT INTO PROCEDIMIENTO_MEDICO
VALUES(4,'Cesarea',3);

INSERT INTO PROCEDIMIENTO_MEDICO
VALUES(5,'Histerectomia',6);

INSERT INTO PROCEDIMIENTO_MEDICO
VALUES(6,'Cirugia para la Lumbalgia',12);

INSERT INTO PROCEDIMIENTO_MEDICO
VALUES(7,'Mastectomia',5);

INSERT INTO PROCEDIMIENTO_MEDICO
VALUES(8,'Amigdalectomia',1);

INSERT INTO patologia
VALUES(DEFAULT, 'Rinitis Cronica');

INSERT INTO patologia
VALUES(DEFAULT, 'Dermatitis');

INSERT INTO patologia
VALUES(DEFAULT, 'Psoriasis');

INSERT INTO patologia
VALUES(DEFAULT, 'Acne');

INSERT INTO patologia
VALUES(DEFAULT, 'Alopecia');

INSERT INTO patologia
VALUES(DEFAULT, 'Amigdalitis');

INSERT INTO patologia
VALUES(DEFAULT, 'Hipertensión');

INSERT INTO patologia
VALUES(DEFAULT, 'Asma');

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

INSERT INTO equipo_por_cama
values(1,1);
INSERT INTO equipo_por_cama
values(1,4);
INSERT INTO equipo_por_cama
values(2,7);
INSERT INTO equipo_por_cama
values(1,5);
INSERT INTO equipo_por_cama
values(3,3);

