using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.EL.NonInherited;

namespace UPC.SISGFRAN.EL.Helpers
{
    public class FTPUtil
    {
        /// <summary>
        /// Permite subir un archivo a un directorio ftp remoto
        /// </summary>
        /// <param name="filename">Nombre del archivo completo que se desea subir</param>
        /// <param name="ftpdirectorysource">Directorio ftp de destino a donde se va a subir el archivo</param>
        /// <param name="ftpUserID">Usuario del ftp</param>
        /// <param name="ftpPassword">Password</param>
        /// <returns>Valor de tipo Boolean = true que indica que el archivo se subio al directorio ftp correctamente</returns>
        public static Boolean Subir_Archivo_Ftp(string filenamesource, string ftpdirectorydest, string ftpUserID, string ftpPassword)
        {
            Boolean ok = false;
            try
            {
                FileInfo fileInf = new FileInfo(filenamesource);
                string uri = "ftp://";
                string directory = ftpdirectorydest + "/" + fileInf.Name;
                directory = directory.Replace("//", "/");
                uri = uri + directory;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.UseBinary = true;
                reqFTP.ContentLength = fileInf.Length;
                int buffLength = 64;
                int contenido;
                reqFTP.Proxy = null;
                FileStream fs = fileInf.OpenRead();
                byte[] buff = new byte[fs.Length];
                Stream strm = null;
                try
                {
                    strm = reqFTP.GetRequestStream();
                    contenido = fs.Read(buff, 0, buffLength);
                    while (contenido != 0)
                    {
                        strm.Write(buff, 0, contenido);
                        contenido = fs.Read(buff, 0, buffLength);
                    }
                    strm.Close();
                    fs.Flush();
                    fs.Close();
                    ok = true;
                    return ok;
                }
                catch (Exception ex1)
                {
                    throw ex1;
                }
                finally
                {
                    if (strm != null) { strm.Close(); }
                    if (fs != null) { fs.Close(); }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Boolean Subir_Archivo_Ftp_(string sourceftpfilename, string destftpfilename, string ftpUserID, string ftpPassword)
        {
            try
            {


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// Permite bajar un archivo de un directorio ftp remoto.
        /// </summary>
        /// <param name="filename">Nombre del archivo completo en donde se copiara el archivo</param>
        /// <param name="ftpdirectorysource">Directorio ftp de origen de donde se desea bajar el archivo</param>
        /// <param name="ftpUserID">Usuario del directorio ftp remoto</param>
        /// <param name="ftpPassword">Password</param>
        /// <returns></returns>
        public static Boolean Bajar_Archivo_Ftp(string filenamedestino, string ftpdirectorysource, string ftpUserID, string ftpPassword)
        {
            Boolean ok = false;
            try
            {
                FtpWebRequest reqFTP;
                FileInfo fileInf = new FileInfo(filenamedestino);
                string uri = ftpdirectorysource + fileInf.Name;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.Proxy = null;
                reqFTP.UseBinary = true;
                Stream data = reqFTP.GetResponse().GetResponseStream();
                FileStream fstr = new FileStream(filenamedestino, FileMode.Create);

                byte[] buffer = new Byte[2048];
                int readstr = 0;
                readstr = data.Read(buffer, 0, buffer.Length);
                while (readstr != 0)
                {
                    fstr.Write(buffer, 0, readstr);
                    readstr = data.Read(buffer, 0, buffer.Length);
                }
                data.Close();
                fstr.Flush();
                fstr.Close();
                ok = true;
                return ok;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ArchivoFtpEN> Listar_Archivos_Ftp(string ftpdirectory, string ftpUserID, string ftpPassword)
        {
            ArrayList archivos;
            List<ArchivoFtpEN> archivosFtp = null;
            try
            {
                FtpWebRequest reqFTP;
                string uri = ftpdirectory;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.Proxy = null;
                StreamReader sr = new StreamReader(reqFTP.GetResponse().GetResponseStream());
                archivos = new ArrayList();
                string str = sr.ReadLine();
                while (str != null)
                {
                    string[] datos = str.Split(' ');
                    string dato = "";
                    for (int i = 0; i < datos.Length; i++)
                    {
                        if (datos[i].Length > 0)
                        {
                            if (dato.Equals(""))
                            {
                                dato = datos[i];
                            }
                            else
                            {
                                dato = datos[i] + "|" + dato;
                            }
                        }
                    }
                    archivos.Add(dato);
                    str = sr.ReadLine();
                }
                sr.Close();

                if (archivos.Count > 0)
                {
                    archivosFtp = new List<ArchivoFtpEN>();
                    for (int i = 0; i < archivos.Count; i++)
                    {
                        ArchivoFtpEN archivo = new ArchivoFtpEN();
                        archivo.Nombre = archivos[i].ToString();
                        archivosFtp.Add(archivo);
                        //int count = archivos[i].ToString().Split('|').Length;
                        //for (int j = 0; j < count; j++){
                        //    if (count == 4){
                        //        archivo.Nombre = archivos[i].ToString().Split('|').ElementAt(0);
                        //        archivo.FechaCreacion = archivos[i].ToString().Split('|').ElementAt(3) + " " + archivos[i].ToString().Split('|').ElementAt(2);
                        //        archivo.Tipo = archivos[i].ToString().Split('|').ElementAt(1).Equals("<DIR>") ? "Carpeta de Archivos" : "Archivo";
                        //        if (!archivos[i].ToString().Split('|').ElementAt(1).Equals("<DIR>")){
                        //            if (IsNumeric(archivos[i].ToString().Split('|').ElementAt(1))){
                        //                Int64 tamanio = Convert.ToInt64(archivos[i].ToString().Split('|').ElementAt(1));
                        //                archivo.Bytes = tamanio;
                        //                if (tamanio <= 1024)
                        //                {
                        //                    archivo.Tamaño = archivos[i].ToString().Split('|').ElementAt(1) + " bytes";
                        //                }
                        //                if (tamanio > 1024 && tamanio <= 1048576)
                        //                {
                        //                    archivo.Tamaño = Math.Round((Convert.ToDouble(tamanio) / 1024.0), 2) + " KB";
                        //                }
                        //                if (tamanio > 1048576 && tamanio <= 1073741824)
                        //                {
                        //                    archivo.Tamaño = Math.Round((Convert.ToDouble(tamanio) / 1048576.0), 2) + " MB";
                        //                }
                        //                if (tamanio > 1073741824)
                        //                {
                        //                    archivo.Tamaño = Math.Round((Convert.ToDouble(tamanio) / 1073741824.0), 2) + " GB";
                        //                }
                        //            }
                        //        }else{
                        //            archivo.Tamaño = string.Empty;
                        //        }
                        //    }else{
                        //        archivo.Nombre = archivos[i].ToString().Split('|').ElementAt(0);
                        //        archivo.FechaCreacion = archivos[i].ToString().Split('|').ElementAt(count - 1) + " " + archivos[i].ToString().Split('|').ElementAt(count - 2);
                        //        archivo.Tipo = archivos[i].ToString().Split('|').ElementAt(count - 3).Equals("<DIR>") ? "Carpeta de Archivos" : "Archivo";
                        //        if (IsNumeric(archivos[i].ToString().Split('|').ElementAt(count - 3))){
                        //            Int64 tamanio = Convert.ToInt64(archivos[i].ToString().Split('|').ElementAt(count - 3));
                        //            archivo.Bytes = tamanio;
                        //        }
                        //    }
                        //}

                    }
                }
                return archivosFtp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="sftphost"></param>
        /// <param name="sftpdirectory"></param>
        /// <param name="sftpUserID"></param>
        /// <param name="sftpPassword"></param>
        /// <returns></returns>
        //public static Boolean Subir_Archivo_Sftp(string filename, string sftphost, string sftpdirectory, string sftpUserID, string sftpPassword)
        //{
        //    Boolean ok = false;
        //    FileInfo file;
        //    try
        //    {
        //        JSch jsch = new JSch();
        //        file = new FileInfo(filename);
        //        //file = filename.Substring(filename.LastIndexOf(@"\")+1, filename.Length - (filename.LastIndexOf(@"\")+1));
        //        Session session = jsch.getSession(sftpUserID, sftphost, 22);
        //        UserInfo ui = new MyUserInfo();
        //        session.setUserInfo(ui);
        //        session.setPassword(sftpPassword);
        //        session.connect();
        //        Channel channel = session.openChannel("sftp");
        //        channel.connect();
        //        ChannelSftp c = (ChannelSftp)channel;
        //        c.cd(sftpdirectory);
        //        c.put(filename, file.Name);
        //        c.exit();
        //        c.disconnect();
        //        session.disconnect();
        //        return ok;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public class MyUserInfo : UserInfo
        //{
        //    public String getPassword() { return "Ame2009"; }
        //    public bool promptYesNo(String str)
        //    {
        //        DialogResult returnVal = MessageBox.Show(
        //            str,
        //            "SharpSSH",
        //            MessageBoxButtons.YesNo,
        //            MessageBoxIcon.Warning);
        //        return (returnVal == DialogResult.Yes);
        //    }

        //    String passwd = "Ame2009";
        //    InputForm passwordField = new InputForm();

        //    public String getPassphrase() { return null; }
        //    public bool promptPassphrase(String message) { return true; }
        //    public bool promptPassword(String message)
        //    {
        //        InputForm inForm = new InputForm();
        //        inForm.Text = message;
        //        inForm.PasswordField = true;

        //        //if (inForm.PromptForInput())
        //        //{
        //        passwd = "Ame2009";
        //        return true;
        //        //}
        //        //else { return false; }
        //    }
        //    public void showMessage(String message)
        //    {
        //        MessageBox.Show(
        //            message,
        //            "SharpSSH",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Asterisk);
        //    }
        //}

        /// <summary>
        /// Valida si un numero es de tipo numerico o no. Devuelve true si el valor validado es un numero, false si no lo es.
        /// </summary>
        /// <param name="valor">Valor de tipo String que contiene el numero a validar.</param>
        /// <returns></returns>
        public static Boolean IsNumeric(string valor)
        {
            Boolean isNumber = false;
            try
            {
                Int64 value = 0;
                value = Convert.ToInt64(valor);
                isNumber = true;
            }
            catch (Exception ex)
            {
                isNumber = false;
                throw ex;
            }
            return isNumber;
        }
    }
}
