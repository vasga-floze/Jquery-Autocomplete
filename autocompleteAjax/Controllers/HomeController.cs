using autocompleteAjax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Collections.Generic;
using System;
//incluir las librerias del paquete instalado desde el administrador de paquetes
using System.Data;
using System.Data.SqlClient;

namespace autocompleteAjax.Controllers
{
    public class HomeController : Controller
    {
        private readonly string connString; //variable para almacenar la cadena de conexion

        public HomeController(IConfiguration config)
        {
            //obtener y almacenar en variable la cadena de conexion
            connString = config.GetConnectionString("ConnString");
        }

        public IActionResult Index()
        {
            return View();
        }

        //metodo para ejecutar procedimiento almacenado
        [HttpGet] //identifica la accion como un metodo HttpGet
        public JsonResult SearchCar( string search)
        {
            //se crea lista para pasar los datos desde un data reader
            //Search es la clase creada en Models
            List<Search> list = new List<Search>();

            using (var connection = new SqlConnection(connString))
            {
                connection.Open(); //abrir la conexion
                //configurar el comando de sql, (nombre del sp)
                SqlCommand cmd = new SqlCommand("sp_search_car", connection);
                //indicar el nombre del parametro y el valor que se pasará
                cmd.Parameters.AddWithValue("search", search);
                //indicar el tipo de comando (sp)
                cmd.CommandType = CommandType.StoredProcedure;

                //lectura al resultado de la ejecucion del sp
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //empujar los resultados a la lista
                        list.Add(
                            new Search()
                            {
                                value = Convert.ToInt32(dr["id"]),
                                label = dr["description"].ToString(),
                                car_make = dr["car_make"].ToString(),
                                car_model = dr["car_model"].ToString(),
                                car_model_year = Convert.ToInt32(dr["car_model_year"])
                            }
                            );
                    }
                }
            }

                return Json(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
