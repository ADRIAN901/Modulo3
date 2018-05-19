using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM3.Entidades.AlgoritmoGrafos;
using MM3.Negocio.AlgoritmoGrafos;
using System.Configuration;

namespace MM3.Consola.AlgoritmoGenetico
{
    class Program
    {
        static void Main(string[] args)
        {
            int numeroHijos = Convert.ToInt32(ConfigurationManager.AppSettings["NumeroHijos"].ToString());
            Pinta oPinta = new Pinta();

            EntContenedorGenetico oEntContenedorGenetico = new EntContenedorGenetico();
            oEntContenedorGenetico.iteraciones = Convert.ToInt32(ConfigurationManager.AppSettings["NumeroIteraciones"].ToString());
            oEntContenedorGenetico.cantidadPoblacion = Convert.ToInt32(ConfigurationManager.AppSettings["NumeroPoblacion"].ToString());
            oEntContenedorGenetico.lstEntIndividuo = new ManejadorGeneticos().GeneraArreglo(oEntContenedorGenetico.cantidadPoblacion);
            oPinta.PintaTitulo("POBLACIÓN INICIAL");

            oPinta.PintaCabecero();
            for (int i = 0; i < oEntContenedorGenetico.lstEntIndividuo.Count; i++)
                Console.WriteLine(oEntContenedorGenetico.lstEntIndividuo[i].cadenaCompleta);

            for (int i = 1; i <= oEntContenedorGenetico.iteraciones; i++)
            {
                oPinta.PintaTitulo("ITERACIÓN ###" + i.ToString() + "###");
                oPinta.PintaCabecero();
                oEntContenedorGenetico.lstEntIndividuo = new ManejadorGeneticos().GeneraIteracion(oEntContenedorGenetico.lstEntIndividuo, oEntContenedorGenetico.cantidadPoblacion);
                for (int j = 0; j < oEntContenedorGenetico.lstEntIndividuo.Count; j++)
                    Console.WriteLine(oEntContenedorGenetico.lstEntIndividuo[j].cadenaCompleta);
            }
                
            Console.ReadLine();
        }
    }
}
