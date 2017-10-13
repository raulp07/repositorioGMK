using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.BL
{
    public class REST
    {
        public static string urlSite = ConfigurationManager.AppSettings["urlWS"];
        public string ConectREST(string url, string method, string postdata = "")
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlSite + "WCFPardos.svc/" + url);
                HttpWebResponse res = null;
                StreamReader reader = null;
                switch (method)
                {
                    case "GET":
                        req.Method = method;
                        if (Tokens.token_Client != "")
                        {
                            req.Headers.Add("token-client", Tokens.token_Client);
                        }
                        res = (HttpWebResponse)req.GetResponse();
                        reader = new StreamReader(res.GetResponseStream());
                        return reader.ReadToEnd();
                    case "POST":
                        byte[] data = Encoding.UTF8.GetBytes(postdata);
                        req.Method = method;
                        req.ContentLength = data.Length;
                        req.ContentType = "application/json";
                        if (Tokens.token_Client != null)
                        {
                            req.Headers.Add("token-client", Tokens.token_Client);
                        }
                        var reqStream = req.GetRequestStream();
                        reqStream.Write(data, 0, data.Length);
                        res = (HttpWebResponse)req.GetResponse();
                        if (url == "Login")
                        {
                            Tokens.token_Client = res.Headers.GetValues("token-client")[0];
                        }
                        reader = new StreamReader(res.GetResponseStream());
                        return reader.ReadToEnd();
                    default: return "";
                }
            }
            catch (Exception e)
            {
                if (Tokens.token_Client == null)
                {
                    throw new ArgumentNullException("1001");
                }
                else
                {
                    throw new ArgumentNullException(e.Message);
                }                
            }
            
        }
    }
}
