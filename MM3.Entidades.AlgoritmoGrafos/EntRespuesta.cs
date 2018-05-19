using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM3.Entidades.AlgoritmoGrafos
{
    public class EntRespuesta
    {
        public string mensajeError { get; set; }
        public TipoError eTipoError { get; set; }

        public EntRespuesta()
        {
            this.mensajeError = string.Empty;
            this.eTipoError = TipoError.SinError;
        }
    }
}
