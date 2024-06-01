import { BrowserRouter, Routes, Route } from "react-router-dom";


//Paciente
import LoginPaciente from "./VistaPaciente/LoginPaciente";

function App() {
  

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPaciente />} />
      </Routes>
    
    
    </BrowserRouter>

  )
}

export default App
