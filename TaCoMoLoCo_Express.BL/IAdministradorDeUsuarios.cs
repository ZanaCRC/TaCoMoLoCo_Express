﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.BL
{
    public interface IAdministradorDeUsuarios
    {
        public string GetContrasenaAPartirDeUsuario(string username);
        public bool ExisteElUsuario(string usuario);
        public bool YaPoseeUnaCuenta(string cedula);
        public void InsertarUsuario(string cedula, string nombre1, string nombre2, string apellido1, string apellido2, int idRol);
        public int VerifiqueElRol(Usuario usuario);
        public void InsertarLogin(string cedula, string usuario, string contrasenia);
        public Login BusqueUsuarioParaLogin(string nombreUsuario);
        public Usuario BusqueUsuarioPorCedula(string cedula);

        public List<Canton> BuscarCantonesPorIdProvincia(int idProvincia);
        public List<Distrito> BuscarDistritosPorIdCanton(int IdCanton);
        public List<Barrio> BuscarBarriosPorIdDistrito(int idDistrito);
        //public void AsignarDireccionAUsuario(Model.Direccion nuevaDireccion, string cedulaDelCliente);
        public void InsertarDireccionUsuario(string cedulaUsuario, int idProvincia, int idCanton, int idDistrito, int idBarrio, string calle, string numero, string piso);
        public bool ExisteDireccionParaUsuario(string cedulaUsuario);
      //  public int ObtenerUltimoIdDireccion();
      // public void ActualizarIdDireccionPorCedula(string cedula, int nuevoIdDireccion);
    }
}
