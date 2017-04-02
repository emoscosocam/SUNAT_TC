using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;   // Se importa esto para poder usar el método Regex.Split()


namespace SUNAT_TC_Test_CS
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }


        // Este es un helper method porque DateSerial() no existe en C#
        private DateTime DateSerial(int intYear, int intMonth, int intDay)
        {
            return new DateTime(intYear, intMonth, intDay);
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            InicializarCombos();
        }
        private void InicializarCombos()
        {
            int i = 0;

            for (i = 1; i <= 12; i++)
            {
                CmbMes.Items.Add(i.ToString("0#"));
            }
            CmbMes.Text = DateTime.Now.Month.ToString("0#");

            for (i = DateTime.Now.Year; i >= 1995; i += -1)
            {
                CmbAño.Items.Add(i);
            }

            CmbAño.SelectedIndex = 0;
        }

        private void BtnCargar_Click(object sender, EventArgs e)
        {
            // Se manda poblar la tabla...
            ObtenerDatos(this.DataTable1, Convert.ToInt32(this.CmbAño.Text), Convert.ToInt32(this.CmbMes.Text));
        }


        private void ObtenerDatos(System.Data.DataTable objDataTable, int intAño, int intMes)
        {
            System.Net.WebClient objWebClient;
            string strHTML = "";
            string strURL;
            

            objWebClient = new System.Net.WebClient();

           
            // La página del TC de la SUNAT acepta un Query String donde se le indica el año y el mes
            strURL = "http://www.sunat.gob.pe/cl-at-ittipcam/tcS01Alias?mes=" + intMes + "&anho=" + intAño;

            // Si la función retorna falso, entonces no se procesa
            if (CargarDocHTML(objWebClient, strURL, ref strHTML))
            {
                ProcesarDocHTML(strHTML, intAño, intMes, objDataTable);
            }

          
            objWebClient.Dispose();

            objDataTable.AcceptChanges();// Commit
        }

        // Esta rutina sólo se encarga de conectarse a la web y obtener el HTML
        private bool CargarDocHTML(System.Net.WebClient objWebClient, string strURL,  ref string strHTML)
        {
            bool bAux = false;
           
            try
            {
                strHTML = objWebClient.DownloadString(strURL);
                // Esto podría fallar (debido a problemas con la red, o que el servidor esté fuera de linea, etc)

                bAux = !(strHTML.IndexOf("No existe Informaci") > 0);
                // Esta función retorna False si no hay datos
            }
            catch (Exception ex)
            {
                // Si hay una excepción no se hace nada               
            }

            return bAux;
        }

        // Esta rutina "procesa" el HTML como String
        // Lo que hace es recorrer el código HTML de la página y obtener de allí subcadenas...
        private void ProcesarDocHTML(string strHTML, int intAño, int intMes, System.Data.DataTable objDataTable)
        {
            string strTable;
            string[] strFilas;
            string[] strCeldas;
            int intFirstPosition;
            int intLastPosition;
            int i;
            int j;

            System.DateTime dtFecha = DateTime.Now; // En C# se debe inicializar la variable para poder pasarle por Referencia
            double dblCompra = 0.0;
            double dblVenta = 0.0;

            intFirstPosition = strHTML.IndexOf("form-table");  // Primero se obtiene la posición de la cadena "form-table"

            intFirstPosition = strHTML.IndexOf("<tr>", intFirstPosition) - 1; // Una vez que el elemento table se ha localizado, se avanza el puntero hasta la primera fila
                                                                              // En una tabla HTML, las filas se identifican con <tr>

            intLastPosition = strHTML.IndexOf("</table>", intFirstPosition) - 7;  // Se obtiene la posición del elemento de cierre "</table>"

            strTable = strHTML.Substring(intFirstPosition, intLastPosition - intFirstPosition);  // Se extrae una subcadena que contiene todas las filas de la tabla

            strFilas = Regex.Split(strTable, "</tr>"); // Se usa Split() para obtener un arreglo de filas. Ojo que la primera fila contiene las cabeceras de la tabla

            // El bucle empieza desde 1 en vez de 0 para saltearse la fila de cabeceras
            for (i = 1; i <= strFilas.GetUpperBound(0); i++)
            {
                strCeldas = Regex.Split(strFilas[i].Trim(), "/td>");  // Se usa Split() para obtener un arrego de celdas
                                                                    // Al dejar el caracter '<' se puede delimitar luego el contenido de la celda

                // strCeldas tiene TODAS las celdas de una fila, pero se necesitan leer las celdas 3 a la vez... 
                //Las celdas siempre aparecen en grupos de 3: ("Dia", "Compra", "Venta")
                for (j = 0; j <= strCeldas.GetUpperBound(0); j += 3)
                {
                    // La función RegEx.Split() a veces deja una cadena nula al final del arreglo. Por eso se itera sólo donde hay datos
                    if (!string.IsNullOrEmpty(strCeldas[j]))
                    {
                        ObtenerTriplete(strCeldas, j, intAño, intMes, ref dtFecha, ref dblCompra, ref dblVenta); // Un triplete se define como (dtFecha, dblCompra, dblVenta)
                        AddRow(objDataTable, dtFecha, dblCompra, dblVenta);
                    }
                }
            }
        }

        // Valores posibles para intIndice: 0, 3, 6, 9
        // Un triplete es un grupo de 3 celdas, una para la Fecha, otra para el valor de Compra y finalmente otra para el valor de Venta
        private void ObtenerTriplete(string[] strCeldas, int intIndice, int intAño, int intMes, ref System.DateTime dtFecha, ref double dblCompra, ref double dblVenta)
        {
            int intDia = 0;

            intDia = Convert.ToInt32(ObtenerValorCelda(strCeldas[intIndice]));
            dtFecha = DateSerial(intAño, intMes, intDia);

            dblCompra = Convert.ToDouble(ObtenerValorCelda(strCeldas[intIndice + 1]));
            dblVenta = Convert.ToDouble(ObtenerValorCelda(strCeldas[intIndice + 2]));
        }

        // Esta función extrae el Valor del elemento <td>
        private string ObtenerValorCelda(string strCelda)
        {
            string strValorCelda;
            int intFirstPosition;
            int intLastPosition;

            intFirstPosition = strCelda.IndexOf("<strong>"); // El elemento "<strong>" sólo se encuentra en la primera columna del triplete ("Dia")
            if (intFirstPosition >= 0) // Los indices de IndexOf() empiezan en 0
            {
                intLastPosition = strCelda.IndexOf( "</strong>");
                intFirstPosition = intFirstPosition + 8; 
            }
            else // Si el elemento "<strong>" no se ha encontrado, entonces estamos en la columna "Compra" o "Venta"
            {
                intFirstPosition = strCelda.IndexOf("tne10"); // la cadena 'class = "tne10"' sólo se encuentra en las columnas "Compra" o "Venta"
                intFirstPosition = intFirstPosition + 8;
                intLastPosition = strCelda.IndexOf("<", intFirstPosition);  // El caracter '<' fue dejado intencionalmente cuando se usó RegEx.Split() para poderse usarsele como marcador aquí
            }

            strValorCelda = strCelda.Substring(intFirstPosition, intLastPosition - intFirstPosition).Trim();
            // Después de obtenerse las posiciones, se extrae la subcadena del valor

            return strValorCelda;
        }

        // Agrega una fila en objDataTable
        private void AddRow(System.Data.DataTable objDataTable, System.DateTime dtFecha, double dblCompra, double dblVenta)
        {
            System.Data.DataRow objDataRow;

            objDataRow = objDataTable.NewRow();

            objDataRow[0] = dtFecha;
            objDataRow[1] = dblCompra;
            objDataRow[2] = dblVenta;

            objDataTable.Rows.Add(objDataRow);
        }

       
    }
}
