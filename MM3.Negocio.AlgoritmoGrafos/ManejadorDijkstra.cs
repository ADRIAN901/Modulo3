using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM3.Entidades.AlgoritmoGrafos;

namespace MM3.Negocio.AlgoritmoGrafos
{
    public class ManejadorDijkstra
    {
        public EntContenedor AplicaAlgoritmo(EntContenedor oEntContenedor, TipoAlgoritmo eTipoAlgoritmo)
        {
            string camino = string.Empty;
            string nodoHijo = string.Empty;
            string nodoPadre = string.Empty;
            int aux = 0;
            try
            {
                for (int i = 0; i < oEntContenedor.lstEntNodo.Count; i++)
                {
                    EntNodo oEntNodo = oEntContenedor.lstEntNodo[i];
                    if (i == 0)
                    {
                        oEntNodo.esSeleccionado = true;
                        oEntNodo.nodoPrevio = oEntNodo.nombreNodo;
                        nodoHijo = oEntNodo.nombreNodo;
                        nodoPadre = oEntNodo.nombreNodo;
                        if (eTipoAlgoritmo == TipoAlgoritmo.Greedy)
                            oEntContenedor.lstEntNodo = this.ActualizaVertices(oEntContenedor.lstEntNodo, oEntNodo.idNodo);
                        oEntContenedor.lstResultados.Add(this.CreaResultado(aux, nodoPadre));
                    }
                    if (oEntNodo.nombreNodo == nodoHijo)
                    {                        
                        oEntNodo.esSeleccionado = true;
                        oEntNodo.nodoPrevio = nodoPadre;
                        oEntNodo.peso = aux;
                        oEntNodo.lstEntVertice = this.OrdenaLista(oEntNodo.lstEntVertice);
                        for (int j = 0; j < oEntNodo.lstEntVertice.Count; j++)
                        {                            
                            EntVertice oEntVertice = oEntNodo.lstEntVertice[j];
                            if (j == 0)
                            {
                                i = oEntVertice.idNodo - 2;
                                aux = oEntNodo.peso + oEntVertice.peso;
                                nodoHijo = oEntVertice.nombreNodo;
                            }
                            oEntContenedor.lstEntNodo = this.ActualizaVertices(oEntContenedor.lstEntNodo, oEntVertice.idNodo);                            
                        }
                        nodoPadre = oEntNodo.nombreNodo;
                        if (eTipoAlgoritmo == TipoAlgoritmo.Greedy)
                            oEntContenedor.lstEntNodo = this.ActualizaVerticesHijos(oEntContenedor.lstEntNodo, oEntNodo);
                        oEntContenedor.lstResultados.Add(this.CreaResultado(aux, nodoPadre));
                    }                    
                }
            }
            catch (Exception ex)
            {
                oEntContenedor.mensajeError = ex.Message;
                oEntContenedor.eTipoError = TipoError.Error;
            }
            return oEntContenedor;
        }

        public List<EntVertice> OrdenaLista(List<EntVertice> lstEntVertice)
        {
            List<EntVertice> lstEntVerticeAux = new List<EntVertice>();
            lstEntVerticeAux.AddRange(lstEntVertice);
            EntVertice t;
            for (int a = 1; a < lstEntVerticeAux.Count; a++)
                for (int b = lstEntVerticeAux.Count - 1; b >= a; b--)
                {
                    if (lstEntVerticeAux[b - 1].peso > lstEntVerticeAux[b].peso)
                    {
                        t = lstEntVerticeAux[b - 1];
                        lstEntVerticeAux[b - 1] = lstEntVerticeAux[b];
                        lstEntVerticeAux[b] = t;
                    }
                }
            return lstEntVerticeAux;
        }

        private List<EntNodo> ActualizaVertices(List<EntNodo> lstEntNodo, int identificador)
        {
            for (int i = 0; i < lstEntNodo.Count; i++)
            {
                EntNodo oEntNodo = lstEntNodo[i];
                for (int j = 0; j < lstEntNodo[i].lstEntVertice.Count; j++)
                    if (lstEntNodo[i].lstEntVertice[j].idNodo == identificador)
                    {
                        oEntNodo.lstEntVertice.Remove(lstEntNodo[i].lstEntVertice[j]);
                        j--;
                    }                
            }
            return lstEntNodo;
        }

        private List<EntNodo> ActualizaVerticesHijos(List<EntNodo> lstEntNodo, EntNodo oEntNodo)
        {
            for (int i = 0; i < oEntNodo.lstEntVertice.Count; i++)
            {
                EntVertice oEntVertice = oEntNodo.lstEntVertice[i];
                for (int j = 0; j < lstEntNodo.Count; j++)
                    for (int x = 0; x < lstEntNodo[j].lstEntVertice.Count; x++)
                    {
                        EntVertice oEntVerticeB = lstEntNodo[j].lstEntVertice[x];
                        if (oEntVertice.idNodo == oEntVerticeB.idNodo && oEntVerticeB.idNodo != lstEntNodo.Count)
                        {
                            lstEntNodo[j].lstEntVertice.Remove(oEntVerticeB);
                            x--;
                        }
                    }
            }
            return lstEntNodo; 
        }

        private string CreaResultado(int pesoAcumulado, string nodoPadre)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            sb.Append(pesoAcumulado.ToString());
            sb.Append(", ");
            sb.Append(nodoPadre);
            sb.Append(")");
            return sb.ToString();
        }
    }
}
