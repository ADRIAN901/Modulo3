using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM3.Entidades.AlgoritmoGrafos
{
    public class EntContenedorGenetico:EntRespuesta 
    {
        public List<EntIndividuo> lstEntIndividuo { get; set; }
        public int iteraciones { get; set; }
        public int cantidadPoblacion { get; set; }

        public EntContenedorGenetico()
            : base()
        {
            this.lstEntIndividuo = new List<EntIndividuo>();
            this.iteraciones = 0;
            this.cantidadPoblacion = 0;
        }
    }
}
