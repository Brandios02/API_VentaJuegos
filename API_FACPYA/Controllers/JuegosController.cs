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
    
    public class JuegosController : Controller
    {

        private readonly string Cadena;

        public JuegosController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PRODServer");
        }

        [HttpPost]
        [Route("GuardarInfoJuegos")]
        public IActionResult GuardarInfoJuegos([FromBody] DtoJuegos Juegos)
        {

            List<string> lstValidaciones = BL_JUEGOS.ValidaInfo(Cadena, Juegos);

            if (lstValidaciones.Count == 0)
            {

                List<string> lstDatos = BL_JUEGOS.GuardarInfo(Cadena, Juegos);

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
            List<DtoCatJuegos> lstJuegos = BL_JUEGOS.ConsultaJuegos(Cadena);

            return Ok(new { Codigo = "00", Respuesta = lstJuegos });
        }



        [HttpGet]
        [Route("Consultar Campos/{Descrip}")]

        public IActionResult GetAllNombre(string Descrip)
        {
            List<DtoCatJuegos> lstJuegos = BL_JUEGOS.ConsultaJuegos(Cadena, Descrip);

            return Ok(new { Codigo = "00", Respuesta = lstJuegos });
        }

        [HttpPut]
        [Route("ModificarInfoJuegos")]
        public IActionResult ModificarInfoJuegos([FromBody] DtoJuegos Juegos)
        {

            List<string> lstValidaciones = BL_JUEGOS.ValidaInfo(Cadena, Juegos);

            if (lstValidaciones.Count == 0)
            {

                List<string> lstDatos = BL_JUEGOS.ModificarInfo(Cadena, Juegos);

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
        [Route("BorrarJuego")]
        public IActionResult Borrarinfo(int IdJuego)
        {

            List<string> lstDatos = BL_JUEGOS.Borrarinfo(Cadena, IdJuego);

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

