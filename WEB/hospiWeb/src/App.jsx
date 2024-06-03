import { BrowserRouter, Routes, Route } from "react-router-dom";


//Paciente
import LoginPaciente from "./VistaPaciente/LoginPaciente";
import RegistroPaciente from "./VistaPaciente/RegistroPaciente";

//Doctor
import LoginDoctor from "./VistaDoctor/LoginDoctor";

function App() {
  

  return (
    <BrowserRouter>
      <Routes>
        {/**Logins */}
        <Route path="/" element={<LoginPaciente />} />
        <Route path="/loginpaciente" element={<LoginPaciente />} />
        <Route path="/logindoctor" element={<LoginDoctor />} />

        {/**Rutas Paciente */}
        <Route path="/registropaciente" element={<RegistroPaciente />} />


      </Routes>
    
    
    </BrowserRouter>

  )
}

export default App
