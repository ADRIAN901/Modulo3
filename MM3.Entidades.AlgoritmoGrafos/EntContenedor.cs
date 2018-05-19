using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM3.Entidades.AlgoritmoGrafos
{
    public class EntContenedor : EntRespuesta
    {
        public List<EntNodo> lstEntNodo { get; set; }
        public List<string> lstResultados { get; set; }

        public EntContenedor()
            : base()
        {
            this.lstEntNodo = new List<EntNodo>();
            this.lstResultados = new List<string>();
        }
    }
}
