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
    public class ClienteController : Controller
    {

        private readonly string Cadena;

        public ClienteController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PRODServer");
        }

        [HttpPost]
        [Route("GuardarInfoCliente")]
        public IActionResult GuardarInfoCliente([FromBody] DtoCliente Cliente)
        {

            List<string> lstValidaciones = BL_CLIENTE.ValidaInfo(Cadena, Cliente);

            if (lstValidaciones.Count == 0)
            {

                List<string> lstDatos = BL_CLIENTE.GuardarInfo(Cadena, Cliente);

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
            List<DtoCatCliente> lstCliente = BL_CLIENTE.ConsultaCliente(Cadena);

            return Ok(new { Codigo = "00", Respuesta = lstCliente });
        }



        [HttpGet]
        [Route("GetAllNombre/{Descrip}")]

        public IActionResult GetAllNombre(string Descrip)
        {
            List<DtoCatCliente> lstCliente = BL_CLIENTE.ConsultaCliente(Cadena, Descrip);

            return Ok(new { Codigo = "00", Respuesta = lstCliente });
        }

        [HttpPut]
        [Route("ModificarInfoCliente")]
        public IActionResult ModificarInfoCliente([FromBody] DtoCliente Cliente)
        {

            List<string> lstValidaciones = BL_CLIENTE.ValidaInfo(Cadena, Cliente);

            if (lstValidaciones.Count == 0)
            {

                List<string> lstDatos = BL_CLIENTE.ModificarInfo(Cadena, Cliente);

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
        [Route("BorrarInfoCliente")]
        public IActionResult BorrarInfoCliente(int IdCliente)
        {

            List<string> lstDatos = BL_CLIENTE.BorrarInfo(Cadena, IdCliente);

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

