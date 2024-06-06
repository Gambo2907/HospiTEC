import "../App.css";
import { AiOutlineHome, AiOutlineApartment } from 'react-icons/ai';
import { MdOutlineAnalytics, MdLogout } from 'react-icons/md';
import "bootstrap/dist/css/bootstrap.min.css";
import React, { useEffect, useState } from 'react';
import styled, { ThemeProvider } from 'styled-components';
import { Light, Dark } from '../styles/themes';
import { NavLink } from 'react-router-dom';
import logo from '../assets/react.svg';
import axios from 'axios';

import {
  Table,
  Button,
  Container,
  Modal,
  ModalHeader,
  ModalBody,
  FormGroup,
  ModalFooter,
  Dropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem
} from "reactstrap";

const procedimientoOptions = [
  "Apendicectomía", 
  "Biopsia de mama", 
  "Cirugía de cataratas", 
  "Cesárea",
  "Histerectomía", 
  "Cirugía para la lumbalgia", 
  "Mastectomía", 
  "Amigdalectomía"
];

const patologiaOptions = [
  { id: 1, nombre: "Rinitis Crónica" },
  { id: 2, nombre: "Dermatitis" },
  { id: 3, nombre: "Psoriasis" },
  { id: 4, nombre: "Acné" },
  { id: 5, nombre: "Alopecia" },
  { id: 6, nombre: "Amigdalitis" },
  { id: 7, nombre: "Hipertensión" },
  { id: 8, nombre: "Asma" },
];

const linksArray = [
  {
    label: 'Gestion Salones',
    icon: <AiOutlineHome />,
    to: '/gestion_salones',
  },
  {
    label: 'Gestion Equipo',
    icon: <MdOutlineAnalytics />,
    to: '/gestion_equipo_medico',
  },
  {
    label: 'Gestion Camas',
    icon: <AiOutlineApartment />,
    to: '/gestion_camas',
  },
  {
    label: 'Procedimientos',
    icon: <MdOutlineAnalytics />,
    to: '/gestion_procedimientos_medicos',
  },
  {
    label: 'Gestión Personal',
    icon: <MdOutlineAnalytics />,
    to: '/gestion_personal',
  },
  {
    label: 'Carga de pacientes',
    icon: <MdOutlineAnalytics />,
    to: '/carga_pacientes',
  },
  {
    label: 'Reportes',
    icon: <MdOutlineAnalytics />,
    to: '/reporte_areas_mejora',
  },
];

const secondarylinksArray = [
  {
    label: 'Salir',
    icon: <MdLogout />,
    to: '/',
  },
];

const Sidebar = styled.div`
  color: ${(props) => props.theme.text};
  background: ${(props) => props.theme.bg};
  position: fixed;
  width: 300px;
  height: 100vh;
  z-index: 1000;

  .Logocontent {
    display: flex;
    justify-content: center;
    align-items: center;
    padding-bottom: 20px;
    .imgcontent {
      display: flex;
      img {
        max-width: 100%;
        height: auto;
      }
    }
  }

  .LinkContainer {
    margin: 8px 0;
    padding: 0 15%;
    :hover {
      background: ${(props) => props.theme.bg3};
    }
    .Links {
      display: flex;
      align-items: center;
      text-decoration: none;
      padding: 8px 0;
      color: ${(props) => props.theme.text};
      .Linkicon {
        padding: 8px 16px;
        display: flex;
        svg {
          font-size: 25px;
        }
      }
      &.active .Linkicon svg {
        color: ${(props) => props.theme.bg4};
      }
    }
  }

  .Themecontent {
    display: flex;
    align-items: center;
    justify-content: space-between;
    .titletheme {
      padding: 10px;
      font-weight: 700;
    }
    .Togglecontent {
      margin: auto 40px;
      width: 36px;
      height: 20px;
      border-radius: 10px;
    }
  }
`;

const Content = styled.div`
  margin-left: 300px; // Asegurar que el contenido comience después de la barra lateral
  flex-grow: 1; // Permitir que el contenido crezca para llenar el espacio restante
`;

const Divider = styled.div`
  height: 1px;
  width: 100%;
  background: ${(props) => props.theme.bg3};
  margin: 20px 0;
`;

const DataTableContainer = styled.div`
  width: 80%;
  margin-left: auto;
  margin-right: auto;
`;

