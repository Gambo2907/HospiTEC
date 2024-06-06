import "../App.css";
import { AiOutlineHome, AiOutlineApartment } from 'react-icons/ai';
import { MdOutlineAnalytics, MdLogout } from 'react-icons/md';
import "bootstrap/dist/css/bootstrap.min.css";
import React, { useContext, useState } from 'react';
import styled, { ThemeProvider } from 'styled-components';
import { Light, Dark } from '../styles/themes';
import { NavLink } from 'react-router-dom';
import { ThemeContext } from '../App';
import logo from '../assets/react.svg';

import {
  Table,
  Button,
  Container,
} from "reactstrap";

// JSON con datos aleatorios de pacientes
const pacientesData = [
  {
    nombre: "Emmanuel",
    apellidos: "Esquivel Chavarria",
    cedula: "504560886",
    telefono: "87654321",
    direccion: "San José, Costa Rica",
    fecha_nacimiento: "28/10/2004",
    patologias: [
      {
        nombre: "Hipertensión",
        tratamiento: "Losartan"
      },
      {
        nombre: "Diabetes",
        tratamiento: "Metformina"
      }
    ]
  },
  {
    nombre: "Carlos",
    apellidos: "Rodríguez Pérez",
    cedula: "103450879",
    telefono: "81234567",
    direccion: "Alajuela, Costa Rica",
    fecha_nacimiento: "23/08/1997",
    patologias: [
      {
        nombre: "Asma",
        tratamiento: "Salbutamol"
      }
    ]
  },
  {
    nombre: "Juan",
    apellidos: "Martínez Solano",
    cedula: "103410687",
    telefono: "85432176",
    direccion: "Heredia, Costa Rica",
    fecha_nacimiento: "23/06/1987",
    patologias: [
      {
        nombre: "Artritis",
        tratamiento: "Ibuprofeno"
      }
    ]
  },
  {
    nombre: "Pepe",
    apellidos: "González Gómez",
    cedula: "119200368",
    telefono: "88889999",
    direccion: "Cartago, Costa Rica",
    fecha_nacimiento: "23/08/1975",
    patologias: [
      {
        nombre: "Colesterol alto",
        tratamiento: "Atorvastatina"
      }
    ]
  },
  {
    nombre: "Emilio",
    apellidos: "Fernández Ruiz",
    cedula: "123467809",
    telefono: "83334444",
    direccion: "Puntarenas, Costa Rica",
    fecha_nacimiento: "23/08/1999",
    patologias: [
      {
        nombre: "Migraña",
        tratamiento: "Paracetamol"
      }
    ]
  }
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

class Gestion_salones extends React.Component {
  
  state = {
    data: [], // Tabla inicialmente vacía
  };

  cargarPacientes = () => {
    this.setState({ data: pacientesData });
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
                
              </div>
            </div>
          </div>
        </Sidebar>

        <Content> 
          <Container>
            <h1 style={{ position: 'relative', left: '50%', transform: 'translateX(-50%)', top: '-1px' }}>Gestión de Pacientes</h1>
          </Container>
          <Container>
            <br />
            <Table>
              <thead>
                <tr>
                  <th>Nombre</th>
                  <th>Apellidos</th>
                  <th>Cédula</th>
                  <th>Teléfono</th>
                  <th>Dirección</th>
                  <th>Fecha de Nacimiento</th>
                  <th>Patologías</th>
                </tr>
              </thead>
  
              <tbody>
                {this.state.data.map((paciente, index) => (
                  <tr key={index}>
                    <td>{paciente.nombre}</td>
                    <td>{paciente.apellidos}</td>
                    <td>{paciente.cedula}</td>
                    <td>{paciente.telefono}</td>
                    <td>{paciente.direccion}</td>
                    <td>{paciente.fecha_nacimiento}</td>
                    <td>
                      {paciente.patologias.map((patologia, index) => (
                        <div key={index}>
                          <strong>{patologia.nombre}:</strong> {patologia.tratamiento}
                        </div>
                      ))}
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
            <Button color="primary" onClick={this.cargarPacientes}>Cargar Pacientes</Button>
          </Container>
        </Content> 
      </ThemeProvider>
    );
  }
}

export default Gestion_salones;
