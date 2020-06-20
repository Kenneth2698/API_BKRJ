using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo_Ingenieria.Models;
using Demo_Ingenieria.Data;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;


namespace Demo_Ingenieria.Controllers
{
    public class HomeController : Controller
    {

        public IConfiguration Configuration { get; }

        public HomeController(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public string Index()
        {

            string output = JsonConvert.SerializeObject("API Corriendo");
            return output;

        }

        [HttpGet("Prueba")]
        public string Prueba()
        {
            PublicidadData data = new PublicidadData(this.Configuration);

            List<Publicidad> lista = data.ObtenerListaPublicidad();
            string output = JsonConvert.SerializeObject(lista);
            return output;

        }

        [HttpPost("RegistrarUsuario")]

        public bool RegistrarUsuario([FromBody] Usuario usuario)
        {
            PublicidadData data = new PublicidadData(this.Configuration);

            return data.RegistrarUsuario(usuario);

        }

        [HttpPost("ValidarUsuario")]

        public Respuesta ValidarUsuario([FromBody] Usuario usuario)
        {
            PublicidadData data = new PublicidadData(this.Configuration);
            Respuesta r = new Respuesta();

            r.ID = data.ValidarUsuario(usuario);

            return r;

        }

        [HttpPost("RegistrarPublicidad")]

        public bool RegistrarPublicidad([FromBody] Publicidad publicidad)
        {
            PublicidadData data = new PublicidadData(this.Configuration);


            return data.RegistrarPublicidad(publicidad);

        }

    }
}