const Gestion_salones = () => {
  const [data, setData] = useState([]);
  const [modalActualizar, setModalActualizar] = useState(false);
  const [modalInsertar, setModalInsertar] = useState(false);
  const [dropdownNombreOpen, setDropdownNombreOpen] = useState(false);
  const [dropdownPatologiaOpen, setDropdownPatologiaOpen] = useState(false);
  const [form, setForm] = useState({
    id: "",
    nombre: "",
    patologia: "",
    dias_tratamiento: ""
  });

  useEffect(() => {
    // Fetch data from API
    const fetchData = async () => {
      try {
        const response = await axios.get('http://localhost:5197/api/ProcedimientoMedico/procedimientos_medicos');
        const procedimientoData = response.data;

        // Map the API data to the required format
        const updatedData = procedimientoData.map((item) => {
          const patologiaObj = patologiaOptions.find(p => p.nombre === item.patologia);
          return {
            id: item.id,
            nombre: item.nombre,
            patologia: patologiaObj ? patologiaObj.nombre : item.patologia,
            dias_tratamiento: item.cantdias
          };
        });
        setData(updatedData);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, []);

  const mostrarModalActualizar = (dato) => {
    setForm(dato);
    setModalActualizar(true);
  };

  const cerrarModalActualizar = () => {
    setModalActualizar(false);
  };

  const mostrarModalInsertar = () => {
    setForm({
      id: "",
      nombre: "",
      patologia: "",
      dias_tratamiento: ""
    });
    setModalInsertar(true);
  };

  const cerrarModalInsertar = () => {
    setModalInsertar(false);
  };

  const editar = async (dato) => {
    try {
      const procedimiento = {
        nombre: dato.nombre,
        idpatologia: patologiaOptions.find(p => p.nombre === dato.patologia).id,
        cantdias: parseInt(dato.dias_tratamiento, 10)
      };
      await axios.put(`http://localhost:5197/api/ProcedimientoMedico/actualizar_procedimiento/${dato.id}`, procedimiento);
      const newData = data.map(item => (item.id === dato.id ? { ...item, nombre: dato.nombre, patologia: dato.patologia, dias_tratamiento: dato.dias_tratamiento } : item));
      setData(newData);
      setModalActualizar(false);
    } catch (error) {
      console.error('Error updating data:', error);
    }
  };

  const insertar = async () => {
    try {
      const nuevoId = data.length > 0 ? Math.max(...data.map(item => item.id)) + 1 : 1; // Calcula el nuevo ID
      const nuevoDato = {
        id: nuevoId,
        nombre: form.nombre,
        idpatologia: patologiaOptions.find(p => p.nombre === form.patologia).id,
        cantdias: parseInt(form.dias_tratamiento, 10)
      };
      const response = await axios.post('http://localhost:5197/api/ProcedimientoMedico/crear_procedimiento_medico', nuevoDato);
      const newEntry = { ...nuevoDato, id: response.data.id, patologia: form.patologia }; // Assuming response contains the new entry ID
      setData([...data, newEntry]);
      setModalInsertar(false);
    } catch (error) {
      console.error('Error creating data:', error);
    }
  };

  const handleChange = (e) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value,
    });
  };

  const toggleDropdownNombre = () => {
    setDropdownNombreOpen(!dropdownNombreOpen);
  };

  const toggleDropdownPatologia = () => {
    setDropdownPatologiaOpen(!dropdownPatologiaOpen);
  };

  const handleSelectNombre = (e) => {
    setForm({
      ...form,
      nombre: e.target.innerText
    });
  };

  const handleSelectPatologia = (e) => {
    setForm({
      ...form,
      patologia: e.target.innerText
    });
  };

  return (
    <ThemeProvider theme={Dark}>
      <Sidebar>
        <div className="Logocontent">
          <div className="imgcontent">
            <img src={logo} alt="logo" />
          </div>
          <h2>LabCE</h2>
        </div>
        {linksArray.map(({ icon, label, to }) => (
          <div className="LinkContainer" key={label}>
            <NavLink to={to} className={({ isActive }) => isActive ? 'active Links' : 'Links'}>
              <div className="Linkicon">{icon}</div>
              <span>{label}</span>
            </NavLink>
          </div>
        ))}
        <Divider />
        {secondarylinksArray.map(({ icon, label, to }) => (
          <div className="LinkContainer" key={label}>
            <NavLink to={to} className={({ isActive }) => isActive ? 'active Links' : 'Links'}>
              <div className="Linkicon">{icon}</div>
              <span>{label}</span>
            </NavLink>
          </div>
        ))}
        <Divider />
        <div className="Themecontent">
          <div className="Togglecontent">
            <div className="grid theme-container">
              {/* Theme toggle logic here */}
            </div>
          </div>
        </div>
      </Sidebar>

      <Content> 
        <Container>
          <h1 style={{ position: 'relative', left: '50%', transform: 'translateX(-50%)', top: '-1px' }}>Gestion Procedimientos</h1>
        </Container>
        <Container>
          <br />
          <Button color="success" onClick={mostrarModalInsertar}>Crear</Button>
          <br />
          <Table>
            <thead>
              <tr>
                <th>Nombre</th>
                <th>Patología</th>
                <th>Días de Tratamiento</th>
                <th>Acciones</th>
              </tr>
            </thead>

            <tbody>
              {data.map((dato) => (
                <tr key={dato.id}>
                  <td>{dato.nombre}</td>
                  <td>{dato.patologia}</td>
                  <td>{dato.dias_tratamiento}</td>
                  <td>
                    <Button
                      color="primary"
                      onClick={() => mostrarModalActualizar(dato)}
                    >
                      Editar
                    </Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        </Container>
        <Modal isOpen={modalActualizar}>
          <ModalHeader>
            <div><h3>Editar Registro</h3></div>
          </ModalHeader>

          <ModalBody>
            <FormGroup>
              <label>Nombre:</label>
              <Dropdown isOpen={dropdownNombreOpen} toggle={toggleDropdownNombre}>
                <DropdownToggle caret className="form-control">
                  {form.nombre || "Seleccione un procedimiento"}
                </DropdownToggle>
                <DropdownMenu>
                  {procedimientoOptions.map((option, index) => (
                    <DropdownItem key={index} onClick={handleSelectNombre}>
                      {option}
                    </DropdownItem>
                  ))}
                </DropdownMenu>
              </Dropdown>
            </FormGroup>
            <FormGroup>
              <label>Patología:</label>
              <Dropdown isOpen={dropdownPatologiaOpen} toggle={toggleDropdownPatologia}>
                <DropdownToggle caret className="form-control">
                  {form.patologia || "Seleccione una patología"}
                </DropdownToggle>
                <DropdownMenu>
                  {patologiaOptions.map((option) => (
                    <DropdownItem key={option.id} onClick={handleSelectPatologia}>
                      {option.nombre}
                    </DropdownItem>
                  ))}
                </DropdownMenu>
              </Dropdown>
            </FormGroup>
            <FormGroup>
              <label>Días de Tratamiento:</label>
              <input
                className="form-control"
                name="dias_tratamiento"
                type="text"
                onChange={handleChange}
                value={form.dias_tratamiento}
              />
            </FormGroup>
          </ModalBody>

          <ModalFooter>
            <Button color="primary" onClick={() => editar(form)}>Editar</Button>
            <Button color="danger" onClick={cerrarModalActualizar}>Cancelar</Button>
          </ModalFooter>
        </Modal>
        <Modal isOpen={modalInsertar}>
          <ModalHeader>
            <div><h3>Insertar Procedimiento</h3></div>
          </ModalHeader>

          <ModalBody>
            <FormGroup>
              <label>Nombre:</label>
              <Dropdown isOpen={dropdownNombreOpen} toggle={toggleDropdownNombre}>
                <DropdownToggle caret className="form-control">
                  {form.nombre || "Seleccione un procedimiento"}
                </DropdownToggle>
                <DropdownMenu>
                  {procedimientoOptions.map((option, index) => (
                    <DropdownItem key={index} onClick={handleSelectNombre}>
                      {option}
                    </DropdownItem>
                  ))}
                </DropdownMenu>
              </Dropdown>
            </FormGroup>
            <FormGroup>
              <label>Patología:</label>
              <Dropdown isOpen={dropdownPatologiaOpen} toggle={toggleDropdownPatologia}>
                <DropdownToggle caret className="form-control">
                  {form.patologia || "Seleccione una patología"}
                </DropdownToggle>
                <DropdownMenu>
                  {patologiaOptions.map((option) => (
                    <DropdownItem key={option.id} onClick={handleSelectPatologia}>
                      {option.nombre}
                    </DropdownItem>
                  ))}
                </DropdownMenu>
              </Dropdown>
            </FormGroup>
            <FormGroup>
              <label>Días de Tratamiento:</label>
              <input
                className="form-control"
                name="dias_tratamiento"
                type="text"
                onChange={handleChange}
                value={form.dias_tratamiento}
              />
            </FormGroup>
          </ModalBody>

          <ModalFooter>
            <Button color="primary" onClick={insertar}>Insertar</Button>
            <Button className="btn btn-danger" onClick={cerrarModalInsertar}>Cancelar</Button>
          </ModalFooter>
        </Modal>
      </Content> 
    </ThemeProvider>
  );
}

export default Gestion_salones;
