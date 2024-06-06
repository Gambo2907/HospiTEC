import "../App.css";
import { AiOutlineHome, AiOutlineApartment } from 'react-icons/ai';
import { MdOutlineAnalytics, MdLogout } from 'react-icons/md';
import "bootstrap/dist/css/bootstrap.min.css";
import React, { useContext, useState, useEffect } from 'react';
import styled, { ThemeProvider } from 'styled-components';
import { Light, Dark } from '../styles/themes';
import { NavLink } from 'react-router-dom';
import { ThemeContext } from '../App';
import logo from '../assets/react.svg';

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

const Gestion_personal = () => {
  const [data, setData] = useState([]);
  const [modalActualizar, setModalActualizar] = useState(false);
  const [modalInsertar, setModalInsertar] = useState(false);
  const [form, setForm] = useState({
    id: "",
    nombre: "",
    telefono: "",
    apellidos: '',
    cedula: '',
    direccion: '',
    fecha_nacimiento: '',
    fecha_ingreso: '',
    correo: '',
    password: ''
  });

  useEffect(() => {
    fetch("http://localhost:5197/api/Personal/personal_registrado")
      .then(response => response.json())
      .then(data => {
        const formattedData = data.map((item, index) => ({
          id: index + 1,
          nombre: item.nombre,
          apellidos: `${item.ap1} ${item.ap2}`,
          cedula: item.cedula,
          telefono: '',
          direccion: item.direccion,
          fecha_nacimiento: item.nacimiento.split('T')[0],
          fecha_ingreso: item.fechaingreso.split('T')[0],
          correo: item.correo,
          password: item.password
        }));
        setData(formattedData);
        fetchTelefonos(formattedData);
      })
      .catch(error => console.error("Error fetching data:", error));
  }, []);

  const fetchTelefonos = async (personalData) => {
    const updatedData = await Promise.all(personalData.map(async (person) => {
      try {
        const phoneResponse = await fetch(`http://localhost:5197/api/Personal/telefonos_personal/${person.cedula}`);
        const phoneData = await phoneResponse.json();
        return { ...person, telefono: phoneData[0]?.telefono || 'No phone available' };
      } catch (error) {
        console.error(`Error fetching phone for ${person.cedula}:`, error);
        return { ...person, telefono: 'No phone available' };
      }
    }));
    setData(updatedData);
  };

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
      telefono: "",
      apellidos: '',
      cedula: '',
      direccion: '',
      fecha_nacimiento: '',
      fecha_ingreso: '',
      correo: '',
      password: ''
    });
    setModalInsertar(true);
  };

  const cerrarModalInsertar = () => {
    setModalInsertar(false);
  };

  const editar = async () => {
    const ap1 = form.apellidos.split(' ')[0];
    const ap2 = form.apellidos.split(' ')[1];
    const requestBody = {
      nombre: form.nombre,
      ap1: ap1,
      ap2: ap2,
      direccion: form.direccion,
      nacimiento: form.fecha_nacimiento,
      correo: form.correo,
      password: form.password,
      fechaingreso: form.fecha_ingreso,
      idtipopersonal: 3
    };

    try {
      await fetch(`http://localhost:5197/api/Personal/actualizar_personal/${form.cedula}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(requestBody)
      });
      const updatedData = data.map(item => (item.id === form.id ? { ...item, ...requestBody, apellidos: `${ap1} ${ap2}` } : item));
      setData(updatedData);
      setModalActualizar(false);
    } catch (error) {
      console.error(`Error updating person with cedula ${form.cedula}:`, error);
    }
  };

  const insertar = async () => {
    const ap1 = form.apellidos.split(' ')[0];
    const ap2 = form.apellidos.split(' ')[1];
    const requestBody = {
      nombre: form.nombre,
      ap1: ap1,
      ap2: ap2,
      cedula: form.cedula,
      direccion: form.direccion,
      nacimiento: form.fecha_nacimiento,
      correo: form.correo,
      password: form.password,
      fechaingreso: form.fecha_ingreso,
      idtipopersonal: 1
    };

    try {
      await fetch("http://localhost:5197/api/Personal/registrar_personal", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(requestBody)
      });

      await fetch("http://localhost:5197/api/Personal/registrar_telefono_personal", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({ cedula: form.cedula, telefono: form.telefono })
      });

      const nuevoDato = {
        ...form,
        id: data.length + 1,
        apellidos: `${ap1} ${ap2}`
      };
      setData([...data, nuevoDato]);
      setModalInsertar(false);
    } catch (error) {
      console.error('Error inserting new personal:', error);
    }
  };

  const eliminar = (dato) => {
    const opcion = window.confirm("Estás Seguro que deseas Eliminar el elemento " + dato.cedula);
    if (opcion === true) {
      fetch(`http://localhost:5197/api/Personal/eliminar_personal/${dato.cedula}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json"
        }
      })
      .then(response => {
        if (!response.ok) {
          return response.json().then(error => {
            throw new Error(error.message || 'Error deleting data');
          });
        }
        return null;
      })
      .then(() => {
        const newData = data.filter(item => item.cedula !== dato.cedula);
        setData(newData);
      })
      .catch(error => console.error('Error deleting data:', error));
    }
  };

  const handleChange = (e) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value,
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
            <NavLink
              to={to}
              className={({ isActive }) => "Links" + (isActive ? " active" : "")}
            >
              <div className="Linkicon">{icon}</div>
              <span>{label}</span>
            </NavLink>
          </div>
        ))}
        <Divider />
        {secondarylinksArray.map(({ icon, label, to }) => (
          <div className="LinkContainer" key={label}>
            <NavLink
              to={to}
              className={({ isActive }) => "Links" + (isActive ? " active" : "")}
            >
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
          <h1 style={{ position: 'relative', left: '50%', transform: 'translateX(-50%)', top: '-1px' }}>Gestion Personal</h1>
        </Container>
        <Container>
          <br />
          <Button color="success" onClick={mostrarModalInsertar}>Crear</Button>
          <br />
          <Table>
            <thead>
              <tr>
                <th>Nombre</th>
                <th>Apellidos</th>
                <th>Cedula</th>
                <th>Telefono</th>
                <th>Direccion</th>
                <th>Fecha Nacimiento</th>
                <th>Fecha Ingreso</th>
                <th>Correo</th>
                <th>Acciones</th>
              </tr>
            </thead>

            <tbody>
              {data.map((dato) => (
                <tr key={dato.id}>
                  <td>{dato.nombre}</td>
                  <td>{dato.apellidos}</td>
                  <td>{dato.cedula}</td>
                  <td>{dato.telefono}</td>
                  <td>{dato.direccion}</td>
                  <td>{dato.fecha_nacimiento}</td>
                  <td>{dato.fecha_ingreso}</td>
                  <td>{dato.correo}</td>
                  <td>
                    <Button
                      color="primary"
                      onClick={() => mostrarModalActualizar(dato)}
                    >
                      Editar
                    </Button>{" "}
                    <Button color="danger" onClick={() => eliminar(dato)}>Eliminar</Button>
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
              <input
                className="form-control"
                name="nombre"
                type="text"
                onChange={handleChange}
                value={form.nombre}
              />
            </FormGroup>

            <FormGroup>
              <label>Apellidos:</label>
              <input
                className="form-control"
                name="apellidos"
                type="text"
                onChange={handleChange}
                value={form.apellidos}
              />
            </FormGroup>

            <FormGroup>
              <label>Cedula:</label>
              <input
                className="form-control"
                name="cedula"
                type="text"
                onChange={handleChange}
                value={form.cedula}
              />
            </FormGroup>
            <FormGroup>
              <label>Telefono:</label>
              <input
                className="form-control"
                name="telefono"
                type="text"
                onChange={handleChange}
                value={form.telefono}
              />
            </FormGroup>
            <FormGroup>
              <label>Direccion:</label>
              <input
                className="form-control"
                name="direccion"
                type="text"
                onChange={handleChange}
                value={form.direccion}
              />
            </FormGroup>
            <FormGroup>
              <label>Fecha Nacimiento:</label>
              <input
                className="form-control"
                name="fecha_nacimiento"
                type="date"
                onChange={handleChange}
                value={form.fecha_nacimiento}
              />
            </FormGroup>
            <FormGroup>
              <label>Fecha Ingreso:</label>
              <input
                className="form-control"
                name="fecha_ingreso"
                type="date"
                onChange={handleChange}
                value={form.fecha_ingreso}
              />
            </FormGroup>
            <FormGroup>
              <label>Correo:</label>
              <input
                className="form-control"
                name="correo"
                type="email"
                onChange={handleChange}
                value={form.correo}
              />
            </FormGroup>
            <FormGroup>
              <label>Contraseña:</label>
              <input
                className="form-control"
                name="password"
                type="password"
                onChange={handleChange}
                value={form.password}
              />
            </FormGroup>
          </ModalBody>

          <ModalFooter>
            <Button color="primary" onClick={editar}>Editar</Button>
            <Button color="danger" onClick={cerrarModalActualizar}>Cancelar</Button>
          </ModalFooter>
        </Modal>
        <Modal isOpen={modalInsertar}>
          <ModalHeader>
            <div><h3>Insertar Personal</h3></div>
          </ModalHeader>

          <ModalBody>
            <FormGroup>
              <label>Nombre:</label>
              <input
                className="form-control"
                name="nombre"
                type="text"
                onChange={handleChange}
              />
            </FormGroup>

            <FormGroup>
              <label>Apellidos:</label>
              <input
                className="form-control"
                name="apellidos"
                type="text"
                onChange={handleChange}
              />
            </FormGroup>

            <FormGroup>
              <label>Cedula:</label>
              <input
                className="form-control"
                name="cedula"
                type="text"
                onChange={handleChange}
              />
            </FormGroup>
            <FormGroup>
              <label>Telefono:</label>
              <input
                className="form-control"
                name="telefono"
                type="text"
                onChange={handleChange}
              />
            </FormGroup>
            <FormGroup>
              <label>Direccion:</label>
              <input
                className="form-control"
                name="direccion"
                type="text"
                onChange={handleChange}
              />
            </FormGroup>
            <FormGroup>
              <label>Fecha Nacimiento:</label>
              <input
                className="form-control"
                name="fecha_nacimiento"
                type="date"
                onChange={handleChange}
              />
            </FormGroup>
            <FormGroup>
              <label>Fecha Ingreso:</label>
              <input
                className="form-control"
                name="fecha_ingreso"
                type="date"
                onChange={handleChange}
              />
            </FormGroup>
            <FormGroup>
              <label>Correo:</label>
              <input
                className="form-control"
                name="correo"
                type="email"
                onChange={handleChange}
              />
            </FormGroup>
            <FormGroup>
              <label>Contraseña:</label>
              <input
                className="form-control"
                name="password"
                type="password"
                onChange={handleChange}
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
};

export default Gestion_personal;
