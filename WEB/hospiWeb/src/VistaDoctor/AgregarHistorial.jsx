import React, { useState, useEffect } from 'react';
import { Link, useNavigate, useParams } from 'react-router-dom';
import axios from 'axios';

const AgregarHistorial = () => {
  // Valores a enviar
  const { id } = useParams(); // Obtiene el ID del activo de los parámetros de la URL
  const [values, setValues] = useState({
    fecha: null,
    tratamiento: '',
    cedulapaciente: id,
    id_procedimiento: 0,
  });

  const [procedimientos, setProcedimientos] = useState([]);

  const navigate = useNavigate();

  // Fetch procedimientos médicos desde la API cuando el componente se monte
  useEffect(() => {
    axios.get('http://localhost:5197/api/ProcedimientoMedico/procedimientos_medicos')
      .then(res => {
        setProcedimientos(res.data);
      })
      .catch(err => console.log(err));
  }, []);




  // Funcion al presionar aceptar
  const handleSubmit = (event) => {
    event.preventDefault();
    axios.post('https://localhost:7061/api/Historial/registrar_historial', values)
      .then(res => {
        console.log(res);
        navigate('/doctor/verhistorial/'+id);
      })
      .catch(err => console.log(err));
  };

  return (
    // Formulario para llenar los datos necesarios para crear al activo
    <div className='d-flex w-100 vh-100 justify-content-center align-items-center bg-light'>
      <div className='w-50 border bg-white shadow px-5 pt-3 pb-5 rounded'>
        <h1>AGREGAR HISTORIAL</h1>
        <form onSubmit={handleSubmit}>
          <div className='mb-2'>
            <label htmlFor='fecha'>Fecha del procedimiento</label>
            <input type='text' name='fecha' className='form-control' placeholder='Ingrese la fecha de realizacion'
              onChange={e => setValues({ ...values, fecha: e.target.value })} />
          </div>
          <div className='mb-2'>
            <label htmlFor='id_procedimiento'>Procedimiento Médico Realizado</label>
            <select name='id_procedimiento' className='form-control' onChange={e => setValues({ ...values, id_procedimiento: e.target.value })}>
              <option value=''>Seleccione un procedimiento</option>
              {procedimientos.map(proc => (
                <option key={proc.id} value={proc.id}>
                  {proc.nombre}
                </option>
              ))}
            </select>
          </div>
          <div className='mb-2'>
            <label htmlFor='tratamiento'>Tratamiento prescrito</label>
            <input type='text' name='tratamiento' className='form-control' placeholder='Ingrese el tratamiento'
              onChange={e => setValues({ ...values, tratamiento: e.target.value })} />
          </div>
          <button className='btn btn-success'>GUARDAR</button>
          <Link to="/doctor/menu" className='btn btn-primary ms-3'>VOLVER</Link>
        </form>
      </div>
    </div>
  );
};

export default AgregarHistorial