using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM3.Entidades.AlgoritmoGrafos
{
    public enum TipoError
    {
        SinError = 0,
        Warning = 1,
        Error = 2
    }

    public enum TipoAlgoritmo
    {
        Dijkstra = 0,
        Greedy = 1
    }
}
