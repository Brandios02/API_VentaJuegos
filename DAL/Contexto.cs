using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Collections.Generic;

namespace DAL
{
    public class Contexto
    {
        public static DataTable Funcion_StoreDB(String cadena, String P_Sentencia, object P_Parametro)
        {
            DataTable Dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    var lst = conn.ExecuteReader(P_Sentencia, P_Parametro, commandType: CommandType.StoredProcedure);
                    Dt.Load(lst);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return Dt;
        }

        public static void Procedimiento_StoreDB(String cadena, String P_Sentencia, object P_Parametro)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    var lst = conn.ExecuteReader(P_Sentencia, P_Parametro, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public static void SentenciaSql(String cadena, String P_Sentencia, object P_Parametro)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    SqlCommand comando = new SqlCommand(P_Sentencia, conn);
                    using SqlDataReader reader = comando.ExecuteReader();
                    //var lst = conn.ExecuteReader(P_Sentencia, P_Parametro);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}
