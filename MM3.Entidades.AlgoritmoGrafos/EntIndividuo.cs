using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM3.Entidades.AlgoritmoGrafos
{
    public class EntIndividuo
    {
        public int identificador{get;set;}
        public string individuo { get; set; }
        public int valor { get; set; }
        public int valorFX { get; set; }
        public int rival { get; set; }
        public int puntoCruce { get; set; }
        public string cadenaCompleta { get; set; }
        public bool esValidado { get; set; }

        public EntIndividuo()
        {
            this.identificador = 0;
            this.individuo = string.Empty;
            this.valor = 0;
            this.valorFX = 0;
            this.rival = 0;
            this.puntoCruce = 0;
            this.cadenaCompleta = string.Empty;
            this.esValidado = false;
        }
    }
}
