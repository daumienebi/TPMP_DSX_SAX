using BackEnd;
using Nucleo.Modelos;

namespace Backend
{
    public class Datos : IDatos
    {
        public Dictionary<int, Usuario> Usuarios => new Dictionary<int, Usuario>();

        public Dictionary<int, Rol> Roles => throw new NotImplementedException();

        public Dictionary<int, Proyecto> Proyectos => throw new NotImplementedException();

        public List<EntradaLog> Logs => new List<EntradaLog>();
        int siguienteUsuario = 1;
        int siguienteRol = 1;
        int siguienteProyecto = 1;
        int siguienteEntradaLog = 1;
        #region LOGS
        public void AgregarLog(EntradaLog log)
        {
            if (log == null) throw new ArgumentNullException();
            // Caso en el que es un log nuevo
            if(log.Id < 0)
            {
                log.Id = siguienteEntradaLog++;
            }
            Logs.Add(log);
            // Entendemos que no se debe poder manipular un log existente,
            // por lo que no se contempla la actualizacion de un log ya existente
        }

        public int LimpiarLog()
        {
            // Limpiamos todos los logs y devolvemos el número de logs eliminados
            int cantidadEliminada = Logs.Count;
            Logs.Clear();
            return cantidadEliminada;
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
            // Leemos el usuario por id, si no existe devolvemos null
            if (Usuarios.ContainsKey(idUsuario))
            {
                return Usuarios[idUsuario];
            }
            else
            {
                return null;
            }
        }

        public Usuario? LeerUsuario(string eMail)
        {
            // Leemos el usuario por email, si no existe devolvemos null
            foreach (var usuario in Usuarios.Values)
            {
                if (usuario.Email == eMail)
                {
                    return usuario;
                }
            }
            return null;
        }

        public Usuario? EliminarUsuario(int idUsuario)
        {
            // Eliminamos el usuario y devolvemos el usuario eliminado, si no existe devolvemos null
            if (Usuarios.ContainsKey(idUsuario))
            {
                Usuario usuarioEliminado = Usuarios[idUsuario];
                Usuarios.Remove(idUsuario);
                // Creamos una entrada de log para la eliminacion del usuario
                String descripcionLog = $"Se ha eliminado el usuario con ID {usuarioEliminado.Id} y email {usuarioEliminado.Email}.";
                EntradaLog log = new EntradaLog(DateTime.Now, TipoEntradaLog.AVISO, descripcionLog);
                Logs.Add(log);
                return usuarioEliminado;
            }
            else
            {
                return null;
            }
        }

        public Usuario? GuardarUsuario(Usuario usuario)
        {
            if(usuario == null) throw new ArgumentNullException();
            // caso de un nuevo usuario
            if(usuario.Id < 0)
            {
                usuario.Id = siguienteUsuario++;
                Usuarios.Add(usuario.Id,usuario);
                // Creamos una entrada de log para el usuario nuevo
                String descripcionLog = $"Se ha creado un nuevo usuario con ID {usuario.Id} y email {usuario.Email}.";
                EntradaLog log = new EntradaLog(DateTime.Now, TipoEntradaLog.AVISO, descripcionLog);
                Logs.Add(log);
            }
            else
            {
                // Es un usuario existente, entonces es una actualizacion
                Usuarios[usuario.Id] = usuario;
                // agregar log para la actualizacion
                String descripcionLog = $"Se ha actualizado el usuario con ID {usuario.Id} y email {usuario.Email}.";
                EntradaLog log = new EntradaLog(DateTime.Now, TipoEntradaLog.INFO, descripcionLog);
                Logs.Add(log);
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
