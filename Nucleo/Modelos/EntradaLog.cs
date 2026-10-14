using System;
using System.Collections.Generic;
using System.Text;

namespace Nucleo.Modelos
{
    public class EntradaLog
    {
        private int id = -1;
        private Usuario usuario;
        private DateTime fechaHora;
        //private TipoEntradaLog tipoEntradaLog;
        private String tipoEntradaLog;
        private string descripcion;

        public EntradaLog(Usuario usuario, DateTime fechaHora, String tipoEntradaLog, string descripcion)
        {
            this.usuario = usuario;
            this.fechaHora = fechaHora;
            this.tipoEntradaLog = tipoEntradaLog;
            this.descripcion = descripcion;
        }

        /*Otro constructor para utiliza cuando no se tiene un usuario específico */
        public EntradaLog(DateTime fechaHora, String tipoEntradaLog, string descripcion)
        {
            this.fechaHora = fechaHora;
            this.tipoEntradaLog = tipoEntradaLog;
            this.descripcion = descripcion;
        }
        public int Id { get => id; set => id = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public String TipoEntradaLog { get => tipoEntradaLog; set => tipoEntradaLog = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
