import React, { useState, createContext } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Log_in_administracion from './pages/log_in_administracion';
import Carga_pacientes from './pages/carga_pacientes';
import Gestion_camas from './pages/gestion_camas';
import Gestion_equipo_medico from './pages/gestion_equipo_medico';
import Gestion_personal from './pages/gestion_personal';
import Gestion_procedimientos_medicos from './pages/gestion_procedimientos_medicos';
import Gestion_salones from './pages/gestion_salones';
import Reporte_areas_mejora from './pages/reporte_areas_mejora';
import './App.css';

export const ThemeContext = createContext({
  loggedIn: false,
  setLoggedIn: () => {},
  email: '',
  setEmail: () => {},
  theme: 'light',
  setTheme: () => {},
});

function App() {
  const [loggedIn, setLoggedIn] = useState(false);
  const [email, setEmail] = useState('');
  const [theme, setTheme] = useState('light');

  return (
    <div className="App">
      <BrowserRouter>
        <ThemeContext.Provider value={{ loggedIn, setLoggedIn, email, setEmail, theme, setTheme }}>
          <Routes>
            <Route path="/" element={<Log_in_administracion setLoggedIn={setLoggedIn} setEmail={setEmail} />} />
            <Route path="/carga_pacientes" element={<Carga_pacientes />} />
            <Route path="/gestion_camas" element={<Gestion_camas />} />
            <Route path="/gestion_equipo_medico" element={<Gestion_equipo_medico />} />
            <Route path="/gestion_personal" element={<Gestion_personal />} />
            <Route path="/gestion_procedimientos_medicos" element={<Gestion_procedimientos_medicos />} />
            <Route path="/gestion_salones" element={<Gestion_salones />} />
            <Route path="/reporte_areas_mejora" element={<Reporte_areas_mejora />} />
          </Routes>
        </ThemeContext.Provider>
      </BrowserRouter>
    </div>
  );
}

export default App;
