CREATE EXTENSION IF NOT EXISTS pgcrypto;

--Funcion para encriptar un password
CREATE OR REPLACE FUNCTION encriptar_password()
RETURNS TRIGGER AS $$
BEGIN
  NEW.password := md5(NEW.password);
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION encriptar_passwords(passwords_ TEXT)
RETURNS TEXT AS $$
BEGIN
  RETURN md5(passwords_);
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION validar_reservacion_unica()
RETURNS TRIGGER AS $$
BEGIN
    IF EXISTS (
        SELECT 1
        FROM reservacion
        WHERE numcama = NEW.numcama
        AND fechaingreso = NEW.fechaingreso
    ) THEN
        RAISE EXCEPTION 'No se puede generar una reservación con el mismo num_cama y fecha_ingreso';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

--Triggers 
CREATE TRIGGER encriptar_password_paciente
BEFORE INSERT ON paciente
FOR EACH ROW
EXECUTE FUNCTION encriptar_password();

CREATE TRIGGER encriptar_password_personal
BEFORE INSERT ON personal
FOR EACH ROW
EXECUTE FUNCTION encriptar_password();

CREATE TRIGGER encriptar_password_personal_update
BEFORE UPDATE OF password
ON personal
FOR EACH ROW
EXECUTE FUNCTION encriptar_password();

CREATE TRIGGER trigger_validar_reservacion_unica
BEFORE INSERT ON reservacion
FOR EACH ROW
EXECUTE FUNCTION validar_reservacion_unica();


--Procedimientos Almacenados

CREATE OR REPLACE PROCEDURE crear_personal(
	nombre VARCHAR = null,
	ap1 VARCHAR = null,
	ap2 VARCHAR = null,
	cedula INT = null,
	direccion VARCHAR = null,
	nacimiento DATE = null,
	correo VARCHAR = null,
	password VARCHAR = null,
	fechaingreso DATE = null,
	idtipopersonal INT = null
)
LANGUAGE plpgsql
AS $$
BEGIN
	INSERT INTO personal
	VALUES(nombre,ap1,ap2,cedula,direccion,nacimiento,correo,password,fechaingreso,idtipopersonal);
END;
$$;

CREATE OR REPLACE PROCEDURE update_personal(
	u_nombre VARCHAR = null,
	u_ap1 VARCHAR = null,
	u_ap2 VARCHAR = null,
	u_cedula INT = null,
	u_direccion VARCHAR = null,
	u_nacimiento DATE = null,
	u_correo VARCHAR = null,
	u_password VARCHAR = null,
	u_fechaingreso DATE = null,
	u_idtipopersonal INT = null
)
LANGUAGE plpgsql
AS $$
BEGIN
	UPDATE personal
	SET nombre = u_nombre,ap1 = u_ap1,ap2 = u_ap2,direccion = u_direccion,
	nacimiento = u_nacimiento,correo = u_correo,password = u_password,
	fechaingreso = u_fechaingreso,idtipopersonal = u_idtipopersonal
	WHERE cedula = u_cedula;
END;
$$;


CREATE OR REPLACE PROCEDURE eliminar_personal(
	ced_elimin INT = null
	
)
LANGUAGE plpgsql
AS $$
BEGIN
	DELETE FROM personal
	WHERE ced_elimin = cedula;
END;
$$;

CREATE OR REPLACE PROCEDURE crear_reservacion(
	fechaingreso date = null,
	cedpaciente int = null,
	idprocmed int = null,
	numcama int = null
)
LANGUAGE plpgsql
AS $$
BEGIN
	INSERT INTO reservacion
	values(default,fechaingreso,cedpaciente,idprocmed,numcama);
END;
$$;

