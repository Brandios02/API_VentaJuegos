using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Entity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_FACPYA77.Controllers
{
    [Route("api/[Controller]")]
    //[Authorize] ESTO ES PARFA LA AUTORIZACION
    public class TrabajadorController : Controller
    {

        private readonly string Cadena;

        public TrabajadorController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PRODServer");
        }

        [HttpPost]
        [Route("GuardarInfoTrabajador")]
        public IActionResult GuardarInfoTrabajador([FromBody] DtoTrabajador Trabajador)
        {

            List<string> lstValidaciones = BL_TRABAJADOR.ValidaInfo(Cadena, Trabajador);

            if (lstValidaciones.Count == 0)
            {

                List<string> lstDatos = BL_TRABAJADOR.GuardarInfo(Cadena, Trabajador);

                if (lstDatos[0] == "00")
                {
                    return Ok(new { Codigo = "00", response = lstDatos[1] });
                }
                else
                {
                    return Ok(new { Codigo = lstDatos[0], response = lstDatos[1] });
                }


            }
            else
            {
                return Ok(new { Codigo = "14", response = lstValidaciones });
            }

        }


        [HttpGet]
        [Route("GetAll")]

        public IActionResult GetAll()
        {
            List<DtoCatTrabajador> lstTrab = BL_TRABAJADOR.ConsultaTrab(Cadena);

            return Ok(new { Codigo = "00", Respuesta = lstTrab });
        }



        [HttpGet]
        [Route("GetAllTrabajadores/{Descrip}")]

        public IActionResult GetAllNombre(string Descrip)
        {
            List<DtoCatTrabajador> lstTrabajador = BL_TRABAJADOR.ConsultaTrabajador(Cadena, Descrip);

            return Ok(new { Codigo = "00", Respuesta = lstTrabajador });
        }

        [HttpPut]
        [Route("ModificarInfoTrabajador")]
        public IActionResult ModificarInfo([FromBody] DtoTrabajador Trabajador)
        {

            List<string> lstValidaciones = BL_TRABAJADOR.ValidaInfo(Cadena, Trabajador);

            if (lstValidaciones.Count == 0)
            {

                List<string> lstDatos = BL_TRABAJADOR.ModificarInfo(Cadena, Trabajador);

                if (lstDatos[0] == "00")
                {
                    return Ok(new { Codigo = "00", response = lstDatos[1] });
                }
                else
                {
                    return Ok(new { Codigo = lstDatos[0], response = lstDatos[1] });
                }


            }
            else
            {
                return Ok(new { Codigo = "14", response = lstValidaciones });
            }

        }


        [HttpDelete]
        [Route("BorrarInfoTrabajador")]
        public IActionResult BorrarInfoCliente(int IdTrabajador)
        {

            List<string> lstDatos = BL_TRABAJADOR.BorrarInfoTrabajador(Cadena, IdTrabajador);

            if (lstDatos[0] == "00")
            {
                return Ok(new { Codigo = "00", response = lstDatos[1] });
            }
            else
            {
                return Ok(new { Codigo = lstDatos[0], response = lstDatos[1] });
            }


        }


    }
}

