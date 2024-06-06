import "../App.css";
import { AiOutlineHome, AiOutlineApartment } from 'react-icons/ai';
import { MdOutlineAnalytics, MdLogout } from 'react-icons/md';
import "bootstrap/dist/css/bootstrap.min.css";
import React, { useContext, useEffect, useState } from 'react';
import styled, { ThemeProvider } from 'styled-components';
import { Light, Dark } from '../styles/themes';
import { NavLink } from 'react-router-dom';
import { ThemeContext } from '../App';
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
} from "reactstrap";

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

const equipmentOptions = [
  { id: 1, name: "Luces" },
  { id: 2, name: "Ultrasonido" },
  { id: 3, name: "Esterilizadores" },
  { id: 4, name: "Desfibriladores" },
  { id: 5, name: "Monitores" },
  { id: 6, name: "Respiradores Artificiales" },
  { id: 7, name: "Electrocardiografos" },
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
      &.active {
        .Linkicon {
          svg {
            color: ${(props) => props.theme.bg4};
          }
        }
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

const Gestion_camas = () => {
  const [data, setData] = useState([]);
  const [modalActualizar, setModalActualizar] = useState(false);
  const [modalInsertar, setModalInsertar] = useState(false);
  const [form, setForm] = useState({
    id: "",
    numero: "",
    equipo_medico_disponible: "",
    salon_ubicada: "",
    uci: false,
    equipo_medico: "",
  });

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const camasResponse = await axios.get('http://localhost:5197/api/Cama/camas');
      const camasData = camasResponse.data;

      const dataWithEquipos = await Promise.all(camasData.map(async (cama) => {
        const equipoResponse = await axios.get(`http://localhost:5197/api/Cama/equipo_por_cama/${cama.numcama}`);
        const equipoData = equipoResponse.data.length ? equipoResponse.data.map(equipo => equipo.nombre).join(', ') : "No posee equipo disponible";

        return {
          numero: cama.numcama,
          equipo_medico_disponible: equipoData,
          salon_ubicada: cama.num_salon,
          uci: cama.uci ? "✔" : "✘"
        };
      }));

      setData(dataWithEquipos);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const mostrarModalActualizar = (dato) => {
    setForm({
      ...dato,
      uci: dato.uci === "✔",
      equipo_medico: "",
    });
    setModalActualizar(true);
  };

  const cerrarModalActualizar = () => {
    setModalActualizar(false);
  };

  const mostrarModalInsertar = async () => {
    try {
      const camasResponse = await axios.get('http://localhost:5197/api/Cama/camas');
      const nuevoNumero = camasResponse.data.length + 1;

      setForm({
        id: "",
        numero: nuevoNumero,
        equipo_medico_disponible: "",
        salon_ubicada: "",
        uci: false,
        equipo_medico: "",
      });
      setModalInsertar(true);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const cerrarModalInsertar = () => {
    setModalInsertar(false);
  };

  const editar = async (dato) => {
    const updatedCama = {
      numcama: dato.numero,
      uci: dato.uci,
      id_estadocama: 1,
      num_salon: parseInt(dato.salon_ubicada, 10),
    };

    try {
      await axios.put(`http://localhost:5197/api/Cama/actualizar_cama/${dato.numero}`, updatedCama);
      console.log("Updated Cama JSON:", updatedCama);
      if (dato.equipo_medico) {
        const equipoPorCama = {
          num_cama: dato.numero,
          id_equipomedico: parseInt(dato.equipo_medico, 10),
        };
        await axios.post('http://localhost:5197/api/Cama/crear_equipo_por_cama', equipoPorCama);
        console.log("Equipo por Cama JSON:", equipoPorCama);
      }
      fetchData();
      setModalActualizar(false);
    } catch (error) {
      console.error('Error updating data:', error);
    }
  };

  const insertar = async () => {
    const nuevoRegistro = {
      numcama: form.numero,
      uci: form.uci,
      id_estadocama: 1,
      num_salon: parseInt(form.salon_ubicada, 10),
    };

    try {
      await axios.post('http://localhost:5197/api/Cama/crear_cama', nuevoRegistro);
      console.log("Nuevo Cama JSON:", nuevoRegistro);
      const equipoPorCama = {
        num_cama: form.numero,
        id_equipomedico: parseInt(form.equipo_medico, 10),
      };
      await axios.post('http://localhost:5197/api/Cama/crear_equipo_por_cama', equipoPorCama);
      console.log("Nuevo Equipo por Cama JSON:", equipoPorCama);
      fetchData();
      setModalInsertar(false);
    } catch (error) {
      console.error('Error inserting data:', error);
    }
  };

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setForm({
      ...form,
      [name]: type === "checkbox" ? checked : value,
    });
  };

  const CambiarTheme = () => {
    const { setTheme, theme } = useContext(ThemeContext);
    setTheme((theme) => (theme === 'light' ? 'dark' : 'light'));
  };

  const themeStyle = useContext(ThemeContext) === 'dark' ? Light : Dark;

  return (
    <ThemeProvider theme={themeStyle}>
      <Sidebar>
        <div className="Logocontent">
          <div className="imgcontent">
            <img src={logo} alt="logo" />
          </div>
          <h2>LabCE</h2>
        </div>
        {linksArray.map(({ icon, label, to }) => (
          <div className="LinkContainer" key={label}>
            <NavLink to={to} className="Links" activeClassName="active">
              <div className="Linkicon">{icon}</div>
              <span>{label}</span>
            </NavLink>
          </div>
        ))}
        <Divider />
        {secondarylinksArray.map(({ icon, label, to }) => (
          <div className="LinkContainer" key={label}>
            <NavLink to={to} className="Links" activeClassName="active">
              <div className="Linkicon">{icon}</div>
              <span>{label}</span>
            </NavLink>
          </div>
        ))}
        <Divider />
        <div className="Themecontent">
          <div className="Togglecontent">
            <div className="grid theme-container">
              
            </div>
          </div>
        </div>
      </Sidebar>

      <Content>
        <Container>
          <h1 style={{ position: 'relative', left: '50%', transform: 'translateX(-50%)', top: '-1px' }}>Gestion de Camas</h1>
        </Container>
        <Container>
          <br />
          <Button color="success" onClick={mostrarModalInsertar}>Crear</Button>
          <br />
          <Table>
            <thead>
              <tr>
                <th>Numero</th>
                <th>Equipo Medico Disponible</th>
                <th>Salon Ubicada</th>
                <th>UCI</th>
                <th>Acciones</th>
              </tr>
            </thead>

            <tbody>
              {data.map((dato) => (
                <tr key={dato.numero}>
                  <td>{dato.numero}</td>
                  <td>{dato.equipo_medico_disponible}</td>
                  <td>{dato.salon_ubicada}</td>
                  <td>{dato.uci}</td>
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
              <label>
                Numero:
              </label>

              <input
                className="form-control"
                name="numero"
                type="text"
                onChange={handleChange}
                value={form.numero}
                disabled
              />
            </FormGroup>

            <FormGroup>
              <label>
                Salon donde se ubica: 
              </label>
              <input
                className="form-control"
                name="salon_ubicada"
                type="text"
                onChange={handleChange}
                value={form.salon_ubicada}
              />
            </FormGroup>
            <FormGroup>
              <label>
                UCI: 
              </label>
              <input
                type="checkbox"
                name="uci"
                checked={form.uci}
                onChange={handleChange}
              />{" "}
              {form.uci ? "✔" : "✘"}
            </FormGroup>
            <FormGroup>
              <label>
                Equipo Médico:
              </label>
              <select
                className="form-control"
                name="equipo_medico"
                onChange={handleChange}
                value={form.equipo_medico}
              >
                <option value="">Seleccione un equipo</option>
                {equipmentOptions.map((equipo) => (
                  <option key={equipo.id} value={equipo.id}>{equipo.name}</option>
                ))}
              </select>
            </FormGroup>
          </ModalBody>

          <ModalFooter>
            <Button
              color="primary"
              onClick={() => editar(form)}
            >
              Editar
            </Button>
            <Button
              color="danger"
              onClick={cerrarModalActualizar}
            >
              Cancelar
            </Button>
          </ModalFooter>
        </Modal>
        <Modal isOpen={modalInsertar}>
          <ModalHeader>
            <div><h3>Insertar Cama</h3></div>
          </ModalHeader>

          <ModalBody>
            <FormGroup>
              <label>
                Numero: 
              </label>
              
              <input
                className="form-control"
                name="numero"
                type="text"
                value={form.numero}
                disabled
              />
            </FormGroup>

            <FormGroup>
              <label>
                Salon donde se ubica: 
              </label>
              <input
                className="form-control"
                name="salon_ubicada"
                type="text"
                onChange={handleChange}
              />
            </FormGroup>
            <FormGroup>
              <label>
                UCI: 
              </label>
              <input
                type="checkbox"
                name="uci"
                checked={form.uci}
                onChange={handleChange}
              />{" "}
              {form.uci ? "✔" : "✘"}
            </FormGroup>
            <FormGroup>
              <label>
                Equipo Médico:
              </label>
              <select
                className="form-control"
                name="equipo_medico"
                onChange={handleChange}
                value={form.equipo_medico}
              >
                <option value="">Seleccione un equipo</option>
                {equipmentOptions.map((equipo) => (
                  <option key={equipo.id} value={equipo.id}>{equipo.name}</option>
                ))}
              </select>
            </FormGroup>
          </ModalBody>

          <ModalFooter>
            <Button
              color="primary"
              onClick={insertar}
            >
              Insertar
            </Button>
            <Button
              className="btn btn-danger"
              onClick={cerrarModalInsertar}
            >
              Cancelar
            </Button>
          </ModalFooter>
          
        </Modal>
          
      </Content> 
      
    </ThemeProvider>
  );
}

export default Gestion_camas;
