using System.Collections.Generic;
using Entity;
using System.Data.SqlClient;
using System.Data;
using DAL;

namespace BLL
{
    public class BL_TRABAJADOR
    {
        public static List<string> ValidaInfo(string PCadena, DtoTrabajador PTrabajador)
        {
            //Me falta hacer la validacion de nombre pero con el rfc
            List<string> lstValidaciones = new List<string>();
            if(ValidarNomTrabajador(PCadena, PTrabajador.Nombre))
            {
                lstValidaciones.Add("El Trabajador ya fue registrado");
            }
            

            if (ValidacionTexto(PTrabajador.Nombre))
            {
                lstValidaciones.Add("Revise el Nombre");
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

        public static List<string> GuardarInfo(string PCadena, DtoTrabajador PTrabajador)
        {
            List<string> lstDatos = new List<string>();

            try
            {
                var dpParametros = new
                {
                    P_Nombre = PTrabajador.Nombre,
                    P_RFC = PTrabajador.RFC
                };

                Contexto.Procedimiento_StoreDB(PCadena, "spInsTrabajador", dpParametros);

                lstDatos.Add("00");
                lstDatos.Add("El Trabajador fue registrado con éxito");
            }
            catch (SqlException e)
            {
                lstDatos.Add("14");
                lstDatos.Add(e.Message);
            }

            return lstDatos;
        }

        public static List<DtoCatTrabajador> ConsultaTrab(string PCadena)
        {
            List<DtoCatTrabajador> lstTrab = new List<DtoCatTrabajador>();

            var dpParametros = new
            {
                P_Accion = 1
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spConsTrab", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                lstTrab = (from item in Dt.AsEnumerable()
                              select new DtoCatTrabajador
                              {
                                  IdTrabajador = item.Field<Int32>("IdTrabajador"),
                                  NombreC = item.Field<string>("NombreC"),
                                  FecRegistro = item.Field<string>("FecRegistro")

                              }).ToList();
            }

            return lstTrab;
        }

        public static List<DtoCatTrabajador> ConsultaTrabajador(string PCadena, string PTexto)
        {
            List<DtoCatTrabajador> lstTrab = new List<DtoCatTrabajador>();

            var dpParametros = new
            {
                P_Accion = 2,
                P_Texto = PTexto
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spConsTrab", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                lstTrab = (from item in Dt.AsEnumerable()
                              select new DtoCatTrabajador
                              {
                                  IdTrabajador = item.Field<Int32>("IdTrabajador"),
                                  NombreC = item.Field<string>("NombreC"),
                                  FecRegistro = item.Field<string>("FecRegistro")

                              }).ToList();
            }

            return lstTrab;

        }

        public static List<string> ModificarInfo(string PCadena, DtoTrabajador PTrabajador)
        {
            List<string> lstDatos = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_Nombre = PTrabajador.Nombre,
                    P_RFC = PTrabajador.RFC,
                    P_IdTrabajador = PTrabajador.IdTrabajador
                };

                Contexto.Procedimiento_StoreDB(PCadena, "spModifTrab", dpParametros);

                lstDatos.Add("00");
                lstDatos.Add("El Trabajador fue modificado con éxito");
            }
            catch (SqlException e)
            {
                lstDatos.Add("14");
                lstDatos.Add(e.Message);
            }
            return lstDatos;
        }

        public static List<string> BorrarInfoTrabajador(string PCadena, int IdTrabajador)
        {
            List<string> lstDatos = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_IdTrabajador = IdTrabajador
                };

                Contexto.Procedimiento_StoreDB(PCadena, "spBorrarTrab", dpParametros);

                lstDatos.Add("00");
                lstDatos.Add("El Trabajador fue borrado con éxito");
            }
            catch (SqlException e)
            {
                lstDatos.Add("14");
                lstDatos.Add(e.Message);
            }
            return lstDatos;
        }
        private static bool ValidarNomTrabajador(string PCadena, string Trabajador)
        {
            bool Validacion = false;

            var dpParametros = new
            {
                P_Accion = 3,
                P_Texto = Trabajador
            };
            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spConsTrab", dpParametros);
            if(Dt.Rows.Count > 0)
            {
                Validacion = true;
            }
            return Validacion;

            

        }

    }
}


