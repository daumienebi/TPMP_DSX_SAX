using Backend;
using Nucleo.Modelos;
using static System.Net.Mime.MediaTypeNames;

namespace Backend
{
    public class Datos : IDatos
    {
        // Colecciones persistentes en la instancia
        private readonly Dictionary<int, Usuario> usuarios = new();
        private readonly Dictionary<int, Rol> roles = new();
        private readonly Dictionary<int, Proyecto> proyectos = new();
        private readonly List<EntradaLog> logs = new();

        public Dictionary<int, Usuario> Usuarios => usuarios;
        public Dictionary<int, Rol> Roles => throw new NotImplementedException();
        public Dictionary<int, Proyecto> Proyectos => throw new NotImplementedException();
        public List<EntradaLog> Logs => logs;
        int siguienteUsuario = 1;
        int siguienteRol = 1;
        int siguienteProyecto = 1;
        int siguienteEntradaLog = 1;

        public Datos()
        {
            Reiniciar();
        }

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
            // Leemos el rol por id, si no existe devolvemos null
            if (Roles.ContainsKey(idRol))
            {
                return Roles[idRol];
            }
            else
            {
                return null;
            }   
        }

        public Rol? GuardarRol(Rol rol)
        {
            // Guardamos el rol, si es nuevo le asignamos un id y lo agregamos a la coleccion, si ya existe lo actualizamos
            if (rol == null) throw new ArgumentNullException();
            if (rol.Id < 0)
            {
                rol.Id = siguienteRol++;
                Roles.Add(rol.Id, rol);
                // Creamos una entrada de log para el rol nuevo
                String descripcionLog = $"Se ha creado un nuevo rol con ID {rol.Id} y nombre {rol.Nombre}.";
                EntradaLog log = new EntradaLog(DateTime.Now, TipoEntradaLog.AVISO, descripcionLog);
                Logs.Add(log);
            }
            else
            {
                // Es un rol existente, entonces es una actualizacion
                Roles[rol.Id] = rol;
                // agregar log para la actualizacion
                String descripcionLog = $"Se ha actualizado el rol con ID {rol.Id} y nombre {rol.Nombre}.";
                EntradaLog log = new EntradaLog(DateTime.Now, TipoEntradaLog.INFO, descripcionLog);
                Logs.Add(log);
            }
            return rol;
        }

        public Rol? EliminarRol(int idRol)
        {
            // Eliminamos el rol y devolvemos el rol eliminado, si no existe devolvemos null
            if (Roles.ContainsKey(idRol))
            {
                Rol rolEliminado = Roles[idRol];
                Roles.Remove(idRol);
                // Creamos una entrada de log para la eliminacion del rol
                String descripcionLog = $"Se ha eliminado el rol con ID {rolEliminado.Id} y nombre {rolEliminado.Nombre}.";
                EntradaLog log = new EntradaLog(DateTime.Now, TipoEntradaLog.AVISO, descripcionLog);
                Logs.Add(log);
                return rolEliminado;
            }
            else
            {
                return null;
            }
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
            // Guardamos el proyecto, si es nuevo le asignamos un id y lo agregamos a la coleccion, si ya existe lo actualizamos
            if (proyecto == null) throw new ArgumentNullException();
            if (proyecto.Id < 0)
            {
                proyecto.Id = siguienteProyecto++;
                Proyectos.Add(proyecto.Id, proyecto);
                // Creamos una entrada de log para el proyecto nuevo
                String descripcionLog = $"Se ha creado un nuevo proyecto con ID {proyecto.Id} y nombre {proyecto.Nombre}.";
                EntradaLog log = new EntradaLog(DateTime.Now, TipoEntradaLog.AVISO, descripcionLog);
                Logs.Add(log);
            }
            else
            {
                // Es un proyecto existente, entonces es una actualizacion
                Proyectos[proyecto.Id] = proyecto;
                // agregar log para la actualizacion
                String descripcionLog = $"Se ha actualizado el proyecto con ID {proyecto.Id} y nombre {proyecto.Nombre}.";
                EntradaLog log = new EntradaLog(DateTime.Now, TipoEntradaLog.INFO, descripcionLog);
                Logs.Add(log);
            }
            return proyecto;
        }
        public Proyecto? EliminarProyecto(int idProyecto)
        {
            // Eliminamos el proyecto y devolvemos el proyecto eliminado, si no existe devolvemos null
            if (Proyectos.ContainsKey(idProyecto))
            {
                Proyecto proyectoEliminado = Proyectos[idProyecto];
                Proyectos.Remove(idProyecto);
                // Creamos una entrada de log para la eliminacion del proyecto
                String descripcionLog = $"Se ha eliminado el proyecto con ID {proyectoEliminado.Id} y nombre {proyectoEliminado.Nombre}.";
                EntradaLog log = new EntradaLog(DateTime.Now, TipoEntradaLog.AVISO, descripcionLog);
                Logs.Add(log);
                return proyectoEliminado;
            }
            else
            {
                return null;
            }
        }

        public Proyecto? LeerProyecto(int idProyecto)
        {
            if (Proyectos.ContainsKey(idProyecto))
            {
                return Proyectos[idProyecto];
            }
            else
            {
                return null;
            }
        }
        #endregion

        public void crearUsuariosDePrueba()
        {
            // Agregar un par de usuarios de prueba que estaran disponibles al iniciar la aplicacion
            Usuario u1 = new Usuario("Juan", "Pérez", "a@a.com", "password1");
            Usuario u2 = new Usuario("María", "Gómez", "b@b.com", "password2");
            GuardarUsuario(u1);
            GuardarUsuario(u2);
        }

        public void crearRolesDePrueba()
        {
            // Agregar un par de roles de prueba que estaran disponibles al iniciar la aplicacion
            Rol r1 = new Rol("Administrador", "Rol con todos los permisos");
            Rol r2 = new Rol("Usuario", "Rol con permisos limitados");
            GuardarRol(r1);
            GuardarRol(r2);
        }

        public void crearProyectosDePrueba()
        {
            // Agregar un par de proyectos de prueba que estaran disponibles al iniciar la aplicacion
            Proyecto p1 = new Proyecto("Proyecto 1", "PJ", "Descripción del proyecto 1",DateTime.Now, false, false);
            Proyecto p2 = new Proyecto("Proyecto 2", "HM", "Descripción del proyecto 2", DateTime.Now, false, false);
            GuardarProyecto(p1);
            GuardarProyecto(p2);
        }

        public void Reiniciar()
        {
            crearProyectosDePrueba();
            crearRolesDePrueba();
            crearUsuariosDePrueba();

        }
    }
}
