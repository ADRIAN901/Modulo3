using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM3.Entidades.AlgoritmoGrafos;
using MM3.Negocio.AlgoritmoGrafos;

namespace MM3.Consola.AlgoritmoGrafos
{
    class Program
    {
        static void Main(string[] args)
        {
            string rutaArchivo = ConfigurationManager.AppSettings["RutaArchivoDijkstra"].ToString();
            string nombreArchivo = ConfigurationManager.AppSettings["NombreArchivoDijkstra"].ToString();
            TipoAlgoritmo eTipoAlgoritmo = (TipoAlgoritmo)Convert.ToInt32(ConfigurationManager.AppSettings["TipoAlgoritmo"].ToString());

            EntContenedor oEntContenedor = new ManejadorArchivo(rutaArchivo, nombreArchivo).LecturaArchivo();
            if (oEntContenedor.eTipoError == TipoError.SinError)
            {
                oEntContenedor = new ManejadorDijkstra().AplicaAlgoritmo(oEntContenedor, eTipoAlgoritmo);
                if (oEntContenedor.eTipoError == TipoError.SinError)                
                    for (int i = 0; i < oEntContenedor.lstResultados.Count - 1; i++)
                        Console.WriteLine(oEntContenedor.lstResultados[i]);
                else
                    Console.WriteLine(oEntContenedor.eTipoError.ToString() + ": \n" + oEntContenedor.mensajeError);
            }
            else
                Console.WriteLine(oEntContenedor.eTipoError.ToString() + ": \n" + oEntContenedor.mensajeError);
            
            Console.ReadLine();
        }
    }
}
