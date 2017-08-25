using DAAB;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UPC.SISGFRAN.DAL.Base
{
    public class configPARDOSDB
    {

        //string cadCon = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

        public DAABRequest CreaRequest()
        {
            string providerName = Properties.Settings.Default.providerName;
            string serverName = Properties.Settings.Default.serverName;
            string databaseName = Properties.Settings.Default.databaseName;

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

            sqlBuilder.DataSource = serverName;
            sqlBuilder.PersistSecurityInfo = Properties.Settings.Default.PersistSecurityInfo;
            sqlBuilder.InitialCatalog = databaseName;
            sqlBuilder.UserID = Properties.Settings.Default.UserID;
            sqlBuilder.Password = Properties.Settings.Default.Password;
            sqlBuilder.ConnectTimeout = Properties.Settings.Default.ConnectTimeout;
            sqlBuilder.MultipleActiveResultSets = Properties.Settings.Default.MultipleActiveResultSets;
            sqlBuilder.IntegratedSecurity = Properties.Settings.Default.IntegratedSecurity;

            string providerString = sqlBuilder.ToString();

            DAABRequest.TipoOrigenDatos obOrigen;
            obOrigen = DAABRequest.TipoOrigenDatos.SQL;

            return new DAABRequest(obOrigen, providerString);
        }
    }
}
