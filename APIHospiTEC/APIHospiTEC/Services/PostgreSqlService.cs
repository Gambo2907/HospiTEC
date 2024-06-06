using APIHospiTEC.Data;
using APIHospiTEC.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIHospiTEC.Services
{
    public class PostgreSqlService
    {
        private readonly DataAccessLayer _dataAccess;

        public PostgreSqlService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSqlConnection");
            _dataAccess = new DataAccessLayer(connectionString);
        }

        //Obtiene los procedimientos medicos que hay en la db 
        public async Task<List<Dictionary<string, object>>> GetProcedimientosMedicosAsync()
        {
            var query = @"SELECT pm.id,pm.nombre,pat.nombre as patologia,pm.cantdias
                FROM procedimiento_medico as pm
                JOIN patologia as pat ON pm.idpatologia = pat.id; ";
            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        //Inserta un procedimiento medico en la tabla procedimientos_medicos en la db 
        public async Task<int> InsertProcedimientoMedicoAsync(ProcedimientoMedico procedimiento)
        {
            var query = @"INSERT INTO
                        procedimiento_medico
                        VALUES(@id,@nombre,@idpat,@cantdias);";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@id", procedimiento.id),
            new NpgsqlParameter("@nombre", procedimiento.nombre),
            new NpgsqlParameter("@idpat", procedimiento.idpatologia),
            new NpgsqlParameter("@cantdias", procedimiento.cantdias),
            };
            return  await _dataAccess.ExecuteNonQueryAsync(query,parameters);
            
        }

        public async Task<Dictionary<string, object>> GetProcedimientoMedicoPorIDAsync(int id)
        {
            var query = @"SELECT pm.id,pm.nombre,pat.nombre as patologia,pm.cantdias
                FROM procedimiento_medico as pm
                JOIN patologia as pat ON pm.idpatologia = pat.id
                WHERE pm.id = @id; ";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@id", id)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }
        public async Task<int> UpdateProcedimientoMedicoAsync(ProcedimientoMedicoParaPut procedimiento, int id)
        {
            var query = @"UPDATE procedimiento_medico 
                SET nombre = @nombre,idpatologia = @idpat,cantdias = @cantdias 
                WHERE id = @id;";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@id", id),
            new NpgsqlParameter("@nombre",procedimiento.nombre),
            new NpgsqlParameter("@idpat", procedimiento.idpatologia),
            new NpgsqlParameter("@cantdias", procedimiento.cantdias)
            };
            return await _dataAccess.ExecuteNonQueryAsync(query, parameters);
        }
        public async Task<List<Dictionary<string, object>>> GetHistorialPorCedulaAsync(int cedula)
        {
            var query = @"
                SELECT hm.id,hm.fecha,hm.tratamiento,hm.cedulapaciente,t.nombre
                FROM historial_medico as hm
                JOIN procedimiento_medico as t ON hm.id_procedimiento = t.id
                WHERE hm.cedulapaciente = @cedula;
            ";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@cedula", cedula)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }


        //Inserta un paciente en la tabla paciente de la db 
        public async Task<int> InsertPacienteAsync(string nombre, string ap1, string ap2, int cedula, DateOnly nacimiento,
            string? direccion, string correo, string password)
        {
            var query = $"INSERT INTO " +
                $"paciente " +
                $"VALUES ('{nombre}','{ap1}','{ap2}','{cedula}','{nacimiento}','{direccion}','{correo}','{password}')";
            return await _dataAccess.ExecuteNonQueryAsync(query);

        }

        //Obtiene los pacientes que hay en la db 
        public async Task<List<Dictionary<string, object>>> GetPacientesAsync()
        {
            var query = "SELECT nombre,ap1,ap2,cedula,nacimiento,direccion,correo " +
                "FROM paciente";
            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<Dictionary<string, object>> GetPacientePorCedulaAsync(int cedula)
        {
            var query = $"SELECT nombre,ap1,ap2,cedula,nacimiento,direccion,correo " +
                $"FROM paciente WHERE cedula = @cedula";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@cedula", cedula)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }


        public async Task<int> InsertHistorialAsync(Historial historial)
        {
            var query = $"INSERT INTO " +
                $"historial_medico (fecha,tratamiento,cedulapaciente,id_procedimiento) " +
                $"VALUES ('{historial.fecha}','{historial.tratamiento}','{historial.cedulapaciente}','{historial.id_procedimiento}')";
            return await _dataAccess.ExecuteNonQueryAsync(query);
        }

        //Obtiene el historial de pacientes que hay en la db 
        public async Task<List<Dictionary<string, object>>> GetHistorialAsync()
        {
            var query = @"
                SELECT hm.id,hm.fecha,hm.tratamiento,hm.cedulapaciente,t.nombre
                FROM historial_medico as hm
                JOIN procedimiento_medico as t ON hm.id_procedimiento = t.id;
            ";
            
            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        
        public async Task<int> UpdateHistorialAsync(HistorialParaPut historial, int id)
        {
            var query = "UPDATE historial_medico " +
                "SET  fecha = @fecha, tratamiento = @tratamiento, id_procedimiento = @procedimiento " +
                "WHERE id = @id";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@fecha", historial.fecha),
            new NpgsqlParameter("@tratamiento", historial.tratamiento),
            new NpgsqlParameter("@procedimiento", historial.id_procedimiento),
            new NpgsqlParameter("@id", id)
            };
            return await _dataAccess.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<int> InsertSalonAsync(int numsalon, string nombre, int? piso, int tipo_de_medicina)
        {
            var query = $"INSERT INTO " +
                $"salon " +
                $"VALUES ('{numsalon}','{nombre}','{piso}','{tipo_de_medicina}')";
            return await _dataAccess.ExecuteNonQueryAsync(query);

        }

        public async Task<List<Dictionary<string, object>>> GetSalonesAsync()
        {
            var query = @"
                SELECT s.numsalon, s.nombre, s.piso, tp.descripcion AS TipoMedicina, COUNT(c.numcama) AS CantCamas
                FROM salon AS s
                JOIN tipo_medicina AS tp ON s.id_tipo_medicina = tp.id
                LEFT JOIN cama AS c ON s.numsalon = c.num_salon
                GROUP BY s.numsalon, s.nombre, s.piso, tp.descripcion
                ORDER BY s.numsalon;
            ";
            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<Dictionary<string, object>?> GetSalonPorNumSalonAsync(int numsalon)
        {
            var query = @"
                SELECT s.nombre, s.piso, tp.descripcion AS TipoMedicina, COUNT(c.numcama) AS CantCamas
                FROM salon AS s
                JOIN tipo_medicina AS tp ON s.id_tipo_medicina = tp.id
                LEFT JOIN cama AS c ON s.numsalon = c.num_salon
                WHERE s.numsalon = @numsalon
                GROUP BY s.nombre, s.piso, tp.descripcion;
            ";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@numsalon", numsalon)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }

        public async Task<int> UpdateSalonAsync(SalonParaPut salon, int numsalon)
        {
            var query = "UPDATE salon " +
                "SET nombre = @nombre, piso = @piso, id_tipo_medicina = @id_tipo_medicina " +
                "WHERE numsalon = @numsalon";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@numsalon", numsalon),
            new NpgsqlParameter("@nombre", salon.nombre),
            new NpgsqlParameter("@piso", salon.piso),
            new NpgsqlParameter("@id_tipo_medicina", salon.id_tipo_medicina)
            };
            return await _dataAccess.ExecuteNonQueryAsync(query, parameters);
        }
        public async Task<int> DeleteSalonAsync(int numsalon)
        {
            var query = @"DELETE FROM salon
                        WHERE numsalon = @numsalon;";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@numsalon", numsalon),
            };
            return await _dataAccess.ExecuteNonQueryAsync(query, parameters);
        }

        //Logins

        public async Task<Dictionary<string, object>?> LoginPacienteAsync(string? correo,string? password)
        {
            var query = @"
                SELECT nombre,ap1,ap2,cedula,nacimiento,direccion,correo
                FROM paciente
                WHERE correo = @correo AND password = @password;
            ";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@correo", correo),
            new NpgsqlParameter("@password", password)

            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }

        public async Task<Dictionary<string, object>?> LoginAdminAsync(string? correo, string? password)
        {
            var query = @"
                SELECT nombre,ap1,ap2,cedula,direccion,nacimiento,correo,fechaingreso
                FROM personal
                WHERE correo = @correo AND password = @password AND idtipopersonal = 3;
            ";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@correo", correo),
            new NpgsqlParameter("@password", password)

            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }
        public async Task<Dictionary<string, object>?> LoginDoctorAsync(string? correo, string? password)
        {
            var query = @"
                SELECT nombre,ap1,ap2,cedula,direccion,nacimiento,correo,fechaingreso
                FROM personal
                WHERE correo = @correo AND password = @password AND idtipopersonal = 1 ;
            ";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@correo", correo),
            new NpgsqlParameter("@password", password)

            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return dataTable.Rows.Count > 0 ? ConvertDataRowToDictionary(dataTable.Rows[0]) : null;
        }

        public async Task<int> InsertPersonalAsync(Personal personal)
        {
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@nombre",personal.nombre),
            new NpgsqlParameter("@ap1",personal.ap1),
            new NpgsqlParameter("@ap2",personal.ap2),
            new NpgsqlParameter("@cedula",personal.cedula),
            new NpgsqlParameter("@direccion",personal.direccion),
            new NpgsqlParameter("@nacimiento",personal.nacimiento),
            new NpgsqlParameter("@correo",personal.correo),
            new NpgsqlParameter("@password",personal.password),
            new NpgsqlParameter("@fechaingreso",personal.fechaingreso),
            new NpgsqlParameter("@idtipopersonal",personal.idtipopersonal),
            };
            return await _dataAccess.CallStoredProcedureAsync("crear_personal", parameters);
        }

        public async Task<List<Dictionary<string, object>>> GetPersonalAsync()
        {
            var query = @"
                SELECT p.nombre, p.ap1, p.ap2, p.cedula, p.direccion, p.nacimiento, p.correo, p.fechaingreso,
                tpe.descripcion as TipoPersonal
                FROM personal AS p
                JOIN tipo_personal as tpe ON p.idtipopersonal = tpe.id;
            ";
            
            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<List<Dictionary<string, object>>> GetPersonalPorCedulaAsync(int cedula)
        {
            var query = @"
                SELECT p.nombre, p.ap1, p.ap2, p.cedula, p.direccion, p.nacimiento, p.correo, p.fechaingreso,
                tpe.descripcion as TipoPersonal
                FROM personal AS p
                JOIN tipo_personal as tpe ON p.idtipopersonal = tpe.id
                WHERE @cedula = p.cedula;
            ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@cedula", cedula)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query,parameters);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<int> UpdatePersonalAsync(PersonalParaPut personal, int cedula)
        {
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@u_nombre",personal.nombre),
            new NpgsqlParameter("@u_ap1",personal.ap1),
            new NpgsqlParameter("@u_ap2",personal.ap2),
            new NpgsqlParameter("@u_cedula",cedula),
            new NpgsqlParameter("@u_direccion",personal.direccion),
            new NpgsqlParameter("@u_nacimiento",personal.nacimiento),
            new NpgsqlParameter("@u_correo",personal.correo),
            new NpgsqlParameter("@u_password",personal.password),
            new NpgsqlParameter("@u_fechaingreso",personal.fechaingreso),
            new NpgsqlParameter("@u_idtipopersonal",personal.idtipopersonal),
            };
            return await _dataAccess.CallStoredProcedureAsync("update_personal", parameters);
        }

        public async Task<int> DeletePersonalAsync(int cedula)
        {
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@ced_elimin",cedula),
            };
            return await _dataAccess.CallStoredProcedureAsync("eliminar_personal", parameters);
        }


        // Método para convertir DataTable a List<Dictionary<string, object>>
        public List<Dictionary<string, object>> ConvertDataTableToList(DataTable dataTable)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in dataTable.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn column in dataTable.Columns)
                {
                    dict[column.ColumnName] = row[column];
                }

                list.Add(dict);
            }

            return list;
        }

        public async Task<List<Dictionary<string, object>>> GetTipoPersonalAsync()
        {
            var query = @"
                SELECT *
	            FROM tipo_personal;
            ";

            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<List<Dictionary<string, object>>> GetTipoPersonalPoridAsync(int id)
        {
            var query = @"
                SELECT *
                FROM tipo_personal
                WHERE @id = id;
            ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id", id)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<int> InsertCamaAsync(Cama cama)
        {
            var query = $"INSERT INTO cama (numcama,uci,id_estadocama,num_salon) " +
                $"VALUES ('{cama.numcama}','{cama.uci}','{1}','{cama.num_salon}')";
            return await _dataAccess.ExecuteNonQueryAsync(query);

        }
        public async Task<List<Dictionary<string, object>>> GetCamasAsync()
        {
            var query = @"
                SELECT c.numcama, c.uci, c.num_salon
                FROM cama as c
                ORDER BY c.numcama;
            ";

            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }
        public async Task<List<Dictionary<string, object>>> GetCamaPorNumCamaAsync(int numcama)
        {
            var query = @"
                SELECT c.numcama, c.uci,  c.num_salon
                FROM cama as c
                WHERE @numcama = c.numcama;
            ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@numcama", numcama)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }
        public async Task<List<Dictionary<string, object>>> GetCamasPorNumSalonAsync(int numsalon)
        {
            var query = @"
                SELECT c.numcama, c.uci, c.num_salon 
                FROM cama as c
                WHERE @numsalon = c.num_salon;
            ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@numsalon", numsalon)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }
        
            
        public async Task<List<Dictionary<string, object>>> GetCamasDisponiblesPorFechaAsync(DateOnly fecha)
        {
            var query = @"
                select c.numcama, c.uci, c.num_salon from reservacion as r
	            RIGHT JOIN cama as c on r.numcama = c.numcama
	            Where r.fechaingreso IS NULL OR r.fechaingreso != @fecha;
            ";
            var parameters = new NpgsqlParameter[]
           {
                new NpgsqlParameter("@fecha", fecha)
           };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query,parameters);
            return ConvertDataTableToList(dataTable);
        }
        public async Task<List<Dictionary<string, object>>> GetEquipoPorCamaAsync(int numcama)
        {
            var query = @"
                SELECT em.nombre
                FROM equipo_por_cama as epc
                JOIN cama as c ON epc.num_cama = c.numcama
                JOIN estado_cama as ec ON c.id_estadocama = ec.id
                JOIN equipo_medico as em ON epc.id_equipomedico = em.id
                WHERE @numcama = epc.num_cama;
            ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@numcama", numcama)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<int> UpdateCamaAsync(CamaParaPut cama, int numcama)
        {
            var query = "UPDATE cama " +
                "SET  uci = @uci, num_salon = @num_salon " +
                "WHERE numcama = @numcama";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@numcama", numcama),
            new NpgsqlParameter("@uci", cama.uci),
            new NpgsqlParameter("@num_salon", cama.num_salon)
            };
            return await _dataAccess.ExecuteNonQueryAsync(query, parameters);
        }
        public async Task<int> InsertPatologiaAsync(Patologia modelo)
        {
            var query = $"INSERT INTO " +
            $"patologia" +
                $"VALUES ('{modelo.id}','{modelo.nombre}')";
            return await _dataAccess.ExecuteNonQueryAsync(query);

        }
        public async Task<List<Dictionary<string, object>>> GetPatologiasAsync()
        {
            var query = @"
                SELECT *
                FROM patologia;
            ";

            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }
        public async Task<List<Dictionary<string, object>>> GetPatologiaPorIdAsync(int id)
        {
            var query = @"
                SELECT *
                FROM patologia
                WHERE @id = id;
            ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id", id)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<int> InsertPatologiaPorPacienteAsync(int id_patologia,int cedulapaciente, string tratamiento)
        {
            var query = @"
                        INSERT INTO 
                        patologia_por_paciente
                        VALUES (@id_patologia,@cedulapaciente,@tratamiento);";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@id_patologia", id_patologia),
            new NpgsqlParameter("@cedulapaciente", cedulapaciente),
            new NpgsqlParameter("@tratamiento", tratamiento)
            };
            return await _dataAccess.ExecuteNonQueryAsync(query,parameters);

        }
        public async Task<List<Dictionary<string, object>>> GetPatologiasPorPacienteAsync(int cedula)
        {
            var query = @"
                SELECT p.nombre, pu.tratamiento
                FROM patologia as p
                JOIN patologia_por_paciente as pu on p.id = pu.id_patologia
                WHERE @cedulapaciente = pu.cedulapaciente;
            ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@cedulapaciente", cedula)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }

        public async Task<int> InsertTelefonoPacienteAsync(Telefonos modelo)
        {
            var query = $"INSERT INTO " +
            $"telefonos_paciente (pacientecedula,telefono)" +
                $"VALUES ('{modelo.cedula}','{modelo.telefono}')";
            return await _dataAccess.ExecuteNonQueryAsync(query);

        }
        public async Task<List<Dictionary<string, object>>> GetTelefonosPorPacienteAsync(int cedula)
        {
            var query = @"
                SELECT t.telefono
                FROM telefonos_paciente as t
                WHERE @cedula = t.pacientecedula;
            ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@cedula", cedula)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }
        public async Task<List<Dictionary<string, object>>> GetTelefonosPorPersonalAsync(int cedula)
        {
            var query = @"
                SELECT t.telefono
                FROM telefonos_personal as t
                WHERE @cedula = t.personalcedula;
            ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@cedula", cedula)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }
        public async Task<int> InsertTelefonoPersonalAsync(Telefonos modelo)
        {
            var query = $"INSERT INTO " +
            $"telefonos_personal (personalcedula , telefono)" +
                $"VALUES ('{modelo.cedula}','{modelo.telefono}')";
            return await _dataAccess.ExecuteNonQueryAsync(query);

        }
        public async Task<int> InsertEquipoPorCamaAsync(EquipoPorCama modelo)
        {
            var query = @"INSERT INTO
                         equipo_por_cama
                        VALUES(@numcama,@idequipo);";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@numcama", modelo.num_cama),
                 new NpgsqlParameter("@idequipo", modelo.id_equipomedico)
            };
            return await _dataAccess.ExecuteNonQueryAsync(query,parameters);

        }
        public async Task<int> InsertEquipoAsync(Equipo modelo)
        {
            var query = $"INSERT INTO " +
            $"equipo_medico (id,nombre , proveedor,cantdisponible)" +
                $"VALUES ('{modelo.id}','{modelo.nombre}','{modelo.proveedor}','{modelo.cantdisponible}')";
            return await _dataAccess.ExecuteNonQueryAsync(query);

        }
        public async Task<List<Dictionary<string, object>>> GetEquipoPorIdAsync(int id)
        {
            var query = @"
                SELECT *
                FROM equipo_medico as em
                WHERE @id = em.id;
            ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id",id )
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }
        public async Task<List<Dictionary<string, object>>> GetEquiposMedicosAsync()
        {
            var query = @"
                SELECT *
                FROM equipo_medico;
            ";

            var dataTable = await _dataAccess.ExecuteQueryAsync(query);
            return ConvertDataTableToList(dataTable);
        }
        public async Task<int> InsertReservacionAsync(Reservacion reservacion)
        {
            var query = @"
                        CALL crear_reservacion(@fechaingreso,@cedpaciente,@idprocmed,@numcama);";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@fechaingreso", reservacion.fechaingreso),
            new NpgsqlParameter("@cedpaciente", reservacion.cedpaciente),
            new NpgsqlParameter("@idprocmed", reservacion.idprocmed),
            new NpgsqlParameter("@numcama", reservacion.numcama)
            };
            return await _dataAccess.ExecuteNonQueryAsync(query, parameters);

        }
        public async Task<int> UpdateEquipoAsync(EquipoParaPut modelo, int id)
        {
            var query = @"UPDATE equipo_medico
                        SET nombre = @nombre, proveedor = @proveedor, cantdisponible = @cant
                        WHERE @id = id;";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@id", id),
            new NpgsqlParameter("@nombre", modelo.nombre),
            new NpgsqlParameter("@proveedor", modelo.proveedor),
            new NpgsqlParameter("@cant", modelo.cantdisponible)
            };
            return await _dataAccess.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<List<Dictionary<string, object>>> GetReservacionesPorCedulaAsync(int cedula)
        {
            var query = @"
                SELECT r.id,
                r.fechaingreso, r.cedpaciente, r.numcama,p.nombre, 
                r.fechaingreso + INTERVAL '1 day' * p.cantdias AS fecha_salida
                FROM reservacion r
                JOIN procedimiento_medico p ON r.idprocmed = p.id
                WHERE cedpaciente = @cedula;
                ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@cedula", cedula)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }
        public async Task<List<Dictionary<string, object>>> GetReservacionesPorFechaAsync(DateOnly fecha,int cedula)
        {
            var query = @"
                SELECT r.id,
                r.fechaingreso, r.cedpaciente, r.numcama,p.nombre, 
                r.fechaingreso + INTERVAL '1 day' * p.cantdias AS fecha_salida
                FROM reservacion r
                JOIN procedimiento_medico p ON r.idprocmed = p.id
                WHERE cedpaciente = @cedula AND r.fechaingreso = @fecha;
                ";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@cedula", cedula),
                new NpgsqlParameter("@fecha", fecha)
            };
            var dataTable = await _dataAccess.ExecuteQueryAsync(query, parameters);
            return ConvertDataTableToList(dataTable);
        }
        public async Task<int> UpdateReservacionAsync(ReservacionParaPut modelo, int id)
        {
            var query = @"UPDATE reservacion
                         SET fechaingreso = @fecha, idprocmed = @procmed, numcama = @numcama
                        WHERE @id = id;";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@id", id),
            new NpgsqlParameter("@fecha", modelo.fechaingreso),
            new NpgsqlParameter("@procmed", modelo.idprocmed),
            new NpgsqlParameter("@numcama", modelo.numcama)
            };
            return await _dataAccess.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<int> DeleteReservacionAsync(int id)
        {
            var query = @"CALL eliminar_reservacion(@id);";
            var parameters = new NpgsqlParameter[]
            {
            new NpgsqlParameter("@id", id)
            };
            return await _dataAccess.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<string> EncryptPassword(string? password)
        {
            // Llamar a la función en la base de datos para encriptar la contraseña
            var query = "SELECT encriptar_passwords(@password)";
            var parameters = new NpgsqlParameter("@password", password);
            var encryptedPassword = await _dataAccess.ExecuteScalarAsync<string>(query, parameters);
            return encryptedPassword;
        }

        // Método para convertir DataRow a Dictionary<string, object>
        private Dictionary<string, object> ConvertDataRowToDictionary(DataRow row)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn column in row.Table.Columns)
            {
                dict[column.ColumnName] = row[column];
            }

            return dict;
        }
        public async Task ImportarPacientesAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No se ha subido ningún archivo.");
            }

            var filePath = Path.GetTempFileName() + ".xlsx";
            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            await ImportExcelAsync(filePath);

            // Ejecutar el procedimiento almacenado
            await _dataAccess.ExecuteStoredProcedureAsync("transferir_pacientes_desde_temporal");
        }
        private async Task ImportExcelAsync(string filePath)
        {
            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);

            var dataTable = new DataTable();
            dataTable.Columns.Add("nombre", typeof(string));
            dataTable.Columns.Add("ap1", typeof(string));
            dataTable.Columns.Add("ap2", typeof(string));
            dataTable.Columns.Add("cedula", typeof(int));
            dataTable.Columns.Add("nacimiento", typeof(DateTime));
            dataTable.Columns.Add("direccion", typeof(string));
            dataTable.Columns.Add("correo", typeof(string));
            dataTable.Columns.Add("password", typeof(string));

            var telefonoTable = new DataTable();
            telefonoTable.Columns.Add("cedula", typeof(int));
            telefonoTable.Columns.Add("telefono1", typeof(string));
            telefonoTable.Columns.Add("telefono2", typeof(string));

            foreach (var row in worksheet.RowsUsed().Skip(1))
            {
                var newRow = dataTable.NewRow();
                newRow["nombre"] = row.Cell(1).GetValue<string>();
                newRow["ap1"] = row.Cell(2).GetValue<string>();
                newRow["ap2"] = row.Cell(3).GetValue<string>();
                newRow["cedula"] = row.Cell(4).GetValue<int>();
                newRow["nacimiento"] = row.Cell(5).GetValue<DateTime>();
                newRow["direccion"] = row.Cell(6).GetValue<string>();
                newRow["correo"] = row.Cell(7).GetValue<string>();
                newRow["password"] = row.Cell(8).GetValue<string>();
                dataTable.Rows.Add(newRow);

                var telRow = telefonoTable.NewRow();
                telRow["cedula"] = row.Cell(4).GetValue<int>();
                telRow["telefono1"] = row.Cell(9).GetValue<string>();
                telRow["telefono2"] = row.Cell(10).GetValue<string>();
                telefonoTable.Rows.Add(telRow);
            }

            await _dataAccess.BulkInsertAsync("temp_paciente", dataTable);
            await _dataAccess.BulkInsertAsync("temp_telefonos_paciente", telefonoTable);
        }

    }
}
