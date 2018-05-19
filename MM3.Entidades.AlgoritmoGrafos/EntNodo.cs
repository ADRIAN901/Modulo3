using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM3.Entidades.AlgoritmoGrafos
{
    public class EntNodo : EntVertice
    {        
        public string nodoPrevio { get; set; }
        public bool esSeleccionado { get; set; }
        public List<EntVertice> lstEntVertice { get; set; }

        public EntNodo()
            : base()
        {            
            this.nombreNodo = string.Empty;
            this.nodoPrevio = string.Empty;
            this.esSeleccionado = false;
            this.lstEntVertice = new List<EntVertice>();
        }
    }
}
