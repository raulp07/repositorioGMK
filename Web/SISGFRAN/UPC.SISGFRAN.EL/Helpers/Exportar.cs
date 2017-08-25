using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Helpers
{
    public class Exportar
    {
        //#region "Excel"
        ///// <summary>
        ///// Exporta a Excel
        ///// </summary>
        ///// <param name="oPage">Página actual, use "Me" o "this" según corresponda</param>
        ///// <param name="log">log4net.ILog inicializado</param>
        ///// <param name="dtExportar">Datatable a exportar</param>
        ///// <param name="strNombreArchivo">Nombre del archivo resultante</param>
        //public static void Excel(ref Page oPage, ref log4net.ILog log, DataTable dtExportar,
        //                                     string strNombreArchivo)
        //{ Excel(ref oPage, ref log, DataTable2Html(dtExportar), strNombreArchivo); }
        ///// <summary>
        ///// Exporta a Excel
        ///// </summary>
        ///// <param name="oPage">Página actual, use "Me" o "this" según corresponda</param>
        ///// <param name="log">log4net.ILog inicializado</param>
        ///// <param name="dtExportar">Datatable a exportar</param>
        ///// <param name="strEstiloCelda">Estilo a usar en las celdas</param>
        ///// <param name="bClassCelda">True si es una clase css; False si es un script para style</param>
        ///// <param name="strNombreArchivo">Nombre del archivo resultante</param>
        //public static void Excel(ref Page oPage, ref log4net.ILog log, DataTable dtExportar,
        //                                    string strEstiloCelda, bool bClassCelda,
        //                                     string strNombreArchivo)
        //{ Excel(ref oPage, ref log, DataTable2Html(dtExportar, strEstiloCelda, bClassCelda), strNombreArchivo); }
        ///// <summary>
        ///// Exporta a Excel
        ///// </summary>
        ///// <param name="oPage">Página actual, use "Me" o "this" según corresponda</param>
        ///// <param name="log">log4net.ILog inicializado</param>
        ///// <param name="dtExportar">Datatable a exportar</param>
        ///// <param name="strEstiloCabecera">Estilo a usar en las cabeceras</param>
        ///// <param name="bClassCabecera">True si es una clase css; False si es un script para style</param>
        ///// <param name="strEstiloCelda">Estilo a usar en las celdas</param>
        ///// <param name="bClassCelda">True si es una clase css; False si es un script para style</param>
        ///// <param name="strNombreArchivo">Nombre del archivo resultante</param>
        //public static void Excel(ref Page oPage, ref log4net.ILog log, DataTable dtExportar,
        //                                     string strEstiloCabecera, bool bClassCabecera,
        //                                     string strEstiloCelda, bool bClassCelda,
        //                                     string strNombreArchivo)
        //{ Excel(ref oPage, ref log, DataTable2Html(dtExportar, strEstiloCabecera, bClassCabecera, strEstiloCelda, bClassCelda), strNombreArchivo); }
        ///// <summary>
        ///// Exporta a Excel
        ///// </summary>
        ///// <param name="oPage">Página actual, use "Me" o "this" según corresponda</param>
        ///// <param name="log">log4net.ILog inicializado</param>
        ///// <param name="dtExportar">Datatable a exportar</param>
        ///// <param name="strEstiloTitulo"></param>
        ///// <param name="bClassTitulo"></param>
        ///// <param name="strEstiloCabecera">Estilo a usar en las cabeceras</param>
        ///// <param name="bClassCabecera">True si es una clase css; False si es un script para style</param>
        ///// <param name="strEstiloCelda">Estilo a usar en las celdas</param>
        ///// <param name="bClassCelda">True si es una clase css; False si es un script para style</param>
        ///// <param name="strTitulo">Titulo en el contenido del Excel</param>
        ///// <param name="separacion">Indique si desea na separación entre el título y el contenido</param>
        ///// <param name="strNombreArchivo">Nombre del archivo resultante</param>
        //public static void Excel(ref Page oPage, ref log4net.ILog log, DataTable dtExportar, string strEstiloTitulo, bool bClassTitulo,
        //                                     string strEstiloCabecera, bool bClassCabecera,
        //                                     string strEstiloCelda, bool bClassCelda,
        //                                     string strTitulo, bool separacion,
        //                                     string strNombreArchivo)
        //{ Excel(ref oPage, ref log, DataTable2Html(dtExportar, strEstiloTitulo, bClassTitulo, strEstiloCabecera, bClassCabecera, strEstiloCelda, bClassCelda, strTitulo, separacion), strNombreArchivo); }
        ///// <summary>
        ///// Exporta a Excel
        ///// </summary>
        ///// <param name="oPage">Página actual, use "Me" o "this" según corresponda</param>
        ///// <param name="dtExportar">Datatable a exportar</param>
        ///// <param name="strNombreArchivo">Nombre del archivo resultante</param>
        //public static void Excel(ref Page oPage, DataTable dtExportar,
        //                             string strNombreArchivo)
        //{ Excel(ref oPage, DataTable2Html(dtExportar), strNombreArchivo); }
        ///// <summary>
        ///// Exporta a Excel
        ///// </summary>
        ///// <param name="oPage">Página actual, use "Me" o "this" según corresponda</param>
        ///// <param name="dtExportar">Datatable a exportar</param>
        ///// <param name="strEstiloCelda">Estilo a usar en las celdas</param>
        ///// <param name="bClassCelda">True si es una clase css; False si es un script para style</param>
        ///// <param name="strNombreArchivo">Nombre del archivo resultante</param>
        //public static void Excel(ref Page oPage, DataTable dtExportar,
        //                                    string strEstiloCelda, bool bClassCelda,
        //                                     string strNombreArchivo)
        //{ Excel(ref oPage, DataTable2Html(dtExportar, strEstiloCelda, bClassCelda), strNombreArchivo); }
        ///// <summary>
        ///// Exporta a Excel
        ///// </summary>
        ///// <param name="oPage">Página actual, use "Me" o "this" según corresponda</param>
        ///// <param name="dtExportar">Datatable a exportar</param>
        ///// <param name="strEstiloCabecera">Estilo a usar en las cabeceras</param>
        ///// <param name="bClassCabecera">True si es una clase css; False si es un script para style</param>
        ///// <param name="strEstiloCelda">Estilo a usar en las celdas</param>
        ///// <param name="bClassCelda">True si es una clase css; False si es un script para style</param>
        ///// <param name="strNombreArchivo">Nombre del archivo resultante</param>
        //public static void Excel(ref Page oPage, DataTable dtExportar,
        //                                     string strEstiloCabecera, bool bClassCabecera,
        //                                     string strEstiloCelda, bool bClassCelda,
        //                                     string strNombreArchivo)
        //{ Excel(ref oPage, DataTable2Html(dtExportar, strEstiloCabecera, bClassCabecera, strEstiloCelda, bClassCelda), strNombreArchivo); }
        ///// <summary>
        ///// Exporta a Excel
        ///// </summary>
        ///// <param name="oPage">Página actual, use "Me" o "this" según corresponda</param>
        ///// <param name="dtExportar">Datatable a exportar</param>
        ///// <param name="strEstiloTitulo"></param>
        ///// <param name="bClassTitulo"></param>
        ///// <param name="strEstiloCabecera">Estilo a usar en las cabeceras</param>
        ///// <param name="bClassCabecera">True si es una clase css; False si es un script para style</param>
        ///// <param name="strEstiloCelda">Estilo a usar en las celdas</param>
        ///// <param name="bClassCelda">True si es una clase css; False si es un script para style</param>
        ///// <param name="strTitulo">Titulo en el contenido del Excel</param>
        ///// <param name="separacion">Indique si desea na separación entre el título y el contenido</param>
        ///// <param name="strNombreArchivo">Nombre del archivo resultante</param>
        //public static void Excel(ref Page oPage, DataTable dtExportar, string strEstiloTitulo, bool bClassTitulo,
        //                                     string strEstiloCabecera, bool bClassCabecera,
        //                                     string strEstiloCelda, bool bClassCelda,
        //                                     string strTitulo, bool separacion,
        //                                     string strNombreArchivo)
        //{ Excel(ref oPage, DataTable2Html(dtExportar, strEstiloTitulo, bClassTitulo, strEstiloCabecera, bClassCabecera, strEstiloCelda, bClassCelda, strTitulo, separacion), strNombreArchivo); }
        ///// <summary>
        ///// Exporta a Excel
        ///// </summary>
        ///// <param name="oPage">Página actual, use "Me" o "this" según corresponda</param>
        ///// <param name="strContenido">Contenido en formato de tabla HTML para convertir</param>
        ///// <param name="strNombreArchivo">Nombre del archivo resultante</param>
        //public static void Excel(ref Page oPage, string strContenido, string strNombreArchivo)
        //{
        //    log4net.ILog log = null;
        //    Excel(ref oPage, ref log, strContenido, strNombreArchivo);
        //}
        ///// <summary>
        ///// Exporta a Excel
        ///// </summary>
        ///// <param name="oPage">Página actual, use "Me" o "this" según corresponda</param>
        ///// <param name="log">log4net.ILog inicializado</param>
        ///// <param name="strContenido">Contenido en formato de tabla HTML para convertir</param>
        ///// <param name="strNombreArchivo">Nombre del archivo resultante</param>
        //public static void Excel(ref Page oPage, ref log4net.ILog log, string strContenido, string strNombreArchivo)
        //{
        //    ExportaArchivo(ref oPage, ref log, strNombreArchivo, "application/vnd.xls", "xls", "ISO-8859-1", strContenido);
        //}
        //#endregion
        //#region "Exportar"
        //public static void ExportaArchivo(ref Page oPage, string strNombreArchivo, string strContentType, string strExtension, string strEncoding, string strContenido)
        //{
        //    log4net.ILog log = null;
        //    ExportaArchivo(ref oPage, ref log, strNombreArchivo, strContentType, strExtension, strEncoding, strContenido);
        //}

        ///// <summary>
        ///// Exporta a un archivo en el cliente
        ///// </summary>
        ///// <param name="oPage">Página actual, use "Me" o "this" según corresponda</param>
        ///// <param name="log">log4net.ILog inicializado</param>
        ///// <param name="strNombreArchivo">Nombre del archivo resultante</param>
        ///// <param name="strContentType">Tipo de contenido</param>
        ///// <param name="strExtension">extension (no inclya el punto[.])</param>
        ///// <param name="strEncoding">Tipo de codificación</param>
        ///// <param name="strContenido">Contenido en formato de texto a exportar</param>
        //public static void ExportaArchivo(ref Page oPage, ref log4net.ILog log, string strNombreArchivo, string strContentType, string strExtension, string strEncoding, string strContenido)
        //{
        //    try
        //    {
        //        oPage.Response.ClearHeaders();
        //        oPage.Response.Buffer = true;
        //        oPage.Response.ContentType = strContentType;
        //        oPage.Response.ContentEncoding = System.Text.Encoding.GetEncoding(strEncoding);
        //        oPage.Response.Clear();
        //        oPage.Response.AppendHeader("content-disposition", string.Concat("attachment;filename=", strNombreArchivo, ".", strExtension));
        //        oPage.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        //        oPage.Response.Write(strContenido);
        //        switch (strExtension)
        //        {
        //            case "xls": oPage.Response.End(); break;
        //            case "csv": oPage.Response.End(); break;
        //            default: oPage.Response.Flush(); break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (log != null)
        //        {
        //            switch (strExtension)
        //            {
        //                case "xls": break;
        //                default:
        //                    throw ex;
        //            }
        //        }
        //    }
        //}
        //#endregion

        //#region "DataTable"
        //private static string DataTable2Html(DataTable dtExportar)
        //{ return DataTable2Html(dtExportar, "", false); }
        //private static string DataTable2Html(DataTable dtExportar,
        //                                     string strEstiloCelda, bool bClassCelda)
        //{ return DataTable2Html(dtExportar, strEstiloCelda, bClassCelda, strEstiloCelda, bClassCelda); }

        //private static string DataTable2Html(DataTable dtExportar,
        //                                     string strEstiloCabecera, bool bClassCabecera,
        //                                     string strEstiloCelda, bool bClassCelda)
        //{ return DataTable2Html(dtExportar, "", false, strEstiloCabecera, bClassCabecera, strEstiloCelda, bClassCelda, "", false); }

        //private static string DataTable2Html(DataTable dtExportar,
        //                                     string strEstiloTitulo, bool bClassTitulo,
        //                                     string strEstiloCabecera, bool bClassCabecera,
        //                                     string strEstiloCelda, bool bClassCelda,
        //                                     string strTitulo, bool separacion)
        //{
        //    StringBuilder str = new StringBuilder();
        //    //Preparacion del Titulo
        //    if (strTitulo != null && !strTitulo.Length.Equals(0))
        //    {
        //        str.Append("<table>");
        //        str.Append("<tr><td align=center valign=middle");
        //        if (bClassTitulo) { str.Append(string.Concat("style=\"", strEstiloTitulo, "\"")); }
        //        else { str.Append(string.Concat("class=\"", strEstiloTitulo, "\"")); }
        //        str.Append(stylo(bClassTitulo, strEstiloTitulo) + " colspan=" + dtExportar.Columns.Count + ">" + strTitulo + "</td></tr>");
        //        str.Append("</table>");
        //        if (separacion != null && separacion)
        //        {
        //            str.Append("<table><tr><td></td></tr></table>");
        //        }
        //    }
        //    //Preparacion de Cabeceras
        //    str.Append("<table border=1 bordercolor='#000000'>");
        //    str.Append("<tr>");
        //    foreach (DataColumn dc in dtExportar.Columns)
        //    {
        //        str.Append("<th " + stylo(bClassCabecera, strEstiloCabecera) + ">");
        //        str.Append(dc.ColumnName);
        //        str.Append("</th>");
        //    }
        //    //Preparacion de Contenido
        //    //Cada Fila
        //    foreach (DataRow dr in dtExportar.Rows)
        //    {
        //        str.Append("<tr>");
        //        //Cada celda
        //        foreach (DataColumn dc in dtExportar.Columns)
        //        {
        //            str.Append("<td " + stylo(bClassCelda, strEstiloCelda) + ">");
        //            if (!Convert.IsDBNull(dr[dc.ColumnName])) { str.Append(dr[dc.ColumnName].ToString().ToUpper()); }
        //            str.Append("</td>");
        //        }
        //        str.Append("</tr>");
        //    }
        //    str.Append("</table>");

        //    return str.ToString();
        //}
        //private static string stylo(bool bClass, string strEstilo)
        //{
        //    try
        //    {
        //        if (strEstilo != null && !strEstilo.Equals(0))
        //        {
        //            string strTagProperty = string.Empty;
        //            string strStyleValue = string.Empty;
        //            strTagProperty = (bClass ? "class" : "style") + "=";
        //            strStyleValue = string.Concat("\"", strEstilo, "\"");

        //            return string.Concat(strTagProperty, strStyleValue);
        //        }
        //        else { return string.Empty; }
        //    }
        //    catch { return string.Empty; }
        //}
        //#endregion
    }
}
