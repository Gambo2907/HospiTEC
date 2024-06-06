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

const Gestion_salones = () => {
  const [data, setData] = useState([]);
  const [modalActualizar, setModalActualizar] = useState(false);
  const [modalInsertar, setModalInsertar] = useState(false);
  const [form, setForm] = useState({
    id: "",
    numero: "",
    nombre: "",
    capacidad_camas: "",
    tipo_medicina: "",
    piso: "",
  });

  useEffect(() => {
    fetch("http://localhost:5197/api/Salon/salones")
      .then(response => response.json())
      .then(data => {
        const formattedData = data.map(item => ({
          numero: item.numsalon,
          nombre: item.nombre,
          capacidad_camas: item.cantcamas,
          tipo_medicina: item.tipomedicina,
          piso: item.piso
        }));
        setData(formattedData);
      })
      .catch(error => console.error("Error fetching data:", error));
  }, []);

  const fetchSalones = () => {
    fetch("http://localhost:5197/api/Salon/salones")
      .then(response => response.json())
      .then(data => {
        const formattedData = data.map(item => ({
          numero: item.numsalon,
          nombre: item.nombre,
          capacidad_camas: item.cantcamas,
          tipo_medicina: item.tipomedicina,
          piso: item.piso
        }));
        setData(formattedData);
      })
      .catch(error => console.error("Error fetching data:", error));
  };

  const mostrarModalActualizar = (dato) => {
    const tipoMedicinaMap = {
      1: "Hombre",
      2: "Mujer",
      3: "Niño"
    };

    setForm({
      numero: dato.numero,
      nombre: dato.nombre,
      capacidad_camas: dato.capacidad_camas,
      tipo_medicina: tipoMedicinaMap[dato.tipo_medicina] || "",
      piso: dato.piso
    });
    setModalActualizar(true);
  };

  const cerrarModalActualizar = () => {
    setModalActualizar(false);
  };

  const mostrarModalInsertar = () => {
    setModalInsertar(true);
  };

  const cerrarModalInsertar = () => {
    setModalInsertar(false);
  };

  const editar = () => {
    const tipoMedicinaMap = {
      "Hombre": 1,
      "Mujer": 2,
      "Niño": 3
    };
    const tipoMedicinaId = tipoMedicinaMap[form.tipo_medicina];

    // Validar campos requeridos
    if (!form.nombre || !form.piso || !tipoMedicinaId) {
      alert('Todos los campos editables son obligatorios.');
      return;
    }

    const requestBody = {
      nombre: form.nombre,
      piso: parseInt(form.piso, 10),
      id_tipo_medicina: tipoMedicinaId
    };

    console.log("URL:", `http://localhost:5197/api/Salon/actualizar_salon/${form.numero}`);
    console.log("JSON enviado:", requestBody); // Mostrar el JSON en la consola

    fetch(`http://localhost:5197/api/Salon/actualizar_salon/${form.numero}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(requestBody)
    })
    .then(response => {
      if (!response.ok) {
        return response.json().then(error => {
          throw new Error(error.message || 'Error updating data');
        });
      }
      return response.json();
    })
    .then(data => {
      console.log("Respuesta de la API:", data); // Mostrar la respuesta de la API en la consola
      fetchSalones();  // Refrescar la lista de salones
      setModalActualizar(false);
    })
    .catch(error => {
      console.error("Error updating data:", error);
      alert(`Error al actualizar el salón: ${error.message}`);
    });
  };

  const eliminar = (dato) => {
    const opcion = window.confirm("Estás Seguro que deseas Eliminar el elemento " + dato.numero);
    if (opcion === true) {
      fetch(`http://localhost:5197/api/Salon/eliminar_salon/${dato.numero}`, {
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
        // Si la respuesta es 204 (No Content), no intente analizar como JSON
        if (response.status === 204) {
          return null;
        }
        return response.json();
      })
      .then(data => {
        console.log("Respuesta de la API:", data); // Mostrar la respuesta de la API en la consola
        fetchSalones();  // Refrescar la lista de salones
        alert('Se ha eliminado el elemento correctamente.');
      })
      .catch(error => {
        console.log("Respuesta de la API:", data); // Mostrar la respuesta de la API en la consola
        fetchSalones();  // Refrescar la lista de salones
        alert('Se ha eliminado el elemento correctamente.');
      });
    }
  };

  const insertar = () => {
    const tipoMedicinaMap = {
      "Hombre": 1,
      "Mujer": 2,
      "Niño": 3
    };
    const tipoMedicinaId = tipoMedicinaMap[form.tipo_medicina];

    const requestBody = {
      numsalon: form.numero,
      nombre: form.nombre,
      piso: parseInt(form.piso, 10),
      id_tipo_medicina: tipoMedicinaId
    };

    console.log("JSON enviado:", requestBody); // Mostrar el JSON en la consola

    fetch("http://localhost:5197/api/Salon/registrar_salon", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(requestBody)
    })
    .then(response => response.json())
    .then(() => {
      fetchSalones();  // Refrescar la lista de salones
      setModalInsertar(false);
    })
    .catch(error => console.error("Error adding data:", error));
  };

  const handleChange = (e) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value,
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
          <h1 style={{ position: 'relative', left: '50%', transform: 'translateX(-50%)', top: '-1px' }}>Gestion Salones</h1>
        </Container>
        <Container>
          <br />
          <Button color="success" onClick={mostrarModalInsertar}>Crear</Button>
          <br />
          <Table>
            <thead>
              <tr>
                <th>Numero</th>
                <th>Nombre</th>
                <th>Capacidad de Camas</th>
                <th>Tipo de Medicina</th>
                <th>Piso</th>
                <th>Acciones</th>
              </tr>
            </thead>

            <tbody>
              {data.map((dato) => (
                <tr key={dato.numero}>
                  <td>{dato.numero}</td>
                  <td>{dato.nombre}</td>
                  <td>{dato.capacidad_camas}</td>
                  <td>{dato.tipo_medicina}</td>
                  <td>{dato.piso}</td>
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
              <label>Numero:</label>
              <input
                className="form-control"
                name="numero"
                type="text"
                value={form.numero}
                disabled
              />
            </FormGroup>

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
              <label>Capacidad de Camas:</label>
              <input
                className="form-control"
                name="capacidad_camas"
                type="text"
                value={form.capacidad_camas}
                disabled
              />
            </FormGroup>

            <FormGroup>
              <label>Tipo de Medicina:</label>
              <select
                className="form-control"
                name="tipo_medicina"
                onChange={handleChange}
                value={form.tipo_medicina}
              >
                <option value="">Seleccione</option>
                <option value="Hombre">Hombre</option>
                <option value="Mujer">Mujer</option>
                <option value="Niño">Niño</option>
              </select>
            </FormGroup>

            <FormGroup>
              <label>Piso:</label>
              <input
                className="form-control"
                name="piso"
                type="number"
                onChange={handleChange}
                value={form.piso}
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
            <div><h3>Insertar Salon</h3></div>
          </ModalHeader>

          <ModalBody>
            <FormGroup>
              <label>Numero:</label>
              <input
                className="form-control"
                name="numero"
                type="text"
                onChange={handleChange}
              />
            </FormGroup>

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
              <label>Capacidad de Camas:</label>
              <input
                className="form-control"
                name="capacidad_camas"
                type="text"
                onChange={handleChange}
              />
            </FormGroup>

            <FormGroup>
              <label>Tipo de Medicina:</label>
              <select
                className="form-control"
                name="tipo_medicina"
                onChange={handleChange}
                value={form.tipo_medicina}
              >
                <option value="">Seleccione</option>
                <option value="Hombre">Hombre</option>
                <option value="Mujer">Mujer</option>
                <option value="Niño">Niño</option>
              </select>
            </FormGroup>

            <FormGroup>
              <label>Piso:</label>
              <input
                className="form-control"
                name="piso"
                type="number"
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

export default Gestion_salones;
