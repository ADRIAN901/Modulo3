using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM3.Entidades.AlgoritmoGrafos
{
    public class EntVertice
    {
        public int idNodo { get; set; }
        public string nombreNodo { get; set; }
        public int peso { get; set; }

        public EntVertice()            
        {
            this.idNodo = 0;
            this.nombreNodo = string.Empty;
            this.peso = 0;
        }
    }
}
