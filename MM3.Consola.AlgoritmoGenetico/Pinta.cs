using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM3.Consola.AlgoritmoGenetico
{
    public class Pinta
    {
        public void PintaTitulo(string titulo)
        {
            Console.WriteLine("\n -----------------------------");
            Console.WriteLine(" --    " + titulo + "    --");
            Console.WriteLine(" -----------------------------\n");
        }
        public void PintaCabecero()
        {
            Console.WriteLine(" ------------------------------------");
            Console.WriteLine(" | Id | Indiv |  Va | FdX |  R | PC |");
            Console.WriteLine(" ------------------------------------");
        }
    }
}
