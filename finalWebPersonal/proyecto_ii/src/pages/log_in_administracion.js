import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const Log_in_administracion = ({ setLoggedIn, setEmail }) => {
  const [email, setEmailInput] = useState('');
  const [password, setPassword] = useState('');
  const [emailError, setEmailError] = useState('');
  const [passwordError, setPasswordError] = useState('');
  const navigate = useNavigate();

  const onButtonClick = async () => {
    setEmailError('');
    setPasswordError('');

    let isValid = true;

    if (!email) {
      setEmailError('Please enter your email');
      isValid = false;
    } else if (!/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/.test(email)) {
      setEmailError('Please enter a valid email');
      isValid = false;
    }

    if (!password) {
      setPasswordError('Please enter a password');
      isValid = false;
    } else if (password.length < 5) {
      setPasswordError('The password must be 8 characters or longer');
      isValid = false;
    }

    if (isValid) {
      try {
        const response = await axios.post('http://localhost:5197/api/Login/login_admin', { 
          correo: email,
          password: password
        });

        if (response.status === 200) {
          setLoggedIn(true);
          setEmail(email);
          navigate('/gestion_salones');
        }

      } catch (error) {
        if (error.response && error.response.status === 404) {
          setEmailError('Invalid email or password');
          setPassword('');
        } else {
          console.error('Error during login:', error);
        }
      }
    }
  };

  return (
    <div className="mainContainer">
      <div className="titleContainer">
        <div>Inserte sus credenciales </div>
      </div>
      <br />
      <div className="inputContainer">
        <input
          value={email}
          placeholder="Enter your email here"
          onChange={(ev) => setEmailInput(ev.target.value)}
          className="inputBox"
        />
        <label className="errorLabel">{emailError}</label>
      </div>
      <br />
      <div className="inputContainer">
        <input
          type='password'
          value={password}
          placeholder="Enter your password here"
          onChange={(ev) => setPassword(ev.target.value)}
          className="inputBox"
        />
        <label className="errorLabel">{passwordError}</label>
      </div>
      <br />
      <div className="inputContainer">
        <input className="inputButton" type="button" onClick={onButtonClick} value="Log in" />
      </div>
    </div>
  );
};

export default Log_in_administracion;
