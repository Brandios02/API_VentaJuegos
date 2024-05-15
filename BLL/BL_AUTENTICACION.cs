using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BL_AUTENTICACION
    {
        public static Boolean ValidaUsuarioInterfaces(string PCadena, string PUsuario, string PContra)
        {
            Boolean Validacion = true;
            var dpParametros = new
            {
                P_Usuario = PUsuario,
                P_Contra = PContra,
                
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spValidaTOKEN", dpParametros);
            if (Dt.Rows.Count > 0)
            {
                Validacion = false;
                //Aqui era true
            }
            return Validacion;
        }
    }
}
