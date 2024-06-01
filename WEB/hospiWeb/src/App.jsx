import { BrowserRouter, Routes, Route } from "react-router-dom";


//Paciente
import LoginPaciente from "./VistaPaciente/LoginPaciente";

//Doctor
import LoginDoctor from "./VistaDoctor/LoginDoctor";

function App() {
  

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPaciente />} />
        <Route path="/loginpaciente" element={<LoginPaciente />} />
        <Route path="/logindoctor" element={<LoginDoctor />} />
      </Routes>
    
    
    </BrowserRouter>

  )
}

export default App
