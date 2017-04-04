

using System;
using System.Data;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;
using System.Text.RegularExpressions;   // Se importa esto para poder usar el método Regex.Split()

[Microsoft.SqlServer.Dts.Pipeline.SSISScriptComponentEntryPointAttribute]
public class ScriptMain : UserComponent
{



    // >>> Escrito por Ernesto Moscoso Cam
    // >>>   Abril 2017
    // >>>
    // >>>     Basado en el código C# hecho por Afu Nang Tse Mundaca: http://r3xet.blogspot.pe/2013/12/obtener-el-tipo-de-cambio-de-sunat-del.html



    // Este es un helper method porque DateSerial() no existe en C#
    private DateTime DateSerial(int intYear, int intMonth, int intDay)
    {
        return new DateTime(intYear, intMonth, intDay);
    }

    // Este es el método que devuelve las filas del Script Component
    public override void CreateNewOutputRows()
    {
        System.Data.DataTable objDataTable;  // Tabla auxiliar para guardar los datos de la Web
        

        // Primero se configura la tabla para que tenga las 3 columnas necesarias...
        objDataTable = new System.Data.DataTable();
        objDataTable.Columns.Add("Fecha", typeof(System.DateTime));
        objDataTable.Columns.Add("Compra", typeof(double));
        objDataTable.Columns.Add("Venta", typeof(double));

        // Se manda poblar la tabla...
        ObtenerDatos(objDataTable);

        // Si toda ha salido bien y objDataTable tiene la data, entonces se copian los datos al buffer...
        foreach (System.Data.DataRow objDataRow in objDataTable.Rows)
        {
            SUNATTCBuffer.AddRow();

            SUNATTCBuffer.Fecha = Convert.ToDateTime(objDataRow[0]);
            SUNATTCBuffer.Compra = Convert.ToDecimal(objDataRow[1]);
            SUNATTCBuffer.Venta = Convert.ToDecimal(objDataRow[2]);
        }
    }


    private void ObtenerDatos(System.Data.DataTable objDataTable)
    {
        System.Net.WebClient objWebClient;
        string strHTML = "";
        string strURL = null;

        int intAño;
        int intMes;

        System.DateTime dtDateAux = default(System.DateTime);

        objWebClient = new System.Net.WebClient();

        dtDateAux = DateSerial(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
        // Este es el último día del mes anterior

        // Primero: Obtener el TC del mes anterior
        intAño = dtDateAux.Year;
        intMes = dtDateAux.Month;

        // La página del TC de la SUNAT acepta un Query String donde se le indica el año y el mes
        strURL = "http://www.sunat.gob.pe/cl-at-ittipcam/tcS01Alias?mes=" + intMes + "&anho=" + intAño;

        // Si la función retorna falso, entonces no se procesa
        if (CargarDocHTML(objWebClient, strURL, ref strHTML))
        {
            ProcesarDocHTML(strHTML, intAño, intMes, objDataTable);
        }

        // Segundo: Obtener el TC del mes en curso
        intAño = DateTime.Now.Year;
        intMes = DateTime.Now.Month;

        strURL = "http://www.sunat.gob.pe/cl-at-ittipcam/tcS01Alias?mes=" + intMes + "&anho=" + intAño;

        // Si la función retorna falso, entonces no se procesa
        if (CargarDocHTML(objWebClient, strURL, ref strHTML))
        {
            ProcesarDocHTML(strHTML, intAño, intMes, objDataTable);
        }

        objWebClient.Dispose();

        
        objDataTable.AcceptChanges(); // Commit
    }

    // Esta rutina sólo se encarga de conectarse a la web y obtener el HTML
    private bool CargarDocHTML(System.Net.WebClient objWebClient, string strURL, ref string strHTML)
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
            intLastPosition = strCelda.IndexOf("</strong>");
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
