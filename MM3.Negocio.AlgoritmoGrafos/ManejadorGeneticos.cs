using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MM3.Entidades.AlgoritmoGrafos;

namespace MM3.Negocio.AlgoritmoGrafos
{
    public class ManejadorGeneticos
    {
        #region Publicos
        /// <summary>
        /// Genera la población inicial
        /// </summary>
        /// <param name="cantidadIndividuos">cantidad de individuos que integrarán la población</param>
        /// <returns></returns>
        public List<EntIndividuo> GeneraArreglo(int cantidadIndividuos)
        {
            List<EntIndividuo> lstEntIndividuo = new List<EntIndividuo>();
            for (int i = 0; i < cantidadIndividuos; i++)
            {
                EntIndividuo oEntIndividuo = new EntIndividuo();
                oEntIndividuo.identificador = i + 1;
                oEntIndividuo.individuo = this.GeneraIndividuo();
                oEntIndividuo.valor = this.CalculaValor(oEntIndividuo.individuo);
                oEntIndividuo.valorFX = oEntIndividuo.valor * oEntIndividuo.valor;
                oEntIndividuo.puntoCruce = this.GeneraAleatorio(1, 3);
                    
                for (int j = 0; j < 1; j++)
                {
                    oEntIndividuo.rival = this.GeneraAleatorio(1, cantidadIndividuos);
                    if (oEntIndividuo.identificador == oEntIndividuo.rival)
                        j--;
                }

                oEntIndividuo.cadenaCompleta = this.GeneraCadenaCompeta(oEntIndividuo);
                lstEntIndividuo.Add(oEntIndividuo);
            }

            return lstEntIndividuo;
        }

        public List<EntIndividuo> GeneraIteracion(List<EntIndividuo> lstEntIndividuo, int cantidadIndividuos)
        {
            string ganadores = this.GeneraGanadores(lstEntIndividuo);
            List<string> lstGanadores = this.OrdenaGanadores(ganadores);
            List<EntIndividuo> lstEntIndividuoPadres = this.GeneraPadres(lstGanadores, lstEntIndividuo);
            List<EntIndividuo> lstEntIndividuoHijos = this.GeneraHijos(lstEntIndividuoPadres, cantidadIndividuos);
            
            return lstEntIndividuoHijos;
        }
        #endregion

        #region Privados
        private string GeneraIndividuo()
        {
            StringBuilder sbIndividuo = new StringBuilder();
            string cadenaRandom = string.Empty;
            int random = 0;
            int final = 0;
            
            for (int i = 0; i < 5; i++)
            {
                random = this.GeneraAleatorio(0, 1000);
                if (random == 1000)
                    cadenaRandom = "1.0";
                else
                    cadenaRandom = "0." + Convert.ToInt32(random).ToString();

                final = decimal.Compare(Convert.ToDecimal(0.5), Convert.ToDecimal(cadenaRandom));
 
                if(final <= 0)
                    sbIndividuo.Append("0");
                else
                    sbIndividuo.Append("1");

                Thread.Sleep(100);
            }
            return sbIndividuo.ToString();
        }

        private int CalculaValor(string Individuo)
        {
            int valor = 0, equiv = 16;
            for (int i = 0; i < Individuo.Length; i++)
            {
                int aux = Convert.ToInt32(Individuo.Substring(i, 1));
                valor += equiv * aux;
                equiv /= 2;
            }
            return valor;
        }

        private int GeneraAleatorio(int rangoMenor, int rangoMayor)
        {
            return new Random().Next(rangoMenor, rangoMayor + 1);
        }

        private string GeneraCadenaCompeta(EntIndividuo oEntIndividuo)
        {
            string cadena = " | " + oEntIndividuo.identificador.ToString("00") + " | " + oEntIndividuo.individuo +
                                               " | " + oEntIndividuo.valor.ToString("000") + " | " +
                                               oEntIndividuo.valorFX.ToString("000") + " | " + oEntIndividuo.rival.ToString("00") +
                                               " | " + oEntIndividuo.puntoCruce.ToString("00") + " |";
            return cadena;
        }

        private string GeneraGanadores(List<EntIndividuo> lstEntIndividuo)
        {
            string ganadores = string.Empty;
            for (int i = 0; i < lstEntIndividuo.Count; i++)
            {
                EntIndividuo oEntIndividuo1 = lstEntIndividuo[i];
                for (int j = 0; j < lstEntIndividuo.Count; j++)
                {
                    EntIndividuo oEntIndividuo2 = lstEntIndividuo[j];
                    if (oEntIndividuo1.rival == oEntIndividuo2.identificador)
                    {
                        if (oEntIndividuo1.valorFX >= oEntIndividuo2.valorFX)
                            ganadores += oEntIndividuo1.identificador.ToString() + ",";
                        else
                            ganadores += oEntIndividuo2.identificador.ToString() + ",";
                    }
                }
            }
            return ganadores;
        }

