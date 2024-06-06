import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import Swal from 'sweetalert2';

const Historial = () => {
    const [data, setData] = useState([]);
    const navigate = useNavigate();
  
  useEffect(() => {
    const pacienteData = JSON.parse(localStorage.getItem('data-paciente'));
    const pacienteCedula = pacienteData ? pacienteData.cedula : '';
    axios.get(`https://localhost:7061/api/Historial/historial/${pacienteCedula}`)
      .then(res => {
        // Parse and sort data
        const sortedData = res.data.sort((a, b) => new Date(a.fecha) - new Date(b.fecha));
        setData(sortedData);
      })
      .catch(err => console.log(err));
  }, []);
  

  return (
    <div className='d-flex flex-column justify-content-center align-items-center bg-light vh-100'>
      <h1>Historial Médcio</h1>
      <div className='w-75 rounded bg-white border shadow p-4'>
        <table className='table table-striped'>
          <thead>
            <tr>
              <th>Fecha de realización</th>
              <th>Operación realizada</th>
              <th>Tratamiento</th>
            </tr>
          </thead>
          <tbody>
            {
              data.map((d, i) =>(
                <tr key={i}>
                  <td>{d.fecha}</td>
                  <td>{d.nombre}</td>
                  <td>{d.tratamiento}</td>
   
                </tr>
              ))
            }
          </tbody>
        </table>
  
      </div>
      
  
    </div>
  )
  
  
  
  
  }
export default Historial