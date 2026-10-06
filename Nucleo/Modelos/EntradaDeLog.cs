using System;
using System.Collections.Generic;
using System.Text;

namespace Nucleo.Modelos
{
    public class EntradaDeLog
    {
        private int id = -1;
        private Usuario usuario;
        private DateTime fechaHora;
        private TipoEntradaLog tipoEntradaLog;
        private string descripcion;

        public EntradaDeLog(int id, Usuario usuario, DateTime fechaHora, TipoEntradaLog tipoEntradaLog, string descripcion)
        {
            this.usuario = usuario;
            this.fechaHora = fechaHora;
            this.tipoEntradaLog = tipoEntradaLog;
            this.descripcion = descripcion;
        }
        public int Id { get => id; set => id = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public TipoEntradaLog TipoEntradaLog { get => tipoEntradaLog; set => tipoEntradaLog = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
