using Demo_Ingenieria.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Ingenieria.Data
{
    public class PublicidadData
    {

        public IConfiguration Configuration { get; }

        public PublicidadData(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }


        public List<Publicidad> ObtenerListaPublicidad()
        {
            List<Publicidad> lista = new List<Publicidad>();

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];


            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = $"EXEC [dbo].[sp_obtener_ofertas]";
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {

                            Publicidad publicidad = new Publicidad();
                            publicidad.Id = Convert.ToInt32(dataReader["id"]);
                            publicidad.Nombre = Convert.ToString(dataReader["nombre"]);
                            publicidad.Descripcion = Convert.ToString(dataReader["descripcion"]);
                            publicidad.Imagen = Convert.ToString(dataReader["imagen"]);
                            publicidad.Link_Destino = Convert.ToString(dataReader["link_destino"]);


                            lista.Add(publicidad);
                        }

                    }
                }

                return lista;
            }
        }

        public bool RegistrarPublicidad(Publicidad publicidad)
        {
            publicidad.Imagen = "/assets/img/promociones/oferta.png";
            int estado = 0;
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = $"EXEC sp_insertar_oferta '{publicidad.Nombre}','{publicidad.Descripcion}','{publicidad.Imagen}','{publicidad.Link_Destino}',{estado},{publicidad.Usuarioid}";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return true;
        }

        public bool RegistrarUsuario(Usuario usuario)
        {

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = $"EXEC sp_insertar_usuario '{usuario.usuario}','{usuario.contra}',2,'{usuario.correo}'";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return true;
        }

        public int ValidarUsuario(Usuario usuario)
        {

            int resultado = 0;

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = $"EXEC [dbo].[sp_validarUsuario_API]'{usuario.usuario}','{usuario.contra}'";
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var dataReader = command.ExecuteReader())
                    {

                        dataReader.Read();
                        if (Convert.ToInt32(dataReader["resultado"]) != 0)
                        {
                            resultado = Convert.ToInt32(dataReader["resultado"]);
                        }

                    }
                    connection.Close();
                }

                return resultado;
            }
        }
    }
}
