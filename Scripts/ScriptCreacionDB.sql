-- Database: HospiTEC

-- DROP DATABASE IF EXISTS "HospiTEC";

CREATE DATABASE "HospiTEC"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Spanish_Spain.1252'
    LC_CTYPE = 'Spanish_Spain.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;


CREATE TABLE TIPO_PERSONAL(
	ID SERIAL PRIMARY KEY,
	Descripcion VARCHAR(20) NOT NULL
);

CREATE TABLE ESTADO_CAMA(
	ID SERIAL PRIMARY KEY,
	Estado VARCHAR(20) NOT NULL
);

CREATE TABLE TIPO_MEDICINA(
	ID SERIAL PRIMARY KEY,
	Descripcion VARCHAR(20) NOT NULL
);



CREATE TABLE PROCEDIMIENTO_MEDICO(
	ID SERIAL PRIMARY KEY,
	Nombre VARCHAR(100) UNIQUE NOT NULL,
	CantDias INT NOT NULL
);

CREATE TABLE PERSONAL(
	Nombre VARCHAR(100) NOT NULL,
	AP1 VARCHAR(100) NOT NULL,
	AP2 VARCHAR(100) NOT NULL,
	Cedula INT UNIQUE NOT NULL,
	Direccion VARCHAR(200),
	Nacimiento DATE NOT NULL,
	Correo VARCHAR(100) NOT NULL,
	Password VARCHAR(100) NOT NULL,
	FechaIngreso DATE NOT NULL,
	IDTipoPersonal INT NOT NULL,
	PRIMARY KEY(Cedula),
	CONSTRAINT "key1" FOREIGN KEY(IDTipoPersonal) REFERENCES TIPO_PERSONAL(ID)
);

CREATE TABLE TELEFONOS_PERSONAL(
	ID SERIAL NOT NULL PRIMARY KEY,
	PersonalCedula INT NOT NULL,
	Telefono VARCHAR(20) NOT NULL,
	CONSTRAINT "key2" FOREIGN KEY(PersonalCedula) REFERENCES PERSONAL(Cedula)
);

CREATE TABLE Paciente(
	Nombre VARCHAR(100) NOT NULL,
	AP1 VARCHAR(100) NOT NULL,
	AP2 VARCHAR(100) NOT NULL,
	Cedula INT UNIQUE NOT NULL,
	Nacimiento DATE NOT NULL,
	Direccion VARCHAR(200),
	Correo VARCHAR(100) NOT NULL,
	Password VARCHAR(100) NOT NULL,
	PRIMARY KEY(Cedula)
);

CREATE TABLE TELEFONOS_PACIENTE(
	ID SERIAL PRIMARY KEY,
	PacienteCedula INT NOT NULL,
	Telefono VARCHAR(20) NOT NULL,
	CONSTRAINT "key3" FOREIGN KEY(PacienteCedula) REFERENCES Paciente(Cedula)
);

CREATE TABLE PATOLOGIA(
	ID SERIAL PRIMARY KEY,
	Nombre VARCHAR(50) UNIQUE NOT NULL,
	Tratamiento VARCHAR(200) NOT NULL
);

CREATE TABLE PATOLOGIA_POR_PACIENTE(
	ID_Patologia INT NOT NULL,
	CedulaPaciente INT NOT NULL,
	PRIMARY KEY(ID_Patologia,CedulaPaciente),
	CONSTRAINT "key4" FOREIGN KEY(ID_Patologia) REFERENCES PATOLOGIA(ID),
	CONSTRAINT "key5" FOREIGN KEY(CedulaPaciente) REFERENCES PACIENTE(Cedula)
);


CREATE TABLE HISTORIAL_MEDICO(
	ID SERIAL NOT NULL,
	Fecha DATE NOT NULL,
	Tratamiento VARCHAR(200) NOT NULL,
	CedulaPaciente INT NOT NULL,
	ID_Procedimiento INT NOT NULL,
	PRIMARY KEY(ID,CedulaPaciente),
	CONSTRAINT "key6" FOREIGN KEY(ID_Procedimiento) REFERENCES PROCEDIMIENTO_MEDICO(ID)
);

CREATE TABLE Salon(
	NumSalon INT UNIQUE NOT NULL PRIMARY KEY,
	Nombre VARCHAR(100) NOT NULL,
	Piso INT NOT NULL,
	Capacidad INT NOT NULL,
	ID_Tipo_Medicina INT NOT NULL,
	CONSTRAINT "key7" FOREIGN KEY(ID_Tipo_Medicina) REFERENCES TIPO_MEDICINA(ID)
);


CREATE TABLE CAMA(
	NumCama INT UNIQUE NOT NULL PRIMARY KEY,
	UCI BIT NOT NULL,
	ID_EstadoCama INT NOT NULL,
	Num_Salon INT NOT NULL,
	CONSTRAINT "key8" FOREIGN KEY(ID_EstadoCama) REFERENCES ESTADO_CAMA(ID),
	CONSTRAINT "key9" FOREIGN KEY(Num_Salon) REFERENCES SALON(NumSalon)
);



CREATE TABLE EQUIPO_MEDICO(
	ID SERIAL PRIMARY KEY,
	Nombre VARCHAR(100) UNIQUE NOT NULL,
	Proveedor VARCHAR(100) NOT NULL,
	CantDisponible INT NOT NULL
);

CREATE TABLE EQUIPO_POR_CAMA(
	Num_Cama INT NOT NULL,
	ID_EquipoMedico INT NOT NULL,
	PRIMARY KEY(Num_Cama, ID_EquipoMedico),
	CONSTRAINT "key13" FOREIGN KEY(Num_Cama) REFERENCES CAMA(NumCama),
	CONSTRAINT "key14" FOREIGN KEY(ID_EquipoMedico) REFERENCES EQUIPO_MEDICO(ID)
);

CREATE TABLE RESERVACION(
	ID SERIAL NOT NULL PRIMARY KEY,
	FechaIngreso DATE NOT NULL,
	FechaSalida DATE NOT NULL,
	CedPaciente INT NOT NULL,
	IDProcMed INT NOT NULL,
	CONSTRAINT "key11" FOREIGN KEY(CedPaciente) REFERENCES PACIENTE(Cedula),
	CONSTRAINT "key12" FOREIGN KEY(IDProcMed) REFERENCES PROCEDIMIENTO_MEDICO(ID)
);