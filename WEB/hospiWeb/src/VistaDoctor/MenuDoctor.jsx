import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import Swal from 'sweetalert2';
const MenuDoctor = () => {
    const [data, setData] = useState([]);
    const navigate = useNavigate();
  
    useEffect(()=>{
      axios.get('https://localhost:7061/api/Paciente/pacientes_registrados')
      .then(res => setData(res.data))
      .catch(err => console.log(err));
    },[]);
  
  

  //Tabla con la informacionde los laboratorios
  return (
    <div className='d-flex flex-column justify-content-center align-items-center bg-light vh-100'>
      <h1>LISTA DE PACIENTES</h1>
      <div className='w-75 rounded bg-white border shadow p-4'>
        <div className='d-flex justify-content-end'>
            <Link to="/doctor/agregarpaciente" className='btn btn-success'>AGREGAR PACIENTE</Link>
        </div>
        <table className='table table-striped'>
          <thead>
            <tr>
              <th>Nombre</th>
              <th>Primero apellido</th>
              <th>Segundo apellido</th>
              <th>Fecha de nacimiento</th>
              <th>Cedula</th>
              <th>Consultar historial</th>
  
            </tr>
          </thead>
          <tbody>
            {
              //Mapea la informacion con las filas
              data.map((d, i) =>(
                <tr key={i}>
                  <td>{d.nombre}</td>
                  <td>{d.ap1}</td>
                  <td>{d.ap2}</td>
                  <td>{d.nacimiento}</td>
                  <td>{d.cedula}</td>
                  <td> <Link to={`/doctor/verhistorial/${d.cedula}`}  className='btn btn-sm btn-primary me-2'><i className='fa-solid fa-eye'></i></Link></td>


                </tr>
              ))
            }
          </tbody>
        </table>
  
      </div>
      
  
    </div>
  )
  
  
  
  
  }
export default MenuDoctor