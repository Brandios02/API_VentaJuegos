using System.Collections.Generic;
using Entity;
using System.Data.SqlClient;
using System.Data;
using DAL;
using System.Data.SqlTypes;

namespace BLL
{
    public class BL_JUEGOS
    {
        public static List<string> ValidaInfo(string PCadena, DtoJuegos PJuegos)
        {
            //Me falta hacer la validacion de nombre pero con el rfc
            List<string> lstValidaciones = new List<string>();
            if(ValidarNomJuegos(PCadena, PJuegos.NombreJ + PJuegos.FecRegistro))
            {
                lstValidaciones.Add("El Juego ya fue registrado");
            }
            

            
            
            return lstValidaciones;

        }

        public static List<string> GuardarInfo(string PCadena, DtoJuegos PJuegos)
        {
            List<string> lstDatos = new List<string>();

            try
            {
                var dpParametros = new
                {
                    P_Nombre = PJuegos.NombreJ,
                    P_FecEstreno = PJuegos.FecRegistro,
                    P_IdProv = PJuegos.IdProv
                };

                Contexto.Procedimiento_StoreDB(PCadena, "spInsertJuego", dpParametros);

                lstDatos.Add("00");
                lstDatos.Add("El Juego fue registrado con éxito");
            }
            catch (SqlException e)
            {
                lstDatos.Add("14");
                lstDatos.Add(e.Message);
            }

            return lstDatos;
        }
        public static List<DtoCatJuegos> ConsultaJuegos(string PCadena)
        {
            List<DtoCatJuegos> lstJuegos = new List<DtoCatJuegos>();

            var dpParametros = new
            {
                P_Accion = 1
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spConsulJuegos", dpParametros);
            if (Dt.Rows.Count > 0)
            {
                lstJuegos = (from item in Dt.AsEnumerable()
                              select new DtoCatJuegos
                              {
                                  IdJuego = item.Field<int>("IdJuego"),
                                  NombreJ = item.Field<string>("NombreJ"),
                                  CategoriaJ = item.Field<string>("CategoriaJ"),
                                  precioJ = item.Field<Decimal>("PrecioJ"),
                                  FecEstrenoJ = item.Field<string>("FecEstrenoJ"),
                                  IdProv = item.Field<int>("IdProvJ")

                              }).ToList();
            }

            return lstJuegos;
        }

        public static List<DtoCatJuegos> ConsultaJuegos(string PCadena, string PTexto)
        {
            List<DtoCatJuegos> lstJuegos = new List<DtoCatJuegos>();

            var dpParametros = new
            {
                P_Accion = 2,
                P_Texto = PTexto
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spConsulJuegos", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                lstJuegos = (from item in Dt.AsEnumerable()
                              select new DtoCatJuegos
                              {
                                  IdJuego = item.Field<Int32>("IdJuego"),
                                  NombreJ = item.Field<string>("NombreJ"),
                                  FecEstrenoJ = item.Field<string>("FecEstrenoJ")

                              }).ToList();
            }

            return lstJuegos;

        }

        public static List<string> ModificarInfo(string PCadena, DtoJuegos PJuegos)
        {
            List<string> lstDatos = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_Nombre = PJuegos.NombreJ,
                    P_FecRegistro = PJuegos.FecRegistro,
                    P_IdProv = PJuegos.IdProv
                };

                Contexto.Procedimiento_StoreDB(PCadena, "spModifJuego", dpParametros);

                lstDatos.Add("00");
                lstDatos.Add("El Juego fue modificado con éxito");
            }
            catch (SqlException e)
            {
                lstDatos.Add("14");
                lstDatos.Add(e.Message);
            }
            return lstDatos;
        }

        public static List<string> Borrarinfo(string PCadena, int IdJuego)
        {
            List<string> lstDatos = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_IdJuego = IdJuego
                };

                Contexto.Procedimiento_StoreDB(PCadena, "spBorrarJuego", dpParametros);

                lstDatos.Add("00");
                lstDatos.Add("El Juego fue borrado con éxito");
            }
            catch (SqlException e)
            {
                lstDatos.Add("14");
                lstDatos.Add(e.Message);
            }
            return lstDatos;

        }
        private static bool ValidarNomJuegos(string PCadena, string Juego)
        {
            bool Validacion = false;

            var dpParametros = new
            {
                P_Accion = 3,
                P_Texto = Juego
            };
            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spConsulJuegos", dpParametros);
            if (Dt.Rows.Count > 0)
            {
                Validacion = true;
            }
            return Validacion;



        }

    }
}


