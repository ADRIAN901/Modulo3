using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MM3.Entidades.AlgoritmoGrafos;

namespace MM3.Negocio.AlgoritmoGrafos
{    
    public class ManejadorArchivo
    {
        private string rutaArchivo = string.Empty;

        public ManejadorArchivo()
        {
            this.rutaArchivo = @"C:\Users\RuizAdri\Desktop\Desarrollo\ProgsNet\Adr\Maestría\ArchivoPrueba.txt";
        }

        public ManejadorArchivo(string ruta, string archivo)
        {
            if (string.IsNullOrEmpty(ruta))
                this.rutaArchivo = @"C:\Users\RuizAdri\Desktop\Desarrollo\ProgsNet\Adr\Maestría\ArchivoPrueba.txt";
            else                
                this.rutaArchivo = Path.Combine(ruta, archivo);            
        }

        public EntContenedor LecturaArchivo()
        {
            EntContenedor oEntContenedor = new EntContenedor();
            try
            {
                StreamReader objReader = new StreamReader(this.rutaArchivo);
                string sLine = "";
                List<string> arrText = new List<string>();
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        arrText.Add(sLine);
                }
                objReader.Close();
                if (arrText.Count > 0)
                {
                    oEntContenedor.lstEntNodo = this.GenerarListaNodos(arrText);
                    oEntContenedor.lstEntNodo = this.GenerarVerticesB(arrText, oEntContenedor.lstEntNodo);
                    oEntContenedor.lstEntNodo = this.ValidaUltimoNodo(arrText, oEntContenedor.lstEntNodo);
                    oEntContenedor.lstEntNodo = this.ActualizaIdVertices(oEntContenedor.lstEntNodo);
                }
                else
                {
                    oEntContenedor.mensajeError = "No hay datos en el archivo";
                    oEntContenedor.eTipoError = TipoError.Warning;
                }
            }
            catch (ApplicationException aEx)
            {
                oEntContenedor.mensajeError = aEx.Message;
                oEntContenedor.eTipoError = TipoError.Warning;
            }
            catch (Exception ex)
            {
                oEntContenedor.mensajeError = ex.Message;
                oEntContenedor.eTipoError = TipoError.Error;
            }
            return oEntContenedor;
        }

        private List<EntNodo> GenerarListaNodos(List<string> lstDatos)
        {
            List<EntNodo> lstEntNodo = new List<EntNodo>();
            EntNodo oEntNodo;
            int identificador = 1;
            for (int i = 0; i < lstDatos.Count; i++)
            {
                string[] datos = lstDatos[i].Split(' ');
                if (datos.Length < 3)
                    throw new ApplicationException("Algunos datos no tienen el formato correcto");

                if (lstEntNodo.Count == 0)
                {
                    oEntNodo = this.CreaNodo(datos[0], identificador);                    
                    oEntNodo.lstEntVertice.Add(this.CreaVertice(datos[1], datos[2]));
                    lstEntNodo.Add(oEntNodo);
                }
                else 
                {
                    bool agrega = true;

                    for (int x = 0; x < lstEntNodo.Count; x++)
                    {
                        if (lstEntNodo[x].nombreNodo == datos[0])
                        {
                            lstEntNodo[x].lstEntVertice.Add(this.CreaVertice(datos[1], datos[2]));
                            agrega = false;
                            break;
                        }                        
                    }
                    if (agrega)
                    {
                        identificador++;
                        oEntNodo = this.CreaNodo(datos[0], identificador);
                        oEntNodo.lstEntVertice.Add(this.CreaVertice(datos[1], datos[2]));
                        lstEntNodo.Add(oEntNodo);
                    }
                }
            }
            return lstEntNodo;
        }

        private List<EntNodo> GenerarVerticesB(List<string> lstDatos, List<EntNodo> lstEntNodo)
        {
            for (int i = 0; i < lstDatos.Count; i++)
            {
                string[] datos = lstDatos[i].Split(' ');                

                for (int j = 0; j < lstEntNodo.Count; j++)                
                    if (datos[1] == lstEntNodo[j].nombreNodo)                    
                        lstEntNodo[j].lstEntVertice.Add(this.CreaVertice(datos[0], datos[2]));
            }
            return lstEntNodo;
        }

        private List<EntNodo> ActualizaIdVertices(List<EntNodo> lstEntNodo)
        {
            for (int i = 0; i < lstEntNodo.Count; i++)
            {
                EntNodo oEntNodo = lstEntNodo[i];
                for (int x = 0; x < lstEntNodo.Count; x++)
                {
                    EntNodo oEntNodoB = lstEntNodo[x];
                    for (int j = 0; j < oEntNodoB.lstEntVertice.Count; j++)
                    {
                        EntVertice oEntVertice = oEntNodoB.lstEntVertice[j];
                        if (oEntVertice.nombreNodo == oEntNodo.nombreNodo)
                            oEntVertice.idNodo = oEntNodo.idNodo;
                    }
                }
            }
            return lstEntNodo;
        }

        private EntNodo CreaNodo(string nombreNodo, int identificador)
        {
            EntNodo oEntNodo = new EntNodo();
            oEntNodo.nombreNodo = nombreNodo;
            oEntNodo.idNodo = identificador;
            return oEntNodo;
        }

        private EntVertice CreaVertice(string nombreNodo, string peso)
        {
            EntVertice oEntVertice = new EntVertice();
            try
            {
                oEntVertice.nombreNodo = nombreNodo;
                oEntVertice.peso = Convert.ToInt32(peso);
            }
            catch
            {
                throw new ApplicationException("Algunos datos no tienen el formato correcto");
            }
            return oEntVertice;
        }

        private List<EntNodo> ValidaUltimoNodo(List<string> lstDatos, List<EntNodo> lstEntNodo)
        {
            List<EntNodo> lstEntNodoAux = new List<EntNodo>();
            lstEntNodoAux.AddRange(lstEntNodo);
            bool agrega;
            for (int i = 0; i < lstDatos.Count; i++)
            {
                agrega = true;
                string[] datos = lstDatos[i].Split(' ');
                for (int j = 0; j < lstEntNodoAux.Count; j++)
                {
                    if (datos[1] == lstEntNodoAux[j].nombreNodo)
                    {
                        agrega = false;
                        break;
                    }
                }
                if (agrega)
                {
                    EntNodo oEntNodo = CreaNodo(datos[1], lstEntNodoAux.Count + 1);
                    lstEntNodoAux.Add(oEntNodo);
                }
            }
            return lstEntNodoAux;
        }
    }
}
