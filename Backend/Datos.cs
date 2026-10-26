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
        public Dictionary<int, Rol> Roles => roles;
        public Dictionary<int, Proyecto> Proyectos => proyectos;
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
            }
            else
            {
                // Es un rol existente, entonces es una actualizacion
                Roles[rol.Id] = rol;
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
                return usuarioEliminado;
            }
            else
            {
                return null;
            }
        }

        public Usuario? GuardarUsuario(Usuario usuario)
        {
            if (usuario == null) throw new ArgumentNullException();
            // caso de un nuevo usuario
            if(usuario.Id < 0)
            {
                usuario.Id = siguienteUsuario++;
                Usuarios.Add(usuario.Id,usuario);
            }
            else
            {
                // Es un usuario existente, entonces es una actualizacion
                Usuarios[usuario.Id] = usuario;
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
            }
            else
            {
                // Es un proyecto existente, entonces es una actualizacion
                Proyectos[proyecto.Id] = proyecto;
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

        /*
         * Creamos un par de usuarios de prueba para poder probar la aplicacion
         * Se crea un usuario administrador y un usuario normal
         */
        public void crearUsuariosDePrueba()
        {
            // Obtenemos el rol de administrador para asignarlo al usuario administrador
            Rol rolAdmin = LeerRol(1); // El 1 será del admin
            Rol rolUser = LeerRol(2); // El 2 será del user
            // Creamos un usuario administrador y un usuario normal para las pruebas
            Usuario admin = new Usuario("Admin", "User", "admin@admin.com",rolAdmin,true,DateTime.Now.AddDays(365), "admin123");
            // Agregar un par de usuarios con todos los campos y roles para probar la aplicacion
            Usuario u1 = new Usuario("Juan", "Pérez", "a@a.com", rolUser, true,DateTime.Now.AddDays(365), "password1");
            Usuario u2 = new Usuario("María", "Gómez", "b@b.com", rolUser, true,DateTime.Now.AddDays(365), "password2");
            GuardarUsuario(admin);
            GuardarUsuario(u1);
            GuardarUsuario(u2);
            // Agregar una entrada de log para indicar que se han creado los usuarios de prueba
            String descripcionLog = "Se han creado los usuarios de prueba: " + admin.Email + ", " + u1.Email + ", " + u2.Email;
            AgregarLog(new EntradaLog(null, DateTime.Now, TipoEntradaLog.INFO, descripcionLog));
        }

        /*
         * Creamos un par de roles de prueba para poder probar la aplicacion
         * Se crea un rol administrador y un rol usuario normal
         */
        public void crearRolesDePrueba()
        {
            // Obtener el primer usuario que es el admin para asociarlo al log
            // (esto se hace al no tener una base de datos real)
            Usuario admin = LeerUsuario(1);
            // Agregar un par de roles de prueba que estaran disponibles al iniciar la aplicacion
            Rol r1 = new Rol("Administrador", "Rol con todos los permisos");
            Rol r2 = new Rol("Usuario", "Rol con permisos limitados");
            GuardarRol(r1);
            GuardarRol(r2);
            // Agregar una entrada de log para indicar que se han creado los roles de prueba
            String descripcionLog = "Se han creado los roles de prueba: " + r1.Nombre + ", " + r2.Nombre;
            AgregarLog(new EntradaLog(null, DateTime.Now, TipoEntradaLog.INFO, descripcionLog));
        }

        /*
         * Creamos un par de proyectos de prueba para poder probar la aplicacion
         * Se crean proyectos con diferentes configuraciones para probar la aplicacion
         */
        public void crearProyectosDePrueba()
        {
            // Agregar un par de proyectos de prueba que estaran disponibles al iniciar la aplicacion
            Proyecto p1 = new Proyecto("SAX DSX", "SD", "Descripción del proyecto 1",DateTime.Now, false, false);
            Proyecto p2 = new Proyecto("Clash Eleven", "CE", "Descripción del proyecto 2", DateTime.Now, true, true);
            Proyecto p3 = new Proyecto("Pico", "PC", "Descripción del proyecto 3", DateTime.Now, true, true);
            GuardarProyecto(p1);
            GuardarProyecto(p2);
            GuardarProyecto(p3);
            // Agregar una entrada de log para indicar que se han creado los proyectos de prueba
            String descripcionLog = "Se han creado los proyectos de prueba: " + p1.Nombre + ", " + p2.Nombre + ", " + p3.Nombre;
            AgregarLog(new EntradaLog(null, DateTime.Now, TipoEntradaLog.INFO, descripcionLog));
        }

        /*
         * Reinicia los datos de la aplicacion, eliminando todos los usuarios, roles y proyectos existentes
         * y creando los de prueba nuevamente
         */
        public void Reiniciar()
        {
            crearRolesDePrueba();
            crearProyectosDePrueba();
            crearUsuariosDePrueba();
        }
    }
}
