import { BrowserRouter, Routes, Route, Outlet } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css';
import Nav from "./Components/Nav";
import NavDoc from "./Components/NavDoc";

//Paciente
import LoginPaciente from "./VistaPaciente/LoginPaciente";
import RegistroPaciente from "./VistaPaciente/RegistroPaciente";

import Evaluacion from "./VistaPaciente/Evaluacion";
import Historial from "./VistaPaciente/Historial";
import Reservacion from "./VistaPaciente/Reservacion";
import RegistrarReservacion from "./VistaPaciente/RegistrarReservacion";
import VerReservaciones from "./VistaPaciente/VerReservaciones";
import UpdateReservacion from "./VistaPaciente/UpdateReservacion";











//Doctor
import LoginDoctor from "./VistaDoctor/LoginDoctor";
import AgregarPaciente from "./VistaDoctor/AgregarPaciente";
import MenuDoctor from "./VistaDoctor/MenuDoctor";
import AgregarHistorial from "./VistaDoctor/AgregarHistorial";
import UpdateHistorial from "./VistaDoctor/UpdateHistorial";
import VerHistorial from "./VistaDoctor/VerHistorial";


function App() {
  
  const LayoutPaciente = () => (
    <>
      <Nav/>
      <Outlet />
    </>
  );

  const LayoutDoctor = () => (
    <>
      <NavDoc/>
      <Outlet />
    </>
  );
  
  return (
    <BrowserRouter>
     
      <Routes>
        
        {/**Logins */}
          <Route path="/" element={<LoginPaciente />} />
          <Route path="/loginpaciente" element={<LoginPaciente />} />
          <Route path="/logindoctor" element={<LoginDoctor />} />
          <Route path="/registropaciente" element={<RegistroPaciente />} />

        {/**Rutas Paciente */}
        <Route element={<LayoutPaciente />}>
          
          <Route path="/paciente/evaluacion" element={<Evaluacion />} />
          <Route path="/paciente/historial" element={<Historial />} />
          <Route path="/paciente/reservacion" element={<Reservacion />} />
          <Route path="/paciente/reservacion/registrar/:id" element={<RegistrarReservacion />} />
          <Route path="/paciente/reservaciones" element={<VerReservaciones />} />
          <Route path="/paciente/reservaciones/update/:id/:idd" element={<UpdateReservacion />} />
        </Route>

        {/**Rutas Doctor */}
        <Route element={<LayoutDoctor />}>
          <Route path="/doctor/menu" element={<MenuDoctor />} />
          <Route path="/doctor/agregarpaciente" element={<AgregarPaciente />} />
          <Route path="/doctor/verhistorial/:id" element={<VerHistorial />} />
          <Route path="/doctor/agregarhistorial/:id" element={<AgregarHistorial />} />
          <Route path="/doctor/updatehistorial/:id" element={<UpdateHistorial />} />

        </Route>

      </Routes>
    
    
    </BrowserRouter>

  )
}

export default App
