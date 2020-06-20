using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Ingenieria.Models
{
    public class Publicidad
    {

        public Publicidad(string nombre, string descripcion, string imagen, string link_Destino)
        {
            Id = 0;
            Nombre = nombre;
            Descripcion = descripcion;
            Imagen = imagen;
            Link_Destino = link_Destino;
        }
        public Publicidad()
        {
            Id = 0;
            Nombre = "";
            Descripcion = "";
            Imagen = "";
            Link_Destino = "";
            Usuarioid = 0;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Link_Destino { get; set; }
        public int Usuarioid { get; set; }
        public string impirmir()
        {
            return Nombre + Descripcion + Link_Destino + Usuarioid;
        }

    }
}
