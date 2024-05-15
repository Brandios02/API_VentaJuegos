using System.Collections.Generic;
using Entity;
using System.Data.SqlClient;
using System.Data;
using DAL;

namespace BLL
{
    public class BL_CLIENTE
    {
        public static List<string> ValidaInfo(string PCadena, DtoCliente PCliente)
        {
            //Me falta hacer la validacion de nombre pero con el rfc
            List<string> lstValidaciones = new List<string>();
            if(ValidarNomCliente(PCadena, PCliente.Nombre + PCliente.APaterno + PCliente.AMaterno))
            {
                lstValidaciones.Add("El cliente ya fue registrado");
            }
            

            if (ValidacionTexto(PCliente.Nombre))
            {
                lstValidaciones.Add("Revise el Nombre");
            }
            if (ValidacionTexto(PCliente.APaterno))
            {
                lstValidaciones.Add("Revise el Apellido Paterno");
            }
            if (ValidacionTexto(PCliente.AMaterno))
            {
                lstValidaciones.Add("Revise el Apellido Materno");
            }
            if (PCliente.Edad <= 17)
            {
                lstValidaciones.Add("El cliente debe ser mayor de edad");
            }

            return lstValidaciones;

        }

        private static Boolean ValidacionTexto(string Texto)
        {
            Boolean Validacion = false;

            foreach (char Letra in Texto.Replace(" ", ""))
            {
                if (!char.IsLetter(Letra))
                {
                    Validacion = true;
                    break;
                }
            }
            return Validacion;
        }

        public static List<string> GuardarInfo(string PCadena, DtoCliente PCliente)
        {
            List<string> lstDatos = new List<string>();

            try
            {
                var dpParametros = new
                {
                    P_Nombre = PCliente.Nombre,
                    P_APaterno = PCliente.APaterno,
                    P_AMaterno = PCliente.AMaterno
                };

                Contexto.Procedimiento_StoreDB(PCadena, "spInsertCliente", dpParametros);

                lstDatos.Add("00");
                lstDatos.Add("El Cliente fue registrado con éxito");
            }
            catch (SqlException e)
            {
                lstDatos.Add("14");
                lstDatos.Add(e.Message);
            }

            return lstDatos;
        }

        public static List<DtoCatCliente> ConsultaCliente(string PCadena)
        {
            List<DtoCatCliente> lstCliente = new List<DtoCatCliente>();

            var dpParametros = new
            {
                P_Accion = 1
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spConsCliente", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                lstCliente = (from item in Dt.AsEnumerable()
                              select new DtoCatCliente
                              {
                                  IdCliente = item.Field<Int32>("IdCliente"),
                                  NombreC = item.Field<string>("NombreC"),
                                  FecRegistro = item.Field<string>("FecRegistro")

                              }).ToList();
            }

            return lstCliente;
        }

        public static List<DtoCatCliente> ConsultaCliente(string PCadena, string PTexto)
        {
            List<DtoCatCliente> lstCliente = new List<DtoCatCliente>();

            var dpParametros = new
            {
                P_Accion = 2,
                P_Texto = PTexto
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spConsCliente", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                lstCliente = (from item in Dt.AsEnumerable()
                              select new DtoCatCliente
                              {
                                  IdCliente = item.Field<Int32>("IdCliente"),
                                  NombreC = item.Field<string>("NombreC"),
                                  FecRegistro = item.Field<string>("FecRegistro")

                              }).ToList();
            }

            return lstCliente;

        }

        public static List<string> ModificarInfo(string PCadena, DtoCliente PCliente)
        {
            List<string> lstDatos = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_Nombre = PCliente.Nombre,
                    P_APaterno = PCliente.APaterno,
                    P_AMaterno = PCliente.AMaterno,
                    P_IdCliente = PCliente.IdCliente
                };

                Contexto.Procedimiento_StoreDB(PCadena, "spModifCliente", dpParametros);

                lstDatos.Add("00");
                lstDatos.Add("El Cliente fue modificado con éxito");
            }
            catch (SqlException e)
            {
                lstDatos.Add("14");
                lstDatos.Add(e.Message);
            }
            return lstDatos;
        }

        public static List<string> BorrarInfo(string PCadena, int IdCliente)
        {
            List<string> lstDatos = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_IdCliente = IdCliente
                };

                Contexto.Procedimiento_StoreDB(PCadena, "spBorrarCliente", dpParametros);

                lstDatos.Add("00");
                lstDatos.Add("El Cliente fue borrado con éxito");
            }
            catch (SqlException e)
            {
                lstDatos.Add("14");
                lstDatos.Add(e.Message);
            }
            return lstDatos;
        }
        private static bool ValidarNomCliente(string PCadena, string Cliente)
        {
            bool Validacion = false;

            var dpParametros = new
            {
                P_Accion = 3,
                P_Texto = Cliente
            };
            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spConsulCliente", dpParametros);
            if(Dt.Rows.Count > 0)
            {
                Validacion = true;
            }
            return Validacion;

            

        }

    }
}