        private List<string> OrdenaGanadores(string cadenaGanadores)
        {
            string[] elem = cadenaGanadores.Split(',');
            List<string> lstGanadores = new List<string>();
            int anterior = 0;

            for (int i = 0; i < elem.Length; i++)
                if (string.IsNullOrEmpty(elem[i]))
                    elem[i] = "1000";

            string t = string.Empty;
            for (int a = 1; a < elem.Length; a++)
                for (int b = elem.Length - 1; b >= a; b--)
                {
                    if (Convert.ToInt32(elem[b - 1]) > Convert.ToInt32(elem[b]))
                    {
                        t = elem[b - 1];
                        elem[b - 1] = elem[b];
                        elem[b] = t;
                    }
                }

            for (int i = 0; i < elem.Length; i++)
            {
                if (anterior == 0)
                {
                    lstGanadores.Add(elem[i]);
                    anterior = Convert.ToInt32(elem[i]);
                }
                else {
                    if (Convert.ToInt32(elem[i]) != anterior)
                    {
                        lstGanadores.Add(elem[i]);
                        anterior = Convert.ToInt32(elem[i]);
                    }
                }
                if (lstGanadores.Count == 3)
                    break;
            }
            return lstGanadores;
        }

        private List<EntIndividuo> GeneraPadres(List<string> lstGanadores, List<EntIndividuo> lstEntIndividuo)
        {
            List<EntIndividuo> lstEntIndividuoPadres = new List<EntIndividuo>();
            for (int i = 0; i < lstGanadores.Count; i++)
            {
                for (int j = 0; j < lstEntIndividuo.Count; j++)
                {
                    if (Convert.ToInt32(lstGanadores[i]) == lstEntIndividuo[j].identificador)
                        lstEntIndividuoPadres.Add(lstEntIndividuo[j]);
                }
            }
            return lstEntIndividuoPadres;
        }

        private List<EntIndividuo> GeneraHijos(List<EntIndividuo> lstEntIndividuoPadres, int cantidadIndividuos)
        {
            List<EntIndividuo> lstEntIndividuoHijos = new List<EntIndividuo>();
            int identificador = 0;
            for (int i = 0; i < lstEntIndividuoPadres.Count; i++)
            {
                EntIndividuo oEntIndividuoPadre1 = lstEntIndividuoPadres[i];
                for (int j = 0; j < lstEntIndividuoPadres.Count; j++)
                {
                    EntIndividuo oEntIndividuoPadre2 = lstEntIndividuoPadres[j];
                    if (oEntIndividuoPadre1.identificador != oEntIndividuoPadre2.identificador && oEntIndividuoPadre2.esValidado == false)
                    {
                        lstEntIndividuoPadres[j].esValidado = true;
                        EntIndividuo oEntIndividuoHijo1 = new EntIndividuo();
                        EntIndividuo oEntIndividuoHijo2 = new EntIndividuo();
                        identificador++;
                        oEntIndividuoHijo1.identificador = identificador;
                        oEntIndividuoHijo1.individuo += oEntIndividuoPadre1.individuo.Substring(0, oEntIndividuoPadre1.puntoCruce);
                        oEntIndividuoHijo1.individuo += oEntIndividuoPadre2.individuo.Substring(oEntIndividuoPadre1.puntoCruce - 1, oEntIndividuoPadre1.individuo.Length - oEntIndividuoPadre1.puntoCruce);
                        oEntIndividuoHijo1.valor = this.CalculaValor(oEntIndividuoHijo1.individuo);
                        oEntIndividuoHijo1.valorFX = oEntIndividuoHijo1.valor * oEntIndividuoHijo1.valor;
                        oEntIndividuoHijo1.puntoCruce = this.GeneraAleatorio(1, 3);
                        for (int x = 0; x < 1; x++)
                        {
                            oEntIndividuoHijo1.rival = this.GeneraAleatorio(1, cantidadIndividuos);
                            if (oEntIndividuoHijo1.identificador == oEntIndividuoHijo1.rival)
                                x--;
                        }
                        oEntIndividuoHijo1.cadenaCompleta = this.GeneraCadenaCompeta(oEntIndividuoHijo1);

                        identificador++;
                        oEntIndividuoHijo2.identificador = identificador;
                        oEntIndividuoHijo2.individuo += oEntIndividuoPadre2.individuo.Substring(0, oEntIndividuoPadre1.puntoCruce);
                        oEntIndividuoHijo2.individuo += oEntIndividuoPadre1.individuo.Substring(oEntIndividuoPadre1.puntoCruce - 1, oEntIndividuoPadre1.individuo.Length - oEntIndividuoPadre1.puntoCruce);
                        oEntIndividuoHijo2.valor = this.CalculaValor(oEntIndividuoHijo2.individuo);
                        oEntIndividuoHijo2.valorFX = oEntIndividuoHijo2.valor * oEntIndividuoHijo1.valor;
                        oEntIndividuoHijo2.puntoCruce = this.GeneraAleatorio(1, 3);
                        for (int x = 0; x < 1; x++)
                        {
                            oEntIndividuoHijo2.rival = this.GeneraAleatorio(1, cantidadIndividuos);
                            if (oEntIndividuoHijo2.identificador == oEntIndividuoHijo2.rival)
                                x--;
                        }
                        oEntIndividuoHijo2.cadenaCompleta = this.GeneraCadenaCompeta(oEntIndividuoHijo2);

                        lstEntIndividuoHijos.Add(oEntIndividuoHijo1);
                        lstEntIndividuoHijos.Add(oEntIndividuoHijo2);
                    }
                }
            }
            return lstEntIndividuoHijos;
        }
        #endregion
    }
}
