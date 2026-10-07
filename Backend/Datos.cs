using BackEnd;
using Nucleo.Modelos;

namespace Backend
{
    public class Datos : IDatos
    {
        public Dictionary<int, Usuario> Usuarios => throw new NotImplementedException();

        public Dictionary<int, Rol> Roles => throw new NotImplementedException();

        public Dictionary<int, Proyecto> Proyectos => throw new NotImplementedException();

        public List<EntradaLog> Logs => throw new NotImplementedException();
        int siguienteUsuario = 1;
        int siguienteRol = 1;
        int siguienteProyecto = 1;
        int siguienteEntradaLog = 1;
        #region LOGS
        public void AgregarLog(EntradaLog log)
        {
            throw new NotImplementedException();
        }
        
        public int LimpiarLog()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region ROLES
        public Rol? LeerRol(int idRol)
        {
            throw new NotImplementedException();
        }

        public Rol? GuardarRol(Rol rol)
        {
            throw new NotImplementedException();
        }

        public Rol? EliminarRol(int idRol)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region USUARIOS
        public Usuario? LeerUsuario(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public Usuario? LeerUsuario(string eMail)
        {
            throw new NotImplementedException();
        }

        public Usuario? EliminarUsuario(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public Usuario? GuardarUsuario(Usuario usuario)
        {
            if(usuario == null) throw new ArgumentNullException();

            // caso de un nuevo usuario
            if(usuario.Id < 0)
            {
                usuario.Id = siguienteUsuario++;
                Usuarios.Add(usuario.Id,usuario);
                //agregar log
            }
            else
            {
                // Es un usuario existente, entonces es una actualizacion
                Usuarios[usuario.Id] = usuario;
                // agregar log para la actualizacion

            }
            return usuario;
        }
        #endregion

        #region PROYECTOS
        public Proyecto? GuardarProyecto(Proyecto proyecto)
        {
            throw new NotImplementedException();
        }
        public Proyecto? EliminarProyecto(int idProyecto)
        {
            throw new NotImplementedException();
        }

        public Proyecto? LeerProyecto(int idProyecto)
        {
            throw new NotImplementedException();
        }
        #endregion
        public void Reiniciar()
        {
            // meter un par de datos aqui para poder probar luego al arrancar
            // la aplicacion

            // luego podemos quitar todos los datos que hay y agregar unos nuevos
            throw new NotImplementedException();
        }
    }
}
