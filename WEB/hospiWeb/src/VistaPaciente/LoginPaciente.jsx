import React,{useEffect, useState} from 'react'
import { useNavigate, Link, Navigate } from 'react-router-dom';

import DivInput from '../Components/DivInput';



const LoginPaciente = () => {
    const [data, setData] = useState([]);

    //Se guarda el corre y password para enviarlo a la api
    const [values, setValues] = useState({
        correo: '',
        password: '',
      })
   
    const go = useNavigate();
    useEffect(() => {
        if(localStorage.getItem('user-info')){
            <Navigate to='/login' />
        }
    }, [])
  
  
  
    const handleSubmit = (event) =>{
        event.preventDefault();
  
      }
    const login = async(e) =>{
      e.preventDefault();
  
      //Si se validan los datos se guarda en el local storage el rol del user
      axios.post('https://localhost:7215/api/Login/login_operador',values)
      .then(res => setData(res.data),  localStorage.setItem('user-info', true), localStorage.setItem('data', JSON.stringify(data)))
      .then(() => {
        console.log(data.correo);
        console.log(data.password);
        console.log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        <Navigate to='/' />
       // localStorage.setItem('user-info', JSON.stringify(res.data));
        console.log(localStorage.getItem('user-info'));
      })
      .catch(err => console.log(err));
        
      //Se guarda la hora de entrrada al trabajo en el localstorage
      axios.post('https://localhost:7215/api/Login/login_operador',values)
      .then(res => localStorage.setItem('data', JSON.stringify(res.data)))
      .then(() => {
        localStorage.setItem('userOperador', true)
        const fechaActual = new Date(); // Obtener la fecha y hora actual
        const fechaFormateada = fechaActual.toISOString().split('T')[0]; // Formatear la fecha a 'YYYY-MM-DD'
      
        const horas = fechaActual.getHours().toString().padStart(2, '0');
        const minutos = fechaActual.getMinutes().toString().padStart(2, '0');
        const segundos = fechaActual.getSeconds().toString().padStart(2, '0');
        const horaActual = `${horas}:${minutos}:${segundos}`;
        localStorage.setItem('horaEntrada', horaActual)
  
        console.log(data.correo);
        console.log(data.password);
        console.log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        go('/operador/prestamoestudiantes')
       // localStorage.setItem('user-info', JSON.stringify(res.data));
        console.log(localStorage.getItem('user-info'));
      })
      .catch(err => console.log(err));
  
  
    }
  
    
    //Formulario y botones
    return (
      <div className='container-fluid'>
        <div className='row mt-5'>
          <div className='col-md-4 offset-md-4'>
            <div className='card border border-dark'>
              <div className='card-header bg-dark border border-dark 
              text-white'>
                LOGIN PACIENTE
              </div>
              <div className='card-body'>
                <form onSubmit={login}>
                  <DivInput type='email' icon='fa-at' value={values.correo}
                  className='form-control' placeholder='Email' required='required'
                  handleChange={(e)=> setValues({...values,correo: e.target.value})} />
                  <DivInput type='password' icon='fa-key' value={values.password}
                  className='form-control' placeholder='Password' required='required'
                  handleChange={(e)=> setValues({...values,password: e.target.value})} />
                  <div className='d-grid col-10 mx-auto'>
                    <button className='btn btn-dark'>
                      <i className='fa-solid fa-door-open'></i>
                      LOGIN
                    </button>
                  </div>
                </form>
                <Link to='/registrooperador'>
                  <i className='fa-solid fa-user-plus'></i>REGISTRO
                </Link>
              </div>
            </div>  
          </div>
        </div>
      </div>
    )
  }

export default LoginPaciente