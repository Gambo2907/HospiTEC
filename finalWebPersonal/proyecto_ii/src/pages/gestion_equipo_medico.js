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

class Gestion_equipo_medico extends React.Component {
  state = {
    data: [],
    modalActualizar: false,
    modalInsertar: false,
    form: {
      id: "",
      nombre: "",
      proveedor: "",
      cantdisponible: "",
    },
  };

  componentDidMount() {
    this.fetchData();
  }

  fetchData = async () => {
    try {
      const response = await axios.get('http://localhost:5197/api/Equipo/equipos');
      this.setState({ data: response.data });
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  mostrarModalActualizar = (dato) => {
    this.setState({
      form: { ...dato },
      modalActualizar: true,
    });
  };

  cerrarModalActualizar = () => {
    this.setState({ modalActualizar: false });
  };

  mostrarModalInsertar = () => {
    this.setState({
      modalInsertar: true,
      form: {
        id: "",
        nombre: "",
        proveedor: "",
        cantdisponible: "",
      }
    });
  };

  cerrarModalInsertar = () => {
    this.setState({ modalInsertar: false });
  };

  editar = async (dato) => {
    try {
      await axios.put(`http://localhost:5197/api/Equipo/actualizar_equipo/${dato.id}`, {
        nombre: dato.nombre,
        proveedor: dato.proveedor,
        cantdisponible: parseInt(dato.cantdisponible, 10),
      });
      this.fetchData();
      this.setState({ modalActualizar: false });
    } catch (error) {
      console.error('Error updating data:', error);
    }
  };

  insertar = async () => {
    try {
      const response = await axios.get('http://localhost:5197/api/Equipo/equipos');
      const newId = response.data.length + 1;

      await axios.post('http://localhost:5197/api/Equipo/crear_equipo', {
        id: newId,
        nombre: this.state.form.nombre,
        proveedor: this.state.form.proveedor,
        cantdisponible: parseInt(this.state.form.cantdisponible, 10),
      });
      this.fetchData();
      this.setState({ modalInsertar: false });
    } catch (error) {
      console.error('Error inserting data:', error);
    }
  };

  handleChange = (e) => {
    this.setState({
      form: {
        ...this.state.form,
        [e.target.name]: e.target.value,
      },
    });
  };

  CambiarTheme = () => {
    const { setTheme, theme } = useContext(ThemeContext);
    setTheme((theme) => (theme === 'light' ? 'dark' : 'light'));
  };

  render() {
    const themeStyle = this.props.theme === 'dark' ? Light : Dark;

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
                {/* Theme toggle logic here */}
              </div>
            </div>
          </div>
        </Sidebar>

        <Content>
          <Container>
            <h1 style={{ position: 'relative', left: '50%', transform: 'translateX(-50%)', top: '-1px' }}>Gestion Equipo Medico</h1>
          </Container>
          <Container>
            <br />
            <Button color="success" onClick={() => this.mostrarModalInsertar()}>Crear</Button>
            <br />
            <Table>
              <thead>
                <tr>
                  <th>Nombre</th>
                  <th>Proveedor</th>
                  <th>Cantidad Disponible</th>
                  <th>Acciones</th>
                </tr>
              </thead>

              <tbody>
                {this.state.data.map((dato) => (
                  <tr key={dato.id}>
                    <td>{dato.nombre}</td>
                    <td>{dato.proveedor}</td>
                    <td>{dato.cantdisponible}</td>
                    <td>
                      <Button
                        color="primary"
                        onClick={() => this.mostrarModalActualizar(dato)}
                      >
                        Editar
                      </Button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </Container>
          <Modal isOpen={this.state.modalActualizar}>
            <ModalHeader>
              <div><h3>Editar Registro</h3></div>
            </ModalHeader>

            <ModalBody>
              <FormGroup>
                <label>
                  Nombre:
                </label>

                <input
                  className="form-control"
                  name="nombre"
                  type="text"
                  onChange={this.handleChange}
                  value={this.state.form.nombre}
                />
              </FormGroup>

              <FormGroup>
                <label>
                  Proveedor:
                </label>
                <input
                  className="form-control"
                  name="proveedor"
                  type="text"
                  onChange={this.handleChange}
                  value={this.state.form.proveedor}
                />
              </FormGroup>

              <FormGroup>
                <label>
                  Cantidad Disponible:
                </label>
                <input
                  className="form-control"
                  name="cantdisponible"
                  type="text"
                  onChange={this.handleChange}
                  value={this.state.form.cantdisponible}
                />
              </FormGroup>
            </ModalBody>

            <ModalFooter>
              <Button
                color="primary"
                onClick={() => this.editar(this.state.form)}
              >
                Editar
              </Button>
              <Button
                color="danger"
                onClick={() => this.cerrarModalActualizar()}
              >
                Cancelar
              </Button>
            </ModalFooter>
          </Modal>
          <Modal isOpen={this.state.modalInsertar}>
            <ModalHeader>
              <div><h3>Insertar Equipo</h3></div>
            </ModalHeader>

            <ModalBody>
              <FormGroup>
                <label>
                  Nombre:
                </label>

                <input
                  className="form-control"
                  name="nombre"
                  type="text"
                  onChange={this.handleChange}
                />
              </FormGroup>

              <FormGroup>
                <label>
                  Proveedor:
                </label>
                <input
                  className="form-control"
                  name="proveedor"
                  type="text"
                  onChange={this.handleChange}
                />
              </FormGroup>

              <FormGroup>
                <label>
                  Cantidad Disponible:
                </label>
                <input
                  className="form-control"
                  name="cantdisponible"
                  type="text"
                  onChange={this.handleChange}
                />
              </FormGroup>
            </ModalBody>

            <ModalFooter>
              <Button
                color="primary"
                onClick={() => this.insertar()}
              >
                Insertar
              </Button>
              <Button
                className="btn btn-danger"
                onClick={() => this.cerrarModalInsertar()}
              >
                Cancelar
              </Button>
            </ModalFooter>

          </Modal>

        </Content>

      </ThemeProvider>
    );
  }
}

export default Gestion_equipo_medico;
