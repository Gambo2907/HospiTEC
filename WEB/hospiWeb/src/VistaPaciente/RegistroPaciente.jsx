import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import axios from 'axios';
const RegistroPaciente = () => {
    const [values, setValues] = useState({
        nombre: '',
        ap1: '',
        ap2: '',
        cedula: '',
        nacimiento: null,
        direccion:'',
        correo:'',
        password: '',
       // telefono:'',

        //patologias:'',
       // tratamientos:'',
      })
      const navigate = useNavigate();
      const handleSubmit = (event) =>{
    
        const formattedValues = {
          ...values,
          nacimiento: values.nacimiento ? values.nacimiento.toISOString().split('T')[0] : null
        };

        event.preventDefault();
//-------------------------------MODIFICAR ESTO -------------------------------------------------------------
        axios.post('https://localhost:7061/api/Paciente/registrar_paciente', formattedValues)
        .then(res => {
          console.log(res);
          navigate('/loginpaciente')
        })
        .catch(err => console.log(err));
      }
      {
    
      }
      //Formulario
      return (
        <div className='d-flex w-100 vh-100 justify-content-center align-items-center bg-light'>
          <div className='w-50 border bg-white shadow px-5 pt-3 pb-5 rounded'>
            <h1>REGISTRO DE PACIENTE</h1>
            <form onSubmit={handleSubmit}>
              <div className='mb-2'>
                <label htmlFor='nombre'>Nombre</label>
                <input type='text' name='nombre' className='form-control' placeholder='Ingrese el nombre'
                onChange={e => setValues({...values, nombre: e.target.value})}/>
              </div>
              <div className='mb-2'>
                <label htmlFor='ap1'>Primer Apellido</label>
                <input type='text' name='ap1' className='form-control' placeholder='Ingrese su primer apellido'
                onChange={e => setValues({...values, ap1: e.target.value})}/>
              </div>
              <div className='mb-2'>
                <label htmlFor='ap2'>Segundo Apellido</label>
                <input type='text' name='ap2' className='form-control' placeholder='Ingrese su segundo apellido'
                onChange={e => setValues({...values,ap2: e.target.value})}/>
              </div>
              <div className='mb-2'>
                <label htmlFor='cedula'>Cedula</label>
                <input type='number' name='cedula' className='form-control' placeholder='Ingrese su cedula'
                onChange={e => setValues({...values, cedula: e.target.value})}/>
              </div>
              <div className='mb-2'>
                <label htmlFor='password'>Contrasena</label>
                <input type='text' name='password' className='form-control' placeholder='Ingrese una contrasena'
                onChange={e => setValues({...values, password: e.target.value})}/>
              </div>
              <div className='mb-2'>
                <label htmlFor='direccion'>Direccion</label>
                <input type='text' name='direccion' className='form-control' placeholder='Ingrese su direccion'
                onChange={e => setValues({...values, direccion: e.target.value})}/>
              </div>
              <div className='mb-2'>
                <label htmlFor='correo'>Correo</label>
                <input type='text' name='Correo' className='form-control' placeholder='Ingrese su correo'
                onChange={e => setValues({...values, direccion: e.target.value})}/>
              </div>
              <div className='mb-2'>
                <label htmlFor='nacimiento'>Fecha de nacimiento</label>
                <input type='date' name='nacimiento' className='form-control' placeholder='Ingrese su fecha de nacimiento'
                onChange={e => setValues({...values, nacimiento: e.target.value})}/>
              </div>
              {/**-------------------  Arreglar esto, agregar las patologias desde la api y hace multi seleccion ///////////////////////////////////////////// 
              <div className='mb-2'>
                <label htmlFor='patologias'>Patologias</label>
                <input type='text' name='patologias' className='form-control' placeholder='Seleccione las patologias que padece'
                onChange={e => setValues({...values, patologias: e.target.value})}/>
              </div>
              <div className='mb-2'>
                <label htmlFor='tratamientos'>Tratamientos</label>
                <input type='text' name='tratamientos' className='form-control' placeholder='Seleccione los tratamientos que usa'
                onChange={e => setValues({...values, tratamientos: e.target.value})}/>
              </div>
              <div className='mb-2'>
                <label htmlFor='telefono'>Telefono</label>
                <input type='number' name='telefono' className='form-control' placeholder='Ingrese su telefono'
                onChange={e => setValues({...values, telefono: e.target.value})}/>
              </div>
              */}
              <button className='btn btn-success'>REGISTRAR</button>
              <Link to="/loginpaciente" className='btn btn-primary ms-3'>VOLVER</Link>
            </form>
          </div>
    
        </div>
      )
    }

export default RegistroPaciente